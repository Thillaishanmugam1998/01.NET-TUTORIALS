using Microsoft.AspNetCore.Mvc;

namespace BindingAttributesToModelProperties.Models
{
    public class ModifyBookingRequest
    {
        #region PROPERTY LEVEL BINDING ATTRIBUTES
        // Route + header + body values are combined into one request model.
        #endregion

        [FromRoute(Name = "hotelId")]
        public int HotelId { get; set; }

        [FromHeader(Name = "X-User-Token")]
        public string UserToken { get; set; } = string.Empty;

        [FromBody]
        public ModifyBooking Modification { get; set; } = new();
    }
}
