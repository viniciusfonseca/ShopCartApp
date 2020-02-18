namespace ShopCart.Models {

    public class Gender
    {
        public static string M = "M";
        public static string F = "M";
    }

    public class Customer : User
    {
        public int Id { get; set; }
        public string Gender { get; set; }
        public Address Address { get; set; }
    }
}