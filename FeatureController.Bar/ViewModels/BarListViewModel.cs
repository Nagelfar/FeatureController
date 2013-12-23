using FeatureController.Features.Bar.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FeatureController.Features.Bar.ViewModels
{
    public class BarListViewModel
    {
        public static BarListViewModel Create(BarModel model)
        {
            return new BarListViewModel
            {
                MyBar = model.Bar,
                MyF00 = model.F00
            };
        }

        [Display(Name="A Bar list")]
        public string MyBar { get; set; }

        [Display(Name = "A F00 int list")]
        public int MyF00 { get; set; }
    }
}