// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ResponsiveDesign.ResponsiveDesignDataProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.ResponsiveDesign.Model;

namespace Telerik.Sitefinity.Modules.ResponsiveDesign
{
  /// <summary>
  /// The base data provider for the Responsive Design module.
  /// </summary>
  public abstract class ResponsiveDesignDataProvider : DataProviderBase
  {
    /// <summary>Gets a unique key for each data provider base.</summary>
    public override string RootKey => (string) null;

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQuery" />.
    /// </summary>
    /// <returns>
    /// A newly created instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQuery" />.
    /// </returns>
    public abstract MediaQuery CreateMediaQuery();

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
    public abstract MediaQuery CreateMediaQuery(Guid id, string applicationName);

    /// <summary>
    /// Gets the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQuery" /> by it's id.
    /// </summary>
    /// <param name="id">
    /// Id of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQuery" /> which ought to be retrieved.
    /// </param>
    /// <returns>
    /// The instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQuery" />.
    /// </returns>
    public abstract MediaQuery GetMediaQuery(Guid id);

    /// <summary>
    /// Gets the query of all <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQuery" /> objects.
    /// </summary>
    /// <returns>
    /// The IQueryable of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQuery" /> objects.
    /// </returns>
    public abstract IQueryable<MediaQuery> GetMediaQueries();

    /// <summary>
    /// Deletes the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQuery" /> object.
    /// </summary>
    /// <param name="mediaQuery">
    /// The instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQuery" /> object to be deleted.
    /// </param>
    public abstract void DeleteMediaQuery(MediaQuery mediaQuery);

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryRule" />.
    /// </summary>
    /// <returns>
    /// A newly created instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryRule" />.
    /// </returns>
    public abstract MediaQueryRule CreateMediaQueryRule();

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
    public abstract MediaQueryRule CreateMediaQueryRule(
      Guid id,
      string applicationName);

    /// <summary>
    /// Gets the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryRule" /> by it's id.
    /// </summary>
    /// <param name="id">
    /// Id of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryRule" /> which ought to be retrieved.
    /// </param>
    /// <returns>
    /// The instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryRule" />.
    /// </returns>
    public abstract MediaQueryRule GetMediaQueryRule(Guid id);

    /// <summary>
    /// Gets the query of all <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryRule" /> objects.
    /// </summary>
    /// <returns>
    /// The IQueryable of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryRule" /> objects.
    /// </returns>
    public abstract IQueryable<MediaQueryRule> GetMediaQueryRules();

    /// <summary>
    /// Deletes the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryRule" /> object.
    /// </summary>
    /// <param name="mediaQueryRule">
    /// The instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryRule" /> object to be deleted.
    /// </param>
    public abstract void DeleteMediaQueryRule(MediaQueryRule mediaQueryRule);

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryLink" />.
    /// </summary>
    /// <returns>
    /// A newly created instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryLink" />.
    /// </returns>
    public abstract MediaQueryLink CreateMediaQueryLink();

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryLink" /> with specified
    /// id and application name.
    /// </summary>
    /// <param name="id">Id with which <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryLink" /> should be created.</param>
    /// <param name="applicationName">
    /// Name of the application under which the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryLink" /> should be created.
    /// </param>
    /// <returns>
    /// A newly created instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryLink" />.
    /// </returns>
    public abstract MediaQueryLink CreateMediaQueryLink(
      Guid id,
      string applicationName);

    /// <summary>
    /// Gets the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryLink" /> by it's id.
    /// </summary>
    /// <param name="id">
    /// Id of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryLink" /> which ought to be retrieved.
    /// </param>
    /// <returns>
    /// The instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryLink" />.
    /// </returns>
    public abstract MediaQueryLink GetMediaQueryLink(Guid id);

    /// <summary>
    /// Gets the query of all <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryLink" /> objects.
    /// </summary>
    /// <returns>
    /// The IQueryable of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryLink" /> objects.
    /// </returns>
    public abstract IQueryable<MediaQueryLink> GetMediaQueryLinks();

    /// <summary>
    /// Deletes the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryLink" /> object.
    /// </summary>
    /// <param name="mediaQueryLink">
    /// The instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.MediaQueryLink" /> object to be deleted.
    /// </param>
    public abstract void DeleteMediaQueryLink(MediaQueryLink mediaQueryLink);

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.NavigationTransformation" />.
    /// </summary>
    /// <returns>
    /// A newly created instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.NavigationTransformation" />.
    /// </returns>
    public abstract NavigationTransformation CreateNavigationTransformation();

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
    public abstract NavigationTransformation CreateNavigationTransformation(
      Guid id,
      string applicationName);

    /// <summary>
    /// Gets the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.NavigationTransformation" /> by it's id.
    /// </summary>
    /// <param name="id">
    /// Id of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.NavigationTransformation" /> which ought to be retrieved.
    /// </param>
    /// <returns>
    /// The instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.NavigationTransformation" />.
    /// </returns>
    public abstract NavigationTransformation GetNavigationTransformation(
      Guid id);

    /// <summary>
    /// Gets the query of all <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.NavigationTransformation" /> objects.
    /// </summary>
    /// <returns>
    /// The IQueryable of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.NavigationTransformation" /> objects.
    /// </returns>
    public abstract IQueryable<NavigationTransformation> GetNavigationTransformations();

    /// <summary>
    /// Deletes the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.NavigationTransformation" /> object.
    /// </summary>
    /// <param name="navigationTransformation">
    /// The instance of the <see cref="T:Telerik.Sitefinity.ResponsiveDesign.Model.NavigationTransformation" /> object to be deleted.
    /// </param>
    public abstract void DeleteNavigationTransformation(
      NavigationTransformation navigationTransformation);

    /// <summary>Get a list of types served by this manager</summary>
    /// <returns></returns>
    public override Type[] GetKnownTypes() => new Type[0];
  }
}
