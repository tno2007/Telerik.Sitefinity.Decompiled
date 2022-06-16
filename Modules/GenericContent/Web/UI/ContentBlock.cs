// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentBlock
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.Web.UI;
using Telerik.Sitefinity.ContentLocations;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Personalization;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.SitefinityExceptions;
using Telerik.Sitefinity.Versioning.Serialization.Attributes;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.PublicControls;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.GenericContent.Web.UI
{
  /// <summary>
  /// Control for adding arbitrary blocks of text to the pages
  /// </summary>
  [ControlDesigner(typeof (ContentBlockDesigner))]
  public class ContentBlock : 
    ContentBlockBase,
    ICustomWidgetTitlebar,
    IZoneEditorReloader,
    IHasCacheDependency,
    IHasContainerType,
    ISearchIndexBehavior,
    IContentItemControl,
    IPersonalizable
  {
    private ContentManager contentManager;
    private bool isControlDefinitionProviderCorrect = true;

    /// <summary>
    /// Gets or sets a value indicating which content filters will be active when the
    /// Content Block widget is used. This poperty overrides AppearanceConfig settings.
    /// </summary>
    public EditorFilters? ContentEditorFilters { get; set; }

    /// <summary>
    /// Gets or sets the ID of the ContentBlockItem if the HTML is shared across multiple controls
    /// </summary>
    public Guid SharedContentID { get; set; }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets or sets the name of the provider.</summary>
    /// <value>The name of the provider.</value>
    public string ProviderName { get; set; }

    /// <summary>Gets or sets the current content version.</summary>
    /// <value>The version.</value>
    public int ContentVersion { get; set; }

    /// <summary>
    /// Gets or sets the HTML editor strip formatting options.
    /// </summary>
    /// <value>The HTML editor strip formatting options.</value>
    public string HtmlEditorStripFormattingOptions { get; set; }

    /// <summary>Gets or sets the page id of the current node.</summary>
    /// <value>The page id.</value>
    [Obsolete("This property is no longer in use.")]
    public Guid PageId { get; set; }

    /// <inheritdoc />
    Type IHasContainerType.ContainerType { get; set; }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      if (this.IsShared)
      {
        string str = string.Empty;
        try
        {
          ContentItem lifecycleItem = this.ContentManager.GetContent(this.SharedContentID);
          object resultItem;
          if (ContentLocatableViewExtensions.TryGetItemWithRequestedStatus((ILifecycleDataItem) lifecycleItem, (ILifecycleManager) this.ContentManager, out resultItem))
            lifecycleItem = resultItem as ContentItem;
          this.Html = (string) lifecycleItem.Content;
        }
        catch (UnauthorizedAccessException ex)
        {
          str = Res.Get<ContentResources>().NoViewPermissionsMessage;
        }
        catch (ItemNotFoundException ex)
        {
          str = ex.Message;
        }
        if (!string.IsNullOrEmpty(str))
        {
          if (!this.IsDesignMode() || this.IsPreviewMode())
            return;
          this.Html = str;
        }
        this.SubscribeCacheDependency();
      }
      base.InitializeControls(container);
      this.IsEmpty = this.SharedContentID == Guid.Empty && string.IsNullOrEmpty(this.ContentHtml.Text);
    }

    /// <inheritdoc />
    public override void RenderBeginTag(HtmlTextWriter writer)
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (this.HasWrappingTag && !this.WrapperTag.IsNullOrWhitespace())
      {
        stringBuilder.Append("<").Append(this.WrapperTag);
        if (!string.IsNullOrEmpty(this.CssClass))
          stringBuilder.Append(" class='" + this.CssClass + "'");
        if (this.IsInlineEditingMode())
        {
          if (this.IsShared)
            stringBuilder.Append(" data-sf-provider='").Append(this.ProviderName).Append("' data-sf-id='").Append(this.SharedContentID.ToString()).Append("' data-sf-type='").Append(typeof (ContentItem).FullName).Append("' data-sf-field='Content' data-sf-ftype='LongText'").Append("data-sf-warning-message='").Append(Res.Get<PublicControlsResources>().SharedContentBlockWarning).Append("'");
          else if (this.IsEditable())
            stringBuilder.Append(" data-sf-provider='").Append(this.ProviderName).Append("' data-sf-type='").Append(typeof (PageDraftControl).FullName).Append("' data-sf-field='Html' data-sf-ftype='LongText'");
        }
        stringBuilder.Append(">");
      }
      writer.Write(stringBuilder.ToString());
    }

    protected override void Render(HtmlTextWriter writer)
    {
      if (this.GetIndexRenderMode() != IndexRenderModes.Normal)
        return;
      base.Render(writer);
    }

    /// <summary>
    /// Gets a list with custom messages which will be applied to dock titlebar.
    /// </summary>
    /// <value>The custom messages.</value>
    [Browsable(false)]
    public virtual IList<string> CustomMessages
    {
      get
      {
        List<string> customMessages = new List<string>();
        if (this.IsShared)
          customMessages.Add(Res.Get<ContentResources>().Shared);
        return (IList<string>) customMessages;
      }
    }

    /// <summary>
    /// Get-only : returns true if the content of the control is shared or not
    /// </summary>
    [ReadOnly(true)]
    [NonSerializableProperty]
    public bool IsShared
    {
      get
      {
        this.InitializeManager();
        return this.SharedContentID != Guid.Empty && this.isControlDefinitionProviderCorrect;
      }
      set
      {
      }
    }

    /// <summary>
    /// Defines whether controls of same key will be
    /// reloaded when this control is updated in the ZoneEditor
    /// </summary>
    /// <returns>True if a reload is required</returns>
    [Browsable(false)]
    bool IZoneEditorReloader.ShouldReloadControlsWithSameKey() => this.SharedContentID != Guid.Empty;

    /// <summary>
    /// Gets unique reload data (i.e. all controls with the same key will get reloaded)
    /// </summary>
    /// <value></value>
    [Browsable(false)]
    string IZoneEditorReloader.Key => "ContentBlock_" + this.SharedContentID.ToString("N", (IFormatProvider) CultureInfo.InvariantCulture);

    public IList<CacheDependencyKey> GetCacheDependencyObjects()
    {
      List<CacheDependencyKey> dependencyObjects = new List<CacheDependencyKey>();
      if (this.IsShared)
        dependencyObjects.AddRange(OutputCacheDependencyHelper.GetPublishedContentCacheDependencyKeys(typeof (ContentItem), this.SharedContentID));
      return (IList<CacheDependencyKey>) dependencyObjects;
    }

    public bool ExcludeFromSearchIndex { get; set; }

    private ContentManager ContentManager
    {
      get
      {
        if (this.contentManager == null)
          this.contentManager = this.InitializeManager();
        return this.contentManager;
      }
    }

    private ContentManager InitializeManager()
    {
      if (this.contentManager != null)
        return this.contentManager;
      try
      {
        return ContentManager.GetManager(this.ProviderName);
      }
      catch (ConfigurationErrorsException ex)
      {
        this.isControlDefinitionProviderCorrect = false;
        return (ContentManager) null;
      }
    }

    /// <summary>Subscribes the cache dependency.</summary>
    private void SubscribeCacheDependency()
    {
      if (this.IsBackend())
        return;
      IList<CacheDependencyKey> dependencyObjects = this.GetCacheDependencyObjects();
      if (!SystemManager.CurrentHttpContext.Items.Contains((object) "PageDataCacheDependencyName"))
        SystemManager.CurrentHttpContext.Items.Add((object) "PageDataCacheDependencyName", (object) new List<CacheDependencyKey>());
      ((List<CacheDependencyKey>) SystemManager.CurrentHttpContext.Items[(object) "PageDataCacheDependencyName"]).AddRange((IEnumerable<CacheDependencyKey>) dependencyObjects);
    }
  }
}
