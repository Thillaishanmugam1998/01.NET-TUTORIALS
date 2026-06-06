namespace ContentNegotiationDemo.Models
{
    public class Employee
    {
        #region SAMPLE RESOURCE RETURNED BY THE API
        // This class is used to demonstrate how the same C# object
        // can be serialized into JSON or XML based on the Accept header.
        #endregion

        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Department { get; set; } = string.Empty;

        public decimal Salary { get; set; }

        public string Email { get; set; } = string.Empty;
    }
}
