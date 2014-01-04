using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeatureController.Common
{
    public interface IMediator
    {
        TResponse Response<TResponse>(IQuery<TResponse> query);
    }
}