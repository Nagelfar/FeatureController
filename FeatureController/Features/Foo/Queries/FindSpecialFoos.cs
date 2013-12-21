using FeatureController.Features.Foo.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeatureController.Features.Foo.Queries
{
    public static class FindSpecialFoos
    {
        public static IEnumerable<FooModel> FindTheSpecialFoos(this IEnumerable<FooModel> foos, int modulo)
        {
            return foos.Where(x => x.F00 % modulo == 0);
        }
    }
}