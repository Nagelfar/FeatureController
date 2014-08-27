using FeatureController.Features.Foo.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FeatureController.Features.Foo.ViewModels
{
    [FluentValidation.Attributes.Validator(typeof(MyValidator))]
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

        [Display(Name = "Foo!", Prompt = "Enter Foo?")]
        [Required(AllowEmptyStrings = false)]
        [MyLength]
        public string FooEingabe { get; set; }

        // public ChangeFooCommand ToCommand() { ... }

    }
    
    public class MyValidator : AbstractValidator<FooEditViewModel>
    {
        public MyValidator()
        {

            Custom(x =>
            {
                if (x.FooEingabe != null && x.FooEingabe.Length >= 16)
                    return new FluentValidation.Results.ValidationFailure("", "Common Model Error");
                return null;
            });
        }
    }
    public class MyLength : MaxLengthAttribute
    {
        public MyLength() : base(17) { }
    }
}