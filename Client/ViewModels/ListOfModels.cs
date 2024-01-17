using Client.Models;
using Client.Models.Items;

namespace Client.ViewModels
{
    public class ListOfModels
    {
        public List<ItemViewModel> Items { get; set; }
        public List<UpdateItemViewModel> Item { get; set; }
    }
}
