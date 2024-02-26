using System;
using System.Collections.Generic;

namespace eCommerce.Domain.Entities
{
    public class Product
    {
        #region Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public string UOM { get; set; }
        public decimal Price { get; set; }
        #endregion
    }

    public class ProductJson
    {
        #region Properties
        public IEnumerable<Product> Products { get; set; }
        public int RowsCount { get; set; }
        #endregion
    }
}