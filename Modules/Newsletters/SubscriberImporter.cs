// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.SubscriberImporter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Newsletters.Model;

namespace Telerik.Sitefinity.Modules.Newsletters
{
  /// <summary>
  /// This class provides functionality for converting imported formats of subscribers
  /// into Sitefinity subcribers.
  /// </summary>
  public class SubscriberImporter
  {
    private string newslettersProviderName;
    private NewslettersManager newslettersManager;

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.SubscriberImporter" /> and uses
    /// default newsletters module provider for improting subscribers.
    /// </summary>
    public SubscriberImporter()
    {
    }

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.SubscriberImporter" /> type.
    /// </summary>
    /// <param name="newslettersProviderName">
    /// Name of the newsletters provider to be used when
    /// importing subscribers.
    /// </param>
    public SubscriberImporter(string newslettersProviderName) => this.newslettersProviderName = newslettersProviderName;

    /// <summary>
    /// Gets the instance of the <see cref="P:Telerik.Sitefinity.Modules.Newsletters.SubscriberImporter.NewslettersManager" /> to be used
    /// for importing subscribers.
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
    /// Imports the subscribers from the comma separated list of values.
    /// </summary>
    /// <param name="fileStream">The file stream representing the document with comma separated items.</param>
    /// <param name="listIds">The array with mailing list ids to which the subscribers ought to be imported.</param>
    /// <param name="overrideExistingSubscribers">
    /// If true will override first and last name of the subscribers with the same email with the imported ones; otherwise will skip
    /// importing of the subscribers that have same emails.
    /// </param>
    /// <param name="skipFirstRow">
    /// If true first row of the file will be skipped (headers); otherwise false.
    /// </param>
    internal bool Import(
      Stream fileStream,
      Guid[] listIds,
      int firstNameColumnIndex,
      int lastNameColumnIndex,
      int emailColumnIndex,
      bool overrideExistingSubscribers,
      bool skipFirstRow,
      char separator)
    {
      StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8, true, 81920);
      HashSet<string> stringSet = new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      int num1 = 3000;
      int num2 = 1000;
      int num3 = 0;
      List<MailingList> mailingLists = new List<MailingList>();
      if (listIds.Length != 0)
        mailingLists.AddRange((IEnumerable<MailingList>) this.NewslettersManager.GetMailingLists().Where<MailingList>((Expression<Func<MailingList, bool>>) (ml => listIds.Contains<Guid>(ml.Id))));
      List<Subscriber> subscriberList = new List<Subscriber>();
      if (skipFirstRow)
        streamReader.ReadLine();
      string str1 = streamReader.ReadLine();
      while (!string.IsNullOrEmpty(str1))
      {
        string[] strArray = str1.Split(separator);
        int num4 = strArray.Length - 1;
        string str2;
        if (num4 >= emailColumnIndex)
          str2 = strArray[emailColumnIndex].Trim().Trim('"');
        else
          str2 = (string) null;
        string str3;
        if (firstNameColumnIndex > -1 && num4 >= firstNameColumnIndex)
          str3 = strArray[firstNameColumnIndex].Trim().Trim('"');
        else
          str3 = (string) null;
        string str4;
        if (lastNameColumnIndex > -1 && num4 >= lastNameColumnIndex)
          str4 = strArray[lastNameColumnIndex].Trim().Trim('"');
        else
          str4 = (string) null;
        if (!string.IsNullOrEmpty(str2) && !stringSet.Contains(str2))
        {
          subscriberList.Add(new Subscriber()
          {
            Email = str2,
            FirstName = str3,
            LastName = str4
          });
          stringSet.Add(str2);
          if (subscriberList.Count<Subscriber>() >= num2)
          {
            this.BatchInsertSubscribers(subscriberList, mailingLists, overrideExistingSubscribers);
            num3 += subscriberList.Count<Subscriber>();
            subscriberList.Clear();
            if (num3 >= num1)
            {
              this.NewslettersManager.SaveChanges();
              num3 = 0;
              stringSet.Clear();
              if (listIds.Length != 0)
              {
                mailingLists.Clear();
                mailingLists.AddRange((IEnumerable<MailingList>) this.NewslettersManager.GetMailingLists().Where<MailingList>((Expression<Func<MailingList, bool>>) (ml => listIds.Contains<Guid>(ml.Id))));
              }
            }
          }
          else
            continue;
        }
        str1 = streamReader.ReadLine();
      }
      this.BatchInsertSubscribers(subscriberList, mailingLists, overrideExistingSubscribers);
      this.NewslettersManager.SaveChanges();
      return true;
    }

    private void BatchInsertSubscribers(
      List<Subscriber> subscriberDataToProccess,
      List<MailingList> mailingLists,
      bool overrideExistingUsers)
    {
      IEnumerable<string> subscriberToProccessEmails = subscriberDataToProccess.Select<Subscriber, string>((Func<Subscriber, string>) (s => s.Email));
      List<Subscriber> list;
      if (this.NewslettersManager.Provider is IOpenAccessDataProvider)
        list = ((IOpenAccessDataProvider) this.NewslettersManager.Provider).GetContext().GetAll<Subscriber>().Where<Subscriber>((Expression<Func<Subscriber, bool>>) (s => subscriberToProccessEmails.Contains<string>(s.Email) && s.ApplicationName == this.NewslettersManager.Provider.ApplicationName)).ToList<Subscriber>();
      else
        list = this.NewslettersManager.GetSubscribers().Where<Subscriber>((Expression<Func<Subscriber, bool>>) (s => subscriberToProccessEmails.Contains<string>(s.Email))).ToList<Subscriber>();
      Subscriber currentSubscriberData = (Subscriber) null;
      for (int index = 0; index < subscriberDataToProccess.Count<Subscriber>(); ++index)
      {
        currentSubscriberData = subscriberDataToProccess[index];
        Subscriber subscriber = list.FirstOrDefault<Subscriber>((Func<Subscriber, bool>) (s => s.Email == currentSubscriberData.Email));
        if (subscriber != null)
        {
          if (overrideExistingUsers)
            this.UpdateSubscriber(subscriber, currentSubscriberData.FirstName, currentSubscriberData.LastName);
        }
        else
          subscriber = this.AddSubscriber(currentSubscriberData.Email, currentSubscriberData.FirstName, currentSubscriberData.LastName);
        foreach (MailingList mailingList in mailingLists)
        {
          if (!subscriber.Lists.Contains(mailingList))
            subscriber.Lists.Add(mailingList);
        }
      }
    }

