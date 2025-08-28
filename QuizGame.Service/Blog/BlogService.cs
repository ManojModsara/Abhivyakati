using QuizGame.Data;
using QuizGame.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Service
{
    public class BlogService : IBlogService
    {
        #region "Fields"
        private IRepository<Blog> repoBlog;
        #endregion

        #region "Cosntructor"
        public BlogService(IRepository<Blog> _repoBlog)
        {
            this.repoBlog = _repoBlog;

        }
        #endregion

        public List<Blog> GetBlog()
        {
            return repoBlog.Query().Get().ToList();
        }
        public List<Blog> GetLatestActiveBlogs(int count)
        {
            return repoBlog.Query()
                .Filter(x => x.IsActive == true).GetQuerable()
                .OrderByDescending(x => x.AddedDate)
                .Take(count)
                .ToList();
        }

        public Blog GetBlogById(int Id)
        {
            return repoBlog.Query().Get().Where(x => x.Id == Id).FirstOrDefault();
        }
        public bool DeleteBlog(int Id)
        {
            repoBlog.Delete(Id);
            return true;
        }

        public Blog SaveBlog(Blog blog)
        {
            if (blog.Id == 0)
            {
                blog.AddedDate = DateTime.Now;
                repoBlog.Insert(blog);
            }
            else
            {
                blog.UpdatedByDate = DateTime.Now;
                repoBlog.Update(blog);
            }
            return blog;
        }
    }
    }
