// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ResponsiveDesign.Data.OpenAccessResponsiveDesignProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Telerik.OpenAccess.Metadata;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Linq;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.ResponsiveDesign.Model;

namespace Telerik.Sitefinity.Modules.ResponsiveDesign.Data
{
  /// <summary>
  /// Telerik OpenAccess ORM implementation of the data provider for the Responsive
  /// Design module data provider.
  /// </summary>
  public class OpenAccessResponsiveDesignProvider : 
    ResponsiveDesignDataProvider,
    IOpenAccessDataProvider,
    IDataProviderBase,
    IDisposable,
    ICloneable,
    IDataProviderEventsCall,
    IOpenAccessMetadataProvider
  {
    /// <summary>Creates new data item.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="id">The id.</param>
    /// <returns>The instance of the newly created item.</returns>
    public override object CreateItem(Type itemType, Guid id)
    {
      if (typeof (MediaQuery).IsAssignableFrom(itemType))
        return (object) this.CreateMediaQuery(id, (string) null);
      throw new NotSupportedException();
    }

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQuery" />.
    /// </summary>
    /// <returns>
    /// A newly created instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQuery" />.
    /// </returns>
    public override MediaQuery CreateMediaQuery() => this.CreateMediaQuery(this.GetNewGuid(), string.Empty);

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQuery" /> with specified
    /// id and application name.
    /// </summary>
    /// <param name="id">Id with which <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQuery" /> should be created.</param>
    /// <param name="applicationName">
    /// Name of the application under which the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQuery" /> should be created.
    /// </param>
    /// <returns>
    /// A newly created instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQuery" />.
    /// </returns>
    public override MediaQuery CreateMediaQuery(Guid id, string applicationName)
    {
      string applicationName1 = applicationName;
      if (string.IsNullOrEmpty(applicationName1))
        applicationName1 = this.ApplicationName;
      MediaQuery entity = new MediaQuery(id, applicationName1);
      entity.LastModified = DateTime.Now;
      entity.Provider = (object) this;
      if (id != Guid.Empty)
        this.GetContext().Add((object) entity);
      return entity;
    }

    /// <summary>
    /// Gets the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQuery" /> by it's id.
    /// </summary>
    /// <param name="id">
    /// Id of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQuery" /> which ought to be retrieved.
    /// </param>
    /// <returns>
    /// The instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQuery" />.
    /// </returns>
    public override MediaQuery GetMediaQuery(Guid id)
    {
      MediaQuery mediaQuery = !(id == Guid.Empty) ? this.GetContext().GetItemById<MediaQuery>(id.ToString()) : throw new ArgumentException("Id cannot be Empty Guid");
      mediaQuery.Provider = (object) this;
      return mediaQuery;
    }

    /// <summary>
    /// Gets the query of all <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQuery" /> objects.
    /// </summary>
    /// <returns>
    /// The IQueryable of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQuery" /> objects.
    /// </returns>
    public override IQueryable<MediaQuery> GetMediaQueries()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<MediaQuery>((DataProviderBase) this, MethodBase.GetCurrentMethod()).Where<MediaQuery>((Expression<Func<MediaQuery, bool>>) (b => b.ApplicationName == appName));
    }

    /// <summary>
    /// Deletes the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQuery" /> object.
    /// </summary>
    /// <param name="mediaQuery">
    /// The instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQuery" /> object to be deleted.
    /// </param>
    public override void DeleteMediaQuery(MediaQuery mediaQuery) => this.GetContext()?.Remove((object) mediaQuery);

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryRule" />.
    /// </summary>
    /// <returns>
    /// A newly created instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryRule" />.
    /// </returns>
    public override MediaQueryRule CreateMediaQueryRule() => this.CreateMediaQueryRule(this.GetNewGuid(), string.Empty);

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryRule" /> with specified
    /// id and application name.
    /// </summary>
    /// <param name="id">Id with which <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryRule" /> should be created.</param>
    /// <param name="applicationName">
    /// Name of the application under which the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryRule" /> should be created.
    /// </param>
    /// <returns>
    /// A newly created instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryRule" />.
    /// </returns>
    public override MediaQueryRule CreateMediaQueryRule(
      Guid id,
      string applicationName)
    {
      string applicationName1 = applicationName;
      if (string.IsNullOrEmpty(applicationName1))
        applicationName1 = this.ApplicationName;
      MediaQueryRule entity = new MediaQueryRule(id, applicationName1);
      entity.LastModified = DateTime.Now;
      entity.Provider = (object) this;
      if (id != Guid.Empty)
        this.GetContext().Add((object) entity);
      return entity;
    }

