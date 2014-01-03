using FeatureController.Features.Foo.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeatureController.Features.Foo.Projections
{
    public class FooProjection // : ProjectionBase<FooModel>
        // , IHandle<FooCreated>
        // / IHandle<FooUpdated>
    {
        // implement projection

        public IEnumerable<FooModel> Foos = Enumerable
          .Range(0, new Random(1).Next(100))
          .Select(x => new FooModel
          {
              F00 = x,
              Foo = x.ToString()
          })
          .ToArray();
    }
}