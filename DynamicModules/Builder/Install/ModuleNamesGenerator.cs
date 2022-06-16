// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Install.ModuleNamesGenerator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.DynamicModules.Builder.Model;

namespace Telerik.Sitefinity.DynamicModules.Builder.Install
{
  /// <summary>
  /// This class provides functions for generating various names required by the module.
  /// </summary>
  internal static class ModuleNamesGenerator
  {
    private const string backendDefinitionSuffix = "BackendDefinition";
    private const string backendListViewNameSuffix = "BackendList";
    private const string backendItemWidgetSuffix = "ItemWidget";
    private const string backendDefinitionNameSuffix = "Backend";
    private const string backendDialogElementSuffix = "DialogElement";
    private const string backendInsertViewNameSuffix = "BackendInsertView";
    private const string backendDuplicateViewNameSuffix = "BackendDuplicateView";
    private const string backendPreviewViewNameSuffix = "BackendPreviewView";
    private const string backendVersionPreviewNameSuffix = "BackendVersionPreview";
    private const string backendComparerVersionNameSuffix = "BackendVersionComparisonView";
    private const string backendEditViewNameSuffix = "BackendEditView";
    public const string pipeNameSuffix = "Pipe";

    /// <summary>
    /// Generates the name of the <see cref="!:ContentViewControlDefinition" /> for the specified module type name.
    /// </summary>
    /// <param name="moduleTypeName">Name of the module type for which the content view definition name should be generated.</param>
    /// <returns>The name of the <see cref="!:ContentViewControlDefinition" /> for the specified module type name.</returns>
    public static string GenerateContentViewDefinitionName(string moduleTypeFullName)
    {
      if (string.IsNullOrEmpty(moduleTypeFullName))
        throw new ArgumentNullException("moduleTypeName");
      return ModuleNamesGenerator.PrepareNameForQueryString(moduleTypeFullName) + "BackendDefinition";
    }

    /// <summary>Generates the name for the specified widget element.</summary>
    /// <param name="widgetName">Name of the widget.</param>
    public static string GenerateWidgetElementName(string widgetName)
    {
      if (string.IsNullOrEmpty(widgetName))
        throw new ArgumentNullException(nameof (widgetName));
      return widgetName + "ItemWidget";
    }

    /// <summary>
    /// Generates the name of the list view for the specified module type.
    /// </summary>
    /// <param name="moduleTypeName">Name of the module type.</param>
    /// <returns></returns>
    public static string GenerateListViewName(string moduleTypeName)
    {
      if (string.IsNullOrEmpty(moduleTypeName))
        throw new ArgumentNullException(nameof (moduleTypeName));
      return ModuleNamesGenerator.PrepareNameForQueryString(moduleTypeName) + "BackendList";
    }

    /// <summary>
    /// Generates the name of the backend insert view for the specified module.
    /// </summary>
    /// <param name="modulName">Name of the module.</param>
    /// <returns></returns>
    public static string GenerateBackendInsertViewName(string moduleName)
    {
      if (string.IsNullOrEmpty(moduleName))
        throw new ArgumentNullException(nameof (moduleName));
      return ModuleNamesGenerator.PrepareNameForQueryString(moduleName) + "BackendInsertView";
    }

    /// <summary>
    /// Generates the name of the backend duplicate view for the specified module.
    /// </summary>
    /// <param name="moduleName">Name of the module.</param>
    /// <returns></returns>
    public static string GenerateBackendDuplicateViewName(string moduleName)
    {
      if (string.IsNullOrEmpty(moduleName))
        throw new ArgumentNullException(nameof (moduleName));
      return ModuleNamesGenerator.PrepareNameForQueryString(moduleName) + "BackendDuplicateView";
    }

    /// <summary>
    /// Generates the name of the backend preview view for the specified module name.
    /// </summary>
    /// <param name="moduleName">Name of the module.</param>
    public static string GenerateBackendPreviewViewName(string moduleName)
    {
      if (string.IsNullOrEmpty(moduleName))
        throw new ArgumentNullException(nameof (moduleName));
      return ModuleNamesGenerator.PrepareNameForQueryString(moduleName) + "BackendPreviewView";
    }

