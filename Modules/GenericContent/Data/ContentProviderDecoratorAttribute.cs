// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.Data.ContentProviderDecoratorAttribute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Modules.GenericContent.Data
{
  /// <summary>
  /// This attribute is used to tell <see cref="T:Telerik.Sitefinity.Modules.GenericContent.ContentDataProviderBase" /> class which data provider
  /// decorator to use.
  /// </summary>
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
  public sealed class ContentProviderDecoratorAttribute : Attribute
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.GenericContent.Data.ContentProviderDecoratorAttribute" /> class.
    /// </summary>
    /// <param name="decoratorType">Type of the decorator.</param>
    public ContentProviderDecoratorAttribute(Type decoratorType) => this.DecoratorType = decoratorType;

    /// <summary>Gets or sets the type of the decorator.</summary>
    /// <value>The type of the decorator.</value>
    public Type DecoratorType { get; private set; }
  }
}
