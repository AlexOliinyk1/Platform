using System.ComponentModel.DataAnnotations;

namespace Platform.Core.Models.Contacts
{
    public class ContactModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Company")]
        public bool IsCompany { get; set; }

        [Display(Name = "Type")]
        public string ContactType { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Vat Number")]
        public string VatNumber { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        public string CustomerType { get; set; }
        public string Zip { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
