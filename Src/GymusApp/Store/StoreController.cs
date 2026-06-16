using FluentValidation;
using gymus_server.GymusApp.Store.Dtos.Requests;
using gymus_server.GymusApp.Store.Dtos.Responses;
using gymus_server.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using static gymus_server.Shared.Utlis.Helpers;

namespace gymus_server.GymusApp.Store;

[ApiController]
[Route("api/products")]
public class StoreController(
    IStoreService storeService,
    IValidator<ProductCreateRequestDto> createProductRequestValidator,
    IValidator<ProductUpdateRequestDto> updateProductRequestValidator,
    IValidator<SaleRegisterRequestDto> registerSaleRequestValidator
) : ControllerBase {
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ViewProducts(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 9
    ) {
        var pagedResponse = await storeService.ViewProducts(page, pageSize);
        return Ok(pagedResponse);
    }

    [HttpGet("{productId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ViewProduct([FromRoute] int productId) {
        if (IsIdValid(productId)) BadRequest("Invalid product id");
        var product = await storeService.ViewProduct(productId);
        return Ok(new ApiResponse<ProductResponseDto>(product));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> AddNewProduct([FromForm] ProductCreateRequestDto dto) {
        createProductRequestValidator.ValidateAndThrow(dto);
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
    ) {
        registerSaleRequestValidator.ValidateAndThrow(dto);
        await storeService.RegisterNewSale(productId, dto);
        return Created();
    }

    [HttpDelete("{productId}")]
    public async Task<IActionResult> DeleteProduct([FromRoute] int productId) => NoContent();

    [HttpPut("{productId}")]
    public async Task<IActionResult> UpdateProduct(
        [FromRoute] int productId,
        [FromBody] ProductUpdateRequestDto dto
    ) {
        updateProductRequestValidator.ValidateAndThrow(dto);
        await storeService.UpdateProduct(productId, dto);
        return NoContent();
    }
}