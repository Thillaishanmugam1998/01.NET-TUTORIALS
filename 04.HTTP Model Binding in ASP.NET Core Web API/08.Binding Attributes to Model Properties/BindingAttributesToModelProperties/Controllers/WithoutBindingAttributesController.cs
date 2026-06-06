using BindingAttributesToModelProperties.Models;
using Microsoft.AspNetCore.Mvc;

namespace BindingAttributesToModelProperties.Controllers
{
    [ApiController]
    [Route("api/without-binding-attributes/hotels")]
    public class WithoutBindingAttributesController : ControllerBase
    {
        #region ACTION LEVEL BINDING ATTRIBUTES
        // This controller follows the first approach from the tutorial.
        // Binding attributes are applied directly on action parameters.
        //
        // Drawback:
        // As the API grows, action methods become longer and repetitive.
        #endregion

        #region BOOK ROOM
        // POST: /api/without-binding-attributes/hotels/501/book?checkIn=2025-12-01&checkOut=2025-12-05
        // Header: X-User-Token: abc123
        // Body: BookingDetails JSON
        #endregion
        [HttpPost("{hotelId}/book")]
        public IActionResult BookRoom(
            [FromRoute] int hotelId,
            [FromQuery] DateTime checkIn,
            [FromQuery] DateTime checkOut,
            [FromHeader(Name = "X-User-Token")] string userToken,
            [FromBody] BookingDetails details)
        {
            if (string.IsNullOrWhiteSpace(userToken))
            {
                return Unauthorized("User token is missing.");
            }

            return Ok(new
            {
                Approach = "Action-level binding attributes",
                HotelId = hotelId,
                CheckIn = checkIn,
                CheckOut = checkOut,
                UserToken = userToken,
                details.GuestName,
                details.RoomType,
                details.Guests,
                details.IncludeBreakfast
            });
        }

        #region MODIFY BOOKING
        // PUT: /api/without-binding-attributes/hotels/501/booking/modify
        // Header: X-User-Token: abc123
        // Body: ModifyBooking JSON
        #endregion
        [HttpPut("{hotelId}/booking/modify")]
        public IActionResult ModifyBooking(
            [FromRoute] int hotelId,
            [FromHeader(Name = "X-User-Token")] string userToken,
            [FromBody] ModifyBooking modification)
        {
            return Ok(new
            {
                Approach = "Action-level binding attributes",
                Message = "Booking updated successfully.",
                HotelId = hotelId,
                UserToken = userToken,
                modification.BookingId,
                modification.UpdatedRoomType,
                modification.IncludeBreakfast
            });
        }

        #region CANCEL BOOKING
        // DELETE: /api/without-binding-attributes/hotels/501/booking/cancel?bookingId=9001
        // Header: X-User-Token: abc123
        #endregion
        [HttpDelete("{hotelId}/booking/cancel")]
        public IActionResult CancelBooking(
            [FromRoute] int hotelId,
            [FromQuery] int bookingId,
            [FromHeader(Name = "X-User-Token")] string userToken)
        {
            return Ok(new
            {
                Approach = "Action-level binding attributes",
                Message = $"Booking {bookingId} for Hotel {hotelId} canceled successfully.",
                UserToken = userToken
            });
        }
    }
}
