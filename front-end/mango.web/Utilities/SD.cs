namespace mango.web.Utilities;

public class SD
{
    public static string CouponAPIURL{get;set;}
    public static string ProductAPIURL { get; set; }
    public static string AuthAPIBase { get; set; }
    public enum APIType
    {
        GET,
        POST,
        PUT,
        DELETE
    }

}
