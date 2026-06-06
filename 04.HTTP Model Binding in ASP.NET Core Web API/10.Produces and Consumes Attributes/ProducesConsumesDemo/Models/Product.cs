namespace ProducesConsumesDemo.Models
{
    public class Product
    {
        #region SAMPLE RESPONSE MODEL
        // This model is used to show how the same .NET object
        // can be returned as JSON or XML.
        #endregion

        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public bool InStock { get; set; }
    }
}
