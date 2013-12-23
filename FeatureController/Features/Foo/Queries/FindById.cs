using FeatureController.Features.Foo.Dtos;
using FeatureController.Features.Foo.Projections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeatureController.Features.Foo.Queries
{
    public class FindById
    {
        public int Id { get; set; }
    }

    public static class FindByIdExtensions
    {
        public static FooModel Find(this FooProjection projections, FindById query)
        {
            return projections.Foos.Single(x => x.F00 == query.Id);
        }
    }

}