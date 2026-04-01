using gymus_server.GymusApp.Users.Dtos.Requests;
using gymus_server.GymusApp.Users.Dtos.Responses;
using gymus_server.GymusApp.Users.Mapper;
using gymus_server.Shared.Abstractions;
using gymus_server.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace gymus_server.GymusApp.Users.Controller
{
    [Route("api/users")]
    [ApiController]
    public class UserController(ICrudService<User, int> userService) : ControllerBase
    {
        // GET: api/<UserController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll()
        {
            var users = await userService.GetAll();
            var userResponseDto = users.ConvertAll(user => user.ToDto());
            return Ok(
                new ApiResponse<List<UserResponseDto>>(userResponseDto));
        }

        // GET api/<UserController>/5
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetOne([FromRoute] int id)
        {
            if (int.IsNegative(id) || id == 0)
                return BadRequest(new ApiResponse<List<string>>(["Invalid Id"]));

            var user = await userService.GetOne(id);

            return user == null
                ? NotFound(new ApiResponse<List<string>>(["User not found"]))
                : Ok(new ApiResponse<UserResponseDto>(user.ToDto()));
        }

        // POST api/<UserController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Post([FromBody] UserCreateRequestDto userCreateRequestDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(new ApiResponse<List<string>>(errors));
            }

            var newUser = await userService.Create(userCreateRequestDto.ToEntity());

            return newUser == null
                ? NotFound(new ApiResponse<List<string>>(["User Failed to save"]))
                : CreatedAtAction("GetOne", new { id = newUser.Id }, newUser);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(
            [FromRoute] int id,
            [FromBody] UserUpdateRequestDto userUpdateRequestDto
        )
        {
            if (id <= 0)
                return BadRequest(new ApiResponse<List<string>>(["Invalid Id"]));

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(new ApiResponse<List<string>>(errors));
            }

            var updatedUser = await userService.Update(userUpdateRequestDto.ToEntity(), id);

            return (updatedUser == null)
                ? NotFound(new ApiResponse<List<string>>(["User not found"]))
                : Ok(new ApiResponse<UserResponseDto>(updatedUser.ToDto()));
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (id <= 0) return BadRequest(new ApiResponse<List<string>>(["Invalid Id"]));
            
            return await userService.Delete(id)
                ? NoContent()
                : NotFound(new ApiResponse<List<string>>(["User failed to delete"]));
        }
    }
}