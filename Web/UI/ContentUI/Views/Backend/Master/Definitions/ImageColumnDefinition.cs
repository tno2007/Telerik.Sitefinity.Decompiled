// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config.ImageColumnDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Definitions;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config
{
  public class ImageColumnDefinition : 
    ColumnDefinition,
    IImageColumnDefinition,
    IColumnDefinition,
    IDefinition
  {
    private string boundPropertyName;
    private string clientTemplate;
    private string srcPropertyName;
    private string altPropertyName;
    private string heightPropertyName;
    private string widthPropertyName;

    public ImageColumnDefinition(ConfigElement configElement)
      : base(configElement)
    {
    }

    public ImageColumnDefinition GetDefinition() => this;

    public string ClientTemplate
    {
      get => this.ResolveProperty<string>(nameof (ClientTemplate), this.clientTemplate);
      set => this.clientTemplate = value;
    }

    public string SrcPropertyName
    {
      get => this.ResolveProperty<string>("ClientTemplate", this.srcPropertyName);
      set => this.srcPropertyName = value;
    }

    public string AltPropertyName
    {
      get => this.ResolveProperty<string>("ClientTemplate", this.altPropertyName);
      set => this.altPropertyName = value;
    }

    public string HeightPropertyName
    {
      get => this.ResolveProperty<string>("ClientTemplate", this.heightPropertyName);
      set => this.heightPropertyName = value;
    }

    public string WidthPropertyName
    {
      get => this.ResolveProperty<string>("ClientTemplate", this.widthPropertyName);
      set => this.widthPropertyName = value;
    }

    public override string RenderMarkup() => throw new NotImplementedException();
  }
}
