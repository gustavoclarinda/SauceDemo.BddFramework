// Api/Models/AuthRequest.cs
namespace SauceDemo.BddFramework.Api.Models
{
    public class AuthRequest
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}

// Api/Models/AuthResponse.cs
namespace SauceDemo.BddFramework.Api.Models
{
    public class AuthResponse
    {
        public string token { get; set; }
    }
}

// Api/Models/BookingDates.cs
namespace SauceDemo.BddFramework.Api.Models
{
    public class BookingDates
    {
        public string checkin { get; set; }
        public string checkout { get; set; }
    }
}

// Api/Models/Booking.cs
namespace SauceDemo.BddFramework.Api.Models
{
    public class Booking
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public int totalprice { get; set; }
        public bool depositpaid { get; set; }
        public BookingDates bookingdates { get; set; }
        public string additionalneeds { get; set; }
    }
}

// Api/Models/CreateBookingResponse.cs
namespace SauceDemo.BddFramework.Api.Models
{
    public class CreateBookingResponse
    {
        public int bookingid { get; set; }
        public Booking booking { get; set; }
    }
}
