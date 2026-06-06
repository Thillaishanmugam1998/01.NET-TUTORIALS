using BindingAttributesToModelProperties.Models;
using Microsoft.AspNetCore.Mvc;

namespace BindingAttributesToModelProperties.Controllers
{
    [ApiController]
    [Route("api/with-binding-attributes/hotels")]
    public class WithBindingAttributesController : ControllerBase
    {
        #region WITH PROPERTY LEVEL BINDING ATTRIBUTES
        // This controller follows the property-level approach
        // shown in the referenced tutorial.
        //
        // Benefits:
        // 1. Cleaner controller methods
        // 2. Less repeated binding code
        // 3. Easier maintenance and reuse
        #endregion

        #region BOOK ROOM
        // POST: /api/with-binding-attributes/hotels/501/book?checkIn=2025-12-01&checkOut=2025-12-05
        // Header: X-User-Token: abc123
        // Body: BookingDetails JSON
        #endregion
        [HttpPost("{hotelId}/book")]
        public IActionResult BookRoom(BookingRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.UserToken))
            {
                return Unauthorized("User token is missing.");
            }

            return Ok(new
            {
                Approach = "Property-level binding attributes",
                request.HotelId,
                request.CheckIn,
                request.CheckOut,
                request.UserToken,
                request.Details.GuestName,
                request.Details.RoomType,
                request.Details.Guests,
                request.Details.IncludeBreakfast
            });
        }

        #region MODIFY BOOKING
        // PUT: /api/with-binding-attributes/hotels/501/booking/modify
        // Header: X-User-Token: abc123
        // Body: ModifyBooking JSON
        #endregion
        [HttpPut("{hotelId}/booking/modify")]
        public IActionResult ModifyBooking(ModifyBookingRequest request)
        {
            return Ok(new
            {
                Approach = "Property-level binding attributes",
                Message = "Booking updated successfully.",
                request.HotelId,
                request.UserToken,
                request.Modification.BookingId,
                request.Modification.UpdatedRoomType,
                request.Modification.IncludeBreakfast
            });
        }

        #region CANCEL BOOKING
        // DELETE: /api/with-binding-attributes/hotels/501/booking/cancel?bookingId=9001
        // Header: X-User-Token: abc123
        #endregion
        [HttpDelete("{hotelId}/booking/cancel")]
        public IActionResult CancelBooking(CancelBookingRequest request)
        {
            return Ok(new
            {
                Approach = "Property-level binding attributes",
                Message = $"Booking {request.BookingId} for Hotel {request.HotelId} canceled successfully.",
                request.UserToken
            });
        }
    }
}
