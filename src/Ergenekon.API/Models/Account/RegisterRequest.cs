﻿using System.ComponentModel.DataAnnotations;

namespace Ergenekon.API.Models.Account
{
    public class RegisterRequest
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }

    public class RequireConfirmedAccountResponse
    {
        public bool RequireConfirmedAccount { get; set; }
        public string Email { get; set; }
    }

    public class RegisterResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
