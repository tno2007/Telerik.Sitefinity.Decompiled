// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.FormRadCaptchaDesignerView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Web.UI;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.Forms.Web.UI.Designers
{
  public class FormRadCaptchaDesignerView : ContentViewDesignerView, IScriptControl
  {
    internal const string designerViewJs = "Telerik.Sitefinity.Modules.Forms.Web.UI.Scripts.FormRadCaptchaFieldDesingerView.js";
    internal const string designerViewInterfaceControlJs = "Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.IDesignerViewControl.js";
    internal const string layoutTemplatePath = "Telerik.Sitefinity.Resources.Templates.Designers.Forms.FormRadCaptchaFieldDesignerView.ascx";

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.GetType().FullName, this.ClientID);
      controlDescriptor.AddComponentProperty("imageTextLength", this.ImageTextLength.ClientID);
      controlDescriptor.AddComponentProperty("fontFamily", this.GetFontFamilyChoiceField.ClientID);
      controlDescriptor.AddComponentProperty("textColorField", this.GetTextColorField.ClientID);
      controlDescriptor.AddComponentProperty("backgroundColorField", this.BackgroundColorField.ClientID);
      controlDescriptor.AddComponentProperty("lineNoiseField", this.LineNoiseField.ClientID);
      controlDescriptor.AddComponentProperty("fontWarpField", this.FontWarpField.ClientID);
      controlDescriptor.AddComponentProperty("backgroundNoiseLevelField", this.BackgroundNoiseLevelField.ClientID);
      controlDescriptor.AddComponentProperty("displayOnlyForUnauthenticatedUsers", this.DisplayOnlyForUnauthenticatedUsers.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      string assembly = this.GetType().Assembly.GetName().ToString();
      return (IEnumerable<ScriptReference>) new List<ScriptReference>()
      {
        new ScriptReference("Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.IDesignerViewControl.js", assembly),
        new ScriptReference("Telerik.Sitefinity.Modules.Forms.Web.UI.Scripts.FormRadCaptchaFieldDesingerView.js", assembly)
      };
    }

    public override string LayoutTemplatePath
    {
      get => string.IsNullOrWhiteSpace(base.LayoutTemplatePath) ? ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Designers.Forms.FormRadCaptchaFieldDesignerView.ascx") : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets the name of the view.</summary>
    /// <value>The name of the view.</value>
    public override string ViewName => this.GetType().Name;

    /// <summary>Gets the view title.</summary>
    /// <value>The view title.</value>
    public override string ViewTitle => Res.Get<FormsResources>().CaptchaDesignerViewTitle;

    protected override void InitializeControls(GenericContainer container)
    {
      this.BindFontFamilies(this.GetFontFamilyChoiceField);
      this.BindColorChoiceField(this.GetTextColorField);
      this.BindColorChoiceField(this.BackgroundColorField);
      this.BindChoiseFieldToEnum(typeof (CaptchaLineNoiseLevel), this.LineNoiseField);
      this.BindChoiseFieldToEnum(typeof (CaptchaFontWarpFactor), this.FontWarpField);
      this.BindChoiseFieldToEnum(typeof (CaptchaBackgroundNoiseLevel), this.BackgroundNoiseLevelField);
    }

    protected virtual void BindFontFamilies(ChoiceField fontFamilyChoiceField)
    {
      foreach (FontFamily family in FontFamily.Families)
        fontFamilyChoiceField.Choices.Add(new ChoiceItem()
        {
          Text = family.GetName(SystemManager.CurrentContext.Culture.LCID),
          Value = family.Name
        });
    }

    protected virtual void BindColorChoiceField(ChoiceField choiceField)
    {
      foreach (Color standardValue in (IEnumerable) new ColorConverter().GetStandardValues())
      {
        if (standardValue.IsNamedColor)
          choiceField.Choices.Add(new ChoiceItem()
          {
            Text = standardValue.Name,
            Value = standardValue.Name
          });
      }
    }

    protected virtual void BindChoiseFieldToEnum(Type enumType, ChoiceField choiceField)
    {
      foreach (object obj in Enum.GetValues(enumType))
        choiceField.Choices.Add(new ChoiceItem()
        {
          Value = obj.ToString(),
          Text = Enum.GetName(enumType, obj)
        });
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    public TextField ImageTextLength => this.Container.GetControl<TextField>("imageTextLength", true);

    public ChoiceField GetFontFamilyChoiceField => this.Container.GetControl<ChoiceField>("fontFamily", true);

    public ChoiceField GetTextColorField => this.Container.GetControl<ChoiceField>("textColorField", true);

    public ChoiceField BackgroundColorField => this.Container.GetControl<ChoiceField>("backgroundColorField", true);

    public ChoiceField LineNoiseField => this.Container.GetControl<ChoiceField>("lineNoiseField", true);

    public ChoiceField FontWarpField => this.Container.GetControl<ChoiceField>("fontWarpField", true);

    public ChoiceField BackgroundNoiseLevelField => this.Container.GetControl<ChoiceField>("backgroundNoiseLevelField", true);

    public ChoiceField DisplayOnlyForUnauthenticatedUsers => this.Container.GetControl<ChoiceField>("displayOnlyForUnauthenticatedUsers", true);
  }
}
