// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.LayoutControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.ResponsiveDesign;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// The LayoutControl comprises the basic building block of Sitefinity layouts / templates / pages
  /// </summary>
  [RequiresLayoutTransformation]
  [RequiresEmbeddedWebResource("Telerik.Sitefinity.Resources.Themes.LayoutsBasics.css", "Telerik.Sitefinity.Resources.Reference")]
  public class LayoutControl : Control
  {
    public const string LayoutsCssIncludedCacheKey = "LayoutsCssIncluded";
    private PlaceHoldersCollection placeholders = new PlaceHoldersCollection();

    /// <summary>Gets or sets the place holder pageId.</summary>
    /// <value>The place holder pageId.</value>
    [Browsable(false)]
    public virtual string PlaceHolder
    {
      get => (string) this.ViewState[nameof (PlaceHolder)] ?? string.Empty;
      set => this.ViewState[nameof (PlaceHolder)] = (object) value;
    }

    /// <summary>Gets or sets the layout.</summary>
    /// <value>The layout.</value>
    public virtual string Layout
    {
      get => (string) this.ViewState[nameof (Layout)] ?? string.Empty;
      set => this.ViewState[nameof (Layout)] = (object) value;
    }

    /// <summary>
    /// Gets or sets a type assembly qualified name for the assembly containing the embedded layout template.
    /// </summary>
    /// <value>The assembly info.</value>
    public virtual string AssemblyInfo
    {
      get => (string) this.ViewState[nameof (AssemblyInfo)] ?? string.Empty;
      set => this.ViewState[nameof (AssemblyInfo)] = (object) value;
    }

    /// <summary>Gets the placeholders (Columns).</summary>
    /// <value>The placeholders.</value>
    [Browsable(false)]
    public virtual PlaceHoldersCollection Placeholders
    {
      get
      {
        this.EnsureChildControls();
        return this.placeholders;
      }
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.Init" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnInit(EventArgs e)
    {
      base.OnInit(e);
      this.RegisterDynamicResources();
      SystemManager.CurrentHttpContext.Items[(object) "LayoutsCssIncluded"] = (object) true;
    }

    private void RegisterDynamicResources()
    {
      foreach (RequiresEmbeddedWebResourceAttribute resourceAttribute in (IEnumerable<RequiresEmbeddedWebResourceAttribute>) this.GetRequiredEmbeddedWebResourceAttributes())
        ResourceLinks.ContextDynamicWebResources.Add(resourceAttribute);
    }

    /// <summary>
    /// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
    /// </summary>
    protected override void CreateChildControls()
    {
      try
      {
        this.Initialize();
      }
      catch (Exception ex)
      {
        this.ProcessInitializationException(ex);
      }
    }

    private void Initialize()
    {
      if (!string.IsNullOrEmpty(this.Layout))
      {
        ITemplate template = this.GetTemplate();
        if (template is IContentPlaceHolderContainer placeHolderContainer)
        {
          placeHolderContainer.InstantiateIn((Control) this, this.placeholders);
        }
        else
        {
          template.InstantiateIn((Control) this);
          foreach (Control control in new ControlTraverser((Control) this, TraverseMethod.BreadthFirst))
          {
            if (control is HtmlGenericControl htmlGenericControl && htmlGenericControl.Attributes["class"].Contains("sf_colsIn"))
              this.placeholders.Add((Control) htmlGenericControl);
          }
        }
        for (int index = 0; index < this.placeholders.Count; ++index)
        {
          Control placeholder = this.placeholders[index];
          string str = placeholder.ID;
          if (string.IsNullOrEmpty(str))
            str = "Col" + index.ToString("00");
          placeholder.ID = this.ID + "_" + str;
        }
      }
      else
        this.placeholders.Add(this.Parent);
    }

    /// <summary>
    /// Gets the template based on the Layout property that will be instantiated inside the control.
    /// </summary>
    protected virtual ITemplate GetTemplate()
    {
      string layout = this.Layout;
      ITemplate template;
      if (layout.StartsWith("~/"))
        template = ControlUtilities.GetTemplate(layout, (string) null, (Type) null, (string) null);
      else if (layout.EndsWith(".ascx", StringComparison.OrdinalIgnoreCase))
      {
        Type assemblyInfo = !string.IsNullOrEmpty(this.AssemblyInfo) ? TypeResolutionService.ResolveType(this.AssemblyInfo, true) : Config.Get<ControlsConfig>().ResourcesAssemblyInfo;
        template = ControlUtilities.GetTemplate((string) null, layout, assemblyInfo, (string) null);
      }
      else
        template = ControlUtilities.GetTemplate((string) null, layout.GetHashCode().ToString(), (Type) null, layout);
      return template;
    }

    /// <summary>
    /// Displays an error message when an exception is thrown during the rendering of the control.
    /// </summary>
    /// <param name="ex">The thrown exception.</param>
    private void ProcessInitializationException(Exception ex)
    {
      if (this.IsBackend() || SiteMapBase.GetActualCurrentNode() == null)
        this.Controls.Add((Control) new Label()
        {
          Text = ex.Message
        });
      Telerik.Sitefinity.Abstractions.Log.Write((object) ex, TraceEventType.Error);
      Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions);
    }
  }
}
