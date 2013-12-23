using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeatureController.Infrastructure
{
    public interface IQuery { }
    public interface IQuery<out TResponse>:IQuery
    {
    }
}