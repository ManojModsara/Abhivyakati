using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Dto
{
    public class AppVersionDto
    {
        public int Id { get; set; }
        public string minversion { get; set; }
        public string maxversion { get; set; }
        public System.DateTime addeddate { get; set; }
        public System.DateTime updatedate { get; set; }
        public Int64 addedby { get; set; }
        public Int64 updated { get; set; }
    }
}
