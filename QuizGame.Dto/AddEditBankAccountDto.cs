using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Dto
{
    public class AddEditBankAccountDto
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int AccountTypeId { get; set; }
        [Required]
        public string BankName { get; set; }
        [Required]
        public string AccountNumber { get; set; }
        [Required]
        public string AccountHolderName { get; set; }
        [Required]
        public string IFSCCode { get; set; }
        public string UpiAddress { get; set; }
        public string BranchName { get; set; }
        public string BranchAddress { get; set; }
        public string Remark { get; set; }
        public bool IsActive { get; set; }
        public int AddedById { get; set; }
        public DateTime AddedDate { get; set; }
        public int UpdatedById { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
