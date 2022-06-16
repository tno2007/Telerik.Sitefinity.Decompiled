// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerFactory
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Web.UI.ControlDesign
{
  /// <summary>
  /// Factor class which provides methods for creating the instance of control designers.
  /// </summary>
  public class ControlDesignerFactory
  {
    /// <summary>
    /// Gets an instance of a control designer (should inherit ControlDesignerBase) for a given control by its data.
    /// </summary>
    /// <param name="ctrlData">Data of the control to get the designer for</param>
    /// <returns>An matching instance of a control designer (should inherit ControlDesignerBase)</returns>
    public static ControlDesignerBase GetDesigner(ControlData ctrlData) => ControlDesignerFactory.Singleton.GetDesigner(ctrlData);

    /// <summary>
    /// Gets an instance of a control designer (should inherit ControlDesignerBase) for a given control by its AssemblyQualifiedName.
    /// </summary>
    /// <param name="controlTypeString">AssemblyQualifiedName of the control to get the designer for</param>
    /// <returns>An matching instance of a control designer (should inherit ControlDesignerBase)</returns>
    public static ControlDesignerBase GetDesigner(string controlTypeString) => ControlDesignerFactory.Singleton.GetDesigner(controlTypeString);

    internal static class Singleton
    {
      /// <summary>
      /// Gets an instance of a control designer (should inherit ControlDesignerBase) for a given control by its data.
      /// </summary>
      /// <param name="ctrlData">Data of the control to get the designer for</param>
      /// <returns>An matching instance of a control designer (should inherit ControlDesignerBase)</returns>
      public static ControlDesignerBase GetDesigner(ControlData ctrlData)
      {
        Type c = TypeResolutionService.ResolveType(ctrlData.ObjectType, false);
        if (!(c != (Type) null) || !typeof (MvcProxyBase).IsAssignableFrom(c))
          return ControlDesignerFactory.Singleton.GetDesigner(ControlDesignerFactory.Singleton.GetTypeName(ctrlData));
        string name = ctrlData.Properties.Where<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name == "ControllerName" && p.Value != null)).First<ControlProperty>().Value;
        Type type = TypeResolutionService.ResolveType(name);
        if (type == (Type) null)
          throw new ArgumentException(string.Format("The controller with the name '{0}' cannot be resolved.", (object) name));
        return ControlDesignerFactory.Singleton.GetDesigner(string.Format("{0}, {1}", (object) type.FullName, (object) type.Assembly.GetName().Name));
      }

      /// <summary>
      /// Gets an instance of a control designer (should inherit ControlDesignerBase) for a given control by its AssemblyQualifiedName.
      /// </summary>
      /// <param name="controlTypeString">AssemblyQualifiedName of the control to get the designer for</param>
      /// <returns>An matching instance of a control designer (should inherit ControlDesignerBase)</returns>
      public static ControlDesignerBase GetDesigner(string controlTypeString)
      {
        Control control = (Control) null;
        string controlTypeString1 = string.Empty;
        Type controlType = (Type) null;
        if (Config.Get<SystemConfig>().ControlDesigners.Keys.Contains(controlTypeString) && Config.Get<SystemConfig>().ControlDesigners[controlTypeString] != null)
          controlTypeString1 = Config.Get<SystemConfig>().ControlDesigners[controlTypeString].ControlDesigner;
        if (string.IsNullOrEmpty(controlTypeString1))
        {
          object[] attributesForType = ControlDesignerFactory.Singleton.GetCustomAttributesForType(controlTypeString);
          if (((IEnumerable<object>) attributesForType).Count<object>() > 0)
          {
            ControlDesignerAttribute designerAttribute = (ControlDesignerAttribute) attributesForType[0];
            if (!string.IsNullOrEmpty(designerAttribute.ControlDesignerTypeName))
              controlTypeString1 = designerAttribute.ControlDesignerTypeName;
            else if (designerAttribute.ControlDesignerType != (Type) null)
              controlType = designerAttribute.ControlDesignerType;
          }
        }
        if (!string.IsNullOrEmpty(controlTypeString1))
          control = ControlDesignerFactory.Singleton.instantiateDesigner(controlTypeString1);
        else if (controlType != (Type) null)
          control = ControlDesignerFactory.Singleton.instantiateDesigner(controlType);
        return control != null && control is ControlDesignerBase ? (ControlDesignerBase) control : (ControlDesignerBase) null;
      }

      /// <summary>
      /// Creates a control from an AssemblyQualifiedName or a virtual path
      /// </summary>
      /// <param name="controlTypeString">AssemblyQualifiedName or a virtual path of the control to create</param>
      /// <returns>The created control</returns>
      internal static Control instantiateDesigner(string controlTypeString) => !string.IsNullOrEmpty(controlTypeString) ? (!controlTypeString.StartsWith("~/") ? ControlDesignerFactory.Singleton.instantiateDesigner(ControlDesignerFactory.Singleton.ResolveType(controlTypeString)) : (Control) ControlDesignerFactory.Singleton.LoadControl(controlTypeString)) : (Control) null;

      /// <summary>Creates a control from an AssemblyQualifiedName of it</summary>
      /// <param name="controlType">AssemblyQualifiedName of the control to create</param>
      /// <returns>The created control</returns>
      internal static Control instantiateDesigner(Type controlType) => (Control) Activator.CreateInstance(controlType);

      /// <summary>Gets the name of the control assembly qualified.</summary>
      /// <param name="ctrlData">The control data.</param>
      /// <returns></returns>
      internal static string getControlAssemblyQualifiedName(ControlData ctrlData) => ControlManager<PageDataProvider>.GetControlType((ObjectData) ctrlData).AssemblyQualifiedName;

      /// <summary>Gets the name of the type.</summary>
      /// <param name="controlData">The control data.</param>
      /// <returns></returns>
      internal static string GetTypeName(ControlData controlData)
      {
        Type controlType = ControlManager<PageDataProvider>.GetControlType((ObjectData) controlData);
        return controlData.ObjectType.StartsWith("~/") ? controlType.AssemblyQualifiedName : controlType.FullName;
      }

      /// <summary>Gets the type of the custom attributes for.</summary>
      /// <param name="controlTypeString">The control type string.</param>
      /// <returns></returns>
      internal static object[] GetCustomAttributesForType(string controlTypeString) => TypeResolutionService.ResolveType(controlTypeString).GetCustomAttributes(typeof (ControlDesignerAttribute), true);

      /// <summary>Loads the control.</summary>
      /// <param name="ctrlString">The control string.</param>
      /// <returns></returns>
      internal static System.Web.UI.TemplateControl LoadControl(string ctrlString) => ControlUtilities.LoadControl(ctrlString);

      /// <summary>Resolves the type.</summary>
      /// <param name="typeString">The type string.</param>
      /// <returns></returns>
      internal static Type ResolveType(string typeString) => TypeResolutionService.ResolveType(typeString, true);
    }
  }
}
