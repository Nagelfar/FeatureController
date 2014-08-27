using FeatureController.Features.Foo.Dtos;
using FeatureController.Features.Foo.ViewModels;
using FeatureController.Common;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FeatureController.Features.Foo.Queries;
using FeatureController.Features.Foo.Projections;
using FeatureSwitcher;
using FeatureController.Infrastructure;
namespace FeatureController.Features.Foo
{
    public class FooController : Controller, IFeature
    {
        private readonly IMediator _mediator;
        private readonly FooProjection _projection;

        
        public FooController(FooProjection projection, IMediator mediator)
        {
            _projection = projection;
            _mediator = mediator;
        }

        public ActionResult Index()
        {
            var response = _mediator.Response(new SpecialFoosModulo { Modulo = 2 });

            return View(response);
        }

        [HttpGet]
        public ActionResult Edit(FindById query)
        {
            return View(FooEditViewModel.Create(_projection.Find(query)));
        }

        [NormalValidation]
        [HttpPost]
        public ActionResult Edit(FooEditViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _projection.Foos.ElementAt(model.Id).Foo = model.FooEingabe;
            
            return RedirectToAction("Index");
        }
    }
}