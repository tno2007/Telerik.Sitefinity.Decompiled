// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ModuleEditor.Web.Services.Model.ModuleEditorContext
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Telerik.Sitefinity.ModuleEditor.Web.Services.Model
{
  /// <summary>Wcf object for tracking changes in the ModuleEditor</summary>
  [DataContract]
  public class ModuleEditorContext
  {
    private IDictionary<string, WcfField> addFields;
    private IList<string> removeFields;

    /// <summary>Gets or sets the type of the content.</summary>
    /// <value>The type of the content.</value>
    [DataMember]
    public string ContentType { get; set; }

    /// <summary>Gets or sets the fields which have to be added.</summary>
    /// <value>The add fields.</value>
    [DataMember]
    public IDictionary<string, WcfField> AddFields
    {
      get
      {
        if (this.addFields == null)
          this.addFields = (IDictionary<string, WcfField>) new Dictionary<string, WcfField>();
        return this.addFields;
      }
    }

    /// <summary>Gets or sets the remove fields.</summary>
    /// <value>The remove fields.</value>
    [DataMember]
    public IList<string> RemoveFields
    {
      get
      {
        if (this.removeFields == null)
          this.removeFields = (IList<string>) new List<string>();
        return this.removeFields;
      }
    }
  }
}
