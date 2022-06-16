// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Events.FormEntryEditContext
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Modules.Forms.Events
{
  internal class FormEntryEditContext : IFormEntryEditContext
  {
    public Guid EntryId { get; set; }

    public User User { get; set; }

    public string IpAddress { get; set; }

    public Guid FormId { get; set; }

    public string FormName { get; set; }

    public string FormTitle { get; set; }

    public string EntryTypeName { get; set; }

    public string ProviderName { get; set; }
  }
}
