// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.Web.UI.Basic.GenericBasicSettingsView`2
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.SiteSettings;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Configuration.Web.UI.Basic
{
  /// <summary>Generic view for basic settings.</summary>
  public class GenericBasicSettingsView<TFieldsView, TDataContract> : BasicSettingsView
    where TFieldsView : Control, new()
    where TDataContract : ISettingsDataContract
  {
    public static readonly string layoutTemplateName = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Configuration.Basic.GenericBasicSettingsView.ascx");

    public GenericBasicSettingsView() => this.DataContract = typeof (TDataContract);

    public Type DataContract { get; set; }

    /// <summary>Gets the name of the layout template.</summary>
    /// <value>The name of the layout template.</value>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets or sets the layout template path.</summary>
    /// <value>The layout template path.</value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? GenericBasicSettingsView<TFieldsView, TDataContract>.layoutTemplateName : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets the fields client binder.</summary>
    public virtual PlaceHolder FieldsContainer => this.Container.GetControl<PlaceHolder>("fieldsContainer", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="viewContainer">The view container.</param>
    protected override void InitializeControls(Control viewContainer)
    {
      base.InitializeControls(viewContainer);
      this.FieldsBinder.ServiceUrl = !(this.DataContract == (Type) null) ? string.Format("~/Sitefinity/Services/BasicSettings.svc/generic/?itemType={0}", (object) HttpUtility.UrlEncode(this.DataContract.FullName)) : throw new Exception("Required property 'SettingsDataContract' is not provided");
      this.FieldsContainer.Controls.Add((Control) new TFieldsView());
    }
  }
}
