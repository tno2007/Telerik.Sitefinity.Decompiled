// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config.ImageColumnElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config
{
  public class ImageColumnElement : 
    ColumnElement,
    IImageColumnDefinition,
    IColumnDefinition,
    IDefinition
  {
    public const string SRC_PROPERTY_NAME = "srcPropertyName";
    public const string ALT_PROPERTY_NAME = "altPropertyName";
    public const string HEIGHT_PROPERTY_NAME = "heightPropertyName";
    public const string WIDTH_PROPERTY_NAME = "widthPropertyName";
    public const string EMIT_WIDTH_HEIGHT_PROPERTY_NAME = "emitWidthAndHeightPropertyName";

    public ImageColumnElement(ConfigElement parent)
      : base(parent)
    {
    }

    public override DefinitionBase GetDefinition() => (DefinitionBase) new ImageColumnDefinition((ConfigElement) this);

    [ConfigurationProperty("emitWidthAndHeightPropertyName", DefaultValue = true)]
    public bool EmitWidthAndHeight
    {
      get => (bool) this["emitWidthAndHeightPropertyName"];
      set => this["emitWidthAndHeightPropertyName"] = (object) value;
    }

    [ConfigurationProperty("srcPropertyName", DefaultValue = "Item.Thumbnail.Url")]
    public string SrcPropertyName
    {
      get => (string) this["srcPropertyName"];
      set => this["srcPropertyName"] = (object) value;
    }

    [ConfigurationProperty("altPropertyName", DefaultValue = "Item.Thumbnail.AlternativeText.Value")]
    public string AltPropertyName
    {
      get => (string) this["altPropertyName"];
      set => this["altPropertyName"] = (object) value;
    }

    [ConfigurationProperty("heightPropertyName", DefaultValue = "Item.Thumbnail.ThumbnailHeight")]
    public string HeightPropertyName
    {
      get => (string) this["heightPropertyName"];
      set => this["heightPropertyName"] = (object) value;
    }

    [ConfigurationProperty("widthPropertyName", DefaultValue = "Item.Thumbnail.ThumbnailWidth")]
    public string WidthPropertyName
    {
      get => (string) this["widthPropertyName"];
      set => this["widthPropertyName"] = (object) value;
    }

    public string ClientTemplate
    {
      get
      {
        if (!this.EmitWidthAndHeight)
          return string.Format("<img sys:src=\"{{{{{0}}}}}\" sys:alt=\"{{{{{1}}}}}\" />", (object) this.SrcPropertyName, (object) this.AltPropertyName);
        return string.Format("<img sys:src=\"{{{{{0}}}}}\" sys:alt=\"{{{{{1}}}}}\" sys:height=\"{{{{{2}}}}}\" sys:width=\"{{{{{3}}}}}\" />", (object) this.SrcPropertyName, (object) this.AltPropertyName, (object) this.HeightPropertyName, (object) this.WidthPropertyName);
      }
    }
  }
}
