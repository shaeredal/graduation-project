using System;
using OnlinerNotifier.BLL.Mappers.Interfaces;
using OnlinerNotifier.BLL.Models.OnlinerDataModels;
using OnlinerNotifier.DAL.Models;

namespace OnlinerNotifier.BLL.Mappers.Implementations
{
    public class PriceChangesMapper : IPriceChangesMapper
    {
        public ProductPriceChange ToDomain(Product productOld, PriceOnliner newPrice)
        {
            return new ProductPriceChange()
            {
                CheckTime = DateTime.Now,
                Product = productOld,
                OldMinPrice = productOld.MinPrice,
                NewMinPrice = newPrice.Min,
                OldMaxPrice = productOld.MaxPrice,
                NewMaxPrice = newPrice.Max
            };
        }
    }
}
