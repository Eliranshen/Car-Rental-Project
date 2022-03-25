using System;
using System.Collections.Generic;

#nullable disable

namespace carBuisnnesLogic
{
    public partial class CarField
    {
        public int IdOfCar { get; set; }
        public int IdOfField { get; set; }
        public string CarType { get; set; }
        public int? CurrentKm { get; set; }
        public string Picture { get; set; }
        public string ProperToRent { get; set; }
        public string FreeForRent { get; set; }
        public int CarNumber { get; set; }
        public int? BranchNumber { get; set; }
    }
}
