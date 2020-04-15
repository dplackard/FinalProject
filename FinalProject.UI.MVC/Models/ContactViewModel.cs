using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.UI.MVC.Models
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "*Name is REQUIRED")]
        [StringLength(50, ErrorMessage = "*Value must be 50 characters or less")]
        public string Name { get; set; }

        [Required(ErrorMessage = "*Email is REQUIRED")]
        [EmailAddress]
        [StringLength(50, ErrorMessage = "*Value must be 50 characters or less")]
        public string Email { get; set; }

        [Required(ErrorMessage = "*Subject is REQUIRED")]
        [StringLength(150, ErrorMessage = "*Value must be 150 characters or less")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "*Message is REQUIRED")]
        [UIHint("MultilineText")]
        [StringLength(1000, ErrorMessage = "*Value must be 1000 characters or less")]
        public string Message { get; set; }
    }
}