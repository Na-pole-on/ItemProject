namespace Client.Models.Items
{
    public class ItemViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string Price { get; set; }
        public string Quentity { get; set; }
        public string PhotoUrl { get; set; }
    }
}
