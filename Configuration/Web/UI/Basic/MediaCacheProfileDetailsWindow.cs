// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.Web.UI.Basic.MediaCacheProfileDetailsWindow
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration.Web.ViewModels;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Configuration.Web.UI.Basic
{
  /// <summary>
  /// A control managing a client window for creating and updating media cache profiles.
  /// </summary>
  /// <seealso cref="T:Telerik.Sitefinity.Web.UI.SimpleScriptView" />
  public class MediaCacheProfileDetailsWindow : CacheProfileDetailsWindow
  {
    private static readonly string LayoutTemplateVirtualPath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Configuration.Basic.MediaCacheProfileDetailsWindow.ascx");

    /// <summary>Gets the layout template path.</summary>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? MediaCacheProfileDetailsWindow.LayoutTemplateVirtualPath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets the new profile.</summary>
    /// <value>The new profile.</value>
    internal override CacheProfileViewModel NewProfile => new CacheProfileViewModel(new OutputCacheProfileElement((ConfigElement) Config.Get<SystemConfig>().CacheSettings.MediaCacheProfiles), CacheProfileViewModel.MediaCacheItemTypes);

    /// <summary>Gets the HTML control containing the window body.</summary>
    /// <value>The window body.</value>
    protected override HtmlGenericControl WindowBody => this.Container.GetControl<HtmlGenericControl>("windowBody", true);

    /// <summary>Gets the done button.</summary>
    /// <value>The done button.</value>
    protected override HtmlControl DoneButton => this.Container.GetControl<HtmlControl>("doneButton", true);

    /// <summary>Gets the cancel button.</summary>
    /// <value>The cancel button.</value>
    protected override HtmlControl CancelButton => this.Container.GetControl<HtmlControl>("cancelButton", true);

    /// <summary>Gets the location drop down.</summary>
    /// <value>The location drop down.</value>
    protected override DropDownList LocationDropDown => this.Container.GetControl<DropDownList>("cacheLocationDropDown", true);

    /// <summary>Gets the cache profiles settings service URL.</summary>
    /// <value>The service URL.</value>
    protected override string ServiceUrl => this.ResolveClientUrl("~/Sitefinity/Services/CacheProfiles/Settings.svc/media");
  }
}
