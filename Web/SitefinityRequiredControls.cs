// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.SitefinityRequiredControls
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.Sitefinity.Modules.ResponsiveDesign.Web;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.TrackingConsent;

namespace Telerik.Sitefinity.Web
{
  /// <summary>
  /// This control is always included in the frontend and should be used apply common logic.
  /// </summary>
  public class SitefinityRequiredControls : Control
  {
    private static readonly List<SitefinityRequiredControls.ControlDefinition> additionalControls = new List<SitefinityRequiredControls.ControlDefinition>();

    protected override void OnInit(EventArgs e) => this.Page.PreRenderComplete += new EventHandler(this.Page_PreRenderComplete);

    private void Page_PreRenderComplete(object sender, EventArgs e)
    {
      if (!this.IsDesignMode())
        SitefinityRequiredControls.AddTrackingConsentScripts(this.Page.Header.Controls);
      if ((this.IsPreviewMode() || !this.IsDesignMode()) && SystemManager.IsModuleAccessible("ResponsiveDesign"))
        this.Page.Header.Controls.Add((Control) new ResponsiveDesignPageManager());
      foreach (SitefinityRequiredControls.ControlDefinition additionalControl in SitefinityRequiredControls.additionalControls)
      {
        if (additionalControl.ShouldRender == null || additionalControl.ShouldRender())
        {
          Control instance = Activator.CreateInstance(additionalControl.ControlType) as Control;
          switch (additionalControl.PageSection)
          {
            case SitefinityRequiredControls.PageSection.Header:
              this.Page.Header.Controls.Add(instance);
              continue;
            case SitefinityRequiredControls.PageSection.Body:
              this.Page.Controls.Add(instance);
              continue;
            default:
              continue;
          }
        }
      }
    }

    /// <summary>
    /// Registers an additional child control that will be rendered from the this control
    /// </summary>
    /// <param name="controlType">The control type</param>
    /// <param name="pageSection">The section where the control will be placed</param>
    /// <param name="shouldRender">
    ///     Boolean function that defines if the control should be rendered
    ///     e.g. Some controls need to be rendered depending on design/preview mode
    /// </param>
    public static void RegisterControl(
      Type controlType,
      SitefinityRequiredControls.PageSection pageSection,
      Func<bool> shouldRender = null)
    {
      if (!typeof (Control).IsAssignableFrom(controlType))
        throw new NotSupportedException("type System.Web.UI.Control should be assignable from controlType");
      SitefinityRequiredControls.ControlDefinition controlDefinition = new SitefinityRequiredControls.ControlDefinition()
      {
        ControlType = controlType,
        PageSection = pageSection,
        ShouldRender = shouldRender
      };
      SitefinityRequiredControls.additionalControls.Add(controlDefinition);
    }

    internal static void AppendControl(Page page, Control control)
    {
      int index1 = page.Controls.Count - 1;
      MasterPage masterPage = (MasterPage) null;
      bool flag = false;
      for (int index2 = page.Controls.Count - 1; index2 >= 0; --index2)
      {
        if (page.Controls[index2] is LiteralControl)
        {
          index1 = index2;
          flag = true;
          break;
        }
        if (page.Controls[index2] is MasterPage)
        {
          masterPage = page.Controls[index2] as MasterPage;
          break;
        }
      }
      if (masterPage != null)
      {
        for (int index3 = masterPage.Controls.Count - 1; index3 >= 0; --index3)
        {
          if (masterPage.Controls[index3] is LiteralControl)
          {
            index1 = index3;
            flag = true;
            break;
          }
        }
      }
      if (!flag)
      {
        if (page.Form != null)
          page.Form.Controls.Add(control);
        else
          page.Controls.Add(control);
      }
      else if (masterPage != null)
        masterPage.Controls.AddAt(index1, control);
      else
        page.Controls.AddAt(index1, control);
    }

    private static void AddTrackingConsentScripts(ControlCollection controls)
    {
      int index = SitefinityRequiredControls.GetMetaCharsetTagControlIndex(controls) + 1;
      if (index < controls.Count)
        controls.AddAt(index, (Control) new ConsentManagerScriptControl());
      else
        controls.Add((Control) new ConsentManagerScriptControl());
    }

    private static int GetMetaCharsetTagControlIndex(ControlCollection controls)
    {
      int charsetTagControlIndex = -1;
      for (int index = 0; index < controls.Count; ++index)
      {
        if (SitefinityRequiredControls.IsMetaCharsetTag(controls[index]))
        {
          charsetTagControlIndex = index;
          break;
        }
      }
      return charsetTagControlIndex;
    }

    private static bool IsMetaCharsetTag(Control control)
    {
      switch (control)
      {
        case HtmlMeta htmlMeta:
          return htmlMeta.Attributes["charset"] != null;
        case LiteralControl literalControl:
          return literalControl.Text.Contains("<meta") && literalControl.Text.Contains(" charset=\"");
        default:
          return false;
      }
    }

    /// <summary>
    /// Defines in which page section the control will be placed
    /// </summary>
    public enum PageSection
    {
      /// <summary>Header section</summary>
      Header = 1,
      /// <summary>Body section</summary>
      Body = 2,
    }

    private class ControlDefinition
    {
      public Type ControlType { get; set; }

      public SitefinityRequiredControls.PageSection PageSection { get; set; }

      public Func<bool> ShouldRender { get; set; }
    }
  }
}
