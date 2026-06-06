namespace ProducesConsumesDemo.Models
{
    public class ProductCreateRequest
    {
        #region SAMPLE REQUEST MODEL
        // This model is used to explain what request body formats
        // an endpoint is willing to accept.
        #endregion

        public string Name { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public bool InStock { get; set; }
    }
}
