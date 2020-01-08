using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Final_Project.Models
{
    public class ContactViewModels
    {
        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "***Name is async required field***")]
        public string Name { get; set; }

        [StringLength(25, ErrorMessage = "*** Email too long ***")]
        [Required(ErrorMessage = "***Email is async required field***")]
        [DataType(DataType.EmailAddress, ErrorMessage = "*** Please enter a valid email ***")]
        //in JavaScript we validated our emaili testing it against a
        //regulat expression, in C# (server-side validation) we have this
        //built-in to a data annotation
        public string Email { get; set; }
        public string Subject { get; set; }
        [Required(ErrorMessage = "*** Message is a required field ***")]
        //we do not want our message area in the form to be a text box
        //we want this to be a textarea not input type="text"
        [UIHint("MultilineText")]
        public string Message { get; set; }
    }
}