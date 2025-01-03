﻿global using eShop.Application.Behaviours;
global using eShop.Application.Extensions;
global using eShop.Application.Filters;
global using eShop.Application.Mapping;
global using eShop.Application.Middlewares;
global using eShop.Application.Utilities;
global using eShop.Domain.Exceptions;
global using eShop.ReviewsApi.Behaviours;
global using eShop.ReviewsApi.Commands.Comments;
global using eShop.ReviewsApi.Data;
global using eShop.ReviewsApi.Extensions;
global using eShop.ReviewsApi.Queries.Comments;
global using eShop.ReviewsApi.Receivers;
global using eShop.ServiceDefaults;
global using FluentValidation;
global using LanguageExt.Common;
global using MassTransit;
global using MediatR;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;
