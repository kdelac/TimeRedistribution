using Autofac;
using DDD.BuildingBlocks.Infrastructure;
using DDD.BuildingBlocks.Application.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Text;
using DDD.BuildingBlocks.Application;
using Scheduling.Application.Calendars;
using Scheduling.Domain.Calendars;
using Scheduling.Infrastructure.Domain.Calendars;
using Scheduling.Infrastructure.Domain.MedicalStuffs;
using Scheduling.Application.MedicalStaff;
using Scheduling.Domain.MedicalStaff;

namespace Scheduling.Infrastructure.DataAccess
{
    internal class DataAccessModule : Module
    {
        private readonly string _databaseConnectionString;
        internal DataAccessModule(string databaseConnectionString)
        {
            _databaseConnectionString = databaseConnectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SqlConnectionFactory>()
                .As<ISqlConnectionFactory>()
                .WithParameter("connectionString", _databaseConnectionString)
                .InstancePerLifetimeScope();

            builder.RegisterType<CalendarMapper>()
                .As<IConverter<CalendarDto, Calendar>>();
            builder.RegisterType<MedicalStaffMapper>()
                .As<IConverter<MedicalStaffDto, MedicalStuff>>();

            builder
                .Register(c =>
                {
                    var dbContextOptionsBuilder = new DbContextOptionsBuilder<SchedulingContext>();
                    dbContextOptionsBuilder.UseSqlServer(_databaseConnectionString);

                    dbContextOptionsBuilder
                        .ReplaceService<IValueConverterSelector, StronglyTypedIdValueConverterSelector>();

                    return new SchedulingContext(dbContextOptionsBuilder.Options);
                })
                .AsSelf()
                .As<DbContext>()
                .InstancePerLifetimeScope();

            var infrastructureAssembly = typeof(SchedulingContext).Assembly;

            builder.RegisterAssemblyTypes(infrastructureAssembly)
                .Where(type => type.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope()
                .FindConstructorsWith(new AllConstructorFinder());

            //builder.RegisterType<ClinicRepository>()
            //    .As<IClinicRepository>()
            //    .InstancePerLifetimeScope();
        }

    }
}
