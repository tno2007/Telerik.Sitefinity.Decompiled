// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.SiteSync.FormsImporter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Fluent;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Publishing;
using Telerik.Sitefinity.SitefinityExceptions;

namespace Telerik.Sitefinity.SiteSync
{
  internal class FormsImporter : PagesImporter
  {
    private Guid currentFormDescriptionId;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.SiteSync.FormsImporter" /> class.
    /// </summary>
    public FormsImporter()
      : this((string) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.SiteSync.FormsImporter" /> class.
    /// </summary>
    /// <param name="registrationPrefix">The registration prefix.</param>
    public FormsImporter(string registrationPrefix) => this.RegistrationPrefix = registrationPrefix;

    protected override void SetFacadeManager(Type itemType, Telerik.Sitefinity.Fluent.IDataItemFacade.IDataItemFacade facade)
    {
      if (typeof (FormControl).IsAssignableFrom(itemType))
        facade.SetManagerType(typeof (FormsManager));
      else if (typeof (FormDraftControl).IsAssignableFrom(itemType))
        facade.SetManagerType(typeof (FormsManager));
      else if (typeof (FormDescription).IsAssignableFrom(itemType))
        facade.SetManagerType(typeof (FormsManager));
      else if (typeof (FormDraft).IsAssignableFrom(itemType))
        facade.SetManagerType(typeof (FormsManager));
      else if (typeof (ControlData).IsAssignableFrom(itemType))
        facade.SetManagerType(typeof (FormsManager));
      else if (typeof (ControlProperty).IsAssignableFrom(itemType))
        facade.SetManagerType(typeof (FormsManager));
      else if (typeof (ObjectData).IsAssignableFrom(itemType))
        facade.SetManagerType(typeof (FormsManager));
      else
        base.SetFacadeManager(itemType, facade);
    }

    protected override void SetAdditionalValues(
      IDataItem dataItem,
      string provider,
      WrapperObject wrapperObject,
      FluentSitefinity fluent,
      ISiteSyncImportTransaction transaction)
    {
      if (transaction.Headers.ContainsKey("MultisiteMigrationSource"))
      {
        string header = transaction.Headers["MultisiteMigrationSource"];
        this.PrepareData(dataItem, provider, wrapperObject, fluent, header);
        base.SetAdditionalValues(dataItem, provider, wrapperObject, fluent, transaction);
      }
      else
      {
        base.SetAdditionalValues(dataItem, provider, wrapperObject, fluent, transaction);
        if (!(dataItem is FormDescription formDescription))
          return;
        this.currentFormDescriptionId = formDescription.Id;
      }
    }

    protected virtual void PrepareData(
      IDataItem dataItem,
      string provider,
      WrapperObject wrapperObject,
      FluentSitefinity fluent,
      string siteName)
    {
      string formSuffix = this.GetFormSuffix(siteName);
      FormDescription formDescription = dataItem as FormDescription;
      if (formDescription != null)
      {
        string transactionName = fluent.AppSettings.TransactionName;
        this.currentFormDescriptionId = formDescription.Id;
        FormDescription formDescription1 = FormsManager.GetManager(provider, transactionName).GetForms().Where<FormDescription>((Expression<Func<FormDescription, bool>>) (p => p.Id == formDescription.Id)).SingleOrDefault<FormDescription>();
        if (formDescription1 == null)
        {
          if (((FormDescription) dataItem).Name.Contains(formSuffix) || ((Content) dataItem).Title.Contains(formSuffix))
            return;
          ((FormDescription) dataItem).Name = string.Format("{0}_{1}", (object) ((FormDescription) dataItem).Name, (object) formSuffix);
          ((Content) dataItem).Title = (Lstring) string.Format("{0}_{1}", (object) ((Content) dataItem).Title, (object) formSuffix);
        }
        else
        {
          if (formDescription1.Title.Contains(formSuffix))
            return;
          ((FormDescription) dataItem).Name = string.Format("{0}_{1}", (object) ((FormDescription) dataItem).Name, (object) formSuffix);
          ((Content) dataItem).Title = (Lstring) string.Format("{0}_{1}", (object) ((Content) dataItem).Title, (object) formSuffix);
        }
      }
      else
      {
        switch (dataItem)
        {
          case FormDraftControl _:
            if (((FormDraftControl) dataItem).Form == null || ((FormDraftControl) dataItem).Form.Name.Contains(formSuffix))
              break;
            ((FormDraftControl) dataItem).Form.Name = string.Format("{0}_{1}", (object) ((FormDraftControl) dataItem).Form.Name, (object) formSuffix);
            break;
          case FormDraft _:
            if (((FormDraft) dataItem).Name.Contains(formSuffix))
              break;
            ((FormDraft) dataItem).Name = string.Format("{0}_{1}", (object) ((FormDraft) dataItem).Name, (object) formSuffix);
            break;
        }
      }
    }

    protected override void OnTransactionComitted()
    {
      base.OnTransactionComitted();
      if (this.currentFormDescriptionId == Guid.Empty)
        return;
      FormsManager manager = FormsManager.GetManager();
      FormDescription formDescription = (FormDescription) null;
      try
      {
        formDescription = manager.GetForm(this.currentFormDescriptionId);
      }
      catch (ItemNotFoundException ex)
      {
      }
      if (formDescription == null)
        return;
      manager.BuildDynamicType(formDescription);
      MetadataManager.GetManager().SaveChanges();
    }

    protected override void ProcessDataItem(
      IDataItem dataItem,
      string provider,
      WrapperObject wrapperObject,
      FluentSitefinity fluent,
      ISiteSyncImportTransaction importTransaction)
    {
      switch (dataItem)
      {
        case FormControl _:
          FormControl formControl = (FormControl) dataItem;
          formControl.Properties.Clear();
          object propertyOrNull1 = wrapperObject.GetPropertyOrNull("ParentControlPropertyId");
          if (propertyOrNull1 != null)
          {
            IDataItem dataItem1 = this.GetFacade(typeof (FormDescription), fluent, provider).Load((Guid) propertyOrNull1).Get();
            formControl.Form = (FormDescription) dataItem1;
          }
          object actualFieldName = wrapperObject.GetPropertyOrNull("ActualMetaFieldName");
          if (actualFieldName == null)
            break;
          FormsManager manager = (FormsManager) this.GetFacade(typeof (FormDescription), fluent, provider).Manager;
          MetaType metaType = MetadataManager.GetManager().GetMetaType(manager.Provider.FormsNamespace, formControl.Form.Name);
          if (metaType != null)
          {
            if (metaType.Fields.Any<MetaField>((Func<MetaField, bool>) (f => f.FieldName == (string) actualFieldName)))
              break;
            formControl.Published = false;
            break;
          }
          formControl.Published = false;
          break;
        case FormDraftControl _:
          FormDraftControl formDraftControl = (FormDraftControl) dataItem;
          formDraftControl.Properties.Clear();
          object propertyOrNull2 = wrapperObject.GetPropertyOrNull("ParentControlPropertyId");
          if (propertyOrNull2 == null)
            break;
          IDataItem dataItem2 = this.GetFacade(typeof (FormDraft), fluent, provider).Load((Guid) propertyOrNull2).Get();
          formDraftControl.Form = (FormDraft) dataItem2;
          break;
        case FormDescription _:
          FormDescription formDescription = (FormDescription) dataItem;
          IList<FormControl> controls1 = formDescription.Controls;
          if (controls1.Any<FormControl>())
          {
            for (int index = controls1.Count - 1; index >= 0; --index)
              this.itemsToRemove.Add((object) controls1[index]);
          }
          List<Guid> propertyOrDefault1 = wrapperObject.GetPropertyOrDefault<List<Guid>>("SiteIds");
          if (propertyOrDefault1 != null)
            this.LinkItemToSites((IDataItem) formDescription, (IList<Guid>) propertyOrDefault1, fluent);
          List<string> propertyOrDefault2 = wrapperObject.GetPropertyOrDefault<List<string>>("SubscribersEmails");
          if (propertyOrDefault2 == null)
            break;
          FormsManager.GetManager((string) null, fluent.AppSettings.TransactionName);
          FormsExtensions.CreateSubscriptionList(formDescription, formDescription.SubscriptionListId, (IList<string>) propertyOrDefault2, (Action<Guid>) (id => formDescription.SubscriptionListId = id));
          break;
        case FormDraft _:
          FormDraft formDraft = (FormDraft) dataItem;
          IList<FormDraftControl> controls2 = formDraft.Controls;
          if (controls2.Any<FormDraftControl>())
          {
            for (int index = controls2.Count - 1; index >= 0; --index)
              this.itemsToRemove.Add((object) controls2[index]);
          }
          object propertyOrNull3 = wrapperObject.GetPropertyOrNull("ParentId");
          if (propertyOrNull3 == null)
            break;
          FormDescription formDescription1 = (FormDescription) fluent.DataItemFacade(typeof (FormDescription), (Guid) propertyOrNull3).Get();
          formDraft.ParentForm = formDescription1;
          break;
        default:
          base.ProcessDataItem(dataItem, provider, wrapperObject, fluent, importTransaction);
          break;
      }
    }

    protected override void ValidateDataItemConstraints(
      IDataItem dataItem,
      IManager manager,
      ISiteSyncImportTransaction importTransaction)
    {
      if (!(dataItem is FormDescription formDescription))
        return;
      ((FormsManager) manager).ValidateConstraints(formDescription.Name, formDescription.Id);
    }

    private string GetFormSuffix(string siteName) => new Regex("[^a-zA-Z]").Replace(siteName, string.Empty);
  }
}
