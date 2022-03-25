using System;
using System.Collections.Generic;

#nullable disable

namespace carBuisnnesLogic
{
    public partial class CarRentalField
    {
        public int No { get; set; }
        public DateTime StartDay { get; set; }
        public DateTime ReturnDate { get; set; }
        public DateTime? ActualReturnDate { get; set; }
        public int UserId { get; set; }
        public int CarNumber { get; set; }
    }
}
