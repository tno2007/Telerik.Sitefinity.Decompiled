// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.ICommandPanel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Web.UI.Backend
{
  /// <summary>Defines members for command panel implementation.</summary>
  /// <remarks>
  /// CommandPanels are used for Modules. They are connected to control panel and can
  /// not exist without it. Most often they are used as a left side navigation for the
  /// modules admin part.
  /// </remarks>
  /// <example>
  /// 	<code lang="CS" title="ICommandPanel implementation" description="The following example provide implementation for the ICommandPanel interface. It provides two constructors which assigns the CommandPanel control's ControlPanel member.">
  /// public abstract class CommandPanelBase : CompositeControl, ICommandPanel
  /// {
  ///     public CommandPanelBase(IControlPanel controlPanel)
  ///     {
  ///         this.controlPanel = controlPanel;
  ///     }
  /// 
  ///     public CommandPanelBase(IControlPanel controlPanel, string name)
  ///         : this(controlPanel)
  ///     {
  ///         this.name = name;
  ///     }
  /// 
  ///     public virtual string Name
  ///     {
  ///         get
  ///         {
  ///             return this.name;
  ///         }
  ///     }
  /// 
  ///     public virtual string Title
  ///     {
  ///         get
  ///         {
  ///             return this.titleImpl;
  ///         }
  ///     }
  /// 
  ///     public virtual IControlPanel ControlPanel
  ///     {
  ///         get
  ///         {
  ///             return this.controlPanel;
  ///         }
  ///     }
  /// 
  ///     public virtual void Refresh()
  ///     {
  ///         base.RecreateChildControls();
  ///     }
  /// 
  ///     private string name;
  ///     protected string titleImpl;
  ///     private IControlPanel controlPanel;
  /// }
  ///     </code>
  /// </example>
  public interface ICommandPanel
  {
    /// <summary>Name for the command panel.</summary>
    /// <value>Command panel name.</value>
    /// <remarks>
    /// Could be used as command panel identifier if there are more than one command
    /// panels for a module.
    /// </remarks>
    /// <example>
    ///     You can refer to <see cref="T:Telerik.Sitefinity.Web.UI.Backend.ICommandPanel">ICommandPanel</see> interface for more
    ///     complicated example implementing the whole
    ///     <see cref="T:Telerik.Sitefinity.Web.UI.Backend.ICommandPanel">ICommandPanel</see> interface.
    /// </example>
    string Name { get; }

    /// <summary>Title of the command panel.</summary>
    /// <example>
    ///     You can refer to <see cref="T:Telerik.Sitefinity.Web.UI.Backend.ICommandPanel">ICommandPanel</see> interface for more
    ///     complicated example implementing the whole
    ///     <see cref="T:Telerik.Sitefinity.Web.UI.Backend.ICommandPanel">ICommandPanel</see> interface.
    /// </example>
    string Title { get; }

    /// <summary>Reference to the control panel tied to the command panel instance.</summary>
    /// <remarks>
    /// This property is used for communication between the command panel and its control
    /// panel.
    /// </remarks>
    /// <example>
    ///     You can refer to <see cref="T:Telerik.Sitefinity.Web.UI.Backend.ICommandPanel">ICommandPanel</see> interface for more
    ///     complicated example implementing the whole
    ///     <see cref="T:Telerik.Sitefinity.Web.UI.Backend.ICommandPanel">ICommandPanel</see> interface.
    /// </example>
    IControlPanel ControlPanel { get; set; }
  }
}
