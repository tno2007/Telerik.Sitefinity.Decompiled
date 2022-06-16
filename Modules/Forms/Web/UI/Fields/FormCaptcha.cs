// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormCaptcha
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Designers;
using Telerik.Sitefinity.Modules.Pages.PropertyPersisters;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.Forms.Web.UI.Fields
{
  [FormControlDisplayMode(FormControlDisplayMode.Write)]
  [ControlDesigner(typeof (FormRadCaptchaDesigner))]
  [DatabaseMapping(UserFriendlyDataType.YesNo)]
  public class FormCaptcha : FieldControl, IFormFieldControl, IValidatable
  {
    private int imageTextLength = 6;
    private string fontFamily = "Courier New";
    private string textColor = "Gray";
    private string backgroundColor = "White";
    private bool displayOnlyForUnauthenticatedUsers = true;
    private string layoutTemplatePath = "Telerik.Sitefinity.Resources.Templates.Backend.Forms.FormsRadCaptcha.ascx";
    private string layoutTemplatePathDesignMode = "Telerik.Sitefinity.Resources.Templates.Backend.Forms.FormsRadCaptchaDesignMode.ascx";
    private IMetaField metaField;
    private const string script = "Telerik.Sitefinity.Modules.Forms.Web.UI.Scripts.FormCaptcha.js";

    public FormCaptcha()
    {
      this.Title = Res.Get<FormsResources>().RadCaptchaTitle;
      this.InvalidInputMessage = Res.Get<FormsResources>().CaptchaErrorMessage;
    }

    public RadCaptcha.ProtectionStrategies ProtectionStrategy { get; set; }

    public override string Title
    {
      get => base.Title;
      set => base.Title = value;
    }

    public int ImageTextLength
    {
      get => this.imageTextLength;
      set => this.imageTextLength = value;
    }

    public string FontFamily
    {
      get => this.fontFamily;
      set => this.fontFamily = value;
    }

    public string TextColor
    {
      get => this.textColor;
      set => this.textColor = value;
    }

    public string BackgroundColor
    {
      get => this.backgroundColor;
      set => this.backgroundColor = value;
    }

    public CaptchaLineNoiseLevel LineNoise { get; set; }

    public CaptchaFontWarpFactor FontWarp { get; set; }

    public CaptchaBackgroundNoiseLevel BackgroundNoiseLevel { get; set; }

    /// <summary>Gets or sets the storage medium for the CaptchaImage.</summary>
    /// <remarks>When the image is stored in the session the RadCaptcha HttpHandler
    /// definition (in the web.config file) must be changed from type="Telerik.Web.UI.WebResource" to
    /// type="Telerik.Web.UI.WebResourceSession" so that the image can be retrieved from the Session.</remarks>
    /// <value>Gets or sets a value indication where the CaptchaImage is stored.</value>
    public CaptchaImageStorage ImageStorageLocation { get; set; }

    private FormsControl GetFormsControl(Control ctrl)
    {
      if (ctrl is FormsControl)
        return ctrl as FormsControl;
      return ctrl == null ? (FormsControl) null : this.GetFormsControl(ctrl.Parent);
    }

    protected override void InitializeControls(GenericContainer container)
    {
      if (!this.IsDesignMode())
      {
        FormsControl formsControl = this.GetFormsControl(this.Parent);
        if (formsControl != null)
        {
          if (!SystemManager.HttpContextItems.Contains((object) (formsControl.ID + "radCaptcha")))
            SystemManager.HttpContextItems.Add((object) (formsControl.ID + "radCaptcha"), (object) new List<FormCaptcha>());
          ((List<FormCaptcha>) SystemManager.HttpContextItems[(object) (formsControl.ID + "radCaptcha")]).Add(this);
        }
        if (SystemManager.CurrentHttpContext.User != null && SystemManager.CurrentHttpContext.User.Identity != null && SystemManager.CurrentHttpContext.User.Identity.IsAuthenticated && this.DisplayOnlyForUnauthenticatedUsers)
        {
          this.Captcha.Enabled = false;
          this.Captcha.Visible = false;
        }
      }
      RadCaptcha captcha = this.Captcha;
      captcha.ProtectionMode = this.ProtectionStrategy;
      captcha.CaptchaImage.TextLength = this.ImageTextLength;
      if (!string.IsNullOrWhiteSpace(this.FontFamily))
        captcha.CaptchaImage.FontFamily = this.FontFamily;
      if (!string.IsNullOrWhiteSpace(this.TextColor))
        captcha.CaptchaImage.TextColor = Color.FromName(this.TextColor);
      if (!string.IsNullOrWhiteSpace(this.BackgroundColor))
        captcha.CaptchaImage.BackgroundColor = Color.FromName(this.BackgroundColor);
      captcha.CaptchaImage.LineNoise = this.LineNoise;
      captcha.CaptchaImage.FontWarp = this.FontWarp;
      captcha.CaptchaImage.BackgroundNoise = this.BackgroundNoiseLevel;
      captcha.ImageStorageLocation = this.ImageStorageLocation;
      container.Controls.Add((Control) captcha);
      this.Context.Response.DisableKernelCache();
      this.Context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
    }

    public override bool IsValid() => this.Captcha.IsValid;

    public override string LayoutTemplatePath
    {
      get
      {
        if (!string.IsNullOrWhiteSpace(base.LayoutTemplatePath))
          return base.LayoutTemplatePath;
        return !this.IsDesignMode() ? ControlUtilities.ToVppPath(this.layoutTemplatePath) : ControlUtilities.ToVppPath(this.layoutTemplatePathDesignMode);
      }
      set => base.LayoutTemplatePath = value;
    }

    [Browsable(false)]
    public RadCaptcha Captcha => this.Container.GetControl<RadCaptcha>("radCaptcha", true);

    [MultilingualProperty]
    public string InvalidInputMessage { get; set; }

    public bool DisplayOnlyForUnauthenticatedUsers
    {
      get => this.displayOnlyForUnauthenticatedUsers;
      set => this.displayOnlyForUnauthenticatedUsers = value;
    }

    /// <summary>Metafield that represents the texbox control</summary>
    [TypeConverter(typeof (ExpandableObjectConverter))]
    [NonMultilingual]
    public IMetaField MetaField
    {
      get
      {
        if (this.metaField == null)
        {
          this.metaField = (IMetaField) this.LoadDefaultMetaField();
          this.metaField.Hidden = true;
        }
        return this.metaField;
      }
      set => this.metaField = value;
    }

    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
      controlDescriptor.AddComponentProperty("radFormCaptcha", this.Captcha.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      List<ScriptReference> scriptReferences = new List<ScriptReference>(base.GetScriptReferences());
      string str = typeof (FormCaptcha).Assembly.GetName().ToString();
      scriptReferences.Add(new ScriptReference()
      {
        Assembly = str,
        Name = "Telerik.Sitefinity.Modules.Forms.Web.UI.Scripts.FormCaptcha.js"
      });
      return (IEnumerable<ScriptReference>) scriptReferences;
    }
  }
}
