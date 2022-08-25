﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Areas.StaffAccount.Models
{
    public class Staff : IdentityUser<int>
    {
        public override int Id { get; set; }
        [Required]
        public string FullName { get; set; }
    }
}
