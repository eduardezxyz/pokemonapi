using System;
using System.Collections.Generic;

namespace Demoapi.EntityModels
{
    public partial class FarmerType
    {
        public FarmerType()
        {
            Farmers = new HashSet<Farmer>();
        }

        public int Id { get; set; }
        public string FarmerType1 { get; set; } = null!;

        public virtual ICollection<Farmer> Farmers { get; set; }
    }
}
