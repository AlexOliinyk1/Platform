using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Core.Models.Contacts
{
    class ContactModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Company")]
        public bool IsCompany { get; set; }

        [Display(Name = "Type")]
        public ContactTypes ContatctType { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Vat Number")]
        public int VatNumber { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public string Address{ get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Zip { get; set; }

    }
}
