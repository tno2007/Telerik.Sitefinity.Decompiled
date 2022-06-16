// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Web.UI.CodeReference
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.DynamicModules.Builder.Data;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.DynamicModules.Builder.Web.UI.CodeReferences;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.DynamicModules.Builder.Web.UI
{
  public class CodeReference : ModuleEditorBase
  {
    private string articleKey;
    private static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.ModuleBuilder.CodeReference.ascx");
    private const string createItemKey = "create-item";
    private const string deleteItemKey = "delete-item";
    private const string getItemByIDKey = "get-item-by-id";
    private const string getCollectionOfItemsKey = "get-collection-of-items";
    private const string getChildItems = "get-child-items";
    private const string getItemThroughFilteringKey = "get-item-through-filtering";
    private const string publishItem = "publish-item";
    private const string checkInCheckOut = "check-in-check-out";
    private const string integrationExample = "integration-example";
    private const string manageRelatedItems = "manage-related-items";
    private const string createItemKeyJS = "create-item-js";
    private const string deleteItemKeyJS = "delete-item-js";
    private const string getItemByIDKeyJS = "get-item-by-id-js";
    private const string getCollectionOfItemsKeyJS = "get-collection-of-items-js";
    private const string getCollectionOfFilteredItemsKeyJS = "get-collection-of-filtered-items-js";
    private const string getSortedAscItemsKeyJS = "get-sorted-asc-items-js";
    private const string getSortedDescItemsKeyJS = "get-sorted-a-items-js";
    internal const string shCoreScriptRef = "Telerik.Sitefinity.DynamicModules.Builder.Web.Scripts.shCore.js";
    internal const string shBrushCSharp = "Telerik.Sitefinity.DynamicModules.Builder.Web.Scripts.shBrushCSharp.js";
    internal const string shBrushVb = "Telerik.Sitefinity.DynamicModules.Builder.Web.Scripts.shBrushVb.js";
    internal const string shBrushJScript = "Telerik.Sitefinity.DynamicModules.Builder.Web.Scripts.shBrushJScript.js";
    internal const string xregexp = "Telerik.Sitefinity.DynamicModules.Builder.Web.Scripts.xregexp-min.js";
    internal const string scriptRef = "Telerik.Sitefinity.DynamicModules.Builder.Web.Scripts.CodeReference.js";

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    public override string LayoutTemplatePath
    {
      get
      {
        if (string.IsNullOrEmpty(base.LayoutTemplatePath))
          base.LayoutTemplatePath = CodeReference.layoutTemplatePath;
        return base.LayoutTemplatePath;
      }
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets the article key parameter.</summary>
    protected virtual string ArticleKey
    {
      get
      {
        if (string.IsNullOrEmpty(this.articleKey) && ControlExtensions.GetUrlParameters(this.Page).Length > 2)
          this.articleKey = ControlExtensions.GetUrlParameters(this.Page)[2];
        return this.articleKey;
      }
    }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.</returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets the instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Web.UI.IndefiniteArticleResolver" />.
    /// </summary>
    internal virtual IndefiniteArticleResolver IndefiniteArticleNameResolver { get; private set; }

    /// <summary>
    /// Gets the instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Web.UI.PluralsResolver" />.
    /// </summary>
    internal virtual PluralsResolver PluralsNameResolver { get; private set; }

    /// <summary>
    /// Gets the reference to the labelServerSideHeader control.
    /// </summary>
    protected virtual Label LabelServerSideHeader => this.Container.GetControl<Label>("labelServerSideHeader", true);

    /// <summary>Gets the reference to the create_item control.</summary>
    protected virtual HyperLink CreateItem => this.Container.GetControl<HyperLink>("create_item", true);

    /// <summary>Gets the reference to the delete_item control.</summary>
    protected virtual HyperLink DeleteItem => this.Container.GetControl<HyperLink>("delete_item", true);

    /// <summary>Gets the reference to the get_item_by_id control.</summary>
    protected virtual HyperLink GetItemByID => this.Container.GetControl<HyperLink>("get_item_by_id", true);

    /// <summary>
    /// Gets the reference to the get_collection_of_items control.
    /// </summary>
    protected virtual HyperLink GetCollectionOfItems => this.Container.GetControl<HyperLink>("get_collection_of_items", true);

    /// <summary>
    /// Gets the reference to the get_collection_of_child_items control.
    /// </summary>
    protected virtual HyperLink GetChildItems => this.Container.GetControl<HyperLink>("get_child_items", true);

    /// <summary>Gets the reference to the publish_item control.</summary>
    protected virtual HyperLink PublishItem => this.Container.GetControl<HyperLink>("publish_item", true);

    /// <summary>Gets the reference to the check_in_check_out control.</summary>
    protected virtual HyperLink CheckInCheckOut => this.Container.GetControl<HyperLink>("check_in_check_out", true);

    /// <summary>
    /// Gets the reference to the integration_example control.
    /// </summary>
    protected virtual HyperLink IntegrationExample => this.Container.GetControl<HyperLink>("integration_example", true);

    /// <summary>Gets the reference to the get_related_items control.</summary>
    protected virtual HyperLink ManageRelatedItems => this.Container.GetControl<HyperLink>("manage_related_items", true);

    /// <summary>
    /// Gets the reference to the get_item_through_filtering control.
    /// </summary>
    protected virtual HyperLink GetItemThroughFiltering => this.Container.GetControl<HyperLink>("get_item_through_filtering", true);

    /// <summary>Gets the reference to the back_to_dashboard control.</summary>
    protected virtual HyperLink GoBackToDashboardLink => this.Container.GetControl<HyperLink>("back_to_dashboard", true);

    /// <summary>Gets the reference to the contentTypeLiteral control.</summary>
    protected virtual Literal ContentTypeLiteral => this.Container.GetControl<Literal>("contentTypeLiteral", true);

    /// <summary>
    /// Gets the reference to the codeReferenceTitleLiteral control.
    /// </summary>
    protected virtual Literal CodeReferenceTitleLiteral => this.Container.GetControl<Literal>("codeReferenceTitleLiteral", true);

    /// <summary>Gets the reference to the article placeholder.</summary>
    protected virtual PlaceHolder ArticlePlaceholder => this.Container.GetControl<PlaceHolder>("articlePlaceholder", true);

    /// <summary>
    /// Gets the reference to the contentTypesRepeater control.
    /// </summary>
    protected virtual Repeater ContentTypesRepeater => this.Container.GetControl<Repeater>("contentTypesRepeater", true);

    /// <summary>Initializes the controls.</summary>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      base.InitializeControls(container);
      this.IndefiniteArticleNameResolver = new IndefiniteArticleResolver();
      this.PluralsNameResolver = PluralsResolver.Instance;
      if (!this.InitializeURLDependantVariables())
        throw new HttpException(404, "The requested page does not exist.");
      this.GoBackToDashboardLink.Text = "Back";
      this.GoBackToDashboardLink.NavigateUrl = this.GetParentUrl();
      this.ModuleBuilderMngr.LoadDynamicModuleGraph(this.Module);
      this.InitializeContentTypes();
      this.IndefiniteArticleNameResolver.ResolveModuleTypeName((IDynamicModuleType) this.ModuleType);
      this.InitializeHyperlinks(this.ModuleType.DisplayName);
      if (!string.IsNullOrEmpty(this.ArticleKey))
      {
        Control child1 = (Control) null;
        switch (this.ArticleKey)
        {
          case "check-in-check-out":
            child1 = (Control) new Telerik.Sitefinity.DynamicModules.Builder.Web.UI.CodeReferences.CheckInCheckOut();
            this.CheckInCheckOut.CssClass = "sfSel";
            break;
          case "create-item":
            child1 = (Control) new CreateItemArticle();
            this.CreateItem.CssClass = "sfSel";
            break;
          case "delete-item":
            child1 = (Control) new DeleteItemArticle();
            this.DeleteItem.CssClass = "sfSel";
            break;
          case "get-child-items":
            if (this.ModuleBuilderMngr.HasContentTypeChildren(this.Module, this.ModuleType))
            {
              child1 = (Control) new GetChildItemsArticle();
              this.GetChildItems.CssClass = "sfSel";
              break;
            }
            break;
          case "get-collection-of-items":
            child1 = (Control) new GetCollectionOfItemsArticle();
            this.GetCollectionOfItems.CssClass = "sfSel";
            break;
          case "get-item-by-id":
            child1 = (Control) new GetItemByIDArticle();
            this.GetItemByID.CssClass = "sfSel";
            break;
          case "get-item-through-filtering":
            child1 = (Control) new GetItemThroughFilteringArticle();
            this.GetItemThroughFiltering.CssClass = "sfSel";
            break;
          case "integration-example":
            child1 = (Control) new Telerik.Sitefinity.DynamicModules.Builder.Web.UI.CodeReferences.IntegrationExample();
            this.IntegrationExample.CssClass = "sfSel";
            break;
          case "manage-related-items":
            if (((IEnumerable<DynamicModuleField>) this.ModuleType.Fields).Any<DynamicModuleField>((Func<DynamicModuleField, bool>) (f => f.FieldType == FieldType.RelatedData || f.FieldType == FieldType.RelatedMedia)))
            {
              child1 = (Control) new ManageRelatedItemsArticle();
              this.ManageRelatedItems.CssClass = "sfSel";
              break;
            }
            break;
          case "publish-item":
            child1 = (Control) new PublishItemArticle();
            this.PublishItem.CssClass = "sfSel";
            break;
          default:
            child1 = (Control) new CodeReferenceErrorArticle("Code reference article not found", "Error has occured while trying to display code reference with the specified key. Please try again!");
            break;
        }
        this.ArticlePlaceholder.Controls.Clear();
        if (child1 != null)
        {
          if (child1 is CodeArticleBase child2)
          {
            child2.Module = this.Module;
            child2.ModuleType = this.ModuleType;
            this.ArticlePlaceholder.Controls.Add((Control) child2);
          }
          else
            this.ArticlePlaceholder.Controls.Add((Control) (child1 as CodeReferenceErrorArticle));
        }
        else
          this.ArticlePlaceholder.Controls.Add((Control) new CodeReferenceDefaultArticle(string.Format("Welcome to Code-reference for {0}", (object) this.ModuleType.DisplayName), "Please select article from the menu bar."));
      }
      else
        this.ArticlePlaceholder.Controls.Add((Control) new CodeReferenceDefaultArticle(string.Format("Welcome to Code-reference for {0}", (object) this.ModuleType.DisplayName), "Please select article from the menu bar."));
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects
    /// that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" />
    /// objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.DynamicModules.Builder.Web.Scripts.xregexp-min.js", typeof (CodeReference).Assembly.FullName),
      new ScriptReference("Telerik.Sitefinity.DynamicModules.Builder.Web.Scripts.shCore.js", typeof (CodeReference).Assembly.FullName),
      new ScriptReference("Telerik.Sitefinity.DynamicModules.Builder.Web.Scripts.shBrushCSharp.js", typeof (CodeReference).Assembly.FullName),
      new ScriptReference("Telerik.Sitefinity.DynamicModules.Builder.Web.Scripts.shBrushVb.js", typeof (CodeReference).Assembly.FullName),
      new ScriptReference("Telerik.Sitefinity.DynamicModules.Builder.Web.Scripts.shBrushJScript.js", typeof (CodeReference).Assembly.FullName),
      new ScriptReference("Telerik.Sitefinity.DynamicModules.Builder.Web.Scripts.CodeReference.js", typeof (CodeReference).Assembly.FullName)
    };

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(typeof (CodeReference).FullName, this.ClientID);
      controlDescriptor.AddProperty("moduleTypeId", (object) this.ModuleTypeId.ToString());
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">
    /// An <see cref="T:System.EventArgs" /> object that contains the event data.
    /// </param>
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      this.ContentTypesRepeater.DataBind();
    }

    private void InitializeHyperlinks(string moduleTypeDisplayName)
    {
      this.LabelServerSideHeader.Text = "Server side (C#, VB)";
      this.CreateItem.Text = string.Format("Create {0} {1}", (object) this.IndefiniteArticleNameResolver.Prefix, (object) moduleTypeDisplayName);
      this.CreateItem.NavigateUrl = this.GetArticleUrl("create-item");
      this.DeleteItem.Text = string.Format("Delete {0} {1}", (object) this.IndefiniteArticleNameResolver.Prefix, (object) moduleTypeDisplayName);
      this.DeleteItem.NavigateUrl = this.GetArticleUrl("delete-item");
      this.GetItemByID.Text = string.Format("Get {0} {1} by ID", (object) this.IndefiniteArticleNameResolver.Prefix, (object) moduleTypeDisplayName);
      this.GetItemByID.NavigateUrl = this.GetArticleUrl("get-item-by-id");
      this.GetCollectionOfItems.Text = string.Format("Get a collection of {0}", (object) this.PluralsNameResolver.ToPlural(moduleTypeDisplayName));
      this.GetCollectionOfItems.NavigateUrl = this.GetArticleUrl("get-collection-of-items");
      this.GetItemThroughFiltering.Text = string.Format("Get {0} {1} through Filtering", (object) this.IndefiniteArticleNameResolver.Prefix, (object) moduleTypeDisplayName);
      this.GetItemThroughFiltering.NavigateUrl = this.GetArticleUrl("get-item-through-filtering");
      if (this.ModuleBuilderMngr.HasContentTypeChildren(this.Module, this.ModuleType))
      {
        this.GetChildItems.Text = string.Format("Get child items of {0} {1}", (object) this.IndefiniteArticleNameResolver.Prefix, (object) moduleTypeDisplayName);
        this.GetChildItems.NavigateUrl = this.GetArticleUrl("get-child-items");
      }
      else
        this.GetChildItems.Visible = false;
      this.PublishItem.Text = string.Format("Publish {0} {1}", (object) this.IndefiniteArticleNameResolver.Prefix, (object) moduleTypeDisplayName);
      this.PublishItem.NavigateUrl = this.GetArticleUrl("publish-item");
      this.CheckInCheckOut.Text = string.Format("Check in and Check out {0} {1}", (object) this.IndefiniteArticleNameResolver.Prefix, (object) moduleTypeDisplayName);
      this.CheckInCheckOut.NavigateUrl = this.GetArticleUrl("check-in-check-out");
      this.IntegrationExample.Text = string.Format("Integration example for {0} {1}", (object) this.IndefiniteArticleNameResolver.Prefix, (object) moduleTypeDisplayName);
      this.IntegrationExample.NavigateUrl = this.GetArticleUrl("integration-example");
      if (((IEnumerable<DynamicModuleField>) this.ModuleType.Fields).Any<DynamicModuleField>((Func<DynamicModuleField, bool>) (f => f.FieldType == FieldType.RelatedData || f.FieldType == FieldType.RelatedMedia)))
      {
        this.ManageRelatedItems.Text = string.Format("Manage related items of {0} {1}", (object) this.IndefiniteArticleNameResolver.Prefix, (object) moduleTypeDisplayName);
        this.ManageRelatedItems.NavigateUrl = this.GetArticleUrl("manage-related-items");
      }
      else
        this.ManageRelatedItems.Visible = false;
    }

    private string GetParentUrl()
    {
      SiteMapNode siteMapNode = BackendSiteMap.FindSiteMapNode(ModuleBuilderModule.contentTypeDashboardPageId, false);
      return siteMapNode == null ? string.Empty : RouteHelper.ResolveUrl(siteMapNode.Url, UrlResolveOptions.Rooted | UrlResolveOptions.RemoveTrailingSlash) + "/" + (object) this.ModuleId;
    }

    private string GetArticleUrl(string articleKey)
    {
      SiteMapNode siteMapNode = BackendSiteMap.FindSiteMapNode(ModuleBuilderModule.contentTypeCodeReferencePageId, false);
      if (siteMapNode == null)
        return string.Empty;
      return RouteHelper.ResolveUrl(siteMapNode.Url, UrlResolveOptions.Rooted | UrlResolveOptions.RemoveTrailingSlash) + "/" + (object) this.Module.Id + "/" + (object) this.ModuleType.Id + "/" + articleKey;
    }

    private void InitializeContentTypes()
    {
      IEnumerable<ContentTypeTreeItemContext> typeTreeItemContexts = CodeReference.BuildContentTypeTreeHierarchy((IEnumerable<ContentTypeTreeItemContext>) ((IEnumerable<DynamicModuleType>) this.Module.Types).Select<DynamicModuleType, ContentTypeTreeItemContext>((Func<DynamicModuleType, ContentTypeTreeItemContext>) (t => new ContentTypeTreeItemContext()
      {
        ContentTypeId = t.Id,
        Text = t.DisplayName,
        ParentContentTypeId = t.ParentModuleTypeId
      })).ToList<ContentTypeTreeItemContext>());
      this.ContentTypesRepeater.ItemDataBound += new RepeaterItemEventHandler(this.ContentTypesRepeater_ItemDataBound);
      this.ContentTypesRepeater.DataSource = (object) typeTreeItemContexts;
    }

    private void ContentTypesRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
      if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
        return;
      ContentTypeTreeItemContext dataItem = e.Item.DataItem as ContentTypeTreeItemContext;
      if (dataItem.Items == null || dataItem.Items.Length == 0)
        return;
      PlaceHolder control = e.Item.FindControl("childrenPlaceholder") as PlaceHolder;
      Repeater child = this.CloneRepeater(sender as Repeater);
      child.DataSource = (object) dataItem.Items;
      control.Controls.Add((Control) child);
      child.DataBind();
    }

    private static IEnumerable<ContentTypeTreeItemContext> BuildContentTypeTreeHierarchy(
      IEnumerable<ContentTypeTreeItemContext> contentTypeTreeItems)
    {
      foreach (ContentTypeTreeItemContext contentTypeTreeItem1 in contentTypeTreeItems)
      {
        ContentTypeTreeItemContext contentTypeTreeItem = contentTypeTreeItem1;
        contentTypeTreeItem.Items = contentTypeTreeItems.Where<ContentTypeTreeItemContext>((Func<ContentTypeTreeItemContext, bool>) (ct => ct.ParentContentTypeId == contentTypeTreeItem.ContentTypeId)).ToArray<ContentTypeTreeItemContext>();
      }
      return contentTypeTreeItems.Where<ContentTypeTreeItemContext>((Func<ContentTypeTreeItemContext, bool>) (ct =>
      {
        Guid parentContentTypeId = ct.ParentContentTypeId;
        return Guid.Empty.Equals(ct.ParentContentTypeId);
      }));
    }

    private Repeater CloneRepeater(Repeater source)
    {
      Repeater repeater = new Repeater();
      repeater.HeaderTemplate = source.HeaderTemplate;
      repeater.ItemTemplate = source.ItemTemplate;
      repeater.FooterTemplate = source.FooterTemplate;
      repeater.ItemDataBound += new RepeaterItemEventHandler(this.ContentTypesRepeater_ItemDataBound);
      return repeater;
    }
  }
}
