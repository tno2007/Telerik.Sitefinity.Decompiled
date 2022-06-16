// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.ConnectorFormsEventHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Web;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Modules.Forms.Events;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Modules.Forms
{
  /// <summary>Handles form events.</summary>
  public class ConnectorFormsEventHandler
  {
    private static readonly ConcurrentDictionary<Type, IConnectorFormDataSender> ConnectorFormDataSenders = new ConcurrentDictionary<Type, IConnectorFormDataSender>();
    private const string AjaxSubmitRequestEnd = "AjaxSubmit";
    private const string ConnectorSettingsName = "ConnectorSettings";

    /// <summary>
    /// Registers the provided <see cref="T:Telerik.Sitefinity.Modules.Forms.IConnectorFormDataSender" /> sender to the collection of senders that is used
    /// to send form submission data. Use this method to register your custom connector form data sender.
    /// </summary>
    /// <param name="connectorFormDataSender">The form data sender.</param>
    public static void RegisterSender(IConnectorFormDataSender connectorFormDataSender) => ConnectorFormsEventHandler.ConnectorFormDataSenders.AddOrUpdate(connectorFormDataSender.GetType(), connectorFormDataSender, (Func<Type, IConnectorFormDataSender, IConnectorFormDataSender>) ((oldKey, oldValue) => connectorFormDataSender));

    /// <summary>
    /// Unregisters the provided <see cref="T:Telerik.Sitefinity.Modules.Forms.IConnectorFormDataSender" /> sender from the collection of senders that is used
    /// to send form submission data.
    /// </summary>
    /// <param name="connectorFormDataSender">The form data sender.</param>
    public static void UnregisterSender(IConnectorFormDataSender connectorFormDataSender) => ConnectorFormsEventHandler.UnregisterSender(connectorFormDataSender.GetType());

    /// <summary>
    /// Unregisters the sender by provided type from the collection of senders that is used.
    /// to send form submission data.
    /// </summary>
    /// <param name="connectorFormDataSenderType">The form data sender type.</param>
    public static void UnregisterSender(Type connectorFormDataSenderType)
    {
      IConnectorFormDataSender connectorFormDataSender = (IConnectorFormDataSender) null;
      ConnectorFormsEventHandler.ConnectorFormDataSenders.TryRemove(connectorFormDataSenderType, out connectorFormDataSender);
    }

    /// <summary>Initializes this instance.</summary>
    internal void Initialize()
    {
      EventHub.Unsubscribe<IFormEntryCreatedEvent>(new SitefinityEventHandler<IFormEntryCreatedEvent>(this.FormEntryCreatedEventHandler));
      EventHub.Subscribe<IFormEntryCreatedEvent>(new SitefinityEventHandler<IFormEntryCreatedEvent>(this.FormEntryCreatedEventHandler));
    }

    /// <summary>Uninitializes this instance.</summary>
    internal void Uninitialize() => EventHub.Unsubscribe<IFormEntryCreatedEvent>(new SitefinityEventHandler<IFormEntryCreatedEvent>(this.FormEntryCreatedEventHandler));

    /// <summary>
    /// Handles the <see cref="T:Telerik.Sitefinity.Modules.Forms.Events.IFormEntryCreatedEvent" /> event.
    /// </summary>
    /// <param name="formEntryCreatedEvent">The form entry created event args.</param>
    protected virtual void FormEntryCreatedEventHandler(IFormEntryCreatedEvent formEntryCreatedEvent)
    {
      if (formEntryCreatedEvent == null)
        throw new ArgumentNullException(nameof (formEntryCreatedEvent));
      if (!ConnectorFormsEventHandler.ConnectorFormDataSenders.Any<KeyValuePair<Type, IConnectorFormDataSender>>())
        return;
      FormDescription form = this.GetForm(formEntryCreatedEvent.FormId);
      ConnectorFormDataContext dataContext = new ConnectorFormDataContext();
      dataContext.HttpRequest = SystemManager.CurrentHttpContext.Request;
      dataContext.SubmitPageUrl = this.GetSubmitPageUrl(SystemManager.CurrentHttpContext.Request);
      dataContext.FormDescriptionAttributeSettings = form.Attributes;
      IDictionary<string, IDictionary<string, string>> dictionary = (IDictionary<string, IDictionary<string, string>>) new Dictionary<string, IDictionary<string, string>>();
      if (form.Attributes != null && form.Attributes.ContainsKey(FormsDefinitions.DataMappingPropertyName))
        dictionary = this.GetFieldMappings(form.Attributes[FormsDefinitions.DataMappingPropertyName]);
      foreach (IConnectorFormDataSender connectorFormDataSender in (IEnumerable<IConnectorFormDataSender>) ConnectorFormsEventHandler.ConnectorFormDataSenders.Values)
      {
        if (!string.IsNullOrWhiteSpace(connectorFormDataSender.DesignerExtenderName))
          dataContext.WidgetDesignerSettings = this.GetWidgetDesignerSettings(SystemManager.CurrentHttpContext, connectorFormDataSender.DesignerExtenderName);
        if (connectorFormDataSender.ShouldSendFormData(dataContext))
        {
          string mappingExtenderKey = connectorFormDataSender.DataMappingExtenderKey;
          IDictionary<string, string> mappings = dictionary.ContainsKey(mappingExtenderKey) ? dictionary[mappingExtenderKey] : (IDictionary<string, string>) null;
          IDictionary<string, string> mappedFields = this.GetMappedFields(formEntryCreatedEvent.Controls, mappings);
          connectorFormDataSender.SendFormData(mappedFields, dataContext);
        }
      }
    }

    /// <summary>
    /// Gets the widget designer settings from the provided <see cref="T:System.Web.HttpContextBase" /> httpContext and designer extender name.
    /// </summary>
    /// <param name="httpContext">The httpContext used to get the designer settings.</param>
    /// <param name="designerExtenderName">The name of the designer extender, the settings for which will be returned.</param>
    /// <returns>The widgets settings dictionary.</returns>
    protected virtual IDictionary<string, string> GetWidgetDesignerSettings(
      HttpContextBase httpContext,
      string designerExtenderName)
    {
      if (httpContext == null)
        throw new ArgumentNullException(nameof (httpContext));
      if (string.IsNullOrWhiteSpace(designerExtenderName))
        throw new ArgumentException("designerExtenderName cannot be null, empty or whitespace");
      object obj1 = httpContext.Items[(object) "ConnectorSettings"];
      if (obj1 == null)
        return (IDictionary<string, string>) null;
      object obj2 = JsonConvert.DeserializeObject<object>(obj1.ToString());
      // ISSUE: reference to a compiler-generated field
      if (ConnectorFormsEventHandler.\u003C\u003Eo__6.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ConnectorFormsEventHandler.\u003C\u003Eo__6.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof (ConnectorFormsEventHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj3 = ConnectorFormsEventHandler.\u003C\u003Eo__6.\u003C\u003Ep__0.Target((CallSite) ConnectorFormsEventHandler.\u003C\u003Eo__6.\u003C\u003Ep__0, obj2, (object) null);
      // ISSUE: reference to a compiler-generated field
      if (ConnectorFormsEventHandler.\u003C\u003Eo__6.\u003C\u003Ep__5 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ConnectorFormsEventHandler.\u003C\u003Eo__6.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (ConnectorFormsEventHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      if (!ConnectorFormsEventHandler.\u003C\u003Eo__6.\u003C\u003Ep__5.Target((CallSite) ConnectorFormsEventHandler.\u003C\u003Eo__6.\u003C\u003Ep__5, obj3))
      {
        // ISSUE: reference to a compiler-generated field
        if (ConnectorFormsEventHandler.\u003C\u003Eo__6.\u003C\u003Ep__4 == null)
        {
          // ISSUE: reference to a compiler-generated field
          ConnectorFormsEventHandler.\u003C\u003Eo__6.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (ConnectorFormsEventHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, bool> target1 = ConnectorFormsEventHandler.\u003C\u003Eo__6.\u003C\u003Ep__4.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, bool>> p4 = ConnectorFormsEventHandler.\u003C\u003Eo__6.\u003C\u003Ep__4;
        // ISSUE: reference to a compiler-generated field
        if (ConnectorFormsEventHandler.\u003C\u003Eo__6.\u003C\u003Ep__3 == null)
        {
          // ISSUE: reference to a compiler-generated field
          ConnectorFormsEventHandler.\u003C\u003Eo__6.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.Or, typeof (ConnectorFormsEventHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, object, object> target2 = ConnectorFormsEventHandler.\u003C\u003Eo__6.\u003C\u003Ep__3.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, object, object>> p3 = ConnectorFormsEventHandler.\u003C\u003Eo__6.\u003C\u003Ep__3;
        object obj4 = obj3;
        // ISSUE: reference to a compiler-generated field
        if (ConnectorFormsEventHandler.\u003C\u003Eo__6.\u003C\u003Ep__2 == null)
        {
          // ISSUE: reference to a compiler-generated field
          ConnectorFormsEventHandler.\u003C\u003Eo__6.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof (ConnectorFormsEventHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, object, object> target3 = ConnectorFormsEventHandler.\u003C\u003Eo__6.\u003C\u003Ep__2.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, object, object>> p2 = ConnectorFormsEventHandler.\u003C\u003Eo__6.\u003C\u003Ep__2;
        // ISSUE: reference to a compiler-generated field
        if (ConnectorFormsEventHandler.\u003C\u003Eo__6.\u003C\u003Ep__1 == null)
        {
          // ISSUE: reference to a compiler-generated field
          ConnectorFormsEventHandler.\u003C\u003Eo__6.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.GetIndex(CSharpBinderFlags.None, typeof (ConnectorFormsEventHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj5 = ConnectorFormsEventHandler.\u003C\u003Eo__6.\u003C\u003Ep__1.Target((CallSite) ConnectorFormsEventHandler.\u003C\u003Eo__6.\u003C\u003Ep__1, obj2, designerExtenderName);
        object obj6 = target3((CallSite) p2, obj5, (object) null);
        object obj7 = target2((CallSite) p3, obj4, obj6);
        if (!target1((CallSite) p4, obj7))
        {
          // ISSUE: reference to a compiler-generated field
          if (ConnectorFormsEventHandler.\u003C\u003Eo__6.\u003C\u003Ep__8 == null)
          {
            // ISSUE: reference to a compiler-generated field
            ConnectorFormsEventHandler.\u003C\u003Eo__6.\u003C\u003Ep__8 = CallSite<Func<CallSite, Type, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "DeserializeObject", (IEnumerable<Type>) new Type[1]
            {
              typeof (IDictionary<string, string>)
            }, typeof (ConnectorFormsEventHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, Type, object, object> target4 = ConnectorFormsEventHandler.\u003C\u003Eo__6.\u003C\u003Ep__8.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, Type, object, object>> p8 = ConnectorFormsEventHandler.\u003C\u003Eo__6.\u003C\u003Ep__8;
          Type type = typeof (JsonConvert);
          // ISSUE: reference to a compiler-generated field
          if (ConnectorFormsEventHandler.\u003C\u003Eo__6.\u003C\u003Ep__7 == null)
          {
            // ISSUE: reference to a compiler-generated field
            ConnectorFormsEventHandler.\u003C\u003Eo__6.\u003C\u003Ep__7 = CallSite<Func<CallSite, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "ToString", (IEnumerable<Type>) null, typeof (ConnectorFormsEventHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, object, object> target5 = ConnectorFormsEventHandler.\u003C\u003Eo__6.\u003C\u003Ep__7.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, object, object>> p7 = ConnectorFormsEventHandler.\u003C\u003Eo__6.\u003C\u003Ep__7;
          // ISSUE: reference to a compiler-generated field
          if (ConnectorFormsEventHandler.\u003C\u003Eo__6.\u003C\u003Ep__6 == null)
          {
            // ISSUE: reference to a compiler-generated field
            ConnectorFormsEventHandler.\u003C\u003Eo__6.\u003C\u003Ep__6 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.GetIndex(CSharpBinderFlags.None, typeof (ConnectorFormsEventHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj8 = ConnectorFormsEventHandler.\u003C\u003Eo__6.\u003C\u003Ep__6.Target((CallSite) ConnectorFormsEventHandler.\u003C\u003Eo__6.\u003C\u003Ep__6, obj2, designerExtenderName);
          object obj9 = target5((CallSite) p7, obj8);
          object obj10 = target4((CallSite) p8, type, obj9);
          // ISSUE: reference to a compiler-generated field
          if (ConnectorFormsEventHandler.\u003C\u003Eo__6.\u003C\u003Ep__9 == null)
          {
            // ISSUE: reference to a compiler-generated field
            ConnectorFormsEventHandler.\u003C\u003Eo__6.\u003C\u003Ep__9 = CallSite<Func<CallSite, object, IDictionary<string, string>>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (IDictionary<string, string>), typeof (ConnectorFormsEventHandler)));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          return ConnectorFormsEventHandler.\u003C\u003Eo__6.\u003C\u003Ep__9.Target((CallSite) ConnectorFormsEventHandler.\u003C\u003Eo__6.\u003C\u003Ep__9, obj10);
        }
      }
      return (IDictionary<string, string>) null;
    }

    /// <summary>
    /// Gets the <see cref="T:Telerik.Sitefinity.Forms.Model.FormDescription" /> form using the provided form id.
    /// </summary>
    /// <param name="formId">The id of the form.</param>
    /// <returns>The <see cref="T:Telerik.Sitefinity.Forms.Model.FormDescription" /> from.</returns>
    protected virtual FormDescription GetForm(Guid formId) => FormsManager.GetManager().GetForm(formId);

    /// <summary>Gets the page URL where the form was submitted.</summary>
    /// <param name="httpRequest">The <see cref="T:System.Web.HttpRequestBase" /> that will be used to determine the form submit URL.</param>
    /// <returns>The page URL where the form was submitted.</returns>
    protected virtual Uri GetSubmitPageUrl(HttpRequestBase httpRequest)
    {
      Uri submitPageUrl = httpRequest != null ? httpRequest.UrlReferrer : throw new ArgumentNullException(nameof (httpRequest));
      if (submitPageUrl == (Uri) null || string.IsNullOrEmpty(submitPageUrl.AbsoluteUri))
        submitPageUrl = httpRequest.Url;
      return submitPageUrl;
    }

    /// <summary>
    /// Deserializes the provided JSON string into a field mapping dictionary.
    /// </summary>
    /// <param name="json">The JSON string that will be deserialized into a field mapping dictionary.</param>
    /// <returns>The field mapping dictionary.</returns>
    private IDictionary<string, IDictionary<string, string>> GetFieldMappings(
      string json)
    {
      return !string.IsNullOrEmpty(json) ? JsonConvert.DeserializeObject<IDictionary<string, IDictionary<string, string>>>(json) : (IDictionary<string, IDictionary<string, string>>) new Dictionary<string, IDictionary<string, string>>();
    }

    /// <summary>
    /// Creates the mapped name value dictionary from the provided submitted fields.
    /// </summary>
    /// <param name="submittedFields">The submitted fields.</param>
    /// <param name="mappings">The form fields mapping collection.</param>
    /// <returns>The dictionary created from the provided form</returns>
    private IDictionary<string, string> GetMappedFields(
      IEnumerable<IFormEntryEventControl> submittedFields,
      IDictionary<string, string> mappings)
    {
      if (submittedFields == null)
        throw new ArgumentNullException(nameof (submittedFields));
      IDictionary<string, string> mappedFields = (IDictionary<string, string>) new Dictionary<string, string>();
      foreach (IFormEntryEventControl submittedField in submittedFields)
      {
        string key = submittedField.FieldName;
        if (submittedField.Type == FormEntryEventControlType.FieldControl && !mappedFields.ContainsKey(key))
        {
          if (mappings != null && mappings.ContainsKey(key))
            key = mappings[key];
          string fieldValue = this.GetFieldValue(submittedField);
          mappedFields.Add(key, fieldValue);
        }
      }
      return mappedFields;
    }

    /// <summary>
    /// Gets the value of a <see cref="T:Telerik.Sitefinity.Modules.Forms.Events.IFormEntryEventControl" />.
    /// If the field control has a multiple selected values returns them comma separated. Otherwise returns the selected value as string.
    /// </summary>
    /// <param name="formEntryEventControl">The form entry field control.</param>
    /// <returns>If the field control has a multiple selected values returns them comma separated.
    /// Otherwise returns the selected value as string.</returns>
    private string GetFieldValue(IFormEntryEventControl formEntryEventControl)
    {
      if (formEntryEventControl == null)
        throw new ArgumentNullException(nameof (formEntryEventControl));
      string fieldValue = string.Empty;
      if (formEntryEventControl.Value is IEnumerable<string>)
        fieldValue = string.Join(", ", (IEnumerable<string>) (formEntryEventControl.Value as List<string>));
      else if (formEntryEventControl.Value != null)
        fieldValue = formEntryEventControl.Value.ToString();
      return fieldValue;
    }
  }
}
