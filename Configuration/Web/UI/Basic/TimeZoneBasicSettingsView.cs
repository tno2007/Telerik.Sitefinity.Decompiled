// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.Web.UI.Basic.TimeZoneBasicSettingsView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web.UI;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Configuration.Web.UI.Basic
{
  public class TimeZoneBasicSettingsView : SimpleView
  {
    internal static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Configuration.Basic.TimeZoneBasicSettingsView.ascx");

    /// <summary>Gets the dropdown containing all time zones.</summary>
    /// <value>The dropdown containing all time zones.</value>
    public virtual ChoiceField TimeZones => this.Container.GetControl<ChoiceField>("timeZones", true);

    protected override void InitializeControls(GenericContainer container)
    {
      foreach (TimeZoneInfo systemTimeZone in TimeZoneInfo.GetSystemTimeZones())
        this.TimeZones.Choices.Add(new ChoiceItem()
        {
          Text = systemTimeZone.DisplayName,
          Value = systemTimeZone.Id
        });
    }

    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? TimeZoneBasicSettingsView.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;
  }
}
