// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Clients.PagesStatisticsClient
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Services.Statistics;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Clients
{
  /// <summary>
  /// Client for working with <see cref="T:Telerik.Sitefinity.Services.Statistics.IStatisticsService" /> related to Sitefinity pagesS.
  /// </summary>
  [Obsolete("For tracking user interactions on your site use Sitefinity Insight.")]
  public class PagesStatisticsClient : StatisticsClientBase
  {
    /// <summary>Writes the page visit to the statistics server.</summary>
    public void WritePageVisit(PageSiteNode pageNode)
    {
      if (pageNode == null)
        throw new ArgumentNullException(nameof (pageNode));
      ISentence sentence = this.Service.CreateSentence();
      ISentenceSubject visitorSubject = this.GetVisitorSubject();
      sentence.SubjectKey = visitorSubject.Key;
      sentence.SubjectId = visitorSubject.Id;
      sentence.SubjectProvider = visitorSubject.Provider;
      sentence.SubjectType = visitorSubject.Type;
      sentence.Verb = "HasVisited";
      string name = PageManager.GetManager().Provider.Name;
      ISentenceObject pageNodeObject = this.GetPageNodeObject(pageNode.Id, name);
      sentence.ObjectKey = pageNodeObject.Key;
      sentence.ObjectId = pageNodeObject.Id;
      sentence.ObjectProvider = name;
      sentence.ObjectType = pageNodeObject.Type;
      Uri uri = (Uri) null;
      try
      {
        uri = this.CurrentHttpContext.Request.UrlReferrer;
      }
      catch
      {
      }
      if (uri == (Uri) null)
        sentence.ObjectBoolMetaField = true;
      else if (uri.Host != this.CurrentHttpContext.Request.Url.Host)
        sentence.ObjectBoolMetaField = true;
      sentence.TimeStamp = this.CurrentTime;
      this.Service.WriteSentence(sentence);
    }

    /// <summary>
    /// Gets the value indicating weather current user has visited a page
    /// with the specified page node. Returns true if current visitor has
    /// visited the page; otherwise false.
    /// </summary>
    /// <param name="pageNode">
    /// Instance of the <see cref="T:Telerik.Sitefinity.Web.PageSiteNode" /> for which the check is being made.
    /// </param>
    /// <returns>True if the user has visited page; otherwise false.</returns>
    public bool HasVisitedPage(Guid pageNodeId, string provider) => this.Service.WasSentenceWritten(this.GetVisitorSubject(), "HasVisited", this.GetPageNodeObject(pageNodeId, provider));

    /// <summary>
    /// Gets the value indicating weather current user has a visited a page
    /// with the specified url.
    /// </summary>
    /// <param name="pageUrl">
    /// Url of the page against which the check is being made.
    /// </param>
    /// <returns>
    /// True if the user has visited the page; otherwise false.
    /// </returns>
    public bool HasVisitedPage(string pageUrl) => this.Service.WasSentenceWritten(this.GetVisitorSubject(), "HasVisited", this.GetPageUrlObject(pageUrl));

    /// <summary>Gets the last landing page for the current user.</summary>
    /// <returns>
    /// An instance of the <see cref="T:Telerik.Sitefinity.Clients.PageVisitSentence" />.
    /// </returns>
    public PageVisitSentence GetLastLandingPage()
    {
      ISentence sentenceForSubject = this.Service.GetLastSentenceForSubject(this.GetVisitorSubject(), "HasVisited", "ObjectBoolMetaField = True");
      return sentenceForSubject == null ? (PageVisitSentence) null : new PageVisitSentence(sentenceForSubject);
    }

    /// <summary>
    /// Calculates the visit time for the specified period in the specified calculation unit.
    /// </summary>
    /// <param name="period">
    /// The period that should be taken into account when calculation the visit time.
    /// </param>
    /// <param name="calculationUnit">
    /// The unit in which the calculation should be performed.
    /// </param>
    /// <returns>
    /// The total amount of time spent on site during the given period in the specified
    /// calculation unit.
    /// </returns>
    public double CalculateVisitTime(VisitationTimePeriod period, TimeUnit calculationUnit)
    {
      DateTime lastRelevantDate;
      switch (period)
      {
        case VisitationTimePeriod.LastDay:
          lastRelevantDate = DateTime.UtcNow.AddDays(-1.0);
          break;
        case VisitationTimePeriod.LastWeek:
          lastRelevantDate = DateTime.UtcNow.AddDays(-7.0);
          break;
        case VisitationTimePeriod.LastMonth:
          lastRelevantDate = DateTime.UtcNow.AddMonths(-1);
          break;
        case VisitationTimePeriod.LastYear:
          lastRelevantDate = DateTime.UtcNow.AddYears(-1);
          break;
        case VisitationTimePeriod.AllTime:
          lastRelevantDate = DateTime.UtcNow;
          break;
        default:
          throw new NotSupportedException();
      }
      ISentenceSubject visitorSubject = this.GetVisitorSubject();
      IQueryable<ISentence> source1 = this.Service.GetSentences().Where<ISentence>((Expression<Func<ISentence, bool>>) (s => s.SubjectId == visitorSubject.Id && s.SubjectProvider == visitorSubject.Provider && s.SubjectType == visitorSubject.Type));
      IQueryable<DateTime> source2;
      if (period != VisitationTimePeriod.AllTime)
        source2 = source1.Where<ISentence>((Expression<Func<ISentence, bool>>) (s => s.Verb == "HasVisited" && s.TimeStamp >= lastRelevantDate)).OrderBy<ISentence, DateTime>((Expression<Func<ISentence, DateTime>>) (s => s.TimeStamp)).Select<ISentence, DateTime>((Expression<Func<ISentence, DateTime>>) (s => s.TimeStamp));
      else
        source2 = source1.Where<ISentence>((Expression<Func<ISentence, bool>>) (s => s.Verb == "HasVisited")).OrderBy<ISentence, DateTime>((Expression<Func<ISentence, DateTime>>) (s => s.TimeStamp)).Select<ISentence, DateTime>((Expression<Func<ISentence, DateTime>>) (s => s.TimeStamp));
      if (source1.Count<ISentence>() < 2)
        return 0.0;
      DateTime dateTime = source2.First<DateTime>();
      TimeSpan timeSpan = source2.Last<DateTime>().Subtract(dateTime);
      switch (calculationUnit)
      {
        case TimeUnit.Hours:
          return timeSpan.TotalHours;
        case TimeUnit.Minutes:
          return timeSpan.TotalMinutes;
        case TimeUnit.Seconds:
          return timeSpan.TotalSeconds;
        default:
          throw new NotSupportedException();
      }
    }

    /// <summary>
    /// This method returns the object for the specified page node.
    /// </summary>
    /// <param name="pageNodeId">
    /// Id of the page node that represents the sentence object.
    /// </param>
    /// <param name="provider">
    /// Name of the provider to which the page node representing the sentence object belongs to.
    /// </param>
    /// <returns>
    /// An instance of the <see cref="T:Telerik.Sitefinity.Services.Statistics.ISentenceObject" />.
    /// </returns>
    protected ISentenceObject GetPageNodeObject(Guid pageNodeId, string provider)
    {
      if (pageNodeId == Guid.Empty)
        throw new ArgumentException("pageNodeId argument cannot be an empty GUID.");
      return this.Service.CreateSentenceObject(pageNodeId, provider, typeof (PageSiteNode).FullName);
    }

    protected ISentenceObject GetPageUrlObject(string url) => !string.IsNullOrEmpty(url) ? this.Service.CreateSentenceObject(url) : throw new ArgumentNullException("url argument cannot be null or empty.");
  }
}
