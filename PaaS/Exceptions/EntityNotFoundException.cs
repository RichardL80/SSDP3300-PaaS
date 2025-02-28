namespace PaaS.Exceptions;

public class EntityNotFoundException(string message) : Exception(message)
{
}