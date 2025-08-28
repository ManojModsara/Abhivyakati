using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Dto
{
    public class Basically
    {
        [Required(ErrorMessage ="Enter New Version")]
        public string Version { get; set; }
        public string Location { get; set; }
    }
}
