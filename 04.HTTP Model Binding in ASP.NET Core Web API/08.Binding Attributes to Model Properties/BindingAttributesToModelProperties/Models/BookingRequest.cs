using Microsoft.AspNetCore.Mvc;

namespace BindingAttributesToModelProperties.Models
{
    public class BookingRequest
    {
        #region PROPERTY LEVEL BINDING ATTRIBUTES
        // Each property defines where its value comes from.
        // This keeps the controller action small and readable.
        #endregion

        [FromRoute(Name = "hotelId")]
        public int HotelId { get; set; }

        [FromQuery]
        public DateTime CheckIn { get; set; }

        [FromQuery]
        public DateTime CheckOut { get; set; }

        [FromHeader(Name = "X-User-Token")]
        public string UserToken { get; set; } = string.Empty;

        [FromBody]
        public BookingDetails Details { get; set; } = new();
    }
}
