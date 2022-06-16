// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ControlTemplates.ControlTemplates
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.ControlTemplates
{
  /// <summary>
  /// A static class that provides methods for registering, unregistering and getting template-able controls.
  /// </summary>
  public static class ControlTemplates
  {
    internal static IDictionary<string, IControlTemplateInfo> controlTemplates = (IDictionary<string, IControlTemplateInfo>) new Dictionary<string, IControlTemplateInfo>();

    /// <summary>Registers the template-able control.</summary>
    /// <typeparam name="TControl">Type of the control.</typeparam>
    /// <typeparam name="TDataItem">Type of the data item.</typeparam>
    public static void RegisterTemplatableControl<TControl, TDataItem>() => Telerik.Sitefinity.Modules.ControlTemplates.ControlTemplates.RegisterTemplatableControl(typeof (TControl), typeof (TDataItem));

    /// <summary>Registers the template-able control.</summary>
    /// <param name="controlType">Type of the control.</param>
    /// <param name="dataItemType">Type of the data item.</param>
    public static void RegisterTemplatableControl(Type controlType, Type dataItemType)
    {
      Type attributeType = typeof (ControlTemplateInfoAttribute);
      if (!(TypeDescriptor.GetAttributes(controlType)[attributeType] is ControlTemplateInfoAttribute attribute))
        return;
      Telerik.Sitefinity.Modules.ControlTemplates.ControlTemplates.RegisterTemplatableControl(controlType, dataItemType, attribute.ResourceClassId, attribute.AreaName, attribute.ControlDisplayName);
    }

    /// <summary>Registers the template-able control.</summary>
    /// <param name="controlType">Type of the control.</param>
    /// <param name="dataItemType">Type of the data item.</param>
    public static void RegisterTemplatableControl(
      Type controlType,
      Type dataItemType,
      string resourceClassId,
      string areaName,
      string controlDisplayName)
    {
      string controlKey = Telerik.Sitefinity.Modules.ControlTemplates.ControlTemplates.GetControlKey(controlType, areaName);
      if (Telerik.Sitefinity.Modules.ControlTemplates.ControlTemplates.controlTemplates.ContainsKey(controlKey))
        return;
      Telerik.Sitefinity.Modules.ControlTemplates.ControlTemplates.controlTemplates.Add(controlKey, (IControlTemplateInfo) new ControlTemplateInfo()
      {
        ControlType = controlType,
        DataItemType = dataItemType,
        ResourceClassId = resourceClassId,
        AreaName = areaName,
        FriendlyControlName = controlDisplayName
      });
    }

    /// <summary>Unregisters the template-able control.</summary>
    /// <param name="templateControl">The template control.</param>
    public static void UnregisterTemplatableControl(IControlTemplateInfo controlTemplate)
    {
      string controlKey = Telerik.Sitefinity.Modules.ControlTemplates.ControlTemplates.GetControlKey(controlTemplate.ControlType, controlTemplate.AreaName);
      if (!Telerik.Sitefinity.Modules.ControlTemplates.ControlTemplates.controlTemplates.ContainsKey(controlKey))
        return;
      Telerik.Sitefinity.Modules.ControlTemplates.ControlTemplates.controlTemplates.Remove(controlKey);
    }

    /// <summary>Gets the template-able controls.</summary>
    public static IDictionary<string, IControlTemplateInfo> GetTemplatableControls() => Telerik.Sitefinity.Modules.ControlTemplates.ControlTemplates.controlTemplates;

    /// <summary>
    /// Gets the type of the control registered with the specified key.
    /// </summary>
    /// <param name="key">The key.</param>
    internal static Type GetControlTypeFromKey(string key)
    {
      int length = key.IndexOf("-");
      return TypeResolutionService.ResolveType(length < 0 ? key : key.Substring(0, length));
    }

    /// <summary>Gets the control key.</summary>
    /// <param name="controlType">Type of the control.</param>
    /// <param name="areaName">Name of the area.</param>
    internal static string GetControlKey(string controlType, string areaName) => !areaName.IsNullOrEmpty() ? string.Format("{0}-{1}", (object) controlType, (object) areaName) : controlType;

    /// <summary>Gets the control key.</summary>
    /// <param name="controlType">Type of the control.</param>
    /// <param name="areaName">Name of the area.</param>
    internal static string GetControlKey(Type controlType, string areaName) => Telerik.Sitefinity.Modules.ControlTemplates.ControlTemplates.GetControlKey(controlType.FullName, areaName);

    /// <summary>Clears the control templates.</summary>
    public static void Clear() => Telerik.Sitefinity.Modules.ControlTemplates.ControlTemplates.controlTemplates.Clear();
  }
}
