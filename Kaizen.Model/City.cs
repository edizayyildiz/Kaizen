using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaizen.Model
{
    public class City : BaseEntity
    {
        public City()
        {
            Counties = new HashSet<County>();
        }
        public string Name { get; set; }
        public Guid CountryId { get; set; }
        public virtual Country Country { get; set; }
        public virtual ICollection<County> Counties { get; set; }
    }
}
