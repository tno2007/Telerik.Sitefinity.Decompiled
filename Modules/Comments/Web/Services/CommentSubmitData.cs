// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Comments.Web.Services.CommentSubmitData
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Modules.Comments.Web.Services
{
  [Obsolete("Use Telerik.Sitefinity.Services.Comments.DTO.CommentCreateRequest instead.")]
  [DataContract]
  internal class CommentSubmitData
  {
    private Dictionary<string, string> details;
    private const string nameKey = "Name";
    private const string emailKey = "Email";

    [DataMember]
    public Dictionary<string, string> Details
    {
      get
      {
        this.details = this.details ?? new Dictionary<string, string>();
        return this.details;
      }
      set => this.details = value;
    }

    internal string Name
    {
      get
      {
        string str;
        return this.Details.TryGetValue(nameof (Name), out str) ? str : (string) null;
      }
      set => this.Details[nameof (Name)] = value;
    }

    internal string Email
    {
      get
      {
        string str;
        return this.Details.TryGetValue(nameof (Email), out str) ? str : (string) null;
      }
      set => this.Details[nameof (Email)] = value;
    }

    [DataMember]
    public string Message { get; set; }

    [DataMember]
    public string Thread { get; set; }

    [DataMember]
    public string Language { get; set; }

    [DataMember]
    public Decimal? Rating { get; set; }
  }
}
