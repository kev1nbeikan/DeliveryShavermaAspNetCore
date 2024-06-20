using System;

namespace UserService.Core.Exceptions;

public class NotFoundException(string message) : Exception(message);