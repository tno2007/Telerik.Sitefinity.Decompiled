// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Versioning.Web.UI.Views.VersionComparisonView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Versioning.Comparison;
using Telerik.Sitefinity.Versioning.Model;
using Telerik.Sitefinity.Versioning.Web.UI.Contracts;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend;
using Telerik.Sitefinity.Web.Utilities;

namespace Telerik.Sitefinity.Versioning.Web.UI.Views
{
  public class VersionComparisonView : ViewBase
  {
    public static readonly string templatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Versioning.VersionComparisonView.ascx");
    private Guid firstVersionGuid = Guid.Empty;
    private Guid secodVersionGuid = Guid.Empty;
    private Type itemType;
    private Change firstChange;
    private Change secondChange;
    private int latestPublishedChangeNumber;

    private HtmlGenericControl FirstVersionInfo => this.Container.GetControl<HtmlGenericControl>("firstVersionInfo", true);

    private HtmlGenericControl FirstVersionModifiedBy => this.Container.GetControl<HtmlGenericControl>("firstVersionModifiedBy", true);

    private HtmlGenericControl ItemTitleHeaderControl => this.Container.GetControl<HtmlGenericControl>("itemTitleHead", true);

    private HtmlGenericControl SecondVersionInfo => this.Container.GetControl<HtmlGenericControl>("secondVersionInfo", true);

    private HtmlGenericControl SecondVersionModifiedBy => this.Container.GetControl<HtmlGenericControl>(nameof (SecondVersionModifiedBy), true);

    internal Repeater VersionComparisonResultsRepeater => this.Container.GetControl<Repeater>("versionComparisonResults", true);

    internal Repeater FullScreenPropertiesRepeater => this.Container.GetControl<Repeater>(nameof (FullScreenPropertiesRepeater), true);

    internal HtmlGenericControl FullScreenButtonContainer => this.Container.GetControl<HtmlGenericControl>("fullScreenLinkContainer", true);

    internal Change FirstChange => this.firstChange;

    internal Change SecondChange => this.secondChange;

    private Guid SecondVersionId
    {
      get
      {
        if (this.secodVersionGuid == Guid.Empty)
          this.secodVersionGuid = new Guid(this.Page.Request.QueryString["SecondItemId"].ToString());
        return this.secodVersionGuid;
      }
    }

    private Guid FirstVersionId
    {
      get
      {
        if (this.firstVersionGuid == Guid.Empty)
          this.firstVersionGuid = new Guid(this.Page.Request.QueryString["FirstItemId"].ToString());
        return this.firstVersionGuid;
      }
    }

    internal Type GetItemType => this.itemType == (Type) null ? this.Host.ControlDefinition.ContentType : this.itemType;

    private string ItemTitle => this.Page.Request.QueryString[nameof (ItemTitle)].ToString();

    internal bool IsEditable => this.Page.Request.QueryString[nameof (IsEditable)].ToString().ToLower().Trim() == "true";

    private string UICulture => this.Page.Request.QueryString[nameof (UICulture)].ToString();

    internal HiddenField HfContentServiceUrl => this.Container.GetControl<HiddenField>("hfHistoryWebService", true);

    internal HiddenField HIsEditable => this.Container.GetControl<HiddenField>("hIsEditable", true);

    private void BindData()
    {
      this.BindVersionInfos();
      this.BindVersionCopmarerInfo();
    }

    private string GetVersionStatus(Change change)
    {
      if (!change.IsPublishedVersion)
        return Res.Get<VersionResources>().Draft;
      return change.Version >= this.latestPublishedChangeNumber ? Res.Get<VersionResources>().LastPublished : Res.Get<VersionResources>().PreviouslyPublished;
    }

    private void BindVersionInfos()
    {
      string versionInfoTemplate = Res.Get<VersionResources>().VersionInfoTemplate;
      string modifiedByTemplate = Res.Get<VersionResources>().ModifiedByTemplate;
      Guid owner1 = this.FirstChange.Owner;
      Guid owner2 = this.SecondChange.Owner;
      this.FirstVersionInfo.InnerHtml = string.Format(versionInfoTemplate, (object) VersionDataProvider.BuildUIVersionNumber(this.FirstChange.Version), (object) this.GetVersionStatus(this.FirstChange));
      string userDisplayName1 = UserProfilesHelper.GetUserDisplayName(owner1);
      this.FirstVersionModifiedBy.InnerHtml = string.Format(modifiedByTemplate, (object) userDisplayName1, (object) this.FirstChange.LastModified.ToString("dd MMM, yyyy"));
      this.SecondVersionInfo.InnerHtml = string.Format(versionInfoTemplate, (object) VersionDataProvider.BuildUIVersionNumber(this.SecondChange.Version), (object) this.GetVersionStatus(this.SecondChange));
      string userDisplayName2 = UserProfilesHelper.GetUserDisplayName(owner2);
      this.SecondVersionModifiedBy.InnerHtml = string.Format(modifiedByTemplate, (object) userDisplayName2, (object) this.SecondChange.LastModified.ToString("dd MMM, yyyy"));
      this.ItemTitleHeaderControl.InnerHtml = this.ItemTitle;
      this.ItemTitleHeaderControl.Attributes.Add("title", this.ItemTitle);
    }

