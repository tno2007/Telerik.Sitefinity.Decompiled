// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.PublicControls.TwitterFeedWidget
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.Sitefinity.Publishing;
using Telerik.Sitefinity.Publishing.Model;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.PublicControls.Designers;

namespace Telerik.Sitefinity.Web.UI.PublicControls
{
  /// <summary>Twitter Feed Widget</summary>
  [ControlTemplateInfo("PublishingMessages", "TwitterFeedWidgetTitle", "SocialSection")]
  [ControlDesigner(typeof (TwitterFeedWidgetDesigner))]
  public class TwitterFeedWidget : SimpleScriptView
  {
    private int maxItems = 20;
    private int refreshInterval = 5;
    /// <summary>Layout Template Path</summary>
    public static readonly string layoutTemplatePath = "Telerik.Sitefinity.Resources.Templates.PublicControls.TwitterFeedWidget.ascx";
    internal const string JsComponentPath = "Telerik.Sitefinity.Web.UI.PublicControls.Scripts.TwitterFeedWidget.js";
    /// <summary>Data service url</summary>
    public string svcUrl = "~/Services/Content/PublishingDataService.svc/";

    public Guid TwitterPipeId { get; set; }

    /// <summary>Maximum queried items</summary>
    public int MaxItems
    {
      get => this.maxItems;
      set => this.maxItems = value;
    }

    /// <summary>The refresh interval for publishing point service</summary>
    public int RefreshInterval
    {
      get => this.refreshInterval;
      set => this.refreshInterval = value;
    }

    /// <summary>Initialize Controls</summary>
    /// <param name="container">The controls container </param>
    protected override void InitializeControls(GenericContainer container)
    {
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnPreRender(EventArgs e) => base.OnPreRender(e);

    /// <summary>Controls Layout Template</summary>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? ControlUtilities.ToVppPath(TwitterFeedWidget.layoutTemplatePath) : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Tag Key</summary>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    public HtmlGenericControl itemsTemplate => this.Container.GetControl<HtmlGenericControl>("items", true);

    /// <summary>Get Script Descriptors</summary>
    /// <returns></returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(typeof (TwitterFeedWidget).FullName, this.ClientID);
      string str = VirtualPathUtility.ToAbsolute(this.svcUrl);
      if (this.TwitterPipeId != Guid.Empty)
      {
        PipeSettings pipeSettings = PublishingManager.GetManager().GetPipeSettings().Where<PipeSettings>((Expression<Func<PipeSettings, bool>>) (ps => ps.Id == this.TwitterPipeId)).FirstOrDefault<PipeSettings>();
        if (pipeSettings != null)
          str = str + pipeSettings.PublishingPoint.Id.ToString() + "/?take=" + (object) this.MaxItems;
      }
      controlDescriptor.AddProperty("serviceUrl", (object) str);
      controlDescriptor.AddProperty("itemsControlId", (object) this.itemsTemplate.ClientID);
      controlDescriptor.AddProperty("refreshInterval", (object) this.RefreshInterval);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    /// <summary>Get script references</summary>
    /// <returns></returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>()
    {
      new ScriptReference("Telerik.Sitefinity.Web.UI.PublicControls.Scripts.TwitterFeedWidget.js", typeof (TwitterFeedWidget).Assembly.FullName)
    };
  }
}
