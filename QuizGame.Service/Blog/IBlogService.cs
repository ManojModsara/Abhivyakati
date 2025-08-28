using QuizGame.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Service
{
    public interface IBlogService
    {
        List<Blog> GetBlog();
        List<Blog> GetLatestActiveBlogs(int count);
        Blog GetBlogById(int Id);
        bool DeleteBlog(int Id);
        Blog SaveBlog(Blog blog);
    }
}
