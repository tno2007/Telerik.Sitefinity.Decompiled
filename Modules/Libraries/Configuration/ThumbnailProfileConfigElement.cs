// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Configuration.ThumbnailProfileConfigElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Libraries.ImageProcessing;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Modules.Libraries.Configuration
{
  /// <summary>
  /// Provides configuration information about the Thumbnail sets in the Libraries module.
  /// </summary>
  public class ThumbnailProfileConfigElement : ConfigElement
  {
    private object methodArgument;
    private bool methodArgsCached;
    private readonly object syncLock = new object();

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Configuration.ThumbnailProfileConfigElement" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    /// <remarks>
    /// ConfigElementCollection generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ConfigElementCollection, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public ThumbnailProfileConfigElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets or sets the title.</summary>
    [ConfigurationProperty("title", IsRequired = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ThumbnailProfileTitleDescription", Title = "ThumbnailProfileTitle")]
    public string Title
    {
      get => (string) this["title"];
      set => this["title"] = (object) value;
    }

    /// <summary>Gets or sets the name.</summary>
    [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ThumbnailProfileNameDescription", Title = "ThumbnailProfileName")]
    public string Name
    {
      get => (string) this["name"];
      set => this["name"] = value.Length >= 1 && value.Length <= 10 ? (object) value : throw new ArgumentException(Res.Get<LibrariesResources>().InvalidProfileName);
    }

    /// <summary>
    /// Gets a collection of user-defined parameters for the resize method.
    /// </summary>
    [ConfigurationProperty("parameters", IsRequired = false)]
    public NameValueCollection Parameters
    {
      get => (NameValueCollection) this["parameters"];
      set => this["parameters"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the max height of the generated thumbnail.
    /// </summary>
    [ConfigurationProperty("method", IsRequired = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ThumbnailTransformMethodTitleDescription", Title = "ThumbnailTransformMethodTitle")]
    public string Method
    {
      get => (string) this["method"];
      set => this["method"] = (object) value;
    }

    /// <summary>
    /// Gets or sets if the profile is a default for thumbnail generation when creating a new library.
    /// </summary>
    [ConfigurationProperty("default")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ThumbnailProfileDefaultDescription", Title = "ThumbnailProfileDefaultTitle")]
    public bool IsDefault
    {
      get => (bool) this["default"];
      set => this["default"] = (object) value;
    }

    /// <summary>Gets or sets the name.</summary>
    [ConfigurationProperty("tags", IsRequired = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "TagsDescription", Title = "TagsName")]
    public string Tags
    {
      get => (string) this["tags"];
      set => this["tags"] = (object) value;
    }

    internal object MethodArgument
    {
      get
      {
        if (!this.methodArgsCached)
        {
          lock (this.syncLock)
          {
            if (!this.methodArgsCached)
            {
              ImageProcessingMethod processingMethod;
              this.methodArgument = !ObjectFactory.Resolve<IImageProcessor>().Methods.TryGetValue(this.Method, out processingMethod) ? (object) null : processingMethod.CreateArgumentInstance(this.Parameters);
              this.methodArgsCached = true;
            }
          }
        }
        return this.methodArgument;
      }
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct PropNames
    {
      public const string Name = "name";
      public const string Title = "title";
      public const string IsDefault = "default";
      public const string Method = "method";
      public const string Tags = "tags";
    }
  }
}
