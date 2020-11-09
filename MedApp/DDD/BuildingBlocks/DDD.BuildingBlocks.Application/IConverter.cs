using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.BuildingBlocks.Application
{
    public interface IConverter<TSource, TDestination>
    {
        TDestination Convert(TSource source_object);
        TSource Convert(TDestination source_object);
        List<TDestination> ConvertList(List<TSource> source_object);
        List<TSource> ConvertList(List<TDestination> source_object);
    }
}
