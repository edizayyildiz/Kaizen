using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaizen.Model
{
    public class Country : BaseEntity
    {
        public Country()
        {
            Cities = new HashSet<City>();
        }
        public string Name { get; set; }

        public virtual ICollection<City> Cities { get; set; }
    }
}
