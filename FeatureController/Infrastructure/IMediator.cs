using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeatureController.Infrastructure
{
    public interface IMediator
    {
        TResponse Response<TResponse>(IQuery<TResponse> query);
    }
}