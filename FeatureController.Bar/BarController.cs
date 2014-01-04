
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using FeatureController.Features.Bar.Projections;
using FeatureController.Features.Bar.Queries;
using FeatureController.Features.Bar.ViewModels;
using FeatureController.Common;

namespace FeatureController.Features.Bar
{
    [RouteArea("BarArea",AreaPrefix="BarArea")]
    public class BarController : Controller, FeatureSwitcher.IFeature
    {
        private readonly IMediator _mediator;
        private readonly BarProjection _projection;

        
        public BarController(BarProjection projection, IMediator mediator)
        {
            _projection = projection;
            _mediator = mediator;
        }

        public ActionResult Index()
        {
            var response = _mediator.Response(new SpecialBarsModulo { Modulo = 2 });
            
            return View(response);
        }

        [HttpGet]
        public ActionResult Edit(FindById query)
        {
            return View(BarEditViewModel.Create(_projection.Find(query)));
        }

        [HttpPost]
        public ActionResult Edit(BarEditViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _projection.Bars.ElementAt(model.Id).Bar = model.BarEingabe;

            return RedirectToAction("Index");
        }
    }
}