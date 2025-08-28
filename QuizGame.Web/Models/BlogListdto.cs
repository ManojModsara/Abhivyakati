using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizGame.Web.Models
{
    public class BlogListdto
    {
        public BlogListdto()
        {
            this.Blogs = new List<BlogDetailsModel>();
        }
        public List<BlogDetailsModel> Blogs { get; set; }
    }
}