    internal virtual void BindVersionCopmarerInfo()
    {
      IManager mappedManager = ManagerBase.GetMappedManager(this.GetItemType);
      VersionManager manager = VersionManager.GetManager();
      object obj1 = mappedManager.CreateItem(this.GetItemType);
      object obj2 = mappedManager.CreateItem(this.GetItemType);
      manager.GetSpecificVersionByChangeId(obj1, this.FirstChange.Id);
      manager.GetSpecificVersionByChangeId(obj2, this.SecondChange.Id);
      ContentComparator contentComparator = new ContentComparator((this.Definition as IComparisonViewDefinition).Fields);
      if ((this.Definition as IComparisonViewDefinition).Fields.Any<IComparisonFieldDefinition>((Func<IComparisonFieldDefinition, bool>) (a => a.IsHtmlEnchancedField)))
      {
        contentComparator.Settings.EncodeStrings = true;
        contentComparator.Settings.BeginTagFormat = "</pre><pre class=\"{0}\">";
        contentComparator.Settings.EndTagFormat = "</pre><pre>";
        contentComparator.Settings.DateTimeDisplayFormat = "dd MMM, yyyy; hh:mm tt";
        contentComparator.ApplySettings();
      }
      IList<CompareResult> source = this.FixContents(this.FixTitles(contentComparator.Compare(obj1, obj2), (this.Definition as IComparisonViewDefinition).Fields), (this.Definition as IComparisonViewDefinition).Fields);
      List<CompareResult> list = source.Where<CompareResult>((Func<CompareResult, bool>) (a => (this.Definition as IComparisonViewDefinition).Fields.Any<IComparisonFieldDefinition>((Func<IComparisonFieldDefinition, bool>) (fld => fld.FieldName == a.PropertyName && fld.IncludeInDetails)))).ToList<CompareResult>();
      if (list.Count > 0)
      {
        this.FullScreenPropertiesRepeater.DataSource = (object) list;
        this.FullScreenPropertiesRepeater.ItemDataBound += new RepeaterItemEventHandler(this.VersionComparisonResultsRepeater_ItemDataBound);
        this.FullScreenPropertiesRepeater.DataBind();
      }
      else
        this.FullScreenButtonContainer.Visible = false;
      this.VersionComparisonResultsRepeater.DataSource = (object) source;
      this.VersionComparisonResultsRepeater.ItemDataBound += new RepeaterItemEventHandler(this.VersionComparisonResultsRepeater_ItemDataBound);
      this.VersionComparisonResultsRepeater.DataBind();
      this.HfContentServiceUrl.Value = this.FirstChange.Parent.Id.ToString();
      this.HIsEditable.Value = this.IsEditable.ToString().ToLower();
    }

    internal void VersionComparisonResultsRepeater_ItemDataBound(
      object sender,
      RepeaterItemEventArgs e)
    {
      CompareResult dataItem = e.Item.DataItem as CompareResult;
      Control control1 = e.Item.FindControl("defaultComparerWrapper");
      Control control2 = e.Item.FindControl("customComparerWrapper");
      if (string.IsNullOrEmpty(dataItem.CompareType))
      {
        control1.Visible = true;
        control2.Visible = false;
        Label control3 = e.Item.FindControl("lbDiffValue") as Label;
        Label control4 = e.Item.FindControl("lbOldValue") as Label;
        Literal control5 = e.Item.FindControl("ltrlDiffValue") as Literal;
        Literal control6 = e.Item.FindControl("ltrlOldValue") as Literal;
        if (dataItem.IsHtmlEnchancedField)
        {
          control3.Text = "<pre>" + dataItem.DiffHtml + "</pre>";
          control4.Text = "<pre>" + dataItem.OldValue + "</pre>";
          control5.Visible = false;
          control6.Visible = false;
        }
        else
        {
          control5.Text = dataItem.DiffHtml;
          control6.Text = dataItem.OldValue;
          control3.Visible = false;
          control4.Visible = false;
        }
      }
      else
      {
        control1.Visible = false;
        control2.Visible = true;
      }
    }

    internal IList<CompareResult> FixTitles(
      IList<CompareResult> originalCompareResult,
      IEnumerable<IComparisonFieldDefinition> fields)
    {
      foreach (CompareResult compareResult in (IEnumerable<CompareResult>) originalCompareResult)
      {
        CompareResult item = compareResult;
        IComparisonFieldDefinition comparisonFieldDefinition = fields.Where<IComparisonFieldDefinition>((Func<IComparisonFieldDefinition, bool>) (field => field.FieldName == item.PropertyName)).FirstOrDefault<IComparisonFieldDefinition>();
        if (comparisonFieldDefinition != null)
        {
          item.PropertyName = !string.IsNullOrWhiteSpace(comparisonFieldDefinition.ResourceClassId) ? Res.Get(comparisonFieldDefinition.ResourceClassId, comparisonFieldDefinition.Title) : comparisonFieldDefinition.Title;
          item.IsHtmlEnchancedField = comparisonFieldDefinition.IsHtmlEnchancedField;
        }
      }
      return originalCompareResult;
    }

