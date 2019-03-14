using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaizen.Model
{
    public class Branch : BaseEntity
    {
        public string Name { get; set; }
        public Guid CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public Guid? CountryId { get; set; }
        public virtual Country Country { get; set; }
        public Guid? CityId { get; set; }
        public virtual City City { get; set; }
        public Guid? CountyId { get; set; }
        public virtual County County { get; set; }
        public string Address { get; set; }
    }
}
