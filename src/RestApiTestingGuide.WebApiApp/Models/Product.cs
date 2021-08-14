namespace RestApiTestingGuide.WebApiApp.Models
{
    public record Product
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public void Update(Product updateProduct)
        {
            if (updateProduct.Description != null)
            {
                Description = updateProduct.Description;
            }
            if (updateProduct.Name != null)
            {
                Name = updateProduct.Name;
            }
        }
    }
}
