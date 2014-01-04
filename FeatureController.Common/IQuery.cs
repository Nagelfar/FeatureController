using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeatureController.Common
{
    public interface IQuery { }
    public interface IQuery<out TResponse>:IQuery
    {
    }
}