    /// <summary>
    /// Gets the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryRule" /> by it's id.
    /// </summary>
    /// <param name="id">
    /// Id of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryRule" /> which ought to be retrieved.
    /// </param>
    /// <returns>
    /// The instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryRule" />.
    /// </returns>
    public override MediaQueryRule GetMediaQueryRule(Guid id)
    {
      MediaQueryRule mediaQueryRule = !(id == Guid.Empty) ? this.GetContext().GetItemById<MediaQueryRule>(id.ToString()) : throw new ArgumentException("Id cannot be Empty Guid");
      mediaQueryRule.Provider = (object) this;
      return mediaQueryRule;
    }

    /// <summary>
    /// Gets the query of all <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryRule" /> objects.
    /// </summary>
    /// <returns>
    /// The IQueryable of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryRule" /> objects.
    /// </returns>
    public override IQueryable<MediaQueryRule> GetMediaQueryRules()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<MediaQueryRule>((DataProviderBase) this, MethodBase.GetCurrentMethod()).Where<MediaQueryRule>((Expression<Func<MediaQueryRule, bool>>) (b => b.ApplicationName == appName));
    }

    /// <summary>
    /// Deletes the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryRule" /> object.
    /// </summary>
    /// <param name="mediaQueryRule">
    /// The instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryRule" /> object to be deleted.
    /// </param>
    public override void DeleteMediaQueryRule(MediaQueryRule mediaQueryRule) => this.GetContext()?.Remove((object) mediaQueryRule);

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryLink" />.
    /// </summary>
    /// <returns>A newly created instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryLink" />.</returns>
    public override MediaQueryLink CreateMediaQueryLink() => this.CreateMediaQueryLink(this.GetNewGuid(), string.Empty);

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryLink" /> with specified
    /// id and application name.
    /// </summary>
    /// <param name="id">Id with which <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryLink" /> should be created.</param>
    /// <param name="applicationName">Name of the application under which the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryLink" /> should be created.</param>
    /// <returns>A newly created instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryLink" />.</returns>
    public override MediaQueryLink CreateMediaQueryLink(
      Guid id,
      string applicationName)
    {
      string applicationName1 = applicationName;
      if (string.IsNullOrEmpty(applicationName1))
        applicationName1 = this.ApplicationName;
      MediaQueryLink entity = new MediaQueryLink(id, applicationName1);
      entity.LastModified = DateTime.Now;
      entity.Provider = (object) this;
      if (id != Guid.Empty)
        this.GetContext().Add((object) entity);
      return entity;
    }

    /// <summary>
    /// Gets the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryLink" /> by it's id.
    /// </summary>
    /// <param name="id">Id of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryLink" /> which ought to be retrieved.</param>
    /// <returns>The instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryLink" />.</returns>
    public override MediaQueryLink GetMediaQueryLink(Guid id)
    {
      MediaQueryLink mediaQueryLink = !(id == Guid.Empty) ? this.GetContext().GetItemById<MediaQueryLink>(id.ToString()) : throw new ArgumentException("Id cannot be Empty Guid");
      mediaQueryLink.Provider = (object) this;
      return mediaQueryLink;
    }

    /// <summary>
    /// Gets the query of all <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryLink" /> objects.
    /// </summary>
    /// <returns>The IQueryable of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryLink" /> objects.</returns>
    public override IQueryable<MediaQueryLink> GetMediaQueryLinks()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<MediaQueryLink>((DataProviderBase) this, MethodBase.GetCurrentMethod()).Where<MediaQueryLink>((Expression<Func<MediaQueryLink, bool>>) (b => b.ApplicationName == appName));
    }

    /// <summary>
    /// Deletes the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryLink" /> object.
    /// </summary>
    /// <param name="mediaQueryLink">The instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryLink" /> object to be deleted.</param>
    public override void DeleteMediaQueryLink(MediaQueryLink mediaQueryLink) => this.GetContext()?.Remove((object) mediaQueryLink);

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.NavigationTransformation" />.
    /// </summary>
    /// <returns>
    /// A newly created instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.NavigationTransformation" />.
    /// </returns>
    public override NavigationTransformation CreateNavigationTransformation() => this.CreateNavigationTransformation(this.GetNewGuid(), string.Empty);

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.NavigationTransformation" /> with specified
    /// id and application name.
    /// </summary>
    /// <param name="id">Id with which <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.NavigationTransformation" /> should be created.</param>
    /// <param name="applicationName">
    /// Name of the application under which the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.NavigationTransformation" /> should be created.
    /// </param>
    /// <returns>
    /// A newly created instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.NavigationTransformation" />.
    /// </returns>
    public override NavigationTransformation CreateNavigationTransformation(
      Guid id,
      string applicationName)
    {
      string applicationName1 = applicationName;
      if (string.IsNullOrEmpty(applicationName1))
        applicationName1 = this.ApplicationName;
      NavigationTransformation entity = new NavigationTransformation(id, applicationName1);
      entity.Provider = (object) this;
      if (id != Guid.Empty)
        this.GetContext().Add((object) entity);
      return entity;
    }

    /// <summary>
    /// Gets the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.NavigationTransformation" /> by it's id.
    /// </summary>
    /// <param name="id">
    /// Id of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.NavigationTransformation" /> which ought to be retrieved.
    /// </param>
    /// <returns>
    /// The instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.NavigationTransformation" />.
    /// </returns>
    public override NavigationTransformation GetNavigationTransformation(
      Guid id)
    {
      NavigationTransformation navigationTransformation = !(id == Guid.Empty) ? this.GetContext().GetItemById<NavigationTransformation>(id.ToString()) : throw new ArgumentException("Id cannot be Empty Guid");
      navigationTransformation.Provider = (object) this;
      return navigationTransformation;
    }

    /// <summary>
    /// Gets the query of all <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.NavigationTransformation" /> objects.
    /// </summary>
    /// <returns>
    /// The IQueryable of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.NavigationTransformation" /> objects.
    /// </returns>
    public override IQueryable<NavigationTransformation> GetNavigationTransformations()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<NavigationTransformation>((DataProviderBase) this, MethodBase.GetCurrentMethod()).Where<NavigationTransformation>((Expression<Func<NavigationTransformation, bool>>) (b => b.ApplicationName == appName));
    }

    /// <summary>
    /// Deletes the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.NavigationTransformation" /> object.
    /// </summary>
    /// <param name="navigationTransformation">
    /// The instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.NavigationTransformation" /> object to be deleted.
    /// </param>
    public override void DeleteNavigationTransformation(
      NavigationTransformation navigationTransformation)
    {
      this.GetContext()?.Remove((object) navigationTransformation);
    }

    /// <summary>
    /// Gets or sets the OpenAccess context. Alternative to Database.
    /// </summary>
    /// <value>The context.</value>
    public OpenAccessProviderContext Context { get; set; }

    /// <summary>Gets the meta data source.</summary>
    /// <returns></returns>
    public MetadataSource GetMetaDataSource(IDatabaseMappingContext context) => (MetadataSource) new ResponsiveDesignMetadataSource(context);
  }
}
