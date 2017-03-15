namespace Platform.Core.Models.Contacts
{
    /// <summary>
    /// 
    /// </summary>
    public class ContactsPagingModel
    {
        public int ByPage { get; set; }
        public int CurrentPage { get; set; }

        public string SearchWord { get; set; }
    }
}
