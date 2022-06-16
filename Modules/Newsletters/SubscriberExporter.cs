// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.SubscriberExporter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Telerik.Sitefinity.Newsletters.Model;

namespace Telerik.Sitefinity.Modules.Newsletters
{
  /// <summary>
  /// This class provides functionality for exporting Sitefinity subscribers
  /// into .csv and .txt formats.
  /// </summary>
  public class SubscriberExporter
  {
    private string newslettersProviderName;
    private NewslettersManager newslettersManager;

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.SubscriberExporter" /> and uses
    /// default newsletters module provider for exporting subscribers.
    /// </summary>
    public SubscriberExporter()
    {
    }

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.SubscriberExporter" /> type.
    /// </summary>
    /// <param name="newslettersProviderName">
    /// Name of the newsletters provider to be used when
    /// exporting subscribers.
    /// </param>
    public SubscriberExporter(string newslettersProviderName) => this.newslettersProviderName = newslettersProviderName;

    /// <summary>
    /// Gets the instance of the <see cref="P:Telerik.Sitefinity.Modules.Newsletters.SubscriberExporter.NewslettersManager" /> to be used
    /// for exporting subscribers.
    /// </summary>
    protected NewslettersManager NewslettersManager
    {
      get
      {
        if (this.newslettersManager == null)
          this.newslettersManager = NewslettersManager.GetManager(this.newslettersProviderName);
        return this.newslettersManager;
      }
    }

    /// <summary>
    /// Exports subscribers to comma separated list of values.
    /// </summary>
    /// <param name="listIds">The array with mailing list ids to which the subscribers ought to be exported.</param>
    /// <param name="doNotExportSubscribersExistingEmails">
    /// If true will skip exporting of the subscribers that have same emails; otherwise will
    /// export subscribers with same email.
    /// </param>
    /// <param name="shouldExportAllSubscribers">
    /// If true will export all subscribers; otherwise will export only subscribers that belong to selected mailing lists or
    /// subscribers that do not belong on any list.
    /// </param>
    public string ExportToCommaSeparatedList(
      Guid[] listIds,
      bool doNotExportSubscribersSameEmails,
      bool shouldExportAllSubscribers)
    {
      string delimiter = ", ";
      return shouldExportAllSubscribers ? this.ExportAllSubscribers(doNotExportSubscribersSameEmails, delimiter) : this.ExportSubscribersInMailingLists(doNotExportSubscribersSameEmails, listIds, delimiter);
    }

    /// <summary>Exports subscribers to tab separated list of values.</summary>
    /// <param name="listIds">The array with mailing list ids to which the subscribers ought to be exported.</param>
    /// <param name="doNotExportSubscribersExistingEmails">
    /// If true will skip exporting of the subscribers that have same emails; otherwise will
    /// export subscribers with same email.
    /// </param>
    /// <param name="shouldExportAllSubscribers">
    /// If true will export all subscribers; otherwise will export only subscribers that belong to selected mailing lists or
    /// subscribers that do not belong on any list.
    /// </param>
    public string ExportToTabSeparatedList(
      Guid[] listIds,
      bool doNotExportSubscribersSameEmails,
      bool shouldExportAllSubscribers)
    {
      string delimiter = "\t";
      return shouldExportAllSubscribers ? this.ExportAllSubscribers(doNotExportSubscribersSameEmails, delimiter) : this.ExportSubscribersInMailingLists(doNotExportSubscribersSameEmails, listIds, delimiter);
    }

    /// <summary>
    /// Exports subscribers to comma separated list of values.
    /// </summary>
    /// 
    ///             /// <param name="listIds">The id of the mailing list from which the subscribers ought to be exported.</param>
    /// <param name="doNotExportSubscribersExistingEmails">
    /// If true will skip exporting of the subscribers that have same emails; otherwise will
    /// export subscribers with same email.
    /// </param>
    /// <param name="shouldExportAllSubscribers">
    /// If true will export all subscribers; otherwise will export only subscribers that belong to selected mailing lists or
    /// subscribers that do not belong on any list.
    /// </param>
    public string ExportToCommaSeparatedList(
      Guid listId,
      bool doNotExportSubscribersExistingEmails,
      bool shouldExportAllSubscribers)
    {
      return this.ExportToCommaSeparatedList(new Guid[1]
      {
        listId
      }, doNotExportSubscribersExistingEmails, shouldExportAllSubscribers);
    }

