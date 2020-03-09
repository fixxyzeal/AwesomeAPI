using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ServiceLB.Models
{
    public class Auth
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}