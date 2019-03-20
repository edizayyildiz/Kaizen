using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaizen.Model
{
    public class County : BaseEntity
    {
        public County()
        {
            Branches = new HashSet<Branch>();
        }
        public string Name { get; set; }
        public Guid? CityId { get; set; }
        public virtual City City { get; set; }
        public virtual ICollection<Branch> Branches { get; set; }
    }
}
