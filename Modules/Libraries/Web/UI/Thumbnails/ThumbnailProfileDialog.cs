// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.ThumbnailProfileDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Libraries.Configuration;
using Telerik.Sitefinity.Scheduling;
using Telerik.Sitefinity.Scheduling.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails
{
  /// <summary>
  /// The dialog that is used for creating and editing the different thumbnail profiles
  /// </summary>
  public class ThumbnailProfileDialog : AjaxDialogBase, IPostBackEventHandler
  {
    internal const string viewScript = "Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.Scripts.ThumbnailProfileDialog.js";
    internal static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Configuration.Basic.ThumbnailProfileDialog.ascx");
    private string libraryType;
    private string selectedProfile;
    private const string doneClickedPostBackKey = "DonePressed";
    private const string regeneratePostBackKey = "Regenerate";
    private const string libraryTypeKey = "libraryType";
    private const string profileKey = "profile";
    private static RegexStrategy regexStrategy = (RegexStrategy) null;

    /// <inheritdoc />
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? ThumbnailProfileDialog.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <inheritdoc />
    public override string ClientComponentType => typeof (ThumbnailProfileDialog).FullName;

    /// <summary>
    /// Gets the reference to the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.TextField" /> that holds the
    /// title of the thumbnail profile.
    /// </summary>
    protected virtual TextField TextFieldProfileTitle => this.Container.GetControl<TextField>("textFieldProfileTitle", true);

    /// <summary>
    /// Gets the reference to the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.TextField" /> that holds the
    /// name of the thumbnail profile.
    /// </summary>
    protected virtual MirrorTextField MirrorFieldProfileName => this.Container.GetControl<MirrorTextField>("mirrorFieldProfileName", true);

    /// <summary>
    /// Gets the reference to the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.ChoiceField" /> that indicates
    /// weather this profile is set as default for libraries.
    /// </summary>
    protected virtual ChoiceField ChoiceFieldIsDefault => this.Container.GetControl<ChoiceField>("choiceFieldIsDefault", true);

    /// <summary>
    /// Gets the reference to the <see cref="T:System.Web.UI.HtmlControls.HtmlContainerControl" /> that is used
    /// for confirming the dialog changes.
    /// </summary>
    protected virtual HtmlContainerControl ButtonDone => this.Container.GetControl<HtmlContainerControl>("buttonDone", true);

    /// <summary>
    /// Gets the reference to the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.MethodInfoContainer" /> that is used
    /// for maintain the state of the dynamic fields that are used for displaying the profile data.
    /// </summary>
    protected virtual MethodInfoContainer DynamicFieldsContainer => this.Container.GetControl<MethodInfoContainer>("dynamicFieldsContainer", true);

    /// <summary>
    /// Gets the reference to the <see cref="T:Telerik.Sitefinity.Web.UI.SitefinityLabel" /> that is used
    /// to show an error when the profile name is invalid.
    /// </summary>
    protected virtual SitefinityLabel LiteralErrorProfileName => this.Container.GetControl<SitefinityLabel>("profileNameError", true);

    /// <summary>
    /// Gets the reference to the <see cref="T:System.Web.UI.WebControls.Literal" /> that is used
    /// for setting the title of the dialog.
    /// </summary>
    protected virtual Literal LiteralDialogTitle => this.Container.GetControl<Literal>("dialogTitle", true);

    /// <summary>
    /// Gets the reference to the <see cref="T:System.Web.UI.HtmlControls.HtmlContainerControl" /> that is used
    /// to close the dialog.
    /// </summary>
    protected virtual HtmlContainerControl ButtonCancel => this.Container.GetControl<HtmlContainerControl>("buttonCancel", true);

    /// <summary>
    /// Gets the reference to the <see cref="T:Telerik.Web.UI.RadWindow" /> that is used
    /// to contain the prompt dialog.
    /// </summary>
    protected virtual Telerik.Web.UI.RadWindow PromptDialog => this.Container.GetControl<Telerik.Web.UI.RadWindow>("promptDialog", true);

    /// <inheritdoc />
    protected override void OnInit(EventArgs e)
    {
      base.OnInit(e);
      if (this.Page == null)
        return;
      this.Page.RegisterRequiresControlState((Control) this);
      this.Page.EnableViewState = true;
    }

    /// <inheritdoc />
    protected override object SaveControlState() => (object) this.selectedProfile;

    /// <inheritdoc />
    protected override void LoadControlState(object savedState) => this.selectedProfile = (string) savedState;

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container)
    {
      NameValueCollection queryString = SystemManager.CurrentHttpContext.Request.QueryString;
      string key = queryString["profile"];
      this.libraryType = queryString["libraryType"];
      if (!this.Page.IsPostBack && !string.IsNullOrEmpty(key))
      {
        ThumbnailProfileConfigElement thumbnailProfile = Config.Get<LibrariesConfig>().GetThumbnailProfiles(this.libraryType)[key];
        if (thumbnailProfile != null)
        {
          this.selectedProfile = key;
          this.DynamicFieldsContainer.LoadMethod(thumbnailProfile.Method, thumbnailProfile.Parameters);
          this.TextFieldProfileTitle.Value = (object) thumbnailProfile.Title;
          this.ChoiceFieldIsDefault.Choices[0].Selected = thumbnailProfile.IsDefault;
          if (this.libraryType == typeof (Album).FullName)
            this.LiteralDialogTitle.Text = Res.Get<LibrariesResources>().EditImageThumbnailProfile.Arrange((object) thumbnailProfile.Title);
          else if (this.libraryType == typeof (VideoLibrary).FullName)
            this.LiteralDialogTitle.Text = Res.Get<LibrariesResources>().EditVideoThumbnailProfile.Arrange((object) thumbnailProfile.Title);
        }
        else
          this.DynamicFieldsContainer.LoadDefault();
      }
      if (this.selectedProfile.IsNullOrEmpty())
        return;
      this.MirrorFieldProfileName.DisplayMode = FieldDisplayMode.Read;
      this.MirrorFieldProfileName.Value = (object) this.selectedProfile;
      this.MirrorFieldProfileName.RegularExpressionFilter = ThumbnailProfileDialog.RgxStrategy.DefaultExpressionFilter;
      if (this.MirrorFieldProfileName.ValidatorDefinition == null)
        return;
      this.MirrorFieldProfileName.ValidatorDefinition.RegularExpression = "^[" + ThumbnailProfileDialog.RgxStrategy.UnicodeCategories + "\\-\\!\\$\\'\\(\\)\\=\\@\\d]{0,10}$";
    }

    /// <inheritdoc />
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = base.GetScriptDescriptors().Last<ScriptDescriptor>() as ScriptControlDescriptor;
      controlDescriptor.AddElementProperty("buttonCancel", this.ButtonCancel.ClientID);
      controlDescriptor.AddElementProperty("buttonDone", this.ButtonDone.ClientID);
      controlDescriptor.AddComponentProperty("textFieldProfileTitle", this.TextFieldProfileTitle.ClientID);
      controlDescriptor.AddComponentProperty("mirrorFieldProfileName", this.MirrorFieldProfileName.ClientID);
      controlDescriptor.AddComponentProperty("promptDialog", this.PromptDialog.ClientID);
      controlDescriptor.AddProperty("regeneratePostBackRef", (object) this.Page.GetPostBackEventReference((Control) this, "Regenerate"));
      controlDescriptor.AddProperty("doneClickedPostBackRef", (object) this.Page.GetPostBackEventReference((Control) this, "DonePressed"));
      controlDescriptor.AddProperty("promptDialogUrl", (object) this.Page.ResolveUrl("~/Sitefinity/Dialog/ThumbnailPromptDialog"));
      List<string> stringList = new List<string>();
      stringList.Add(this.TextFieldProfileTitle.ClientID);
      stringList.AddRange(this.DynamicFieldsContainer.GetFieldControlIds());
      if (this.MirrorFieldProfileName.DisplayMode == FieldDisplayMode.Write)
        stringList.Add(this.MirrorFieldProfileName.ClientID);
      controlDescriptor.AddProperty("fieldControlIds", (object) stringList);
      string str = SystemManager.CurrentHttpContext.Request.QueryString["librariesCount"];
      controlDescriptor.AddProperty("librariesCount", (object) (str ?? 0.ToString()));
      bool flag = !this.selectedProfile.IsNullOrEmpty();
      controlDescriptor.AddProperty("editMode", (object) flag);
      controlDescriptor.AddProperty("currentMethodChanged", (object) this.DynamicFieldsContainer.DropDownSelectedIndexChanged);
      if (flag)
      {
        NameValueCollection originalValues = this.DynamicFieldsContainer.GetFieldValues();
        Dictionary<string, string> dictionary = ((IEnumerable<string>) originalValues.AllKeys).ToDictionary<string, string, string>((Func<string, string>) (x => x), (Func<string, string>) (x => originalValues[x]));
        controlDescriptor.AddProperty("originalRegenFieldValues", (object) dictionary);
        IEnumerable<string> regenFieldIds = this.DynamicFieldsContainer.GetRegenFieldIds();
        controlDescriptor.AddProperty("regenFieldIds", (object) regenFieldIds);
      }
      return (IEnumerable<ScriptDescriptor>) new ScriptDescriptor[1]
      {
        (ScriptDescriptor) controlDescriptor
      };
    }

    /// <inheritdoc />
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      string fullName = typeof (ThumbnailProfileDialog).Assembly.FullName;
      return (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
      {
        new ScriptReference("Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.Scripts.ThumbnailProfileDialog.js", fullName)
      };
    }

    /// <inheritdoc />
    public void RaisePostBackEvent(string eventArgument)
    {
      if (!this.ValidateFields())
        return;
      string profileKey = this.SaveMethodParameters();
      if (eventArgument == "Regenerate")
      {
        this.RegenerateDepenedentLibrariesThumbnails(profileKey);
        this.CloseDialog();
      }
      else
      {
        if (!(eventArgument == "DonePressed"))
          return;
        this.CloseDialog();
      }
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    private void RegenerateDepenedentLibrariesThumbnails(string profileKey)
    {
      foreach (string providerName in LibrariesManager.GetManager().GetProviderNames(ProviderBindingOptions.SkipSystem))
      {
        LibrariesManager manager1 = LibrariesManager.GetManager(providerName);
        IQueryable<Library> source = !typeof (Video).FullName.Equals(this.libraryType) ? (IQueryable<Library>) manager1.GetAlbums() : (IQueryable<Library>) manager1.GetVideoLibraries();
        Expression<Func<Library, bool>> predicate = (Expression<Func<Library, bool>>) (l => l.ThumbnailProfiles.Contains(profileKey));
        foreach (Library library in (IEnumerable<Library>) source.Where<Library>(predicate))
        {
          string[] strArray = new string[1]{ profileKey };
          if (library.RunningTask != Guid.Empty)
          {
            Guid taskId = library.RunningTask;
            SchedulingManager manager2 = SchedulingManager.GetManager();
            ScheduledTaskData scheduledTaskData = manager2.GetTaskData().Where<ScheduledTaskData>((Expression<Func<ScheduledTaskData, bool>>) (t => t.Id == taskId)).SingleOrDefault<ScheduledTaskData>();
            if (scheduledTaskData != null)
            {
              if (scheduledTaskData.TaskName != typeof (LibraryThumbnailsRegenerateTask).FullName)
              {
                library.NeedThumbnailsRegeneration = true;
                manager1.SaveChanges();
                continue;
              }
              Scheduler.Instance.StopTask(taskId);
              Thread.Sleep(200);
              LibraryThumbnailsRegenerateTask thumbnailsRegenerateTask = SchedulingTaskFactory.ResolveTask(scheduledTaskData) as LibraryThumbnailsRegenerateTask;
              strArray = thumbnailsRegenerateTask.ProfilesFilter == null || thumbnailsRegenerateTask.ProfilesFilter.Length == 0 ? new string[0] : ((IEnumerable<string>) strArray).Union<string>((IEnumerable<string>) thumbnailsRegenerateTask.ProfilesFilter).Distinct<string>().ToArray<string>();
              manager2.DeleteTaskData(scheduledTaskData);
              manager2.SaveChanges();
              library.RunningTask = Guid.Empty;
              manager1.SaveChanges();
            }
          }
          LibrariesManager.StartThumbnailRegenerationTask(library.Id, providerName, strArray, (string) library.Title);
        }
      }
    }

    private bool ValidateFields()
    {
      bool flag = true;
      if (this.MirrorFieldProfileName.DisplayMode == FieldDisplayMode.Write)
      {
        flag = flag && this.MirrorFieldProfileName.IsValid();
        ThumbnailProfileConfigElement profileConfigElement = (ThumbnailProfileConfigElement) null;
        string libraryType = SystemManager.CurrentHttpContext.Request.QueryString["libraryType"];
        if (Config.Get<LibrariesConfig>().GetThumbnailProfiles(libraryType).TryGetValue(this.MirrorFieldProfileName.Value.ToString(), out profileConfigElement))
        {
          flag = false;
          this.LiteralErrorProfileName.Visible = true;
        }
      }
      return flag && this.TextFieldProfileTitle.IsValid() && this.DynamicFieldsContainer.IsValid();
    }

    private string SaveMethodParameters()
    {
      NameValueCollection fieldValues = this.DynamicFieldsContainer.GetFieldValues();
      string key = this.MirrorFieldProfileName.Value.ToString().ToLower();
      if (string.IsNullOrEmpty(key))
        key = typeof (TextField).GetField("value", BindingFlags.Instance | BindingFlags.NonPublic).GetValue((object) this.MirrorFieldProfileName).ToString().ToLower();
      if (key.Length > 10)
        key = key.Substring(0, 10);
      ConfigManager manager = ConfigManager.GetManager();
      LibrariesConfig section = manager.GetSection<LibrariesConfig>();
      string libraryType = SystemManager.CurrentHttpContext.Request.QueryString["libraryType"];
      ConfigElementDictionary<string, ThumbnailProfileConfigElement> thumbnailProfiles = section.GetThumbnailProfiles(libraryType);
      ThumbnailProfileConfigElement element = thumbnailProfiles[key];
      bool flag = true;
      if (element == null)
      {
        element = new ThumbnailProfileConfigElement((ConfigElement) thumbnailProfiles);
        element.Parameters = new NameValueCollection();
        element.Name = key;
        flag = false;
      }
      element.Title = (string) this.TextFieldProfileTitle.Value;
      element.Method = this.DynamicFieldsContainer.CurrentMethod;
      element.IsDefault = (bool) this.ChoiceFieldIsDefault.Value;
      element.Parameters.Clear();
      element.Parameters.Add(fieldValues);
      if (!flag)
        thumbnailProfiles.Add(element);
      manager.SaveSection((ConfigSection) section, true);
      return key;
    }

    private void CloseDialog()
    {
      string script = "Sys.Application.add_load(function(){dialogBase.closeAndRebind();})";
      string key = "closeDialog";
      this.Page.ClientScript.RegisterStartupScript(this.GetType(), key, script, true);
    }

    private static RegexStrategy RgxStrategy
    {
      get
      {
        if (ThumbnailProfileDialog.regexStrategy == null)
          ThumbnailProfileDialog.regexStrategy = ObjectFactory.Resolve<RegexStrategy>();
        return ThumbnailProfileDialog.regexStrategy;
      }
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    private struct PropertyNames
    {
      public const string MirrorFieldProfileName = "mirrorFieldProfileName";
      public const string ChoiceFieldIsDefault = "choiceFieldIsDefault";
      public const string TextFieldProfileTitle = "textFieldProfileTitle";
      public const string ButtonDone = "buttonDone";
      public const string ButtonCancel = "buttonCancel";
      public const string ButtonDetails = "buttonDetails";
      public const string PlaceHolderMethodProperties = "placeHolderMethodProperties";
      public const string DetailsContainer = "detailsContainer";
      public const string LiteralErrorProfileName = "profileNameError";
      public const string LiteralDialogTitle = "dialogTitle";
      public const string DynamicFieldsContainer = "dynamicFieldsContainer";
      public const string PromptDialog = "promptDialog";
      public const string RegeneratePostBackReference = "regeneratePostBackRef";
      public const string DoneClickedBackReference = "doneClickedPostBackRef";
      public const string PromptDialogUrl = "promptDialogUrl";
      public const string FieldControlIds = "fieldControlIds";
      public const string EditMode = "editMode";
      public const string LibrariesCount = "librariesCount";
      public const string RegenFieldIds = "regenFieldIds";
      public const string OriginalRegenFieldValues = "originalRegenFieldValues";
      public const string CurrentMethodChanged = "currentMethodChanged";
    }
  }
}
