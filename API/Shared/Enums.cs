namespace FoodDelivery.Shared
{
    public class Enums
    {
        public enum UsersRoles
        {
            Admin = 0,
            Customer,
            Couirer
        }

        public enum OrdersStatus
        {
            Created = 0,
            Finished,
            Canceled
        }
    }
}
