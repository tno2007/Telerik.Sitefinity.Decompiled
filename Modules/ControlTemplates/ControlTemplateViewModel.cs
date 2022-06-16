// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ControlTemplates.ControlTemplateViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Modules.ControlTemplates
{
  /// <summary>Represents Control Template view model.</summary>
  [DataContract]
  public class ControlTemplateViewModel
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.ControlTemplates.ControlTemplateViewModel" /> class.
    /// </summary>
    public ControlTemplateViewModel()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.ControlTemplates.ControlTemplateViewModel" /> class.
    /// </summary>
    /// <param name="controlTemplate">The control template.</param>
    public ControlTemplateViewModel(ControlPresentation controlTemplate) => this.ControlTemplate = controlTemplate;

    /// <summary>Gets or sets the control template.</summary>
    /// <value>The control template.</value>
    public ControlPresentation ControlTemplate { get; set; }

    /// <summary>Gets or sets the control template id.</summary>
    /// <value>The control template id.</value>
    [DataMember]
    public Guid Id
    {
      get => this.ControlTemplate.Id;
      set => this.ControlTemplate.Id = value;
    }

    /// <summary>Gets or sets the control template name.</summary>
    /// <value>The control template name.</value>
    [DataMember]
    public string Name
    {
      get => this.ControlTemplate.Name;
      set => this.ControlTemplate.Name = value;
    }

    /// <summary>
    /// Gets or sets the user friendly presentation of the control type.
    /// </summary>
    /// <value>The user friendly presentation of the control type.</value>
    [DataMember]
    public string FriendlyUserName
    {
      get => this.ControlTemplate.FriendlyControlName;
      set => this.ControlTemplate.FriendlyControlName = value;
    }

    /// <summary>Gets or sets the control template data.</summary>
    /// <value>The control template data.</value>
    [DataMember]
    public string Data
    {
      get => this.ControlTemplate.Data;
      set => this.ControlTemplate.Data = value;
    }

    /// <summary>Gets or sets the date this item was last modified.</summary>
    /// <value>The last modified date.</value>
    [DataMember]
    public DateTime LastModified
    {
      get => this.ControlTemplate.LastModified;
      set => this.ControlTemplate.LastModified = value;
    }

    /// <summary>Gets or sets the owner.</summary>
    /// <value>The owner.</value>
    [DataMember]
    public string Owner
    {
      get => ControlTemplateViewModel.GetUser(this.ControlTemplate.Owner);
      set
      {
      }
    }

    /// <summary>Gets or sets the area name for the template</summary>
    [DataMember]
    public string AreaName
    {
      get => string.IsNullOrEmpty(this.ControlTemplate.ResourceClassId) ? this.ControlTemplate.AreaName : Res.Get(this.ControlTemplate.ResourceClassId, this.ControlTemplate.AreaName);
      set
      {
      }
    }

    /// <summary>Gets or sets the site links string.</summary>
    [DataMember]
    public string SiteLinksString { get; set; }

    private static string GetUser(Guid id) => UserProfilesHelper.GetUserDisplayName(id);
  }
}
