namespace Mango.WebApp.Models.Utility
{
    public class SD
    {
        public enum ApiType
        {
            GET,
            POST,
            DELETE,
            PUT
        }

        //here we store our URL
        public static string CouponAPIBase { get; set; }
        public static string AuthAPIBase { get; set; }
    }
}
