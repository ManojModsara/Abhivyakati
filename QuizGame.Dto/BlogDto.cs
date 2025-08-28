using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace QuizGame.Dto
{
    public class BlogDto
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Enter Url")]
        public string Url { get; set; }
        [Required(ErrorMessage = "Enter Title")]
        public string Title { get; set; }
        public string ShowImg { get; set; }

        public HttpPostedFileBase Image { get; set; }
        [Required(ErrorMessage = "Enter Description")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Enter Short Description")]
        public string ShortDescription { get; set; }
        public Boolean IsActive { get; set; }
        public int ActiveUpdatedBy { get; set; }
        public DateTime ActiveUpdatedOn { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime UpdatedByDate { get; set; }
        public string BlogDate { get; set; }
        public string Href { get; set; }
        public int TotalBlogs { get; set; }
    }
    public class EventDto
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Enter Title")]
        public string Title { get; set; }
        public string ShowImg { get; set; }

        public HttpPostedFileBase Image { get; set; }
        [Required(ErrorMessage = "Enter Description")]
        public string Description { get; set; }
        public int AddedById { get; set; }
        public int UpdatedById { get; set; }
        [Required(ErrorMessage = "Select EventDate")]

        public DateTime? EventDate { get; set; }
        public string EvtDate { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
