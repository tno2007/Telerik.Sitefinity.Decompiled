// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Config.MultiImageFieldElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Definitions;

namespace Telerik.Sitefinity.Web.UI.Fields.Config
{
  /// <summary>The configuration element for multi image fields.</summary>
  public class MultiImageFieldElement : FieldControlDefinitionElement, IMultiImageFieldDefinition
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Config.MultiImageFieldElement" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    /// <remarks>
    /// ConfigElementCollection generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ConfigElementCollection, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public MultiImageFieldElement(ConfigElement parent)
      : base(parent)
    {
    }

    internal MultiImageFieldElement()
    {
    }

    /// <summary>Gets the definition instance.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new MultiImageFieldDefinition((ConfigElement) this);

    /// <summary>
    /// Gets or sets the maximal width in pixels of the image in the multi image field.
    /// </summary>
    [ConfigurationProperty("maxWidth")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "MaxWidthDescription", Title = "MaxWidthCaption")]
    public int? MaxWidth
    {
      get => (int?) this["maxWidth"];
      set => this["maxWidth"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the maximal height in pixels of the image in the multiimage field.
    /// </summary>
    [ConfigurationProperty("maxHeight")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "MaxHeightDescription", Title = "MaxHeightCaption")]
    public int? MaxHeight
    {
      get => (int?) this["maxHeight"];
      set => this["maxHeight"] = (object) value;
    }

    /// <summary>Gets or sets the provider name for default image.</summary>
    /// <value>The provider name for default image.</value>
    [ConfigurationProperty("providerNameForDefaultImage", DefaultValue = null)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ProviderNameForDefaultImageDescription", Title = "ProviderNameForDefaultImageCaption")]
    public string ProviderNameForDefaultImage
    {
      get => this["providerNameForDefaultImage"] as string;
      set => this["providerNameForDefaultImage"] = (object) value;
    }

    /// <summary>Gets or sets the default image id.</summary>
    /// <value>The default image id.</value>
    [ConfigurationProperty("defaultImageId", DefaultValue = "00000000-0000-0000-0000-000000000000")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DefaultImageIdDescription", Title = "DefaultImageIdCaption")]
    public Guid DefaultImageId
    {
      get => (Guid) this["defaultImageId"];
      set => this["defaultImageId"] = (object) value;
    }

    /// <summary>Gets or sets the name of the image provider.</summary>
    /// <value>The name of the image provider.</value>
    [ConfigurationProperty("imageProviderName", DefaultValue = null)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ImageProviderNameDescription", Title = "ImageProviderNameCaption")]
    public string ImageProviderName
    {
      get => this["imageProviderName"] as string;
      set => this["imageProviderName"] = (object) value;
    }

    /// <summary>
    /// Represents the type of the field to which the field control is bound
    /// <remarks>
    /// Used to specify what type of value to control should return both on the server and client sides
    /// </remarks>
    /// </summary>
    [ConfigurationProperty("dataFieldType", DefaultValue = null)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DataFieldTypeDescription", Title = "DataFieldTypeCaption")]
    [TypeConverter(typeof (StringTypeConverter))]
    public Type DataFieldType
    {
      get => (Type) this["dataFieldType"];
      set => this["dataFieldType"] = (object) value;
    }

    [ConfigurationProperty("defaultsrc", DefaultValue = null)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DefaultSrcDescription", Title = "DefaultSrcCaption")]
    public string DefaultSrc
    {
      get => (string) this["defaultsrc"];
      set => this["defaultsrc"] = (object) value;
    }

    /// <summary>
    /// Gets or sets whether to display the image placeholder. The default behavior is to display the placeholder.
    /// </summary>
    /// <value>Gets or sets whether to display the image placeholder.</value>
    [ConfigurationProperty("displayEmptyImage", DefaultValue = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DisplayEmptyImageDescription", Title = "DisplayEmptyImageCaption")]
    public bool DisplayEmptyImage
    {
      get => (bool) this["displayEmptyImage"];
      set => this["displayEmptyImage"] = (object) value;
    }

    /// <summary>Gets the default type of the field.</summary>
    /// <value>The default type of the field.</value>
    public override Type DefaultFieldType => typeof (MultiImageField);

    /// <summary>
    /// Gets an instance of configuration element that represents the definition
    /// object in configuration.
    /// </summary>
    /// <value>Configuration element representing the current definition.</value>
    public new ConfigElement ConfigDefinition => throw new NotImplementedException();

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct PropertyNames
    {
      public const string MaxWidth = "maxWidth";
      public const string MaxHeight = "maxHeight";
      public const string ImageFieldMode = "imagefieldMode";
      public const string ProviderNameForDefaultImage = "providerNameForDefaultImage";
      public const string DefaultImageId = "defaultImageId";
      public const string ImageProviderName = "imageProviderName";
      public const string DataFieldType = "dataFieldType";
      public const string UploadMode = "uploadMode";
      public const string DefaultSrc = "defaultsrc";
      public const string DisplayEmptyImage = "displayEmptyImage";
    }
  }
}
