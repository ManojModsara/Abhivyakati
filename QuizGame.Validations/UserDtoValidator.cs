using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using QuizGame.Dto;
namespace QuizGame.Validation
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        
        public UserDtoValidator()
        {
           
            RuleFor(l => l.Name).NotEmpty().WithMessage("*required");
            RuleFor(l => l.ShopName).NotEmpty().WithMessage("*required");
            RuleFor(l => l.EmailOffice).NotEmpty().WithMessage("*required");
          
            RuleFor(l => l.MobileNumber).NotEmpty().WithMessage("*required");
                   
        }
        
    }
}
