// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Web.FormsSubmitHttpHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Security;
using System.Web;
using System.Web.Script.Serialization;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Forms.Events;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Events;
using Telerik.Sitefinity.Utilities;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Modules.Forms.Web
{
  /// <summary>
  /// Represents a route handler for submitting Sitefinity forms.
  /// </summary>
  public class FormsSubmitHttpHandler : IHttpHandler
  {
    private static readonly JavaScriptSerializer serializer = new JavaScriptSerializer();

    /// <summary>
    /// Enables processing of HTTP Web requests by a custom HttpHandler that implements the <see cref="T:System.Web.IHttpHandler" /> interface.
    /// </summary>
    /// <param name="context">An <see cref="T:System.Web.HttpContext" /> object that provides references to the intrinsic server objects (for example, Request, Response, Session, and Server) used to service HTTP requests.</param>
    public void ProcessRequest(HttpContext context) => this.ProcessRequest((HttpContextBase) new HttpContextWrapper(context));

    /// <summary>Processes the request.</summary>
    /// <param name="context">The context.</param>
    public void ProcessRequest(HttpContextBase context)
    {
      HttpRequestBase request = context.Request;
      context.Response.Clear();
      int num;
      try
      {
        string input = request.QueryString.Get("formId");
        if (string.IsNullOrEmpty(input))
          throw new ArgumentException("The Id of the Form is not valid.");
        string name = request.QueryString.Get("name");
        FormDescription formDescription = !string.IsNullOrEmpty(name) ? FormsHelper.GetFormData(name, Guid.Parse(input)) : throw new ArgumentException("The Name of the Form is not valid.");
        if (formDescription == null)
          throw new NullReferenceException("The form wasn't found.");
        if (this.CheckForCaptcha(formDescription))
          throw new SecurityException("Async form submit is not allowed when the from contains a Captcha control.");
        if (formDescription.Framework == FormFramework.Mvc)
          throw new InvalidOperationException("This handler cannot be used with MVC forms.");
        num = this.ProcessRequest(request.QueryString, request.Form, context.Response, request.Files, request.UserHostAddress);
      }
      catch (EventHandlerInvocationException ex)
      {
        ValidationException validationException = ex.Lookup<ValidationException>();
        if (validationException != null)
        {
          num = 500;
          context.Response.Write(JsonConvert.SerializeObject((object) new
          {
            error = validationException.Message
          }));
        }
        else if (ex.Lookup<CancelationException>() != null)
        {
          num = 409;
        }
        else
        {
          context.Response.Write(JsonConvert.SerializeObject((object) new
          {
            error = ex.Message
          }));
          num = 500;
        }
      }
      catch (Exception ex)
      {
        Log.Write((object) string.Format("An error occupied when submitting the form {0} - {1}", (object) request.QueryString.Get("name"), (object) ex.Message), ConfigurationPolicy.ErrorLog);
        num = 500;
        context.Response.Write(JsonConvert.SerializeObject((object) new
        {
          error = Res.Get<FormsResources>("FormSubmitError")
        }));
      }
      context.Response.ContentType = "application/json; charset=utf-8";
      context.Response.StatusCode = num;
    }

    /// <summary>Processes the request.</summary>
    /// <param name="queryStrings">The query strings.</param>
    /// <param name="formData">The form data.</param>
    /// <param name="response">The response.</param>
    /// <param name="uploadedFiles">The uploaded files.</param>
    /// <param name="ipAddress">The ip address.</param>
    /// <returns></returns>
    internal int ProcessRequest(
      NameValueCollection queryStrings,
      NameValueCollection formData,
      HttpResponseBase response,
      HttpFileCollectionBase uploadedFiles,
      string ipAddress)
    {
      string input = queryStrings.Get("formId");
      FormDescription formData1 = FormsHelper.GetFormData(queryStrings.Get("name"), Guid.Parse(input));
      FormEditRequestContext formEditContext = FormEditRequestContext.Get(queryStrings, formData1.Id);
      if (!this.IsValidRequest(formEditContext, formData))
        return this.SetErrorResponseMessage(response, "Request is not valid!");
      if (formEditContext.IsExpired)
        return this.SetErrorResponseMessage(response, "Request expired!");
      Guid currentUserId = ClaimsManager.GetCurrentUserId();
      string membershipProvider = ClaimsManager.GetCurrentIdentity().MembershipProvider;
      string restrictionsError;
      if (!formEditContext.IsValidUpdateRequest && !FormsHelper.ValidateFormSubmissionRestrictions(formData1, currentUserId, ipAddress, out restrictionsError))
        return this.SetErrorResponseMessage(response, restrictionsError);
      IDictionary<string, List<FormHttpPostedFile>> formHttpFilesData = FormsHelper.CreateFormHttpFilesData(uploadedFiles);
      IEnumerable<IFormFieldControl> formFieldControls = FormsHelper.GetFormFieldControls(formData1, formData1.GetProviderName());
      IDictionary<string, object> dictionary = FormsSubmitHttpHandler.ToDictionary(formData, formFieldControls);
      FormPostedData formPostedData = new FormPostedData()
      {
        FormsData = dictionary,
        Files = formHttpFilesData
      };
      IFormEntryResponseEditContext reponseEditContext = FormsHelper.GenerateReponseEditContext(formEditContext.FormEntryId, formData1);
      IList<IFormEntryEventControl> currentControlsState = FormsHelper.GetCurrentControlsState(formEditContext.IsValidUpdateRequest, formData1, formEditContext.FormEntryId);
      string str = queryStrings.Get("currentMode");
      this.RaiseFormValidatingEvent(formData1, formPostedData, ipAddress, currentUserId, reponseEditContext, currentControlsState, str);
      this.RaiseFormSavingEvent(formData1, formPostedData, ipAddress, currentUserId, reponseEditContext, currentControlsState, str);
      IEnumerable<KeyValuePair<string, object>> validFields = this.GetValidFields(dictionary, formData1, formEditContext.ProviderName, str);
      if (formEditContext.IsValidUpdateRequest)
      {
        FormsHelper.UpdateFormEntry(formEditContext.ProviderName, formEditContext.FormEntryId, formData1, validFields, formHttpFilesData, ipAddress, currentUserId, membershipProvider);
      }
      else
      {
        string formLanguage = SystemManager.CurrentContext.AppSettings.Multilingual ? SystemManager.CurrentContext.Culture.Name : SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage.Name;
        FormsHelper.SaveFormsEntry(formData1.Id, validFields, formHttpFilesData, ipAddress, formLanguage);
      }
      this.RaiseFormSavedEvent(formData1, formPostedData, ipAddress, currentUserId, reponseEditContext, currentControlsState, str);
      return 200;
    }

    internal IEnumerable<KeyValuePair<string, object>> GetValidFields(
      IDictionary<string, object> formDataAsDicitonary,
      FormDescription formDescription,
      string providerName,
      string currentMode)
    {
      List<IFormFieldControl> controlsForUpdate = FormsHelper.ProcessValidForUpdateFieldControls(FormsHelper.GetFormFieldControls(formDescription, providerName), currentMode).ToList<IFormFieldControl>();
      return formDataAsDicitonary.Where<KeyValuePair<string, object>>((Func<KeyValuePair<string, object>, bool>) (kvp => controlsForUpdate.Any<IFormFieldControl>((Func<IFormFieldControl, bool>) (f => f.MetaField.FieldName == kvp.Key))));
    }

    /// <summary>
    /// Gets a value indicating whether another request can use the <see cref="T:System.Web.IHttpHandler" /> instance.
    /// </summary>
    /// <value></value>
    /// <returns>true if the <see cref="T:System.Web.IHttpHandler" /> instance is reusable; otherwise, false.
    /// </returns>
    public bool IsReusable => false;

    private void RaiseFormSavedEvent(
      FormDescription formDescription,
      FormPostedData formPostedData,
      string ipAddress,
      Guid userId,
      IFormEntryResponseEditContext formEntryResponseEditContext,
      IList<IFormEntryEventControl> controlsSate,
      string mode)
    {
      EventHub.Raise((IEvent) FormsHelper.CreateFormSavedEvent(formDescription, formPostedData, ipAddress, userId, formEntryResponseEditContext, controlsSate, mode));
    }

    private void RaiseFormSavingEvent(
      FormDescription formDescription,
      FormPostedData formPostedData,
      string ipAddress,
      Guid userId,
      IFormEntryResponseEditContext formEntryResponseEditContext,
      IList<IFormEntryEventControl> controlsSate,
      string mode)
    {
      EventHub.Raise((IEvent) FormsHelper.CreateFormSavingEvent(formDescription, formPostedData, ipAddress, userId, formEntryResponseEditContext, controlsSate, mode));
    }

    private void RaiseFormValidatingEvent(
      FormDescription formDescription,
      FormPostedData formPostedData,
      string ipAddress,
      Guid userId,
      IFormEntryResponseEditContext formEntryResponseEditContext,
      IList<IFormEntryEventControl> controlsSate,
      string mode)
    {
      EventHub.Raise((IEvent) FormsHelper.CreateFormValidatingEvent(formDescription, formPostedData, ipAddress, userId, formEntryResponseEditContext, controlsSate, mode), true);
    }

    /// <summary>Checks for captcha control.</summary>
    /// <param name="formDescription">The form description.</param>
    /// <returns></returns>
    private bool CheckForCaptcha(FormDescription formDescription)
    {
      FormsManager formsManager = FormsManager.GetManager();
      return FormsHelper.GetSortedControlsData(formDescription).Any<ControlData>((Func<ControlData, bool>) (formControl => formsManager.LoadControl((ObjectData) formControl, (CultureInfo) null) is FormCaptcha));
    }

    private int SetErrorResponseMessage(HttpResponseBase response, string message)
    {
      response.Write(JsonConvert.SerializeObject((object) new
      {
        error = message
      }));
      return 500;
    }

    private bool IsValidRequest(
      FormEditRequestContext formEditContext,
      NameValueCollection collection)
    {
      if (!formEditContext.IsValidUpdateRequest)
        return true;
      string str = collection.Get("_formsQueryDataElement");
      return formEditContext.QueryData == str;
    }

    /// <summary>
    /// Converts a <see cref="!:NameValueColleciton" /> to an <see cref="!:IDictionary&lt;string,object&gt;" />.
    /// </summary>
    private static IDictionary<string, object> ToDictionary(
      NameValueCollection nvc,
      IEnumerable<IFormFieldControl> controls)
    {
      Dictionary<string, object> dictionary = new Dictionary<string, object>();
      foreach (string allKey in nvc.AllKeys)
      {
        string key = allKey;
        dictionary[key] = FormsSubmitHttpHandler.ParseValue(nvc[key], controls.FirstOrDefault<IFormFieldControl>((Func<IFormFieldControl, bool>) (x => x.MetaField.FieldName.Equals(key))));
      }
      return (IDictionary<string, object>) dictionary;
    }

    private static object ParseValue(string value, IFormFieldControl control)
    {
      string[] strArray = value.Split(new string[1]
      {
        "#ClrType#"
      }, StringSplitOptions.RemoveEmptyEntries);
      if (strArray.Length > 1)
      {
        try
        {
          Type targetType = TypeResolutionService.ResolveType(control.MetaField.ClrType, false);
          if (targetType != (Type) null)
            return FormsSubmitHttpHandler.serializer.Deserialize(strArray[0], targetType);
        }
        catch (Exception ex)
        {
          Log.Write((object) ex);
        }
      }
      return (object) strArray[0];
    }
  }
}
