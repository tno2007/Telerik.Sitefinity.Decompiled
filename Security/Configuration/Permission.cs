// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Configuration.Permission
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Security.Configuration
{
  /// <summary>Defines permission settings</summary>
  [DescriptionResource(typeof (ConfigDescriptions), "PermissionConfigDescription")]
  [DebuggerDisplay("[Config] Permission '{Name}', Title = '{Title}'")]
  public class Permission : ConfigElement
  {
    private string[] keys;

    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public Permission(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// A collection of security actions that can be allowed or denied by a permission.
    /// </summary>
    [DescriptionResource(typeof (ConfigDescriptions), "ActionsCollection")]
    [ConfigurationProperty("actions")]
    public ConfigElementDictionary<string, SecurityAction> Actions => (ConfigElementDictionary<string, SecurityAction>) this["actions"];

    /// <summary>Gets or sets the name of the permission.</summary>
    [DescriptionResource(typeof (ConfigDescriptions), "ItemName")]
    [ConfigurationProperty("name", DefaultValue = "", IsKey = true, IsRequired = true)]
    public string Name
    {
      get => (string) this["name"];
      set => this["name"] = (object) value;
    }

    /// <summary>Gets or sets a title for the permission.</summary>
    [DescriptionResource(typeof (ConfigDescriptions), "ItemTitle")]
    [ConfigurationProperty("title", DefaultValue = "")]
    public string Title
    {
      get => (string) this["title"];
      set => this["title"] = (object) value;
    }

    /// <summary>Gets or sets description of the permission.</summary>
    [DescriptionResource(typeof (ConfigDescriptions), "PermissionDescription")]
    [ConfigurationProperty("description", DefaultValue = "")]
    public string Description
    {
      get => (string) this["description"];
      set => this["description"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the global resource class ID for retrieving localized strings.
    /// </summary>
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ResourceClassIdDescription", Title = "ResourceClassIdCaption")]
    [ConfigurationProperty("resourceClassId", DefaultValue = "")]
    public string ResourceClassId
    {
      get => (string) this["resourceClassId"];
      set => this["resourceClassId"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the URL to the login page for this permission.
    /// </summary>
    [DescriptionResource(typeof (ConfigDescriptions), "LoginUrl")]
    [ConfigurationProperty("loginUrl", DefaultValue = "~/Sitefinity/Login")]
    public string LoginUrl
    {
      get => (string) this["loginUrl"];
      set => this["loginUrl"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the URL to the login page used by ajax services
    /// </summary>
    [DescriptionResource(typeof (ConfigDescriptions), "AjaxLoginUrl")]
    [ConfigurationProperty("ajaxLoginUrl", DefaultValue = "~/Sitefinity/Login/Ajax")]
    public string AjaxLoginUrl
    {
      get => (string) this["ajaxLoginUrl"];
      set => this["ajaxLoginUrl"] = (object) value;
    }

    /// <summary>Returns a List collection of strings with keys</summary>
    public string[] Keys
    {
      get
      {
        if (this.keys == null)
          this.keys = this.Actions.Keys.ToArray<string>();
        return this.keys;
      }
    }

    /// <summary>Returns the value of the right given a specified key</summary>
    /// <param name="key">key of type string</param>
    /// <returns>an integer value</returns>
    public virtual int GetValue(string key)
    {
      SecurityAction securityAction;
      if (!this.Actions.TryGetValue(key, out securityAction))
        throw new ArgumentException(Res.Get<ErrorMessages>().KeyNotFound);
      return securityAction.Value;
    }

    /// <summary>Returns the value of the right given a specified key</summary>
    /// <param name="key">key of type string</param>
    /// <returns>an integer value</returns>
    internal int GetValueInternal(string key)
    {
      int y = Array.IndexOf<string>(this.Keys, key);
      return y != -1 ? (int) Math.Pow(2.0, (double) y) : throw new ArgumentException(Res.Get<ErrorMessages>().KeyNotFound);
    }

    /// <summary>
    /// Returns the value of the right given a specified index
    /// </summary>
    /// <param name="index">index of type integer</param>
    /// <returns>an integer value</returns>
    public virtual int GetValue(int index)
    {
      if (index < 0 || index > this.Keys.Length)
        throw new IndexOutOfRangeException();
      return (int) Math.Pow(2.0, (double) index);
    }

    internal void InvalidateCache() => this.keys = (string[]) null;
  }
}