    /// <summary>
    /// Resolves the sitefinity links in the result items of the comparison.
    /// </summary>
    /// <param name="originalCompareResult">The original compare result.</param>
    /// <param name="fields">The fields.</param>
    /// <returns></returns>
    internal IList<CompareResult> FixContents(
      IList<CompareResult> originalCompareResult,
      IEnumerable<IComparisonFieldDefinition> fields)
    {
      foreach (CompareResult compareResult in (IEnumerable<CompareResult>) originalCompareResult)
      {
        CompareResult item = compareResult;
        IComparisonFieldDefinition comparisonFieldDefinition = fields.Where<IComparisonFieldDefinition>((Func<IComparisonFieldDefinition, bool>) (field => field.FieldName == item.PropertyName)).FirstOrDefault<IComparisonFieldDefinition>();
        if (comparisonFieldDefinition != null && !comparisonFieldDefinition.IsHtmlEnchancedField)
          this.FixItemContent(item);
      }
      return originalCompareResult;
    }

    private void FixItemContent(CompareResult item)
    {
      item.NewValue = this.FixContentText(item.NewValue);
      item.OldValue = this.FixContentText(item.OldValue);
      item.DiffHtml = this.FixContentText(item.DiffHtml);
    }

    private string FixContentText(string text) => LinkParser.ResolveLinks(text, new GetItemUrl(DynamicLinksParser.GetContentUrl), (ResolveUrl) null, false, false, LinkParser.GetModifyAttributeProcessChunk("a", "target", "\"_blank\""));

    private void LoadData()
    {
      Guid firstId = this.FirstVersionId;
      Guid secondId = this.SecondVersionId;
      List<Change> list = VersionManager.GetManager().GetChanges().Where<Change>((Expression<Func<Change, bool>>) (change => change.Id == firstId || change.Id == secondId)).OrderBy<Change, int>((Expression<Func<Change, int>>) (change => change.Version)).ToList<Change>();
      this.firstChange = list[0];
      this.secondChange = list[1];
      Change change1 = list.Where<Change>((Func<Change, bool>) (change => change.Parent.Id == this.secondChange.Parent.Id && change.IsPublishedVersion)).OrderByDescending<Change, int>((Func<Change, int>) (change => change.Version)).FirstOrDefault<Change>();
      if (change1 == null)
        return;
      this.latestPublishedChangeNumber = change1.Version;
    }

    /// <inheritdoc />
    protected override void InitializeControls(
      GenericContainer container,
      IContentViewDefinition definition)
    {
      IAppSettings appSettings = SystemManager.CurrentContext.AppSettings;
      if (appSettings.Multilingual)
      {
        string uiCulture = this.UICulture;
        SystemManager.CurrentContext.Culture = string.IsNullOrEmpty(uiCulture) ? appSettings.DefaultFrontendLanguage : CultureInfo.GetCultureInfo(uiCulture);
      }
      this.LoadData();
    }

    /// <inheritdoc />
    protected override string LayoutTemplateName => (string) null;

    /// <inheritdoc />
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrWhiteSpace(base.LayoutTemplatePath) && string.IsNullOrEmpty(this.LayoutTemplateName) ? VersionComparisonView.templatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <inheritdoc />
    protected override void OnPreRender(EventArgs e)
    {
      this.BindData();
      base.OnPreRender(e);
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <inheritdoc />
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      string str = this.GetType().Assembly.GetName().ToString();
      return base.GetScriptReferences().Union<ScriptReference>((IEnumerable<ScriptReference>) new List<ScriptReference>()
      {
        new ScriptReference()
        {
          Assembly = str,
          Name = "Telerik.Sitefinity.Web.Scripts.ClientManager.js"
        },
        new ScriptReference("Telerik.Sitefinity.Resources.Scripts.CodeMirror.codemirror.js", Config.Get<ControlsConfig>().ResourcesAssemblyInfo.Assembly.GetName().Name),
        new ScriptReference("Telerik.Sitefinity.Resources.Scripts.CodeMirror.Mode.htmlmixed.js", Config.Get<ControlsConfig>().ResourcesAssemblyInfo.Assembly.GetName().Name),
        new ScriptReference("Telerik.Sitefinity.Resources.Scripts.CodeMirror.Mode.xml.js", Config.Get<ControlsConfig>().ResourcesAssemblyInfo.Assembly.GetName().Name),
        new ScriptReference("Telerik.Sitefinity.Resources.Scripts.CodeMirror.Mode.css.js", Config.Get<ControlsConfig>().ResourcesAssemblyInfo.Assembly.GetName().Name),
        new ScriptReference("Telerik.Sitefinity.Resources.Scripts.CodeMirror.Mode.javascript.js", Config.Get<ControlsConfig>().ResourcesAssemblyInfo.Assembly.GetName().Name)
      });
    }
  }
}
