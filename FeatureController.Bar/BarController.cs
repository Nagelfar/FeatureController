
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using FeatureController.Features.Bar.Projections;
using FeatureController.Features.Bar.Queries;
using FeatureController.Features.Bar.ViewModels;

namespace FeatureController.Features.Bar
{
    public class BarController : Controller, FeatureSwitcher.IFeature
    {
        //private readonly IMediator _mediator;
        private readonly BarProjection _projection;

        
        public BarController(BarProjection projection)
        {
            _projection = projection;
            //_mediator = mediator;
        }

        public ActionResult Index()
        {
         //   var response = _mediator.Response(new SpecialBarsModulo { Modulo = 2 });

            return View();
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