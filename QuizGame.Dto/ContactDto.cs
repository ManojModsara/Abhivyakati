using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Dto
{
    public class ContactDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter Your Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "10 digit Mobile Number")]
        [MaxLength(10)]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(?:(?:\+|0{0,2})91(\s*[\-]\s*)?|[0]?)?[6789]\d{9}$", ErrorMessage = "Mobile Number is not valid.")]
        public string MobileNumber { get; set; }
        //[Required(ErrorMessage = "Enter Vaild Mail Address")]
        //[DataType(DataType.EmailAddress)]
        //[RegularExpression(@"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$", ErrorMessage = "Email is not valid.")]
        //public string Email { get; set; }
        [Required(ErrorMessage = "Please Enter Something in Message")]
        public string Message { get; set; }
        public string Response { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
    }
}
