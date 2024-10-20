﻿global using Microsoft.EntityFrameworkCore;
global using eShop.Application;
global using eShop.ReviewsWebApi.Data;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.OpenApi.Models;
global using eShop.Domain.DTOs;
global using LanguageExt.Common;
global using AutoMapper;
global using eShop.ReviewsWebApi.Exceptions;
global using MediatR;
global using UnitM = MediatR.Unit;
global using Unit = LanguageExt.Unit;
global using Microsoft.AspNetCore.Mvc;
global using eShop.ReviewsWebApi.Queries.Reviews;
global using eShop.Application.Utilities;
global using eShop.Domain.DTOs.Requests.Review;
global using eShop.ReviewsWebApi.Commands.Reviews;
global using Microsoft.AspNetCore.Authorization;
global using eShop.Application.Extensions;
global using eShop.ReviewsWebApi.Extensions;
