// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Configuration.FieldControls.ToolsConfigManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Configuration;

namespace Telerik.Sitefinity.Web.Configuration.FieldControls
{
  public class ToolsConfigManager
  {
    private const string ToolsConfigurationName = "ToolsConfiguration";
    private static readonly object _controlsTypeCacheRoot = new object();
    private static readonly object _userControlsCacheRoot = new object();
    private static FieldControlsConfig _fieldControlsConfig = Config.Get<FieldControlsConfig>();
    private static Dictionary<string, Type> _controlsTypeCache = new Dictionary<string, Type>();
    private static Dictionary<string, string> _userControlsCache = new Dictionary<string, string>();

    public static FieldControlsConfig FieldControlsConfig
    {
      get => ToolsConfigManager._fieldControlsConfig;
      set => ToolsConfigManager._fieldControlsConfig = value;
    }

    public static Type GetControlTypeFromTool(string toolName)
    {
      if (string.IsNullOrEmpty(toolName))
        return (Type) null;
      if (!ToolsConfigManager._fieldControlsConfig.Tools.Contains(toolName))
        return (Type) null;
      Type controlTypeFromTool = (Type) null;
      if (!ToolsConfigManager._controlsTypeCache.TryGetValue(toolName, out controlTypeFromTool))
      {
        lock (ToolsConfigManager._controlsTypeCacheRoot)
        {
          if (!ToolsConfigManager._controlsTypeCache.TryGetValue(toolName, out controlTypeFromTool))
          {
            controlTypeFromTool = Type.GetType(ToolsConfigManager._fieldControlsConfig.Tools.Get(toolName).ControlTypeName);
            ToolsConfigManager._controlsTypeCache[toolName] = controlTypeFromTool;
          }
        }
      }
      return controlTypeFromTool;
    }

    public static string GetUserControlFromTool(string toolName)
    {
      if (string.IsNullOrEmpty(toolName))
        return (string) null;
      if (!ToolsConfigManager._fieldControlsConfig.Tools.Contains(toolName))
        return (string) null;
      string userControlFromTool = (string) null;
      if (!ToolsConfigManager._userControlsCache.TryGetValue(toolName, out userControlFromTool))
      {
        lock (ToolsConfigManager._userControlsCacheRoot)
        {
          if (!ToolsConfigManager._userControlsCache.TryGetValue(toolName, out userControlFromTool))
          {
            userControlFromTool = ToolsConfigManager._fieldControlsConfig.Tools.Get(toolName).UserControl;
            ToolsConfigManager._userControlsCache[toolName] = userControlFromTool;
          }
        }
      }
      return userControlFromTool;
    }

    public static bool ContainsTool(string toolName) => ToolsConfigManager._fieldControlsConfig.Tools.Contains(toolName);

    public static string GetToolDisplayName(string toolName)
    {
      if (ToolsConfigManager._fieldControlsConfig == null || !ToolsConfigManager._fieldControlsConfig.Tools.Contains(toolName))
        return string.Empty;
      ToolConfigElement toolConfigElement = ToolsConfigManager._fieldControlsConfig.Tools.Get(toolName);
      string toolDisplayName = toolConfigElement.DisplayName;
      if (string.IsNullOrEmpty(toolDisplayName))
        toolDisplayName = toolConfigElement.Name;
      return toolDisplayName;
    }
  }
}
