﻿using Ergenekon.Application.Common.Interfaces;

namespace Ergenekon.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}