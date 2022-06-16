// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.FacadeHelper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Fluent
{
  /// <summary>
  /// Provides a set of useful methods that are commonly used in Content Fluent API
  /// </summary>
  public static class FacadeHelper
  {
    /// <summary>Throw an exception if a condition fails</summary>
    /// <param name="throwExceptionIfFalse">Condition, which would trigger exception if evaluated to false</param>
    /// <param name="message">Message to be used if an exception should be thrown</param>
    /// <exception cref="T:System.InvalidOperationException">If <paramref name="throwExceptionIfFalse" /> is false</exception>
    public static void Assert(bool throwExceptionIfFalse, string message)
    {
      if (!throwExceptionIfFalse)
        throw new InvalidOperationException(message);
    }

    /// <summary>Throw an exception if an argument is null</summary>
    /// <typeparam name="TReference">Type of the argument to check</typeparam>
    /// <param name="argument">Argument to be compared to null</param>
    /// <param name="argumentName">Name of the argument. Optional</param>
    /// <exception cref="T:System.ArgumentNullException">If <paramref name="argument" /> is null</exception>
    public static void AssertArgumentNotNull<TReference>(TReference argument, string argumentName) where TReference : class
    {
      if ((object) argument != null)
        return;
      if (argumentName.IsNullOrWhitespace())
        throw new ArgumentNullException();
      throw new ArgumentNullException(argumentName);
    }

    /// <summary>Throw an exception if an argument is null</summary>
    /// <typeparam name="TReference">Reference type</typeparam>
    /// <param name="argument">parameter to test against null</param>
    /// <param name="message">Message of the exception to throw</param>
    /// <exception cref="T:System.InvalidOperationException">If <paramref name="argument" /> is null</exception>
    public static void AssertNotNull<TReference>(TReference argument, string message) where TReference : class
    {
      if ((object) argument == null)
        throw new InvalidOperationException(message);
    }

    /// <summary>Throws a specific exception if a condition fails</summary>
    /// <typeparam name="TException">Type of the exception to throw</typeparam>
    /// <param name="throwExceptionIfFalse">Exception will be thrown if this is false</param>
    /// <param name="message">Message of the exception to throw</param>
    public static void Assert<TException>(bool throwExceptionIfFalse, string message) where TException : Exception
    {
      if (!throwExceptionIfFalse)
        throw (object) (TException) Activator.CreateInstance(typeof (TException), (object) message);
    }

    /// <summary>Throws an exception if a string is not a valid email</summary>
    /// <param name="email">String to test</param>
    /// <param name="message">Message of the exception to throw</param>
    /// <exception cref="T:System.FormatException">If <paramref name="email" /> is not a valid email.</exception>
    public static void AssertIsValidEmail(string email, string message)
    {
      if (email.IsNullOrWhitespace())
        throw new FormatException(message);
    }

    /// <summary>
    /// Throws an exception if a string is not a valid ip address
    /// </summary>
    /// <param name="ipAddresss">String to validate</param>
    /// <param name="message">Message of the exception to throw</param>
    public static void AssertIsValidIPAddress(string ipAddresss, string message)
    {
      if (ipAddresss.IsNullOrWhitespace())
        throw new FormatException(message);
    }

    /// <summary>
    /// Throws an exception if a stringis not a valid mime extension
    /// </summary>
    /// <param name="extension">String to validate</param>
    /// <param name="message">Message of the exception to throw</param>
    public static void AssertIsValidMimeExtension(string extension, string message)
    {
      if (extension.IsNullOrWhitespace())
        throw new FormatException(message);
    }
  }
}
