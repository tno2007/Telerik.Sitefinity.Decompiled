// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Web.Services.Model.FormDescriptionViewModelContext
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Modules.Forms.Web.Services.Model
{
  /// <summary>
  ///  Defines the properties for the form description context type and is used for transferring the data through WCF.
  /// </summary>
  [DataContract]
  public class FormDescriptionViewModelContext
  {
    private FormDescriptionViewModel item;

    /// <summary>Gets or sets the form description item.</summary>
    /// <value>The item.</value>
    [DataMember]
    public FormDescriptionViewModel Item
    {
      get => this.item;
      set => this.item = value;
    }
  }
}
