namespace BindingAttributesToModelProperties.Models
{
    public class BookingDetails
    {
        #region BOOKING DETAILS SENT IN REQUEST BODY
        // This model represents the JSON payload sent by the client
        // when a room booking is created.
        #endregion

        public string GuestName { get; set; } = string.Empty;

        public string RoomType { get; set; } = string.Empty;

        public int Guests { get; set; }

        public bool IncludeBreakfast { get; set; }
    }
}
