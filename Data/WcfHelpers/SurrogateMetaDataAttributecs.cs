// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.WcfHelpers.SurrogateMetaDataAttribute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Data.WcfHelpers
{
  /// <summary>
  /// A class for specifying the metadata class and method used for constructing a custom surrogate.
  /// The class and method muss be static.
  /// </summary>
  [AttributeUsage(AttributeTargets.Class)]
  internal class SurrogateMetaDataAttribute : Attribute
  {
    private Type metaObjectContainer;
    private string methodName;

    /// <summary>Creates a new instance of the class.</summary>
    public SurrogateMetaDataAttribute()
    {
    }

    /// <summary>Creates a new instance of the class.</summary>
    /// <param name="methodName">The name of the method that will be invoked to retrieve the metadata.</param>
    /// <param name="metaObjectContainer">The type of the class that will hold the method to be invoked.</param>
    public SurrogateMetaDataAttribute(string methodName, Type metaObjectContainer)
    {
      this.metaObjectContainer = metaObjectContainer;
      this.methodName = methodName;
    }

    /// <summary>
    /// Gets or sets the type of the class that will hold the method to be invoked.
    /// </summary>
    public Type MetaObjectContainer
    {
      get => this.metaObjectContainer;
      set => this.metaObjectContainer = value;
    }

    /// <summary>
    /// Gets or sets the name of the method that will be invoked to retrieve the metadata.
    /// </summary>
    public string MethodName
    {
      get => this.methodName;
      set => this.methodName = value;
    }
  }
}
