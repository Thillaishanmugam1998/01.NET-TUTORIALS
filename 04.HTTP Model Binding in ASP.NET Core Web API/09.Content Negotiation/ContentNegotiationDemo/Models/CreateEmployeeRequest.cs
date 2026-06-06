namespace ContentNegotiationDemo.Models
{
    public class CreateEmployeeRequest
    {
        #region SAMPLE REQUEST BODY MODEL
        // This model is used to explain input negotiation.
        // The Content-Type header tells ASP.NET Core how to read this body.
        #endregion

        public string Name { get; set; } = string.Empty;

        public string Department { get; set; } = string.Empty;

        public decimal Salary { get; set; }

        public string Email { get; set; } = string.Empty;
    }
}
