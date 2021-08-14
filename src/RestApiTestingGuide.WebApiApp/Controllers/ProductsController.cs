using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestApiTestingGuide.WebApiApp.Models;

namespace RestApiTestingGuide.WebApiApp.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly ProductRepository _repository;

        public ProductsController(ProductRepository repository, ILogger<ProductsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public IReadOnlyList<Product> Get()
        {
            _logger.LogInformation("Get Products");

            return _repository.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id)
        {
            _logger.LogInformation($"Get Product: {id}");

            var product = _repository.Get(id);
            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Product product)
        {
            _logger.LogInformation($"Add Product: {product.Id}");

            if (product.Id <= 0)
            {
                return BadRequest();
            }

            var isUpdated = _repository.Add(product);
            return isUpdated ? Ok() : Forbid();
        }

        [HttpPut]
        public ActionResult Put([FromBody] Product product)
        {
            _logger.LogInformation($"Put Product: {product.Id}");

            if (product.Id <= 0)
            {
                return BadRequest();
            }

            var isAdded = _repository.AddOrUpdate(product);
            return isAdded ? CreatedAtAction(nameof(Get), product) : Ok();
        }

        [HttpPatch("{id}")]
        public ActionResult Patch([FromBody] Product product)
        {
            _logger.LogInformation($"Patch Product: {product.Id}");

            if (product.Id <= 0)
            {
                return BadRequest();
            }

            var isUpdated = _repository.Update(product);
            return isUpdated ? Ok() : NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _logger.LogInformation($"Delete Product: {id}");

            var isDeleted = _repository.Delete(id);
            return isDeleted ? Ok() : NotFound();
        }
    }
}
