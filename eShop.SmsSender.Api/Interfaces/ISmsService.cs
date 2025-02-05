﻿using eShop.Domain.Requests.Api.Sms;
using eShop.Domain.Responses.Api.Sms;

namespace eShop.SmsSender.Api.Interfaces;

public interface ISmsService
{
    public Task<SingleMessageResponse> SendSingleMessage(SingleMessageRequest request);
}