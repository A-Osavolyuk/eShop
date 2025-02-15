﻿global using eShop.Application;
global using eShop.Application.Behaviours;
global using eShop.Application.Extensions;
global using eShop.Application.Filters;
global using eShop.Application.Middlewares;
global using eShop.Application.Utilities;
global using eShop.Domain.Common.Api;
global using eShop.Domain.Enums;
global using eShop.Domain.Exceptions;
global using eShop.Domain.Interfaces;
global using eShop.Domain.Requests.Api.Brand;
global using eShop.Domain.Requests.Api.Product;
global using eShop.Domain.Requests.Api.Seller;
global using eShop.Domain.Responses.Api.Brand;
global using eShop.Domain.Responses.Api.Products;
global using eShop.Domain.Responses.Api.Seller;
global using eShop.Product.Api.Behaviours;
global using eShop.Product.Api.Commands.Brands;
global using eShop.Product.Api.Commands.Products;
global using eShop.Product.Api.Commands.Sellers;
global using eShop.Product.Api.Data;
global using eShop.Product.Api.Extensions;
global using eShop.Product.Api.Mapping;
global using eShop.Product.Api.Queries.Brands;
global using eShop.Product.Api.Queries.Products;
global using eShop.Product.Api.Rpc;
global using eShop.ServiceDefaults;
global using Grpc.Net.Client;
global using LanguageExt.Common;
global using MassTransit;
global using MediatR;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Caching.Distributed;
global using MongoDB.Bson.Serialization;
global using Newtonsoft.Json;