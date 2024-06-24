namespace StoreService.Core.Exceptions;

public class DuplicateEntryException<T>(string info, string repoName, T entry)
    : Exception($"Duplicate entry in repository {repoName}: {info}")
{
    public T Enrty = entry;
}