using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeatureController.Common
{
    public interface IQueryHandler<in TQuery, out TResponse>
        where TQuery : IQuery<TResponse>
    {
        TResponse Handle(TQuery query);
    }
}