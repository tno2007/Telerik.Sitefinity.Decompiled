// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.Web.Services.Common.WcTaxonDataFlags
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Taxonomies.Web.Services.Common
{
  [Flags]
  internal enum WcTaxonDataFlags
  {
    None = 0,
    SetSynonyms = 1,
    SetMarkedItemsCount = 2,
    All = SetMarkedItemsCount | SetSynonyms, // 0x00000003
    SetTitlePath = 4,
    AutoComplete = SetTitlePath | SetSynonyms, // 0x00000005
  }
}