    /// <summary>
    /// Generates the name of the backend history version preview view for the specified module name.
    /// </summary>
    /// <param name="moduleName">Name of the module.</param>
    public static string GenerateBackendVersionPreview(string moduleName)
    {
      if (string.IsNullOrEmpty(moduleName))
        throw new ArgumentNullException(nameof (moduleName));
      return ModuleNamesGenerator.PrepareNameForQueryString(moduleName) + "BackendVersionPreview";
    }

    /// <summary>
    /// Generates the name of the backend history comparer version view for the specified module name.
    /// </summary>
    /// <param name="moduleName">Name of the module.</param>
    public static string GenerateBackendVersionComparerPreview(string moduleName)
    {
      if (string.IsNullOrEmpty(moduleName))
        throw new ArgumentNullException(nameof (moduleName));
      return ModuleNamesGenerator.PrepareNameForQueryString(moduleName) + "BackendVersionComparisonView";
    }

    /// <summary>
    /// Generates the name of the backend edit view for the specified module.
    /// </summary>
    /// <param name="moduleName">Name of the module.</param>
    public static string GenerateBackendEditViewName(string moduleName)
    {
      if (string.IsNullOrEmpty(moduleName))
        throw new ArgumentNullException(nameof (moduleName));
      return ModuleNamesGenerator.PrepareNameForQueryString(moduleName) + "BackendEditView";
    }

    /// <summary>
    /// Generates the name of the backend definition for the specified module name.
    /// </summary>
    /// <param name="moduleTypeName">Name of the module type.</param>
    public static string GenerateBackendDefinitionName(string moduleTypeName)
    {
      if (string.IsNullOrEmpty(moduleTypeName))
        throw new ArgumentNullException(nameof (moduleTypeName));
      return ModuleNamesGenerator.PrepareNameForQueryString(moduleTypeName) + "Backend";
    }

    /// <summary>
    /// Generates the name of the backend dialog element by the specified dialog name and module type name.
    /// </summary>
    /// <param name="dialogName">Name of the dialog.</param>
    /// <param name="moduleTypeName">Name of the module type.</param>
    public static string GenerateBackendDialogElementName(string dialogName, string moduleTypeName)
    {
      if (string.IsNullOrEmpty(dialogName))
        throw new ArgumentNullException(nameof (dialogName));
      if (string.IsNullOrEmpty(moduleTypeName))
        throw new ArgumentNullException(nameof (moduleTypeName));
      return moduleTypeName + dialogName + "DialogElement";
    }

    /// <summary>Generates a pipe name for the specified module type.</summary>
    /// <param name="moduleType">Type of the module.</param>
    /// <returns></returns>
    public static string GeneratePipeName(IDynamicModuleType moduleType) => moduleType != null ? ModuleNamesGenerator.GeneratePipeName(moduleType.GetFullTypeName()) : throw new ArgumentNullException(nameof (moduleType));

    /// <summary>
    /// Generates a pipe name for tyhe specified module type name.
    /// </summary>
    /// <param name="moduleType">Type of the module.</param>
    /// <returns></returns>
    public static string GeneratePipeName(string moduleType)
    {
      if (moduleType.IsNullOrEmpty())
        throw new ArgumentNullException(nameof (moduleType));
      return moduleType + "Pipe";
    }

    /// <summary>
    /// Resolves the name of the dynamic type name from the specified pipe.
    /// </summary>
    /// <param name="pipeName">Name of the pipe.</param>
    /// <param name="typeNamespace">The type namespace.</param>
    /// <param name="typeName">Name of the type.</param>
    public static void ResolveDynamicTypeNameFromPipeName(
      string pipeName,
      out string typeNamespace,
      out string typeName)
    {
      string str = !string.IsNullOrEmpty(pipeName) ? pipeName.Sub(0, pipeName.Length - ("Pipe".Length + 1)) : throw new ArgumentNullException(nameof (pipeName));
      typeName = str.Substring(str.LastIndexOf('.') + 1);
      typeNamespace = str.Sub(0, str.Length - typeName.Length - 2);
    }

    private static string PrepareNameForQueryString(string name) => name.Replace("?", "").Replace("&", "");
  }
}
