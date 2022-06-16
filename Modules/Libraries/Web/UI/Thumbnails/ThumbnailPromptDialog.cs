// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.ThumbnailPromptDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails
{
  public class ThumbnailPromptDialog : AjaxDialogBase
  {
    internal const string viewScript = "Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.Scripts.ThumbnailPromptDialog.js";
    internal static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Libraries.Thumbnails.ThumbnailPromptDialog.ascx");
    private string defaultCloseArgument = "rebind";
    private const string messageRes = "messageRes";
    private const string messageKey = "messageKey";
    private const string messageArg = "messageArg";
    private const string buttonRes = "buttonRes";
    private const string buttonKey = "buttonKey";
    private const string buttonArg = "buttonArg";

    /// <inheritdoc />
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? ThumbnailPromptDialog.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <inheritdoc />
    public override string ClientComponentType => typeof (ThumbnailPromptDialog).FullName;

    /// <summary>
    /// Gets the reference to the <see cref="T:System.Web.UI.HtmlControls.HtmlContainerControl" /> that is used
    /// for confirming the thumbnail regeneration.
    /// </summary>
    protected virtual HtmlContainerControl ButtonDone => this.Container.GetControl<HtmlContainerControl>("buttonDone", true);

    /// <summary>
    /// Gets the reference to the <see cref="T:System.Web.UI.HtmlControls.HtmlContainerControl" /> that is used
    /// to close the dialog.
    /// </summary>
    protected virtual HtmlContainerControl ButtonCancel => this.Container.GetControl<HtmlContainerControl>("buttonCancel", true);

    /// <summary>Gets the warning message label.</summary>
    protected virtual Label LabelWarningMessage => this.Container.GetControl<Label>("labelWarningMessage", true);

    /// <summary>Gets the button's text literal literal.</summary>
    protected virtual Literal LiteralButtonDoneText => this.Container.GetControl<Literal>("literalButtonDoneText", true);

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container)
    {
      NameValueCollection queryString = SystemManager.CurrentHttpContext.Request.QueryString;
      if (!string.IsNullOrEmpty(queryString["messageRes"]) && !string.IsNullOrEmpty(queryString["messageKey"]))
        this.LabelWarningMessage.Text = string.Format(Res.Get(queryString["messageRes"], queryString["messageKey"]), (object[]) queryString["messageArg"].Split(','));
      if (string.IsNullOrEmpty(queryString["buttonRes"]) || string.IsNullOrEmpty(queryString["buttonKey"]))
        return;
      this.LiteralButtonDoneText.Text = string.Format(Res.Get(queryString["buttonRes"], queryString["buttonKey"]), (object[]) queryString["buttonArg"].Split(','));
    }

    /// <inheritdoc />
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = base.GetScriptDescriptors().Last<ScriptDescriptor>() as ScriptControlDescriptor;
      controlDescriptor.AddElementProperty("buttonDone", this.ButtonDone.ClientID);
      controlDescriptor.AddElementProperty("buttonCancel", this.ButtonCancel.ClientID);
      controlDescriptor.AddElementProperty("labelWarningMessage", this.LabelWarningMessage.ClientID);
      NameValueCollection queryString = SystemManager.CurrentHttpContext.Request.QueryString;
      if (queryString["closeCommand"] == null)
      {
        string defaultCloseArgument = this.defaultCloseArgument;
      }
      controlDescriptor.AddProperty("closeCommand", (object) queryString["closeCommand"]);
      return (IEnumerable<ScriptDescriptor>) new ScriptDescriptor[1]
      {
        (ScriptDescriptor) controlDescriptor
      };
    }

    /// <inheritdoc />
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      string fullName = typeof (ThumbnailPromptDialog).Assembly.FullName;
      return (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
      {
        new ScriptReference("Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.Scripts.ThumbnailPromptDialog.js", fullName)
      };
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct PropertyNames
    {
      public const string ButtonDone = "buttonDone";
      public const string ButtonCancel = "buttonCancel";
      public const string LabelWarningMessage = "labelWarningMessage";
      public const string LiteralButtonDoneText = "literalButtonDoneText";
      public const string CloseCommand = "closeCommand";
    }
  }
}
