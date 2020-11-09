﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.BuildingBlocks.Application
{
    public interface IExecutionContextAccessor
    {
        Guid UserId { get; }

        Guid CorrelationId { get; }

        bool IsAvailable { get; }
    }
}
