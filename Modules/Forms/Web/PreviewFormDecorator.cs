// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Web.PreviewFormDecorator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Forms.Web
{
  /// <summary>Class for objects decorating form preview markup</summary>
  /// <seealso cref="T:Telerik.Sitefinity.Modules.Forms.Web.IPreviewFormDecorator" />
  internal class PreviewFormDecorator : IPreviewFormDecorator
  {
    /// <summary>Decorates the form preview.</summary>
    /// <param name="page">The page.</param>
    /// <param name="draftForm">The draft form.</param>
    public void DecorateFormPreview(Page page, FormDraft draftForm)
    {
      if (page == null)
        throw new ArgumentNullException(nameof (page));
      if (draftForm == null)
        throw new ArgumentNullException(nameof (draftForm));
      if (!(page.FindControl("PublicWrapper") is HtmlGenericControl control))
        return;
      List<string> hiddenFieldIds = draftForm.GetHiddenFieldIds();
      Dictionary<string, string> fieldNames = draftForm.GetFieldNames();
      bool hasFormRules = !string.IsNullOrWhiteSpace(draftForm.Rules);
      if (!(hiddenFieldIds.Count > 0 | hasFormRules))
        return;
      PreviewFormDecorator.AddHiddenFieldsControl(control, hiddenFieldIds);
      PreviewFormDecorator.WrapFormFieldsInScriptTags((Control) control, "Header", fieldNames);
      PreviewFormDecorator.WrapFormFieldsInScriptTags((Control) control, "Body", fieldNames);
      PreviewFormDecorator.WrapFormFieldsInScriptTags((Control) control, "Footer", fieldNames);
      PreviewFormDecorator.AddResourceLinksControl((Control) control, hasFormRules);
      if (hasFormRules)
        PreviewFormDecorator.AddFormRulesControl(draftForm, control);
      PreviewFormDecorator.HideHiddenFieldsOnInitialRender(draftForm, control);
    }

    private static void HideHiddenFieldsOnInitialRender(
      FormDraft draftForm,
      HtmlGenericControl publicWrapperControl)
    {
      publicWrapperControl.Controls.Add((Control) new LiteralControl(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "<input type='hidden' data-sf-key=\"{0}\" />", (object) draftForm.ParentForm.Id)));
      publicWrapperControl.Controls.Add((Control) new LiteralControl(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "<script>formHiddenFieldsInitialization(\"{0}\")</script>", (object) draftForm.ParentForm.Id)));
      Control parent = publicWrapperControl.Parent;
      int index = parent.Controls.IndexOf((Control) publicWrapperControl);
      if (index == -1)
        return;
      parent.Controls.AddAt(index, (Control) new LiteralControl("<div data-sf-role='form-visibility-wrapper' style='display: none'>"));
      parent.Controls.AddAt(index + 2, (Control) new LiteralControl("</div>"));
    }

    private static void AddHiddenFieldsControl(
      HtmlGenericControl publicWrapperControl,
      List<string> hiddenFieldIds)
    {
      HtmlInputHidden child = new HtmlInputHidden();
      child.Attributes.Add("data-sf-role", "form-rules-hidden-fields");
      child.Attributes.Add("name", "sf_FormHiddenFields");
      child.Attributes.Add("autocomplete", "off");
      child.Attributes.Add("value", string.Join(",", (IEnumerable<string>) hiddenFieldIds));
      publicWrapperControl.Controls.Add((Control) child);
    }

    private static void AddFormRulesControl(
      FormDraft draftForm,
      HtmlGenericControl publicWrapperControl)
    {
      HtmlInputHidden child = new HtmlInputHidden();
      child.Attributes.Add("data-sf-role", "form-rules");
      child.Attributes.Add("value", draftForm.Rules);
      publicWrapperControl.Controls.Add((Control) child);
    }

    private static void WrapFormFieldsInScriptTags(
      Control parentContainer,
      string childContainerKey,
      Dictionary<string, string> fieldNames)
    {
      Control control1 = parentContainer.FindControl(childContainerKey);
      if (control1 == null)
        return;
      foreach (Control control2 in control1.Controls.OfType<Control>().ToList<Control>())
      {
        if (control2 is MvcProxyBase mvcProxyBase)
        {
          HtmlGenericControl child1 = new HtmlGenericControl()
          {
            TagName = "script"
          };
          child1.Attributes.Add("data-sf-role", "start_field_" + mvcProxyBase.ID);
          string str = fieldNames.ContainsKey(mvcProxyBase.ID) ? fieldNames[mvcProxyBase.ID] : string.Empty;
          child1.Attributes.Add("data-sf-role-field-name", str);
          HtmlGenericControl child2 = new HtmlGenericControl()
          {
            TagName = "script"
          };
          child2.Attributes.Add("data-sf-role", "end_field_" + mvcProxyBase.ID);
          int index = control1.Controls.IndexOf(control2);
          if (index != -1)
          {
            control1.Controls.AddAt(index, (Control) child1);
            control1.Controls.AddAt(index + 2, (Control) child2);
          }
        }
      }
    }

    private static void AddResourceLinksControl(Control container, bool hasFormRules)
    {
      ResourceLinks child = new ResourceLinks();
      child.UseEmbeddedThemes = false;
      child.Links.Add(new ResourceFile()
      {
        JavaScriptLibrary = JavaScriptLibrary.JQuery
      });
      child.Links.Add(new ResourceFile()
      {
        Name = "Telerik.Sitefinity.Modules.Forms.Web.UI.Scripts.form-hidden-fields.js",
        AssemblyInfo = "Telerik.Sitefinity.Modules.Forms.FormsModule"
      });
      if (hasFormRules)
        child.Links.Add(new ResourceFile()
        {
          Name = "Telerik.Sitefinity.Modules.Forms.Web.UI.Scripts.form-rules.js",
          AssemblyInfo = "Telerik.Sitefinity.Modules.Forms.FormsModule"
        });
      container.Controls.Add((Control) child);
    }
  }
}
