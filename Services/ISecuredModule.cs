// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.ISecuredModule
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Services
{
  public interface ISecuredModule
  {
    /// <summary>Gets the security roots for this module.</summary>
    /// <param name="getContextRootsOnly">If set to true, returns only the security roots relevant explicitly to the current site (not including system providers).</param>
    /// <returns>The list of security roots for the module</returns>
    IList<SecurityRoot> GetSecurityRoots(bool getContextRootsOnly = true);
  }
}
