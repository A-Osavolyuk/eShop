global using eShop.Domain.Entities;
global using eShop.ProductWebApi.Repositories.Interfaces;
global using LanguageExt.Common;
global using MediatR;
global using FluentValidation;
global using AutoMapper;
global using eShop.Domain.DTOs.Requests;
global using eShop.Domain.Exceptions;
global using eShop.Domain.Exceptions.Categories;
global using eShop.Domain.Exceptions.Subcategories;
global using eShop.Domain.Exceptions.Suppliers;
global using Microsoft.EntityFrameworkCore;
global using eShop.ProductWebApi.Data;
global using eShop.Application;
global using Microsoft.AspNetCore.Mvc;
global using eShop.Domain.DTOs.Responses;
global using eShop.ProductWebApi.Repositories.Implementation;
global using LanguageExt;
global using Unit = LanguageExt.Unit;
global using MUnit = MediatR.Unit;

global using eShop.ProductWebApi.Subcategories.Get;
global using eShop.ProductWebApi.Subcategories.Delete;
global using eShop.ProductWebApi.Subcategories.Create;
global using eShop.ProductWebApi.Subcategories.Update;

global using eShop.ProductWebApi.Suppliers.Create;
global using eShop.ProductWebApi.Suppliers.Delete;
global using eShop.ProductWebApi.Suppliers.Get;
global using eShop.ProductWebApi.Suppliers.Update;

