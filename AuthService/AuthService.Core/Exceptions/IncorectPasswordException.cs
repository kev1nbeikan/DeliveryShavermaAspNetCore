namespace AuthService.Core.Exceptions;

public class IncorectPasswordException(string message) : Exception("incorect password for " + message);