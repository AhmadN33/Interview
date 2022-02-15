using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace WebApplication1
{
    public partial class User
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "Please type your username.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please type your Password.")]
        public string Password { get; set; }
        public string DisplayName { get; set; }
        public bool? Active { get; set; }
    }
}
