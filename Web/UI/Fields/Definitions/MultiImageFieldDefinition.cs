// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Definitions.MultiImageFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Web.UI.Fields.Definitions
{
  /// <summary>
  ///  A class that provides all information needed to construct multiimage field control.
  /// </summary>
  public class MultiImageFieldDefinition : FieldControlDefinition, IMultiImageFieldDefinition
  {
    private int? maxWidth;
    private int? maxHeight;
    private string providerNameForDefaultImage;
    private Guid defaultImageId;
    private string imageProviderName;
    private Type dataFieldType;
    private string defaultSrc;
    private int? sizeInPx;
    private bool displayEmptyImage;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Definitions.MultiImageFieldDefinition" /> class.
    /// </summary>
    public MultiImageFieldDefinition()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Definitions.MultiImageFieldDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public MultiImageFieldDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the default type of the field control.</summary>
    /// <value>The default type of the field control.</value>
    public override Type DefaultFieldType => typeof (MultiImageField);

    /// <summary>
    /// Gets or sets the maximal width in pixels of the image in the image field.
    /// </summary>
    /// <value>The maximal width.</value>
    public int? MaxWidth
    {
      get => this.ResolveProperty<int?>(nameof (MaxWidth), this.maxWidth);
      set => this.maxWidth = value;
    }

    /// <summary>
    /// Gets or sets the maximal height in pixels of the image in the image field.
    /// </summary>
    /// <value>The maximal height.</value>
    public int? MaxHeight
    {
      get => this.ResolveProperty<int?>(nameof (MaxHeight), this.maxHeight);
      set => this.maxHeight = value;
    }

    /// <summary>Gets or sets the provider name for default image.</summary>
    /// <value>The provider name for default image.</value>
    public string ProviderNameForDefaultImage
    {
      get => this.ResolveProperty<string>(nameof (ProviderNameForDefaultImage), this.providerNameForDefaultImage);
      set => this.providerNameForDefaultImage = value;
    }

    /// <summary>Gets or sets the default image id.</summary>
    /// <value>The default image id.</value>
    public Guid DefaultImageId
    {
      get => this.ResolveProperty<Guid>(nameof (DefaultImageId), this.defaultImageId);
      set => this.defaultImageId = value;
    }

    /// <summary>Gets or sets the name of the image provider.</summary>
    /// <value>The name of the image provider.</value>
    public string ImageProviderName
    {
      get => this.ResolveProperty<string>(nameof (ImageProviderName), this.imageProviderName);
      set => this.imageProviderName = value;
    }

    /// <summary>
    /// Represents the type of the field to which the field control is bound
    /// <remarks>
    /// Used to specify what type of value to control should return both on the server and client sides
    /// </remarks>
    /// </summary>
    /// <value></value>
    public Type DataFieldType
    {
      get => this.ResolveProperty<Type>(nameof (DataFieldType), this.dataFieldType);
      set => this.dataFieldType = value;
    }

    /// <summary>Gets or sets the default src for the image.</summary>
    /// <value>The default src for the image.</value>
    public string DefaultSrc
    {
      get => this.ResolveProperty<string>(nameof (DefaultSrc), this.defaultSrc);
      set => this.defaultSrc = value;
    }

    /// <summary>
    /// Gets or sets whether to display the image placeholder. The default behavior is to display the placeholder.
    /// </summary>
    /// <value>Gets or sets whether to display the image placeholder.</value>
    public bool DisplayEmptyImage
    {
      get => this.ResolveProperty<bool>(nameof (DisplayEmptyImage), this.displayEmptyImage);
      set => this.displayEmptyImage = value;
    }
  }
}
