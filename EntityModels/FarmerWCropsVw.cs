using System;
using System.Collections.Generic;

namespace Demoapi.EntityModels
{
    public partial class FarmerWCropsVw
    {
        public int? Id { get; set; }
        public string? FarmerName { get; set; }
        public int? Age { get; set; }
        public int? FarmerTypeId { get; set; }
        public string? FarmerType { get; set; }
        public int? CropId { get; set; }
        public string? CropName { get; set; }
        public int? CropSqft { get; set; }
    }
}
