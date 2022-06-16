// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.Web.UI.Basic.TwitterBasicSettingsView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Publishing.Twitter;
using Telerik.Sitefinity.Publishing.Twitter.BackendControls;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI;

namespace Telerik.Sitefinity.Configuration.Web.UI.Basic
{
  /// <summary>View for comments basic settings.</summary>
  public class TwitterBasicSettingsView : BasicSettingsView
  {
    public static readonly string layoutTemplateName = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Configuration.Basic.TwitterBasicSettingsView.ascx");

    /// <summary>Gets the name of the layout template.</summary>
    /// <value>The name of the layout template.</value>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets the layout template path.</summary>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? TwitterBasicSettingsView.layoutTemplateName : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    protected override void OnInit(EventArgs e) => base.OnInit(e);

    protected override void InitializeViews()
    {
      base.InitializeViews();
      this.Container.Controls.Add((Control) new TwitterLandingPageControl()
      {
        Message = this.Container.GetControl<Message>()
      });
      ControlCollection controls1 = this.Container.GetControl<PlaceHolder>("placeHolder", true).Controls;
      BackendContentView child1 = new BackendContentView();
      child1.ModuleName = "Publishing";
      child1.ControlDefinitionName = TwitterDefinitions.BackendDefinitionName;
      controls1.Add((Control) child1);
      ControlCollection controls2 = this.Container.GetControl<PlaceHolder>("urlShorteningHolder", true).Controls;
      BackendContentView child2 = new BackendContentView();
      child2.ModuleName = "Publishing";
      child2.ControlDefinitionName = TwitterUrlShorteningDefinitions.BackendDefinitionName;
      controls2.Add((Control) child2);
    }
  }
}
