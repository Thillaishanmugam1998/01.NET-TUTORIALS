using Microsoft.AspNetCore.Mvc;

namespace BindingAttributesToModelProperties.Models
{
    public class CancelBookingRequest
    {
        #region PROPERTY LEVEL BINDING ATTRIBUTES
        // The cancellation request combines route, query, and header data.
        #endregion

        [FromRoute(Name = "hotelId")]
        public int HotelId { get; set; }

        [FromQuery]
        public int BookingId { get; set; }

        [FromHeader(Name = "X-User-Token")]
        public string UserToken { get; set; } = string.Empty;
    }
}
