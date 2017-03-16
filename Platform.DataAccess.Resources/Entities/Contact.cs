using System.ComponentModel.DataAnnotations;

namespace Platform.DataAccess.Resources.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class Contact
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string Title { get; set; }
        
        public bool IsCompany { get; set; }
        
        public string ContactType { get; set; }
        
        public string Email { get; set; }
        
        public string VatNumber { get; set; }
        
        public string PhoneNumber { get; set; }

        public Address Address { get; set; }
    }
}
