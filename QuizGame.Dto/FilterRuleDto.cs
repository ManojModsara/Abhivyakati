using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Dto
{
    public class FilterRuleDto
    {
        public FilterRuleDto()
        {
            this.rulelists = new List<Rulelist>();
        }
        public int Id { get; set; }
        public string FixLength { get; set; }
        public int RuleId { get; set; }
        public string Rule { get; set; }
        public bool IsAllowSpace { get; set; }
        public string Space { get; set; }
        public string Duplicated { get; set; }
        public bool IsAllowDuplicated { get; set; }
        public string StartWith { get; set; }
        public string EndWith { get; set; }
        public bool IsActive { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int AddedById { get; set; }
        public int UpdatedById { get; set; }
        public List<Rulelist> rulelists { get; set; }
    }
    public class Rulelist
    {
        public int Id { get; set; }
        public string Rule { get; set; }
    }

}
