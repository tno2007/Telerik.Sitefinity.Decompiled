// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.Services.IZoneEditorService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ServiceModel;
using System.ServiceModel.Web;
using Telerik.Sitefinity.Modules.Forms.Web.Services.Model;
using Telerik.Sitefinity.Modules.Pages.Web.Services.Model;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Pages.Web.Services
{
  /// <summary>Defines web service for ZoneEditor.</summary>
  [ServiceContract]
  public interface IZoneEditorService
  {
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "setoverride/{editable}/")]
    ZoneEditorWebServiceArgs SetOverride(
      ZoneEditorWebServiceArgs state,
      string editable);

    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "rollback/")]
    ZoneEditorWebServiceArgs Rollback(ZoneEditorWebServiceArgs state);

    /// <summary>Updates the state of the control.</summary>
    /// <param name="state">The state.</param>
    /// <returns></returns>
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/Control/")]
    [OperationContract]
    [ZoneEditorOperationValidation]
    ZoneEditorWebServiceArgs UpdateControlState(
      ZoneEditorWebServiceArgs state);

    /// <summary>Updates the state of the layout.</summary>
    /// <param name="state">The state.</param>
    /// <returns></returns>
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/Layout/")]
    [OperationContract]
    [ZoneEditorOperationValidation]
    ZoneEditorWebServiceArgs UpdateLayoutState(
      ZoneEditorWebServiceArgs state);

    /// <summary>
    /// Updates the styles of the columns in the layout control.
    /// </summary>
    /// <param name="styles">The array of styles.</param>
    /// <returns></returns>
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/Layout/Style/{LayoutControlId}/{PageId}/{isTemplate}/")]
    [OperationContract]
    [ZoneEditorOperationValidation]
    ZoneEditorWebServiceArgs UpdateLayoutControlStyles(
      ZoneEditorWebServiceArgs args,
      string layoutControlId,
      string pageId,
      string isTemplate);

    /// <summary>
    /// Discards the specified page draft and returns true if the draft has been discarded; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="draftId">The draft ID.</param>
    [OperationContract]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/Page/{draftId}/")]
    [ZoneEditorOperationValidation(true, DesignMediaType.Page)]
    bool DiscardPageDraft(string draftId);

    /// <summary>
    /// Publishes the page draft and returns true if the draft has been published; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="draftId">The draft ID.</param>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/Page/Publish/{draftId}/")]
    [ZoneEditorOperationValidation(true, DesignMediaType.Page)]
    bool PublishPageDraft(string draftId);

    /// <summary>
    /// Saves the page draft and returns true if the draft has been saved; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="pageId">page id </param>
    /// <param name="workflowOperation">workflow operation name</param>
    /// <param name="publicationDate"> publication date</param>
    /// <param name="expirationDate"> expiration date</param>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/Page/Save/?workflowOperation={workflowOperation}")]
    bool SavePageDraft(WcfPageData wcfPageData, string workflowOperation);

    /// <summary>
    /// Sets the theme on the page draft and returns true if the theme has been set; otherwise false.
    /// </summary>
    /// <param name="themeName">Name of the theme to be set.</param>
    /// <returns>True if theme set; otherwise false.</returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/Page/Theme/{draftId}/{themeName}/?isTemplate={isTemplate}")]
    bool ChangeTheme(string draftId, string themeName, bool isTemplate);

    /// <summary>
    /// Take ownership of Page draft. If current user is not administrator throws InvalidOperationException
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="draftId">The draft pageId.</param>
    [OperationContract]
    [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/Page/TakeOwnership/{draftId}/")]
    [ZoneEditorOperationValidation(false, DesignMediaType.Page)]
    void TakePageOwnership(string draftId);

    /// <summary>
    /// Take ownership of Page data. If current user is not administrator throws InvalidOperationException
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="pageDataId">The page data Id.</param>
    [OperationContract]
    [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/Page/UnlockPage/{pageDataId}/")]
    [ZoneEditorOperationValidation(false, DesignMediaType.Page)]
    void UnlockPage(string pageDataId);

    /// <summary>Sets the page localization strategy.</summary>
    /// <param name="pageNodeId">The id of the node to set strategy for.</param>
    /// <param name="strategy">The strategy to set. Specifying split strategy causes the creation of one node+page for each available language.</param>
    /// <param name="copyData">if set to <c>true</c> and strategy is Split, data(controls/layouts) will be copied from the source node to the newly created language nodes.</param>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/Page/LocalizationStrategy/{pageNodeId}/?strategy={strategy}&copyData={copyData}")]
    [ZoneEditorOperationValidation(false, DesignMediaType.Page)]
    void SetPageLocalizationStrategy(string pageNodeId, string strategy, bool copyData);

    /// <summary>
    /// Initizlizes a page which is in Split localization mode. If source language is set, copies the page data(controls/layouts)
    /// from the version of the page in the given language to the given page. Sets the localization strategy to Split so that
    /// it is indicated that initialization is already made.
    /// </summary>
    /// <param name="targetNodeId">The id of the node to copy data to.</param>
    /// <param name="sourceLanguage">The language version from which to obtain data. If null, the invariant language is used.</param>
    /// <param name="targetLanguage">The language in which to set data in the target. If null, the UiCulture of the target page is used.</param>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/Page/InitializeSplitPage/{targetNodeId}/?sourceLanguage={sourceLanguage}&targetLanguage={targetLanguage}")]
    [ZoneEditorOperationValidation(false, DesignMediaType.Page)]
    void InitializeSplitPage(string targetNodeId, string sourceLanguage, string targetLanguage);

    /// <summary>
    /// Splits the page into different versions for each language.
    /// </summary>
    /// <param name="targetNodeId">The target node id.</param>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/Page/SplitPage/{pageNodeId}/")]
    [ZoneEditorOperationValidation(false, DesignMediaType.Page)]
    void SplitPage(string pageNodeId);

    /// <summary>
    /// Discards the specified template draft and returns true if the draft has been discarded; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="draftId">The draft ID.</param>
    [OperationContract]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/Template/{draftId}/")]
    [ZoneEditorOperationValidation(true, DesignMediaType.Template)]
    bool DiscardTemplateDraft(string draftId);

    /// <summary>
    /// Publishes the page draft and returns true if the draft has been published; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="draftId">The draft ID.</param>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/Template/Publish/{draftId}/")]
    [ZoneEditorOperationValidation(true, DesignMediaType.Template)]
    bool PublishTemplateDraft(string draftId);

    /// <summary>
    /// Saves the page draft and returns true if the draft has been saved; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="draftId">The draft pageId.</param>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/Template/Save/{draftId}/")]
    [ZoneEditorOperationValidation(true, DesignMediaType.Template)]
    bool SaveTemplateDraft(string draftId);

    /// <summary>
    /// Take ownership of Template draft. If current user is not administrator throws InvalidOperationException
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="draftId">The draft ID.</param>
    [OperationContract]
    [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/Template/TakeOwnership/{draftId}/")]
    [ZoneEditorOperationValidation(false, DesignMediaType.Template)]
    void TemplateTakeOwnership(string draftId);

    /// <summary>
    /// Take ownership of Template. If current user is not administrator throws InvalidOperationException
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="draftId">The template ID.</param>
    [OperationContract]
    [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/Template/UnlockTemplate/{templateId}/")]
    [ZoneEditorOperationValidation(false, DesignMediaType.Template)]
    void UnlockTemplate(string templateId);

    /// <summary>
    /// Unlocks the given form. If current user is not administrator an InvalidOperationException is thrown.
    /// </summary>
    /// <param name="pageDataId">The form Id.</param>
    [OperationContract]
    [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/Form/UnlockForm/{formId}/")]
    [ZoneEditorOperationValidation(false, DesignMediaType.Form)]
    void UnlockForm(string formId);

    /// <summary>
    /// Publishes the form draft and returns true if the draft has been published; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="formViewModel">The form ViewModel object.</param>
    /// <param name="draftId">The draft id.</param>
    /// <returns></returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/Form/Publish/{draftId}/")]
    [ZoneEditorOperationValidation(true, DesignMediaType.Form, 1)]
    bool PublishFormDraft(FormDescriptionViewModel formViewModel, string draftId);

    /// <summary>
    /// Saves the form draft and returns true if the draft has been saved; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="draftId">The draft pageId.</param>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/Form/Save/{draftId}/")]
    [ZoneEditorOperationValidation(true, DesignMediaType.Form, 1)]
    bool SaveFormDraft(FormDescriptionViewModel formViewModel, string draftId);

    /// <summary>
    /// Discards the specified form draft and returns true if the draft has been discarded; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="draftId">The draft ID.</param>
    [OperationContract]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/Form/{draftId}/")]
    [ZoneEditorOperationValidation(true, DesignMediaType.Form)]
    bool DiscardFormDraft(string draftId);

    /// <summary>Changes the template of a page draft.</summary>
    /// <param name="pageId">The page draft id.</param>
    /// <param name="newTemplateId">The new template id.</param>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/changeTemplate/{pageId}/?newTemplateId={newTemplateId}")]
    [WebHelp(Comment = "Changes the template of the specified page (ID)")]
    [ZoneEditorOperationValidation(true, DesignMediaType.Page)]
    void ChangeTemplate(string pageId, string newTemplateId);

    /// <summary>Changes the parent template of a template draft.</summary>
    /// <param name="draftId">The template draft id.</param>
    /// <param name="newTemplateId">The new template id.</param>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/Template/changeTemplate/{draftId}/?newTemplateId={newTemplateId}")]
    [WebHelp(Comment = "Changes the parent template of the specified template (ID)")]
    [ZoneEditorOperationValidation(true, DesignMediaType.Template)]
    bool ChangeParentTemplate(string draftId, string newTemplateId);
  }
}
