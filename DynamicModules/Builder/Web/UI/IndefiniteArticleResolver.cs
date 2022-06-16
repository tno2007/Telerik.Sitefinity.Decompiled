// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Web.UI.IndefiniteArticleResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.DynamicModules.Builder.Web.UI
{
  internal class IndefiniteArticleResolver
  {
    private string prefix;

    public void ResolveModuleTypeName(IDynamicModuleType moduleType) => this.prefix = this.IsFirstLetterVowel(moduleType.DisplayName) ? IndefiniteArticleResolver.GetResourceLabelAn().ToLower() : IndefiniteArticleResolver.GetResourceLabelA().ToLower();

    public string Prefix => this.prefix;

    private bool IsFirstLetterVowel(string moduleName) => moduleName != null ? new HashSet<char>()
    {
      'a',
      'o',
      'e',
      'u',
      'i'
    }.Contains(char.ToLower(moduleName[0])) : throw new ArgumentNullException("moduleTypeDisplayName");

    private static string GetResourceLabelAn() => Res.Get<Labels>("An");

    private static string GetResourceLabelA() => Res.Get<Labels>("A");
  }
}
