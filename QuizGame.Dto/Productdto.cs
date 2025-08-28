using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Dto
{
   public class Productdto
   {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string BarCode { get; set; }
        public string ProductName { get; set; }
        public string ImagePath { get; set; }
        public string Discription { get; set; }
        public string Unit { get; set; }
        public int UnitID { get; set; }
        public decimal? GSTSlab { get; set; }
        public int AddedByID { get; set; }
        public int MediumID { get; set; }
        public bool IsPublished { get; set; }
        public DateTime? AddedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
