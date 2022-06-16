// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Extenders.IExpandableControlExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.UI.Extenders.Contracts;

namespace Telerik.Sitefinity.Web.UI.Extenders
{
  public static class IExpandableControlExtensions
  {
    public static ScriptComponentDescriptor GetExpandableExtenderDescriptor(
      this IExpandableControl extender,
      string fieldControlElementID)
    {
      ScriptBehaviorDescriptor extenderDescriptor = new ScriptBehaviorDescriptor("Telerik.Sitefinity.Web.UI.Extenders.ExpandableExtender", fieldControlElementID);
      extenderDescriptor.AddProperty("expanded", (object) extender.Expanded);
      extenderDescriptor.AddProperty("expandText", (object) extender.ExpandText);
      extenderDescriptor.AddElementProperty("expandElement", extender.ExpandControl.ClientID);
      extenderDescriptor.AddElementProperty("expandTarget", extender.ExpandTarget.ClientID);
      extenderDescriptor.AddComponentProperty("fieldControl", fieldControlElementID);
      return (ScriptComponentDescriptor) extenderDescriptor;
    }

    public static ScriptReference GetExpandableExtenderScript(
      this IExpandableControl extender)
    {
      return new ScriptReference("Telerik.Sitefinity.Web.UI.Extenders.Scripts.ExpandableExtender.js", typeof (IExpandableControl).Assembly.FullName);
    }

    /// <summary>
    /// The method configures the expandable control according to the properites of the control.
    /// </summary>
    /// <param name="extender">The extender.</param>
    /// <param name="resourceClassId">The resource class pageId.</param>
    public static void ConfigureExpandableControl(
      this IExpandableControl extender,
      string resourceClassId)
    {
      IExpandableControlExtensions.SetText(extender.ExpandControl, extender.ExpandText, resourceClassId);
      if (!extender.Expanded.HasValue || extender.Expanded.Value)
      {
        extender.ExpandControl.Style.Add("display", "none");
        extender.ExpandTarget.Style.Add("display", "");
      }
      else
      {
        extender.ExpandControl.Style.Add("display", "");
        extender.ExpandTarget.Style.Add("display", "none");
      }
    }

    public static void ConfigureExpandableControl(
      this IExpandableControl extender,
      IExpandableControlDefinition definition)
    {
      if (definition == null)
        return;
      IExpandableControlExtensions.SetText(extender.ExpandControl, definition.ExpandText, definition.ResourceClassId);
      extender.Expanded = definition.Expanded;
      extender.ExpandText = definition.ExpandText;
      if (!definition.Expanded.HasValue || definition.Expanded.Value)
      {
        extender.ExpandControl.Style.Add("display", "none");
        extender.ExpandTarget.Style.Add("display", "");
      }
      else
      {
        extender.ExpandControl.Style.Add("display", "");
        extender.ExpandTarget.Style.Add("display", "none");
      }
    }

    public static void ConfigureExpandableControl(
      this IExpandableControl extender,
      IExpandableControlDefinition definition,
      string resourceClassId)
    {
      if (definition == null)
        return;
      IExpandableControlExtensions.SetText(extender.ExpandControl, definition.ExpandText, resourceClassId);
      extender.Expanded = definition.Expanded;
      if (!definition.Expanded.HasValue || definition.Expanded.Value)
      {
        extender.ExpandControl.Style.Add("display", "none");
        extender.ExpandTarget.Style.Add("displaye", "");
      }
      else
      {
        extender.ExpandControl.Style.Add("display", "");
        extender.ExpandTarget.Style.Add("display", "none");
      }
    }

    private static void SetText(WebControl control, string text, string resourceClassId)
    {
      if (string.IsNullOrEmpty(text))
        return;
      string str = text;
      if (!string.IsNullOrEmpty(resourceClassId))
        str = Res.Get(resourceClassId, text);
      switch (control)
      {
        case IButtonControl buttonControl:
          buttonControl.Text = str;
          break;
        case ITextControl textControl:
          textControl.Text = str;
          break;
        default:
          throw new InvalidOperationException("When IExpandableControl is defined through a definition, ExpandableControl property must implement ITextControl or IButtonControl interface.");
      }
    }
  }
}
