﻿using eShop.Domain.Common.Api;
using eShop.Domain.Requests.ProductApi.Product;

namespace eShop.Domain.Interfaces;

public interface IProductService
{
    public ValueTask<Response> CreateProductAsync(CreateProductRequest request);
    public ValueTask<Response> UpdateProductAsync(UpdateProductRequest request);
    public ValueTask<Response> DeleteProductAsync(DeleteProductRequest request);
    public ValueTask<Response> GetProductsAsync();
    public ValueTask<Response> GetProductByNameAsync(string name);
    public ValueTask<Response> GetProductByArticleAsync(string article);
    public ValueTask<Response> GetProductByIdAsync(Guid id);
}