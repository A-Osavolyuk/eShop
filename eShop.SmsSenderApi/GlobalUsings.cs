// Global using directives

global using System.Net;
global using Amazon;
global using Amazon.SimpleNotificationService;
global using Amazon.SimpleNotificationService.Model;
global using eShop.Application.Behaviours;
global using eShop.Application.Extensions;
global using eShop.Application.Middlewares;
global using eShop.Domain.Common.Api;
global using eShop.Domain.Requests.SmsApi;
global using eShop.Domain.Responses.SmsApi;
global using eShop.ServiceDefaults;
global using eShop.SmsSenderApi;
global using eShop.SmsSenderApi.Consumers;
global using eShop.SmsSenderApi.Extensions;
global using eShop.SmsSenderApi.Services;
global using MassTransit;
global using Microsoft.AspNetCore.Mvc;
global using Response = eShop.Domain.Common.Api.Response;