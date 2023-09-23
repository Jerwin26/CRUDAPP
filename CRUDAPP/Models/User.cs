using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CRUDAPP.Models
{
    public class User
    {
        public int UserID { get; set; }

        [Display(Name = "First name")]
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [Display(Name = "Date of birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Date of birth is required")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Gender")]
        [Required(ErrorMessage = "Gender is required")]
        public String Gender { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Address is required")]
        public String Address { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "City is required")]
        public String City { get; set; }

        [Display(Name = "State")]
        [Required(ErrorMessage = "State is required")]
        public String State { get; set; }

        [Display(Name = "Postal code")]
        [Required(ErrorMessage = "Postal Code is required")]
        public String PostalCode { get; set; }

        [Display(Name = "Country")]
        [Required(ErrorMessage = "Country is required")]
        public String Country { get; set; }

        [Display(Name = "Mobile")]
        [Required(ErrorMessage = "Mobile is required")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Please enter a valid 10-digit phone number")]
        public String Phone { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public String Email { get; set; }

        [Display(Name = "Username")]
        [Required(ErrorMessage = "Username is required")]
        public String Username { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public String Password { get; set; }

        [Display(Name = "Profile picture")]
        public byte[] ProfilePicture { get; set; }
    }
}
