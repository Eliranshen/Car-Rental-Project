using System;
using System.Collections.Generic;

#nullable disable

namespace carBuisnnesLogic
{
    public partial class UserDetail
    {
        public string FullName { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public DateTime? Bdate { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Picture { get; set; }
        public string Role { get; set; }
    }
}
