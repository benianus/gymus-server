using gymus_server.GymusApp.Store.Dtos.Requests;
using gymus_server.GymusApp.Store.Dtos.Responses;
using gymus_server.Shared.Dtos;
using gymus_server.Shared.Utlis;
using Microsoft.AspNetCore.Mvc;

namespace gymus_server.GymusApp.Store;

[ApiController]
[Route("api/products")]
public class StoreController(IStoreService storeService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ViewProducts(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 9
    )
    {
        var products = await storeService.ViewProducts(page, pageSize);
        return Ok(new ApiResponse<List<ProductResponseDto>>(products));
    }

    [HttpGet("{productId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ViewProduct([FromRoute] int productId)
    {
        if (Helpers.IsIdValid(productId)) BadRequest("Invalid product id");
        var product = await storeService.ViewProduct(productId);
        return Ok(new ApiResponse<ProductResponseDto>(product));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> AddNewProduct([FromForm] ProductCreateRequestDto dto)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values
                                   .SelectMany(v => v.Errors)
                                   .Select(e => e.ErrorMessage)
                                   .ToList();

            return BadRequest(new ApiResponse<List<string>>(errors));
        }

        await storeService.AddNewProduct(dto);
        return Created();
    }

    [HttpPost("{productId}/sales")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> RegisterNewSale(
        [FromRoute] int productId,
        [FromBody] SaleRegisterRequestDto dto
    )
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values
                                   .SelectMany(v => v.Errors)
                                   .Select(e => e.ErrorMessage)
                                   .ToList();

            return BadRequest(new ApiResponse<List<string>>(errors));
        }

        await storeService.RegisterNewSale(productId, dto);
        return Created();
    }
}