﻿using OnlinerNotifier.BLL.Mappers.Interfaces;
using OnlinerNotifier.BLL.Models;
using OnlinerNotifier.DAL.Models;

namespace OnlinerNotifier.BLL.Mappers.Implementations
{
    public class ProductMapper : IProductMapper
    {
        public Product ToDomain(ProductViewModel model)
        {
            return new Product()
            {
                CatalogId = model.OnlinerId,
                CatalogName = model.CatalogName,
                Name = model.Name,
                MaxPrice = model.MaxPrice,
                MinPrice = model.MinPrice,
                Image = model.Image,
                Url = model.Url
            };
        }

        public ProductViewModel ToModel(Product product)
        {
            return new ProductViewModel()
            {
                Id = product.Id,
                OnlinerId = product.CatalogId,
                CatalogName = product.CatalogName,
                Name = product.Name,
                MaxPrice = product.MaxPrice,
                MinPrice = product.MinPrice,
                Image = product.Image,
                Url = product.Url
            };
        }
    }
}
