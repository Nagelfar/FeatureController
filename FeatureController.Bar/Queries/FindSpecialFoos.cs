using FeatureController.Common;
using FeatureController.Features.Bar.Dtos;
using FeatureController.Features.Bar.Projections;
using FeatureController.Features.Bar.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeatureController.Features.Bar.Queries
{
    public class SpecialBarsModulo
        : IQuery<IReadOnlyCollection<BarListViewModel>>
    {
        public int Modulo { get; set; }
    }

    public class FindSpecialBars
        :IQueryHandler<SpecialBarsModulo,IReadOnlyCollection<BarListViewModel>>
    {
        private readonly BarProjection _projection;
        public FindSpecialBars(BarProjection projection)
        {
            _projection = projection;
        }

        public  IReadOnlyCollection<BarListViewModel> Handle( SpecialBarsModulo query)
        {
            return _projection.Bars
                .Where(x => x.F00 % query.Modulo == 0)
                .Select(BarListViewModel.Create)
                .ToArray();
        }
    }
}