using CatalogAPI.Entities;
using CatalogAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogAPI.Controllers
{
    [ApiController]
    [Route("api/vi/[controller]")]
    public class CatalogController : ControllerBase
    {

        private readonly IProductRepository _productRepository;

        private readonly ILogger<CatalogController> _logger;

        public CatalogController(IProductRepository productRepository, ILogger<CatalogController> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Product>))]

        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productRepository.GetProducts();

            return Ok(products);
        }

        [HttpGet("{id:length(24)}",Name="GetProduct")]
        [ProducesResponseType((int)StatusCodes.Status404NotFound)]
        [ProducesResponseType((int) StatusCodes.Status200OK)]

        public async Task<ActionResult<Product>> GetProduct(string id)
        {
            var product = await _productRepository.Getproduct(id);
            if(product == null)
            {
                _logger.LogError($"Product with {id} not found");
                return NotFound();
            }
            return Ok(product);
        }

        [Route("[action]/{category}",Name ="GetProductByCategory")]
        [HttpGet]
        [ProducesResponseType((int)StatusCodes.Status200OK)]

        public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategory(string category)
        {
            var products = await _productRepository.GetProductByCategory(category);
            if (products == null)
            {
                _logger.LogError($"Product with {category} not found");
                return NotFound();
            }
            return Ok(products);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Product),StatusCodes.Status200OK)]

        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product p)
        {
            await _productRepository.CreateProduct(p);

            return CreatedAtRoute("GetProduct", new { id = p.Id, p });
        }

        [HttpPut]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]

        public async Task<IActionResult> UpdateProduct([FromBody] Product p)
        {

            return Ok(await _productRepository.UpdateProduct(p));
        }

        [HttpDelete]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            return Ok(await _productRepository.DeleteProduct(id));
        }



    }
}
