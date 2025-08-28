using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizGame.Web.Models
{
    public class BlogDetailsModel
    {
        public int ID { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string ShortDescription { get; set; }
        public Boolean IsActive { get; set; }
        public string Dates { get; set; }
        public string Description { get; set; }
        public string MetaTag { get; set; }
    }
}