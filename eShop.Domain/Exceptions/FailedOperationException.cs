﻿using eShop.Domain.Interfaces;

namespace eShop.Domain.Exceptions;

public class FailedOperationException(string message) : Exception(message), IInternalServerError;