    /// <summary>Exports subscribers to tab separated list of values.</summary>
    /// <param name="listId">The id of the mailing list from which the subscribers ought to be exported.</param>
    /// <param name="doNotExportSubscribersExistingEmails">
    /// If true will skip exporting of the subscribers that have same emails; otherwise will
    /// export subscribers with same email.
    /// </param>
    /// <param name="shouldExportAllSubscribers">
    /// If true will export all subscribers; otherwise will export only subscribers that belong to selected mailing lists or
    /// subscribers that do not belong on any list.
    /// </param>
    public string ExportToTabSeparatedList(
      Guid listId,
      bool doNotExportSubscribersExistingEmails,
      bool shouldExportAllSubscribers)
    {
      return this.ExportToTabSeparatedList(new Guid[1]
      {
        listId
      }, doNotExportSubscribersExistingEmails, shouldExportAllSubscribers);
    }

    private string ExportAllSubscribers(bool doNotExportSubscribersExistingEmails, string delimiter)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendLine("Email: " + delimiter + "First Name: " + delimiter + "Last Name: ");
      IQueryable<Subscriber> source = !doNotExportSubscribersExistingEmails ? this.NewslettersManager.GetSubscribers() : this.NewslettersManager.GetSubscribers().Distinct<Subscriber>((IEqualityComparer<Subscriber>) new SubscriberExporter.SubscriberComparer());
      Expression<Func<Subscriber, string>> keySelector = (Expression<Func<Subscriber, string>>) (s => s.Email);
      foreach (Subscriber subscriber in (IEnumerable<Subscriber>) source.OrderBy<Subscriber, string>(keySelector))
        stringBuilder.AppendLine(subscriber.Email + delimiter + subscriber.FirstName + delimiter + subscriber.LastName);
      return stringBuilder.ToString();
    }

    private string ExportSubscribersInMailingLists(
      bool doNotExportSubscribersExistingEmails,
      Guid[] listIds,
      string delimiter)
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (listIds == null)
      {
        IQueryable<Subscriber> source1 = this.NewslettersManager.GetSubscribers().Where<Subscriber>((Expression<Func<Subscriber, bool>>) (s => s.Lists.Count == 0));
        IQueryable<Subscriber> source2 = !doNotExportSubscribersExistingEmails ? source1 : source1.Distinct<Subscriber>((IEqualityComparer<Subscriber>) new SubscriberExporter.SubscriberComparer());
        Expression<Func<Subscriber, string>> keySelector = (Expression<Func<Subscriber, string>>) (s => s.Email);
        foreach (Subscriber subscriber in (IEnumerable<Subscriber>) source2.OrderBy<Subscriber, string>(keySelector))
          stringBuilder.AppendLine(subscriber.Email + delimiter + subscriber.FirstName + delimiter + subscriber.LastName);
      }
      else
      {
        foreach (Guid listId in listIds)
        {
          MailingList mailingList = this.NewslettersManager.GetMailingList(listId);
          IQueryable<Subscriber> source = (IQueryable<Subscriber>) this.NewslettersManager.GetSubscribers().Where<Subscriber>((Expression<Func<Subscriber, bool>>) (s => s.Lists.Contains(mailingList))).OrderBy<Subscriber, string>((Expression<Func<Subscriber, string>>) (s => s.Email));
          if (doNotExportSubscribersExistingEmails)
            source = source.Distinct<Subscriber>((IEqualityComparer<Subscriber>) new SubscriberExporter.SubscriberComparer());
          foreach (Subscriber subscriber in (IEnumerable<Subscriber>) source)
            stringBuilder.AppendLine(subscriber.Email + delimiter + subscriber.FirstName + delimiter + subscriber.LastName);
        }
      }
      stringBuilder.Insert(0, "Email: " + delimiter + "First Name: " + delimiter + "Last Name: \n");
      return stringBuilder.ToString();
    }

    private class SubscriberComparer : IEqualityComparer<Subscriber>
    {
      public bool Equals(Subscriber x, Subscriber y)
      {
        if (x == y)
          return true;
        return x != null && y != null && x.Email == y.Email;
      }

      public int GetHashCode(Subscriber subscriber) => subscriber == null || subscriber.Email == null ? 0 : subscriber.Email.GetHashCode();
    }
  }
}
