using FeatureController.Features.Bar.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeatureController.Features.Bar.Projections
{
    public class BarProjection // : ProjectionBase<FooModel>
        // , IHandle<FooCreated>
        // / IHandle<FooUpdated>
    {
        // implement projection

        public IEnumerable<BarModel> Bars = Enumerable
          .Range(0, new Random(1).Next(100))
          .Select(x => new BarModel
          {
              F00 = x,
              Bar = x.ToString()
          })
          .ToArray();
    }
}