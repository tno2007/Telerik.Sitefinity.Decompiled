// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Decorators.MissingDecoratorException
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.Data.Decorators
{
  /// <summary>
  /// This exception is thrown in the case when data provider decorator has not been found and the method which uses
  /// data provider decorator has not been overridden.
  /// </summary>
  public class MissingDecoratorException : Exception
  {
    private string message;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.Decorators.MissingDecoratorException" /> class.
    /// </summary>
    /// <param name="dataProvider">The data provider.</param>
    /// <param name="methodName">Name of the method.</param>
    public MissingDecoratorException(DataProviderBase dataProvider, string methodName) => this.message = Res.Get<DataResources>().MissingProviderDecorator.Arrange((object) dataProvider.GetType().FullName, (object) methodName);

    /// <summary>Gets a message that describes the current exception.</summary>
    /// <value></value>
    /// <returns>
    /// The error message that explains the reason for the exception, or an empty string("").
    /// </returns>
    public override string Message => this.message;
  }
}
