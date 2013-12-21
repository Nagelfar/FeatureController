using FeatureController.Features.Foo.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FeatureController.Features.Foo.ViewModels
{
    public class FooEditViewModel
    {
        public static FooEditViewModel Create(FooModel model)
        {
            return new FooEditViewModel
            {
                Id = model.F00,
                FooEingabe = model.Foo
            };
        }

        [Editable(false)]
        public int Id { get; set; }

        [Display(Name="Foo!",Prompt="Enter Foo?")]
        [Required(AllowEmptyStrings=false)]
        [MaxLength(15)]
        public string FooEingabe { get; set; }

        // public ChangeFooCommand ToCommand() { ... }
    }
}