using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace QuizGame.Dto
{
    public class ImageUploadDto
    {
        public List<HttpPostedFileBase> Files { get; set; }
    }
}
