// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Configuration.ContentView.Plugins.SocialBookmarkElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using System.Web;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;

namespace Telerik.Sitefinity.Web.Configuration.ContentView.Plugins
{
  public class SocialBookmarkElement : ConfigElement
  {
    /// <summary>
    /// Initializes new instance of ConfigElement with the parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    /// <remarks>
    /// ConfigElementCollection generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ConfigElementCollection, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public SocialBookmarkElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Descriptive name of the social bookmark. Must be unique in a collection of social bookmarks.
    /// </summary>
    [DescriptionResource(typeof (ConfigDescriptions), "SocialBookmarkElementName")]
    [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
    public string Name
    {
      get => (string) this["name"];
      set => this["name"] = (object) value;
    }

    /// <summary>
    /// HTML markup for the social bookmark, containging content property replacement items (i.e. {{PropertyName}} -&gt; PropertyValue)
    /// </summary>
    /// <remarks>
    /// This value is always HTML-encoded, and should be HTML-decoded prior to use
    /// </remarks>
    [DescriptionResource(typeof (ConfigDescriptions), "SocialBookmarkElementMarkup")]
    [ConfigurationProperty("markup", IsRequired = true)]
    public string Markup
    {
      get => HttpUtility.HtmlDecode((string) this["markup"]);
      set => this["markup"] = (object) HttpUtility.HtmlEncode(value);
    }
  }
}
