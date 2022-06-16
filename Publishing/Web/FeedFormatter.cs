// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Web.FeedFormatter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.IO;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Xml;
using Telerik.Sitefinity.Publishing.Pipes;

namespace Telerik.Sitefinity.Publishing.Web
{
  public class FeedFormatter : IFeedFormatter
  {
    /// <summary>Gets the feed formatter.</summary>
    /// <param name="pipe">The pipe.</param>
    /// <param name="syndicationFeedFormatter">The syndication feed formatter.</param>
    /// <returns></returns>
    public virtual SyndicationFeedFormatter GetFeedFormatter(IPipe pipe) => (SyndicationFeedFormatter) ((RSSOutboundPipe) pipe).GetData().First<WrapperObject>().WrappedObject;

    /// <summary>Gets the xml string of the pipe.</summary>
    /// <param name="pipe">The pipe.</param>
    /// <param name="syndicationFeedFormatter">The syndication feed formatter.</param>
    /// <returns></returns>
    public virtual string GetFeedXml(IPipe pipe)
    {
      string empty = string.Empty;
      using (StringWriter w = new StringWriter())
      {
        using (XmlTextWriter writer = new XmlTextWriter((TextWriter) w))
        {
          this.GetFeedFormatter(pipe).WriteTo((XmlWriter) writer);
          return w.ToString();
        }
      }
    }
  }
}
