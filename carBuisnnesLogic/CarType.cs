using System;
using System.Collections.Generic;

#nullable disable

namespace carBuisnnesLogic
{
    public partial class CarType
    {
        public int IdOfCar { get; set; }
        public string ManufactureName { get; set; }
        public string CarModel { get; set; }
        public int? DailyCost { get; set; }
        public int? DailyDelayCost { get; set; }
        public int? YearOfProduction { get; set; }
        public string Gear { get; set; }
    }
}
