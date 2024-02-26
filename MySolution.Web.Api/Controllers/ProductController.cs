using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using eCommerce.Domain.Entities;
using eCommerce.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace eCommerce.Web.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        #region Variables
        private readonly IProductServices _productServices;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public ProductController(IProductServices productServices, IMapper mapper)
        {
            _productServices = productServices;
            _mapper = mapper;
        }
        #endregion

        #region Actions
        [HttpGet("[action]")]
        public async Task<ProductJson> ListAsync(int skip, int take)
        {
            var products = await _productServices.GetListAsync(skip, take);
            var productsCount = await _productServices.GetListCount();

            ProductJson p = new ProductJson();
            p.Products = products;
            p.RowsCount = productsCount;

            return p;
        }

        [HttpGet("{id}")]
        public async Task<Product> GetAsync(int id)
        {
            return await _productServices.GetAsync(id);
        }

        [HttpPost]
        public async Task<bool> AddAsync([FromBody] Product product)
        {
            return await _productServices.AddAsync(product);
        }

        [HttpPut]
        public async Task<bool> UpdateAsync([FromBody] Product product)
        {
            return await _productServices.UpdateAsync(product);
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteAsync(int id)
        {
            return await _productServices.DeleteAsync(id);
        }
        #endregion
    }
}