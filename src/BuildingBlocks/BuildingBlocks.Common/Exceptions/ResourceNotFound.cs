namespace BuildingBlocks.Common.Exceptions;

public class ResourceNotFound(string resourceType, string identifier) 
    : Exception($"Resource of type '{resourceType}' with identifier '{identifier}' was not found");