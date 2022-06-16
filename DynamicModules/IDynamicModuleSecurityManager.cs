// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.IDynamicModuleSecurityManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.DynamicModules
{
  /// <summary>
  /// Defines properties and methods implemented by classes which manage the dynamic modules security.
  /// </summary>
  internal interface IDynamicModuleSecurityManager : IManager, IDisposable, IProviderResolver
  {
    /// <summary>
    /// Gets the secured object for the specified dynamicContentProviderName. If this is the default provider then it returns the specified secured object.
    /// Else if ther related secured object exists for the specified provider and fallback is set to true the again returns the specified secured object.
    /// Else if the related secured object exists returns it, otherwise returns null.
    /// </summary>
    /// <param name="securedObject">The secured object.</param>
    /// <param name="dynamicContentProviderName">Name of the dynamic content provider.</param>
    ISecuredObject GetSecuredObject(
      ISecuredObject securedObject,
      string dynamicContentProviderName);

    /// <summary>
    /// Creates a secured object related to the specified dynamic content provider.
    /// </summary>
    /// <param name="mainSecuredObject">The main secured object.</param>
    /// <param name="dynamicContentProviderName">Name of the dynamic content provider.</param>
    ISecuredObject CreateSecuredObject(
      ISecuredObject mainSecuredObject,
      string dynamicContentProviderName);
  }
}
