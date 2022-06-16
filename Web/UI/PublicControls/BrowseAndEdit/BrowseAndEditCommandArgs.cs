// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit.BrowseAndEditCommandArgs
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;

namespace Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit
{
  [DataContract]
  public class BrowseAndEditCommandArgs
  {
    private Dictionary<string, object> additionalProperties;

    /// <summary>Id of the item</summary>
    [DataMember]
    public Guid? ItemId { get; set; }

    /// <summary>Item type</summary>
    public string ItemType { get; set; }

    [ScriptIgnore]
    public IDialogDefinition DialogDefinition { get; set; }

    /// <summary>
    /// Gets or sets the name of the dialog related to the command. See <see cref="M:DialogDefinition" />.
    /// </summary>
    [DataMember]
    public string DialogName { get; set; }

    /// <summary>
    /// Represents the url parameters to be set for the dialog url
    /// </summary>
    public List<KeyValuePair<string, string>> DialogUrlParameters { get; set; }

    /// <summary>Gets or sets the additional properties.</summary>
    /// <value>The additional properties.</value>
    [DataMember]
    public Dictionary<string, object> AdditionalProperties
    {
      get
      {
        if (this.additionalProperties == null)
          this.additionalProperties = new Dictionary<string, object>();
        return this.additionalProperties;
      }
      set => this.additionalProperties = value;
    }
  }
}
