using FeatureController.Features.Foo.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FeatureController.Features.Foo.ViewModels
{
    public class FooListViewModel
    {
        public static FooListViewModel Create(FooModel model)
        {
            return new FooListViewModel
            {
                MyFoo = model.Foo,
                MyF00 = model.F00
            };
        }

        [Display(Name="A Foo list")]
        public string MyFoo { get; set; }

        [Display(Name = "A F00 int list")]
        public int MyF00 { get; set; }
    }
}