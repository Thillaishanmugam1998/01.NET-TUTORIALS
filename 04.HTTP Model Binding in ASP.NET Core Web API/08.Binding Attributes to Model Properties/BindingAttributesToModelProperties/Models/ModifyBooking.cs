namespace BindingAttributesToModelProperties.Models
{
    public class ModifyBooking
    {
        #region UPDATE DETAILS SENT IN REQUEST BODY
        // This model represents the JSON payload used
        // to update an existing booking.
        #endregion

        public int BookingId { get; set; }

        public string? UpdatedRoomType { get; set; }

        public bool? IncludeBreakfast { get; set; }
    }
}
