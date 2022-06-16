// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Configuration.StatusCommandDescription
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;

namespace Telerik.Sitefinity.Web.Configuration
{
  /// <summary>Defines configuration settings for a status command.</summary>
  public class StatusCommandDescription : ConfigElement
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.Configuration.StatusCommandDescription" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    /// <remarks>
    /// ConfigElementCollection generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ConfigElementCollection, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public StatusCommandDescription(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.Configuration.StatusCommandDescription" /> class.
    /// </summary>
    internal StatusCommandDescription()
      : base(false)
    {
    }

    /// <summary>Gets or sets the name of the command.</summary>
    /// <value>The name of the command.</value>
    [ConfigurationProperty("commandName", DefaultValue = "", IsKey = true, IsRequired = true)]
    public string CommandName
    {
      get => (string) this["commandName"];
      set => this["commandName"] = (object) value;
    }

    /// <summary>Gets or sets the CSS class.</summary>
    /// <value>The CSS class.</value>
    [ConfigurationProperty("cssClass")]
    public string CssClass
    {
      get => (string) this["cssClass"];
      set => this["cssClass"] = (object) value;
    }

    /// <summary>Gets or sets the title.</summary>
    /// <value>The title.</value>
    [ConfigurationProperty("title")]
    public string Title
    {
      get => (string) this["title"];
      set => this["title"] = (object) value;
    }

    /// <summary>Gets or sets the resource key.</summary>
    /// <value>The resource key.</value>
    [ConfigurationProperty("resourceKey")]
    public string ResourceKey
    {
      get => (string) this["resourceKey"];
      set => this["resourceKey"] = (object) value;
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct Props
    {
      public const string CommandName = "commandName";
      public const string CssClass = "cssClass";
      public const string Title = "title";
      public const string ResourceKey = "resourceKey";
    }
  }
}
