﻿using eShop.Application.Utilities;
using eShop.Domain.DTOs.Requests;
using eShop.Domain.Enums;
using eShop.ProductWebApi.Commands.Products;
using eShop.ProductWebApi.Queries.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eShop.ProductWebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ProductController(ISender sender) : ControllerBase
    {
        private readonly ISender sender = sender;

        [HttpGet]
        public async ValueTask<ActionResult<ResponseDTO>> GetProductList()
        {
            var result = await sender.Send(new GetProductsListQuery());
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpGet("{Id:guid}")]
        public async ValueTask<ActionResult<ResponseDTO>> GetProductById(Guid Id)
        {
            var result = await sender.Send(new GetProductByIdQuery(Id));
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpGet("{Name}")]
        public async ValueTask<ActionResult<ResponseDTO>> GetProductByName(string Name)
        {
            var result = await sender.Send(new GetProductByNameQuery(Name));
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpGet("get-by-id/{Id:guid}/with-type/{Type}")]
        public async ValueTask<ActionResult<ResponseDTO>> GetProductByIdWithType(Guid Id, ProductType Type)
        {
            var result = await sender.Send(new GetProductByIdQuery(Id));
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpGet("get-by-name/{Name}/with-type/{Type}")]
        public async ValueTask<ActionResult<ResponseDTO>> GetProductByNameWithType(string Name, ProductType Type)
        {
            var result = await sender.Send(new GetProductByNameQuery(Name));
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpPost("create-product-clothing")]
        public async ValueTask<ActionResult<ResponseDTO>> CreateClothingProduct([FromBody] CreateClothingRequest createProductRequest)
        {
            var result = await sender.Send(new CreateClothingProductCommand(createProductRequest));
            return result.Match(
                s => CreatedAtAction(nameof(GetProductByIdWithType), new { s.Id, Type = s.ProductType }, 
                    new ResponseBuilder().Succeeded().WithResultMessage("Product was successfully created.").WithResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpPost("create-product-shoes")]
        public async ValueTask<ActionResult<ResponseDTO>> CreateShoesProduct([FromBody] CreateShoesRequest createProductRequest)
        {
            var result = await sender.Send(new CreateShoesProductCommand(createProductRequest));
            return result.Match(
                s => CreatedAtAction(nameof(GetProductByIdWithType), new { s.Id, Type = s.ProductType },
                    new ResponseBuilder().Succeeded().WithResultMessage("Product was successfully created.").WithResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }
    }
}