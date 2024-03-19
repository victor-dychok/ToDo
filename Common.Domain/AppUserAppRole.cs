﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain
{
    public class AppUserAppRole
    {
        public int Id { get; set; }
        public int UserId {  get; set; }
        public int RoleId { get; set; }
        public virtual AppUser User { get; set; }
        public virtual AppUserRole Role { get; set; }
    }
}
