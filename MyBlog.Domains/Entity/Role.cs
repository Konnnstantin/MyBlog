﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Domains.Entity
{
    public class Role : IdentityRole
    {
        public string? Description { get; set; }
    }
}
