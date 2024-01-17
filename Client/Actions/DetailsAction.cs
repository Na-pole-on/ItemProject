using Client.Models.Cart;

namespace Client.Actions
{
    public class DetailsAction
    {
        public static decimal GetPrice(IEnumerable<CartDetailsViewModel> cartDetails)
        {
            if(cartDetails is not null)
            {
                decimal result = 0;

                foreach (var cd in cartDetails)
                    result += Convert.ToDecimal(cd.Item.Price.Replace(".", ",")) * cd.Count;

                return result;
            }

            return 0;
        }

        public static int GetQuentity(IEnumerable<CartDetailsViewModel> cartDetails)
        {
            if(cartDetails is not null)
            {
                int result = 0;

                foreach (var cd in cartDetails)
                    result += cd.Count;

                return result;
            }

            return 0;
        }
    }
}
