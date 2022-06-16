// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.DynamicLists.Providers.FormsDynamicListProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Web.UI;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Modules.Forms.Configuration;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Modules.Newsletters.Composition;
using Telerik.Sitefinity.Modules.Newsletters.Data;
using Telerik.Sitefinity.Newsletters.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Modules.Newsletters.DynamicLists.Providers
{
  /// <summary>
  /// Dynamic lists provider that supplies all the forms as dynamic lists.
  /// </summary>
  public class FormsDynamicListProvider : IDynamicListProvider
  {
    public const string providerName = "FormsDynamicListProvider";

    /// <summary>
    /// Gets all the dynamic lists provided by the instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.DynamicLists.IDynamicListProvider" />.
    /// </summary>
    public IEnumerable<DynamicListInfo> GetDynamicLists()
    {
      List<DynamicListInfo> dynamicLists = new List<DynamicListInfo>();
      if (this.GetFormsConfig() != null)
      {
        foreach (DataProviderSettings providerSettings in (IEnumerable<DataProviderSettings>) this.GetFormsConfig().Providers.Values)
        {
          foreach (FormDescription formDescription in this.GetFormDescriptions(providerSettings.Name))
            dynamicLists.Add(new DynamicListInfo(nameof (FormsDynamicListProvider), this.GetFormKey(formDescription, providerSettings.Name), string.Format("{0} ({1})", (object) formDescription.Title, (object) providerSettings.Name)));
        }
      }
      return (IEnumerable<DynamicListInfo>) dynamicLists;
    }

    /// <summary>
    /// Gets all the available properties of the items inside of the dynamic list.
    /// </summary>
    /// <param name="listKey">The key used to identify the list.</param>
    /// <returns>An enumerable of the property descriptor objects.</returns>
    public IList<MergeTag> GetAvailableProperties(string key)
    {
      List<MergeTag> availableProperties = new List<MergeTag>();
      FormsManager formsManager = (FormsManager) null;
      FormDescription formDescription = this.GetFormDescription(key, out formsManager);
      foreach (FormControl control1 in (IEnumerable<FormControl>) formDescription.Controls)
      {
        if (formsManager.LoadControl((ObjectData) control1, (CultureInfo) null) is IFieldControl fieldControl)
        {
          availableProperties.Add(new MergeTag(fieldControl.Title, ((IFormFieldControl) fieldControl).MetaField.FieldName, formDescription.Name));
        }
        else
        {
          Control control2 = formsManager.LoadControl((ObjectData) control1, (CultureInfo) null);
          if (ObjectFactory.Resolve<IControlBehaviorResolver>().GetBehaviorObject(control2) is IFormFieldControl behaviorObject)
            availableProperties.Add(new MergeTag(behaviorObject.MetaField.Title, behaviorObject.MetaField.FieldName, formDescription.Name));
        }
      }
      return (IList<MergeTag>) availableProperties;
    }

    /// <summary>Gets all the subscribers from the dynamic list.</summary>
    /// <param name="listKey">The key of the list.</param>
    /// <returns>An enumerable of objects representing the subscribers.</returns>
    public IEnumerable<object> GetSubscribers(string listKey)
    {
      IQueryable<FormEntry> entries;
      return this.TryGetFormEntries(listKey, out entries) ? (IEnumerable<object>) entries : (IEnumerable<object>) new List<object>();
    }

    /// <summary>
    /// Gets the subscribers from the dynamic list by given filter expression and paging.
    /// </summary>
    /// <param name="listKey">The list key.</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="orderExpression">The order expression.</param>
    /// <param name="skip">The skip.</param>
    /// <param name="take">The take.</param>
    /// <param name="totalCount">The total count.</param>
    /// <returns>An enumerable of objects representing the subscribers.</returns>
    public IEnumerable<object> GetSubscribers(
      string listKey,
      string filterExpression,
      string orderExpression,
      int? skip,
      int? take,
      ref int? totalCount)
    {
      IQueryable<FormEntry> entries;
      if (!this.TryGetFormEntries(listKey, out entries))
        return (IEnumerable<object>) new List<object>();
      if (!string.IsNullOrEmpty(filterExpression))
        entries = entries.Where<FormEntry>(filterExpression);
      totalCount = new int?(entries.Count<FormEntry>());
      if (!string.IsNullOrEmpty(orderExpression))
        entries = entries.OrderBy<FormEntry>(orderExpression);
      int? nullable;
      if (skip.HasValue)
      {
        nullable = skip;
        int num = 0;
        if (nullable.GetValueOrDefault() > num & nullable.HasValue)
          entries = entries.Skip<FormEntry>(skip.Value);
      }
      if (take.HasValue)
      {
        nullable = take;
        int num = 0;
        if (nullable.GetValueOrDefault() > num & nullable.HasValue)
          entries = entries.Take<FormEntry>(take.Value);
      }
      return (IEnumerable<object>) entries;
    }

    /// <summary>Resolves the merge tag to the actual value.</summary>
    /// <param name="mergeTag">Name of the merge tag.</param>
    /// <param name="instance">Instance from which the merge tag should be resolved.</param>
    /// <returns>The value of the resolved merge tag.</returns>
    public object ResolveMergeTag(MergeTag mergeTag, object instance) => TypeDescriptor.GetProperties(instance)[mergeTag.PropertyName].GetValue(instance);

    /// <summary>Gets the number of subscribers in dynamic list.</summary>
    /// <returns>Number of subscribers in dynamic list.</returns>
    public int SubscribersCount(string listKey)
    {
      string formsProviderName = (string) null;
      Guid formsDescriptionId = Guid.Empty;
      this.ResolveFormKey(listKey, out formsDescriptionId, out formsProviderName);
      NewslettersManager manager1 = NewslettersManager.GetManager();
      string transactionName = "getFormEntries" + Guid.NewGuid().ToString();
      FormsManager manager2 = FormsManager.GetManager(formsProviderName, transactionName);
      manager2.Provider.SuppressSecurityChecks = true;
      FormDescription formDescription = manager2.GetForms().FirstOrDefault<FormDescription>((Expression<Func<FormDescription, bool>>) (f => f.Id == formsDescriptionId));
      int num;
      if (formDescription != null)
      {
        if (manager1.Provider is OpenAccessNewslettersDataProvider provider)
        {
          MergeTag emailMergeTag = new MergeTag(provider.GetContext().GetAll<DynamicListSettings>().Where<DynamicListSettings>((Expression<Func<DynamicListSettings, bool>>) (s => s.ListKey == listKey)).FirstOrDefault<DynamicListSettings>().EmailMappedField);
          num = manager2.GetFormEntries(formDescription).GroupBy<FormEntry, string>((Expression<Func<FormEntry, string>>) (r => r.GetValue<string>(emailMergeTag.PropertyName))).Count<IGrouping<string, FormEntry>>();
        }
        else
          num = manager2.GetFormEntries(formDescription).Count<FormEntry>();
      }
      else
        num = 0;
      manager2.Provider.SuppressSecurityChecks = false;
      return num;
    }

    protected FormsConfig GetFormsConfig() => ObjectFactory.GetArgsForType(typeof (ConfigSection)).Any<RegisterEventArgs>((Func<RegisterEventArgs, bool>) (t => t.TypeTo == typeof (FormsConfig))) ? Config.Get<FormsConfig>() : (FormsConfig) null;

    protected IEnumerable<FormDescription> GetFormDescriptions(
      string providerName)
    {
      return (IEnumerable<FormDescription>) FormsManager.GetManager(providerName).GetForms();
    }

    protected FormDescription GetFormDescription(
      string key,
      out FormsManager formsManager)
    {
      string[] strArray = key.Split(new string[3]
      {
        "|",
        "%7C",
        "%7C".ToLower()
      }, StringSplitOptions.None);
      string g = strArray[0];
      string providerName = strArray[1];
      formsManager = FormsManager.GetManager(providerName);
      Guid formId = new Guid(g);
      return formsManager.GetForms().Where<FormDescription>((Expression<Func<FormDescription, bool>>) (f => f.Id == formId)).SingleOrDefault<FormDescription>();
    }

    private bool TryGetFormEntries(string listKey, out IQueryable<FormEntry> entries)
    {
      string formsProviderName = (string) null;
      Guid formsDescriptionId = Guid.Empty;
      entries = (IQueryable<FormEntry>) null;
      this.ResolveFormKey(listKey, out formsDescriptionId, out formsProviderName);
      FormsManager manager = FormsManager.GetManager(formsProviderName);
      manager.Provider.SuppressSecurityChecks = true;
      FormDescription formDescription = manager.GetForms().FirstOrDefault<FormDescription>((Expression<Func<FormDescription, bool>>) (f => f.Id == formsDescriptionId));
      int num = formDescription != null ? 1 : 0;
      if (num != 0)
        entries = manager.GetFormEntries(formDescription);
      manager.Provider.SuppressSecurityChecks = false;
      return num != 0;
    }

    private string GetFormKey(FormDescription formDescription, string providerName) => formDescription.Id.ToString() + "|" + providerName;

    private void ResolveFormKey(
      string key,
      out Guid formDescriptionId,
      out string formsProviderName)
    {
      string[] strArray = key.Split(new string[3]
      {
        "|",
        "%7C",
        "%7C".ToLower()
      }, StringSplitOptions.None);
      formDescriptionId = new Guid(strArray[0]);
      formsProviderName = strArray[1];
    }
  }
}