    /// <summary>
    /// Imports the subscribers from the comma separated list of values.
    /// </summary>
    /// <param name="fileStream">The file stream representing the document with comma separated items.</param>
    /// <param name="listId">The id of the mailing list to which the subscribers ought to be imported.</param>
    /// <param name="overrideExistingSubscribers">
    /// If true will override first and last name of the subscribers with the same email with the imported ones; otherwise will skip
    /// importing of the subscribers that have same emails.
    /// </param>
    /// <param name="skipFirstRow">
    /// If true first row of the file will be skipped (headers); otherwise false.
    /// </param>
    public bool ImportFromCommaSeparatedList(
      Stream fileStream,
      Guid listId,
      int firstNameColumnIndex,
      int lastNameColumnIndex,
      int emailColumnIndex,
      bool overrideExistingSubscribers,
      bool skipFirstRow)
    {
      return this.Import(fileStream, new Guid[1]{ listId }, firstNameColumnIndex, lastNameColumnIndex, emailColumnIndex, (overrideExistingSubscribers ? 1 : 0) != 0, (skipFirstRow ? 1 : 0) != 0, ',');
    }

    /// <summary>
    /// Imports the subscribers from the comma separated list of values.
    /// </summary>
    /// <param name="fileStream">The file stream representing the document with comma separated items.</param>
    /// <param name="listIds">The array with mailing list ids to which the subscribers ought to be imported.</param>
    /// <param name="overrideExistingSubscribers">
    /// If true will override first and last name of the subscribers with the same email with the imported ones; otherwise will skip
    /// importing of the subscribers that have same emails.
    /// </param>
    /// <param name="skipFirstRow">
    /// If true first row of the file will be skipped (headers); otherwise false.
    /// </param>
    public bool ImportFromCommaSeparatedList(
      Stream fileStream,
      Guid[] listIds,
      int firstNameColumnIndex,
      int lastNameColumnIndex,
      int emailColumnIndex,
      bool overrideExistingSubscribers,
      bool skipFirstRow)
    {
      return this.Import(fileStream, listIds, firstNameColumnIndex, lastNameColumnIndex, emailColumnIndex, overrideExistingSubscribers, skipFirstRow, ',');
    }

    /// <summary>
    /// Imports the subscribers from the tab separated list of values.
    /// </summary>
    /// <param name="fileStream">The file stream representing the document with tab separated items.</param>
    /// <param name="mailingListId">The id of the mailing list to which the subscribers ought to be imported.</param>
    /// <param name="overrideExistingSubscribers">
    /// If true will override first and last name of the subscribers with the same email with the imported ones; otherwise will skip
    /// importing of the subscribers that have same emails.
    /// </param>
    /// <param name="skipFirstRow">
    /// If true first row of the file will be skipped (headers); otherwise false.
    /// </param>
    public bool ImportFromTabSeparatedList(
      Stream fileStream,
      Guid mailingListId,
      int firstNameColumnIndex,
      int lastNameColumnIndex,
      int emailColumnIndex,
      bool overrideExistingSubscribers,
      bool skipFirstRow)
    {
      return this.Import(fileStream, new Guid[1]
      {
        mailingListId
      }, firstNameColumnIndex, lastNameColumnIndex, emailColumnIndex, (overrideExistingSubscribers ? 1 : 0) != 0, (skipFirstRow ? 1 : 0) != 0, '\t');
    }

    /// <summary>
    /// Imports the subscribers from the tab separated list of values.
    /// </summary>
    /// <param name="fileStream">The file stream representing the document with tab separated items.</param>
    /// <param name="mailingListIds">The array with mailing list ids to which the subscribers ought to be imported.</param>
    /// <param name="overrideExistingSubscribers">
    /// If true will override first and last name of the subscribers with the same email with the imported ones; otherwise will skip
    /// importing of the subscribers that have same emails.
    /// </param>
    /// <param name="skipFirstRow">
    /// If true first row of the file will be skipped (headers); otherwise false.
    /// </param>
    public bool ImportFromTabSeparatedList(
      Stream fileStream,
      Guid[] mailingListIds,
      int firstNameColumnIndex,
      int lastNameColumnIndex,
      int emailColumnIndex,
      bool overrideExistingSubscribers,
      bool skipFirstRow)
    {
      return this.Import(fileStream, mailingListIds, firstNameColumnIndex, lastNameColumnIndex, emailColumnIndex, overrideExistingSubscribers, skipFirstRow, '\t');
    }

    private Subscriber AddSubscriber(
      string emailAddress,
      string firstName,
      string lastName)
    {
      Subscriber subscriber = this.NewslettersManager.CreateSubscriber(true);
      subscriber.Email = emailAddress;
      subscriber.FirstName = firstName;
      subscriber.LastName = lastName;
      return subscriber;
    }

    private void UpdateSubscriber(Subscriber subscriber, string firstName, string lastName)
    {
      subscriber.FirstName = firstName;
      subscriber.LastName = lastName;
    }
  }
}
