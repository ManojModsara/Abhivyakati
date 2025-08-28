using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Dto
{
    public class AppDto
    {
        public class SignUp : Basically
        {
            public string UserID { get; set; }
            public string TokenID { get; set; }
            public string Image { get; set; }
            public byte[] Image1 { get; set; }
            
            public string IMEI { get; set; }
            public string EmailID { get; set; }
            public string Mobileno { get; set; }
            //public string FireBaseToken { get; set; }
            public string Name { get; set; }
            public string Crby { get; set; }
            public string ShopName { get; set; }
            public string ShopAddress { get; set; }
            public string Reffercode { get; set; }
            public string OTP { get; set; }
            public string Password { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string Pincode { get; set; }
            public string GSTNO { get; set; }
        }
        public class SignupResponse
        {
            public string Error { get; set; }
            public string status { get; set; }
            public string Time { get; set; }
            public string Message { get; set; }
        }
        public class memblist
        {
            public string ERROR { get; set; }
            public string STATUSCODE { get; set; }
            public string MESSAGE { get; set; }
            public string DATA { get; set; }
            public DataTable MEMBERLIST { get; set; }
        }
        public class LoginModel : Basically
        {
            //[Required(ErrorMessage = "Invalid FGSM Parameter")]
            public string IMEI { get; set; }
            [Required(ErrorMessage = "Invalid UserName Parameter")]
            public string Name { get; set; }
            [Required(ErrorMessage = "Invalid Password Parameter")]
            public string Pass { get; set; }
            [Required(ErrorMessage = "Invalid Fire Token Request Parameters")]
            public string FireToken { get; set; }
        }
        public class userlog : Basically
        {
            public string ERROR { get; set; }
            public string STATUSCODE { get; set; }
            public string TOKENID { get; set; }
            public string MESSAGE { get; set; }
            public string USERID { get; set; }
            public string MOBILENUMBER { get; set; }
            public string GSTNO { get; set; }
            public string EMAIL { get; set; }
            public string USERTYPE { get; set; }
            public string SHOPNAME { get; set; }
            public string OTPCheck { get; set; }
            public string RefferCode { get; set; }
            public string IMAGEPATH { get; set; }
            public string IMAGECODE { get; set; }
        }

        public class UserImageUpload : Basically
        {
            [Required(ErrorMessage = "Invalid Tokenid Parameter")]
            public string Tokenid { get; set; }
            [Required(ErrorMessage = "Invalid UserID Parameter")]
            public string UserID { get; set; }
            public string Image { get; set; }

        }
        public class Tok : Basically
        {
            [Required(ErrorMessage = "Invalid Tokenid Parameter")]
            public string Tokenid { get; set; }
            [Required(ErrorMessage = "Invalid UserID Parameter")]
            public string UserID { get; set; }
            public string PageNumber { get; set; }
            public string PageSize { get; set; }


        }

        public class SyncBody : Basically
        {
            [Required(ErrorMessage = "Invalid Tokenid Parameter")]
            public string Tokenid { get; set; }
            [Required(ErrorMessage = "Invalid UserID Parameter")]
            public string UserID { get; set; }
            [Required(ErrorMessage = "Invalid Name Parameter")]
            public string Name { get; set; }
            [Required(ErrorMessage = "Invalid Counts Parameter")]
            public string Counts { get; set; }
            public string PageNumber { get; set; }
            public string PageSize { get; set; }


        }
        public class ForgetModel : Basically
        {
            [Required(ErrorMessage = "Invalid Mobileno Parameter")]
            public string Mobileno { get; set; }
            [Required(ErrorMessage = "Invalid OTP Parameter")]
            public string OTP { get; set; }
            [Required(ErrorMessage = "Invalid Password Parameter")]
            public string Password { get; set; }
        }
        public class Profile
        {
            public string ERROR { get; set; }
            public string STATUSCODE { get; set; }
            public string MESSAGE { get; set; }
            public string USERID { get; set; }
            public string MOBILENUMBER { get; set; }
            public string USERNAME { get; set; }
            public string EMAIL { get; set; }
            public string USERTYPE { get; set; }
            public string WBalance { get; set; }
            public string SecurityDeposit { get; set; }
            public string ShopName { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string Pincode { get; set; }
            public string ShopAddress { get; set; }
            public string GSTNO { get; set; }
            public string Image { get; set; }
        }
        public class ChangepassModel : Basically
        {
            [Required(ErrorMessage = "Invalid Tokenid Parameter")]
            public string Tokenid { get; set; }
            [Required(ErrorMessage = "Invalid UserID Parameter")]
            public string Userid { get; set; }
            [Required(ErrorMessage = "Invalid Password Parameter")]
            public string Password { get; set; }
        }
        public class Leads : Basically
        {
            [Required(ErrorMessage = "Invalid Tokenid Parameter")]
            public string Tokenid { get; set; }
            [Required(ErrorMessage = "Invalid UserID Parameter")]
            public string Userid { get; set; }
            [Required]
            public string Name { get; set; }
            [Required(ErrorMessage = "Invalid Mobileno Parameter")]
            public string MobileNo { get; set; }
            [Required]
            public string ShopName { get; set; }
            [Required]
            public string Emailid { get; set; }
            [Required]
            public string SAddress { get; set; }
            [Required]
            public string City { get; set; }
            [Required]
            public string State { get; set; }
            [Required]
            public string Pincode { get; set; }
            [Required(ErrorMessage = "Invalid APP Request Parameters")]
            
            public string Image { get; set; }
        }

        public class DistributerEnquires : Basically
        {
            [Required]
            public string Name { get; set; }
            [Required]
            public string Mobileno { get; set; }
            [Required]
            public string EmailId { get; set; }
            [Required]
            public string City { get; set; }
            [Required]
            public string State { get; set; }
            [Required]
            public string Pincode { get; set; }
            [Required]
            public string Types { get; set; }
            [Required(ErrorMessage = "Invalid APP Request Parameters")]
            
            public string Message { get; set; }
        }

        public class Complain : Basically
        {
            [Required]
            public string Userid { get; set; }
            [Required]
            public string TokenID { get; set; }
            [Required]
            public string TYpes { get; set; }
            public string Message { get; set; }
            [Required]
            public string Priortiy { get; set; }
            [Required]
            public string Methods { get; set; }

        }

        public class Resenddto : Basically
        {
            public string UserID { get; set; }
        }

        public class AddProduct:Basically
        {
            [Required(ErrorMessage = "Invalid Tokenid Parameter")]
            public string TokenID { get; set; }
            [Required(ErrorMessage = "Invalid Product Name Parameter")]
            public string Name { get; set; }
            [Required(ErrorMessage = "Invalid Bar Code Parameter")]
            public string Barcode { get; set; }
            [Required(ErrorMessage = "Invalid User ID Parameter")]
            public string UserID { get; set; }
            [Required(ErrorMessage = "Invalid LocalId Parameter")]
            public string LocalId { get; set; }
            public string Unit { get; set; }
            [Required(ErrorMessage = "Invalid Types Parameter")]
            public string Types { get; set; }
            public Nullable<decimal> GstId { get; set; }
            public Nullable<decimal> UnitID { get; set; }
            public string Image { get; set; }
        }

        public class Vendor : Basically
        {
            [Required(ErrorMessage = "Invalid Tokenid Parameter")]
            public string TokenID { get; set; }
            [Required(ErrorMessage = "Invalid Vendor Name Parameter")]
            public string Name { get; set; }
            [Required(ErrorMessage = "Invalid CompanyName Parameter")]
            public string CompanyName { get; set; }
            [Required(ErrorMessage = "Invalid User ID Parameter")]
            public string UserID { get; set; }
            [Required(ErrorMessage = "Invalid LocalId Parameter")]
            public string LocalId { get; set; }
            public string FullAddress { get; set; }
            [Required(ErrorMessage = "Invalid Types Parameter")]
            public string Types { get; set; }
            [Required(ErrorMessage = "Invalid MobileNo Parameter")]
            public string MobileNo { get; set; }
            [Required(ErrorMessage = "Invalid Email Parameter")]
            public string Email { get; set; }
            public string GstId { get; set; }
            public string GstAddress { get; set; }
        }

        public class ProductImageUpload : Basically
        {
            [Required(ErrorMessage = "Invalid Tokenid Parameter")]
            public string Tokenid { get; set; }
            [Required(ErrorMessage = "Invalid UserID Parameter")]
            public string UserID { get; set; }
            public string Image { get; set; }
            [Required(ErrorMessage = "Invalid ID Parameter")]
            public string LocalID { get; set; }
        }

        public class AddStock : Basically
        {
            [Required(ErrorMessage = "Invalid Tokenid Parameter")]
            public string TokenID { get; set; }
            [Required(ErrorMessage = "Invalid UserID Parameter")]
            public string UserID { get; set; }
            [Required(ErrorMessage = "Invalid Id Parameter")]
            public string LocalId { get; set; }
            [Required(ErrorMessage = "Invalid Vendor ID Parameter")]
            public string LocalVendorID { get; set; }
            [Required(ErrorMessage = "Invalid Product ID Parameter")]
            public string ProductID { get; set; }
            [Required(ErrorMessage = "Invalid Quantity Parameter")]
            public string Quantity { get; set; }
            [Required(ErrorMessage = "Invalid buyrate Parameter")]
            public string Buyrate { get; set; }
            [Required(ErrorMessage = "Invalid SaleRate Parameter")]
            public string SaleRate { get; set; }
            [Required(ErrorMessage = "Invalid GstSlab Parameter")]
            public string GstSlab { get; set; }
            [Required(ErrorMessage = "Invalid GrossAmount Parameter")]
            public string GrossAmount { get; set; }
            public Boolean IsGstIncluded { get; set; }
            public string TaxAmt { get; set; }
            public string NetAmount { get; set; }
            public string ActualBuyRate { get; set; }
            public string AddedbyId { get; set; }
            public string Mediumid { get; set; }
        }
    }
}
