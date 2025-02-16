namespace Microstore.Service.OrderingDomain.Exceptions;

[Serializable]
public class DomainException : Exception
{
    public DomainException(string message) 
        : base($"Domain Exception: \"{message}\" thrown from Domain Layer.")
    {
    }
}