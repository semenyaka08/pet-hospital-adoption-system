namespace PHA.Domain.DomainExceptions;

public class DomainException(string message) : Exception($"Domain Exception: \"{message}\" throws from Domain Layer" );