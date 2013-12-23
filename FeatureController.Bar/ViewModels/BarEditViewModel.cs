using FeatureController.Features.Bar.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FeatureController.Features.Bar.ViewModels
{
    public class BarEditViewModel
    {
        public static BarEditViewModel Create(BarModel model)
        {
            return new BarEditViewModel
            {
                Id = model.F00,
                BarEingabe = model.Bar
            };
        }

        [Editable(false)]
        public int Id { get; set; }

        [Display(Name="Bar!",Prompt="Enter Bar?")]
        [Required(AllowEmptyStrings=false)]
        [MaxLength(15)]
        public string BarEingabe { get; set; }

        // public ChangeFooCommand ToCommand() { ... }
    }
}