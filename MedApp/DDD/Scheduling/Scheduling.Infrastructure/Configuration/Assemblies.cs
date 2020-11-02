using Scheduling.Application.Configurations.Commands;
using Scheduling.Infrastructure.Configuration.Commands;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Scheduling.Infrastructure.Configuration
{
    internal static class Assemblies
    {
        public static readonly Assembly Application = typeof(ICommandHandler<>).Assembly;
    }
}
