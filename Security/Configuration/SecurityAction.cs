// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Configuration.SecurityAction
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using System.Diagnostics;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Security.Configuration
{
  /// <summary>
  /// Defines an action that can be allowed or denied by a permission.
  /// </summary>
  [ObjectInfo(typeof (ConfigDescriptions), Description = "SecurityActionDescription", Title = "SecurityActionCaption")]
  [DebuggerDisplay("[Config] SecurityAction {Name} = {Value}, Title = {Title}, Type = {Type}")]
  public class SecurityAction : ConfigElement
  {
    private int actionValue = -1;

    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public SecurityAction(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    /// <param name="value">Optionally explicitly set the configuration numeric value of this security action. Use with caution!</param>
    public SecurityAction(ConfigElement parent, int value)
      : base(parent)
    {
      this.actionValue = value;
    }

    /// <summary>Gets or sets the name of the permission.</summary>
    [DescriptionResource(typeof (ConfigDescriptions), "ItemName")]
    [ConfigurationProperty("name", DefaultValue = "", IsKey = true, IsRequired = true)]
    public virtual string Name
    {
      get => (string) this["name"];
      set => this["name"] = (object) value;
    }

    /// <summary>Gets or sets a title for the permission.</summary>
    [DescriptionResource(typeof (ConfigDescriptions), "ItemTitle")]
    [ConfigurationProperty("title", DefaultValue = "")]
    public virtual string Title
    {
      get => !string.IsNullOrEmpty(this.ResourceClassId) ? Res.Get(this.ResourceClassId, (string) this["title"]) : (string) this["title"];
      set => this["title"] = (object) value;
    }

    /// <summary>Gets or sets description of the permission.</summary>
    [DescriptionResource(typeof (ConfigDescriptions), "SecurityActionDescription")]
    [ConfigurationProperty("description", DefaultValue = "")]
    public virtual string Description
    {
      get => !string.IsNullOrEmpty(this.ResourceClassId) ? Res.Get(this.ResourceClassId, (string) this["description"]) : (string) this["description"];
      set => this["description"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the global resource class ID for retrieving localized strings.
    /// </summary>
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ResourceClassIdDescription", Title = "ResourceClassIdCaption")]
    [ConfigurationProperty("resourceClassId", DefaultValue = "")]
    public virtual string ResourceClassId
    {
      get => (string) this["resourceClassId"];
      set => this["resourceClassId"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the generic CRUD type of this security action
    /// </summary>
    [ConfigurationProperty("type", DefaultValue = SecurityActionTypes.None, IsRequired = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "SecurityActionTypeDescription", Title = "SecurityActionTypeCaption")]
    public virtual SecurityActionTypes Type
    {
      get => (SecurityActionTypes) this["type"];
      set => this["type"] = (object) value;
    }

    /// <summary>Gets the bit field value of this action.</summary>
    public virtual int Value
    {
      get
      {
        if (this.actionValue == -1)
          this.actionValue = ((Permission) this.Parent.Parent).GetValueInternal(this.Name);
        return this.actionValue;
      }
    }
  }
}
