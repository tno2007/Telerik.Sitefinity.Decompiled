// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Web.UI.ModuleEditorBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Web.UI;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Web.UI.Kendo;

namespace Telerik.Sitefinity.DynamicModules.Builder.Web.UI
{
  /// <summary>
  /// Base control for all controls that are editing one single module.
  /// </summary>
  public abstract class ModuleEditorBase : KendoView
  {
    private ModuleBuilderManager moduleBuilderMng;
    private Guid moduleId;
    private Guid moduleTypeId;
    private DynamicModule module;
    private static readonly Regex isGuid = new Regex("^(\\{){0,1}[0-9a-fA-F]{8}\\-[0-9a-fA-F]{4}\\-[0-9a-fA-F]{4}\\-[0-9a-fA-F]{4}\\-[0-9a-fA-F]{12}(\\}){0,1}$", RegexOptions.Compiled);
    private DynamicModuleType moduleType;
    private IQueryable<DynamicModuleType> moduleTypes;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    [Obsolete("Use LayoutTemplatePath property instead.")]
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets the instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" /> being edited.
    /// </summary>
    protected virtual DynamicModule Module
    {
      get => this.module;
      private set
      {
        if (value == null)
          return;
        this.module = value;
      }
    }

    /// <summary>
    /// Gets the module <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" /> being edited.
    /// </summary>
    protected virtual DynamicModuleType ModuleType
    {
      get
      {
        if (this.moduleType == null)
        {
          Guid moduleTypeId = this.ModuleTypeId;
          if (this.ModuleTypeId.Equals(Guid.Empty))
            this.moduleType = this.ModuleBuilderMngr.GetDynamicModuleTypes().FirstOrDefault<DynamicModuleType>((Expression<Func<DynamicModuleType, bool>>) (t => t.ParentModuleId == this.ModuleId));
          else
            this.moduleType = this.ModuleBuilderMngr.GetDynamicModuleTypes().FirstOrDefault<DynamicModuleType>((Expression<Func<DynamicModuleType, bool>>) (t => t.ParentModuleId == this.ModuleId && t.Id == this.ModuleTypeId));
        }
        return this.moduleType;
      }
    }

    /// <summary>Gets the module content types.</summary>
    protected virtual IQueryable<DynamicModuleType> ModuleTypes
    {
      get
      {
        if (this.moduleTypes == null)
          this.moduleTypes = this.ModuleBuilderMngr.GetDynamicModuleTypes().Where<DynamicModuleType>((Expression<Func<DynamicModuleType, bool>>) (t => t.ParentModuleId == this.ModuleId));
        return this.moduleTypes;
      }
    }

    /// <summary>
    /// Gets the instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.ModuleBuilderManager" />.
    /// </summary>
    protected virtual ModuleBuilderManager ModuleBuilderMngr
    {
      get
      {
        if (this.moduleBuilderMng == null)
          this.moduleBuilderMng = ModuleBuilderManager.GetManager();
        return this.moduleBuilderMng;
      }
    }

    /// <summary>Gets or sets the id of the module being edited.</summary>
    protected virtual Guid ModuleId
    {
      get => this.moduleId;
      private set => this.moduleId = value;
    }

    /// <summary>Gets or sets the id of the module type being edited.</summary>
    protected virtual Guid ModuleTypeId
    {
      get => this.moduleTypeId;
      private set => this.moduleTypeId = value;
    }

    /// <summary>Initialized the variables dependant on the URL.</summary>
    /// <returns></returns>
    protected bool InitializeURLDependantVariables()
    {
      string[] urlParameters = ControlExtensions.GetUrlParameters(this.Page);
      if (urlParameters == null || urlParameters.Length == 0)
        return false;
      int length = urlParameters.Length;
      if (length > 3 || !ModuleEditorBase.IsGuid(urlParameters[0], out this.moduleId) || length > 1 && !ModuleEditorBase.IsGuid(urlParameters[1], out this.moduleTypeId))
        return false;
      if (this.module == null)
      {
        this.Module = this.ModuleBuilderMngr.GetDynamicModules().Where<DynamicModule>((Expression<Func<DynamicModule, bool>>) (m => m.Id == this.ModuleId)).FirstOrDefault<DynamicModule>();
        if (this.Module == null || this.ModuleType == null)
          return false;
      }
      return true;
    }

    private static bool IsGuid(string candidate, out Guid output)
    {
      bool flag = false;
      output = Guid.Empty;
      if (candidate != null && ModuleEditorBase.isGuid.IsMatch(candidate))
      {
        output = new Guid(candidate);
        flag = true;
      }
      return flag;
    }
  }
}
