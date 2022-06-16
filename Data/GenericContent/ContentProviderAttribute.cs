// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.GenericContent.ContentProviderAttribute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Data.GenericContent
{
  /// <summary>
  /// Applied on content items, registering the data provider they need to use
  /// </summary>
  [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
  public sealed class ContentProviderAttribute : Attribute
  {
    private readonly Type providerType;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.GenericContent.ContentProviderAttribute" /> class.
    /// </summary>
    /// <param name="providerTypeName">Name of the provider type.</param>
    public ContentProviderAttribute(string providerTypeName)
    {
      Type c = TypeResolutionService.ResolveType(providerTypeName);
      if (!typeof (DataProviderBase).IsAssignableFrom(c))
        Telerik.Sitefinity.Abstractions.ThrowHelper.LocalizedThrowGlobal<NotSupportedException>("TypeDoesNotImplementAnotherType", (object) c, (object) typeof (DataProviderBase));
      this.providerType = c;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.GenericContent.ContentProviderAttribute" /> class.
    /// </summary>
    /// <param name="providerType">Type of the provider.</param>
    public ContentProviderAttribute(Type providerType)
      : this(providerType.FullName)
    {
    }

    public Type ProviderType => this.providerType;
  }
}
