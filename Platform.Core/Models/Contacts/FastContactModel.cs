using System.ComponentModel.DataAnnotations;

namespace Platform.Core.Models.Contacts
{
    public class FastContactModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
    }
}
