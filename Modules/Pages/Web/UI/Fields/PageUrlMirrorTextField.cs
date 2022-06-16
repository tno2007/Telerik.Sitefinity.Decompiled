// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.PageUrlMirrorTextField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.Definitions;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Modules.Pages.Web.UI.Fields
{
  [RequiresDataItem]
  internal class PageUrlMirrorTextField : UrlMirrorTextField
  {
    internal const string viewScript = "Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.Scripts.PageUrlMirrorTextField.js";
    internal static readonly string templatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Fields.PageUrlMirrorTextField.ascx");
    private const string reqDataItemScriptFileName = "Telerik.Sitefinity.Web.UI.Fields.Scripts.IRequiresDataItem.js";

    /// <summary>
    /// Gets the reference to the label indicating weather the url is custom or not.
    /// </summary>
    /// <value>The expand link.</value>
    protected internal virtual Label CustomLabel => this.GetConditionalControl<Label>("customText", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Gets the reference to the label indicating weather the url is custom or not.
    /// </summary>
    /// <value>The expand link.</value>
    protected internal virtual LinkButton CreateCustomUrlButton => this.GetConditionalControl<LinkButton>("createCustomUrlButton", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Gets the reference to the label indicating weather the url is custom or not.
    /// </summary>
    /// <value>The expand link.</value>
    protected internal virtual LinkButton DefaultStructureButton => this.GetConditionalControl<LinkButton>("defaultStructureButton", this.DisplayMode == FieldDisplayMode.Write);

    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = base.GetScriptDescriptors().Last<ScriptDescriptor>() as ScriptControlDescriptor;
      if (this.DisplayMode == FieldDisplayMode.Write)
      {
        controlDescriptor.AddElementProperty("customText", this.CustomLabel.ClientID);
        controlDescriptor.AddElementProperty("defaultStructureButton", this.DefaultStructureButton.ClientID);
        controlDescriptor.AddElementProperty("createCustomUrlButton", this.CreateCustomUrlButton.ClientID);
        controlDescriptor.AddProperty("customUrlValidationMessage", (object) this.CustomUrlValidationMessage);
      }
      return (IEnumerable<ScriptDescriptor>) new ScriptDescriptor[1]
      {
        (ScriptDescriptor) controlDescriptor
      };
    }

    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.Scripts.PageUrlMirrorTextField.js", typeof (PageUrlMirrorTextField).Assembly.FullName),
      new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.IRequiresDataItem.js", typeof (PageUrlMirrorTextField).Assembly.FullName)
    };

    public override string LayoutTemplatePath
    {
      get => PageUrlMirrorTextField.templatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Converts a control ID used in conditional templates accoding to this.DisplayMode
    /// </summary>
    /// <param name="originalName">Original ID of the control</param>
    /// <returns>Unique control ID</returns>
    protected virtual string GetConditionalControlName(string originalName)
    {
      string lower = this.DisplayMode.ToString().ToLower();
      return originalName + "_" + lower;
    }

    /// <summary>
    /// Shortcut for this.Container.GetControl(this.GetConditionalControlName(originalName), required)
    /// </summary>
    /// <typeparam name="T">Type of the control to load</typeparam>
    /// <param name="originalName">Original ID of the control</param>
    /// <param name="required">Throw exception if control is not found and this parameter is true</param>
    /// <returns>Loaded control</returns>
    protected T GetConditionalControl<T>(string originalName, bool required) => this.Container.GetControl<T>(this.GetConditionalControlName(originalName), required);

    public override void Configure(IFieldDefinition definition)
    {
      base.Configure(definition);
      if (!(definition is IPageUrlMirrorTextFieldDefinition textFieldDefinition))
        return;
      this.CustomUrlValidationMessage = textFieldDefinition.CustomUrlValidationMessage;
    }

    /// <summary>Gets or sets the custom URL validation message.</summary>
    public string CustomUrlValidationMessage { get; set; }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    private struct PropNames
    {
      public const string CustomLabelText = "customText";
      public const string CreateCustomUrlButton = "createCustomUrlButton";
      public const string DefaultStructureButton = "defaultStructureButton";
      public const string CustomUrlValidationMessage = "customUrlValidationMessage";
    }
  }
}
