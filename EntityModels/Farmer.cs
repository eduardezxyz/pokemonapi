using System;
using System.Collections.Generic;

namespace Demoapi.EntityModels
{
    public partial class Farmer
    {
        public Farmer()
        {
            Crops = new HashSet<Crop>();
        }

        public int Id { get; set; }
        public string FarmerName { get; set; } = null!;
        public int? Age { get; set; }
        public int FarmerTypeId { get; set; }
        public int? YearsFarming { get; set; }

        public virtual FarmerType FarmerType { get; set; } = null!;

        public virtual ICollection<Crop> Crops { get; set; }
    }
}
