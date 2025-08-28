using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Dto
{
    public class JoinUsDto
    {
        [Required(ErrorMessage = "Enter Your Name!")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 200 characters.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name can contain only letters and spaces.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter Father/Husband Name!")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Guardian Name must be between 3 and 200 characters.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Guardian Name can contain only letters and spaces.")]
        public string GuardianName { get; set; }

        [Required(ErrorMessage = "Enter Mobile Number!")]
        [RegularExpression(@"^[6-9]\d{9}$", ErrorMessage = "Enter a valid 10-digit mobile number.")]
        public string MobileNumber { get; set; }

        public string AlternateMobileNumber { get; set; }
        [Required(ErrorMessage = "Select DOB!")]
        public string DOB { get; set; }
        [Required(ErrorMessage = "Fill Village!")]
        public string Village { get; set; }
        [Required(ErrorMessage = "Enter PinCode!")]
        [RegularExpression(@"^[1-9][0-9]{5}$", ErrorMessage = "Enter a valid 6-digit PIN code.")]
        public string PinCode { get; set; }

        [Required(ErrorMessage = "Fill Education Details!")]
        public string Education { get; set; }
        [Required(ErrorMessage = "Fill FullAddress!")]
        public string FullAddress { get; set; }
        [Required(ErrorMessage = "Fill AadharNumber!")]
        [RegularExpression(@"^(?:\d{12}|\d{4} \d{4} \d{4}|\d{4}-\d{4}-\d{4})$", ErrorMessage = "Enter a valid 12-digit Aadhar number (e.g., 123456789012 or 1234 5678 9012 or 1234-5678-9012).")]
        public string AadharNumber { get; set; }

        [Required(ErrorMessage = "Fill PanNumber!")]
        [RegularExpression(@"^[A-Z]{5}[0-9]{4}[A-Z]{1}$", ErrorMessage = "Enter a valid PAN number (e.g., ABCDE1234F).")]
        public string PanNumber { get; set; }

        public string UniqueId { get; set; }
        [Required(ErrorMessage = "Select District")]
        public int DistrictId { get; set; }
        [Required(ErrorMessage = "Select Block")]
        public int BlockId { get; set; }
        [Required(ErrorMessage ="Select Gram Panchayat")]
        public int GramPanchayatId { get; set; }
    }
}
