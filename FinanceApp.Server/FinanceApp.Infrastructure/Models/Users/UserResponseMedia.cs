﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Infrastructure.Models.Users
{
    public class UserResponseMedia
    {
        public Guid id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
    }
}
