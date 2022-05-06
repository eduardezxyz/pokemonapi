using System;
using System.Collections.Generic;

namespace Demoapi.EntityModels
{
    public partial class Crop
    {
        public Crop()
        {
            Farmers = new HashSet<Farmer>();
        }

        public int Id { get; set; }
        public string CropName { get; set; } = null!;
        public int? CropSqft { get; set; }

        public virtual ICollection<Farmer> Farmers { get; set; }
    }
}
