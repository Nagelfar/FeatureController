using FeatureController.Features.Foo.Dtos;
using FeatureController.Features.Foo.Projections;
using FeatureController.Features.Foo.ViewModels;
using FeatureController.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeatureController.Features.Foo.Queries
{
    public class SpecialFoosModulo : IQuery<IReadOnlyCollection<FooListViewModel>>
    {
        public int Modulo { get; set; }
    }

    public class FindSpecialFoos:IQueryHandler<SpecialFoosModulo,IReadOnlyCollection<FooListViewModel>>
    {
        private readonly FooProjection _projection;
        public FindSpecialFoos(FooProjection projection)
        {
            _projection = projection;
        }

        public  IReadOnlyCollection<FooListViewModel> Handle( SpecialFoosModulo query)
        {
            return _projection.Foos
                .Where(x => x.F00 % query.Modulo == 0)
                .Select(FooListViewModel.Create)
                .ToArray();
        }
    }
}