namespace HandlerService.Contracts;

internal record ProductRequest(List<long> ProductIds, Guid StoreId);