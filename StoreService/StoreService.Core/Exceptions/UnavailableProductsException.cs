using BarsGroupProjectN1.Core.Models.Payment;
using StoreService.Core.Abstractions;

namespace StoreService.Core.Exceptions;

public class UnavailableProductsException(List<ProductsInventoryWithoutStore> products)
    : StoreServiceException($"Not enough products in store: {string.Join(", ", products.Select(p => p.ProductId))}");