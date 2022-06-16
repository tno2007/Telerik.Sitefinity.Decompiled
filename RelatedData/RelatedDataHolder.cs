// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.RelatedData.RelatedDataHolder
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.RelatedData
{
  internal class RelatedDataHolder : IRelatedDataHolder, IDataItem
  {
    public Guid ItemId { get; set; }

    public string ProviderName { get; set; }

    public string FullTypeName { get; set; }

    public ContentLifecycleStatus Status { get; set; }

    Guid IDataItem.Id => throw new NotImplementedException();

    object IDataItem.Transaction
    {
      get => throw new NotImplementedException();
      set => throw new NotImplementedException();
    }

    object IDataItem.Provider
    {
      get => throw new NotImplementedException();
      set => throw new NotImplementedException();
    }

    DateTime IDataItem.LastModified
    {
      get => throw new NotImplementedException();
      set => throw new NotImplementedException();
    }

    string IDataItem.ApplicationName
    {
      get => throw new NotImplementedException();
      set => throw new NotImplementedException();
    }
  }
}
