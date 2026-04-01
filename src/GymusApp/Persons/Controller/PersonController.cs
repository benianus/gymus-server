using gymus_server.GymusApp.Persons.Dtos;
using gymus_server.GymusApp.Persons.Dtos.Requests;
using gymus_server.GymusApp.Persons.Dtos.Responses;
using gymus_server.GymusApp.Persons.Mapper;
using gymus_server.GymusApp.Persons.Models;
using gymus_server.Shared.Abstractions;
using gymus_server.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace gymus_server.GymusApp.Persons.Controller;

[Route("api/persons")]
[ApiController]
public class PersonController(ICrudService<Person, int> personService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll()
    {
        var persons = await personService.GetAll();
        return Ok(
            new ApiResponse<List<PersonResponseDto>>(persons.ConvertAll(p => p.ToResponseDto())));
    }

    // GET api/<PersonController>/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse<string>>> GetOne([FromRoute] int id)
    {
        var person = await personService.GetOne(id);
        return person is null
            ? NotFound(new ApiResponse<List<string>>(["Person not found"]))
            : Ok(new ApiResponse<PersonResponseDto>(person.ToResponseDto()));
    }

    // POST api/<PersonController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Post([FromBody] PersonCreateRequestDto personCreateDto)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            return BadRequest(new ApiResponse<List<string>>(errors));
        }

        var person = await personService.Create(personCreateDto.ToEntity());
        return person is null
            ? BadRequest(new ApiResponse<List<string>>(["Person failed to saved"]))
            : CreatedAtAction("GetOne", new { id = person.Id }, person);
    }

    // PUT api/<PersonController>/5
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Put([FromRoute] int id,
        [FromBody] PersonUpdateRequestDto personUpdateRequestDto)
    {
        if (id <= 0) return BadRequest(new ApiResponse<List<string>>(["invalid id, try again"]));

        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            return BadRequest(new ApiResponse<List<string>>(errors));
        }

        var person = await personService.Update(personUpdateRequestDto.ToEntity(), id);
        return person is null
            ? BadRequest(new ApiResponse<List<String>>(["Person failed to update"]))
            : Ok(person);
    }

    // DELETE api/<PersonController>/5
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        if (id <= 0) return BadRequest(new ApiResponse<List<string>>(["invalid id, try again"]));

        return await personService.Delete(id)
            ? NoContent()
            : NotFound(new ApiResponse<List<string>>(["Person failed to delete"]));
    }
}