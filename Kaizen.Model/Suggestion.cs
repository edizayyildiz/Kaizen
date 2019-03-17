using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaizen.Model
{
    public class Suggestion : BaseEntity
    {
        public string Title { get; set; }
        public string CurrentStatus { get; set; }
        public string SuggestedStatus { get; set; }
        public Assessment Assessment { get; set; }
        public Guid? EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
