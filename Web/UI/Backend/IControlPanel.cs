// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.IControlPanel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;

namespace Telerik.Sitefinity.Web.UI.Backend
{
  /// <summary>Defines members for implementation of a ControlPanel control.</summary>
  /// <example>
  /// 	<code lang="CS" title="IControlPanel implementation" description="The following example provides sample IControlPanel implementation for an abstract class so that other ControlPanel controls could use its functionality relying on it.">
  /// public abstract class ControlPanelBase : CompositeControl, IControlPanel
  /// {
  ///     public ControlPanelBase()
  ///     {
  ///     }
  /// 
  ///     public ControlPanelBase(string title)
  ///     {
  ///         this.tlt = title;
  ///     }
  /// 
  ///     public virtual string Title
  ///     {
  ///         get
  ///         {
  ///             return this.tlt;
  ///         }
  ///     }
  /// 
  ///     public virtual string Status
  ///     {
  ///         get
  ///         {
  ///             return this.sts;
  ///         }
  ///     }
  /// 
  ///     public virtual ICommandPanel[] CommandPanels
  ///     {
  ///         get
  ///         {
  ///             if (commandPnls == null)
  ///                 commandPnls = new ICommandPanel[0];
  ///             return commandPnls;
  ///         }
  ///     }
  /// 
  ///     public virtual void Refresh()
  ///     {
  ///         base.RecreateChildControls();
  ///     }
  /// 
  ///     protected string tlt;
  ///     protected string sts;
  ///     protected ICommandPanel[] commandPnls;
  /// }
  ///     </code>
  /// </example>
  /// <remarks>
  /// The IControlPanel interface should be implemented by ControlPanel controls which
  /// are used to provide the administration interaction for a Sitefinity module.
  /// </remarks>
  public interface IControlPanel
  {
    /// <remarks>Most often it provide information for the current control panel status.</remarks>
    /// <summary>ControlPanel title used to display guidance information.</summary>
    /// <value>String value representing the ControlPanel title.</value>
    /// <example>
    ///     You can refer to <see cref="T:Telerik.Sitefinity.Web.UI.Backend.IControlPanel">IControlPanel</see> interface for more
    ///     complicated example implementing the whole
    ///     <see cref="T:Telerik.Sitefinity.Web.UI.Backend.IControlPanel">IControlPanel</see> interface.
    /// </example>
    string Title { get; }

    /// <summary>Status of the control panel.</summary>
    /// <example>
    ///     You can refer to <see cref="T:Telerik.Sitefinity.Web.UI.Backend.IControlPanel">IControlPanel</see> interface for more
    ///     complicated example implementing the whole
    ///     <see cref="T:Telerik.Sitefinity.Web.UI.Backend.IControlPanel">IControlPanel</see> interface.
    /// </example>
    string Status { get; }

    /// <summary>Array of all the command panels connected to the control panel.</summary>
    /// <remarks>
    /// From here any command panel connected to the control panel could be
    /// excerpt.
    /// </remarks>
    /// <example>
    ///     You can refer to <see cref="T:Telerik.Sitefinity.Web.UI.Backend.IControlPanel">IControlPanel</see> interface for more
    ///     complicated example implementing the whole
    ///     <see cref="T:Telerik.Sitefinity.Web.UI.Backend.IControlPanel">IControlPanel</see> interface.
    /// </example>
    ICollection<ICommandPanel> CommandPanels { get; }
  }
}
