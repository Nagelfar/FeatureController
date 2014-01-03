using FeatureController.Features.Bar.Dtos;
using FeatureController.Features.Bar.Projections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeatureController.Features.Bar.Queries
{
    public class FindById
    {
        public int Id { get; set; }
    }

    public static class FindByIdExtensions
    {
        public static BarModel Find(this BarProjection projections, FindById query)
        {
            return projections.Bars.Single(x => x.F00 == query.Id);
        }
    }

}