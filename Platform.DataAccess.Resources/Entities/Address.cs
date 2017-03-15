using System.ComponentModel.DataAnnotations;

namespace Platform.DataAccess.Resources.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class Address
    {
        [Key]
        public int Id { get; set; }
        public string AddressLine { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Zip { get; set; }
    }
}
