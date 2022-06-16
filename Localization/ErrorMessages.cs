// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.ErrorMessages
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Telerik.Sitefinity.Localization.Contracts;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Localization
{
  /// <summary>Represents string resources for error messages.</summary>
  [ObjectInfo("ErrorMessages", ResourceClassId = "ErrorMessages")]
  public sealed class ErrorMessages : Resource, IValidationErrorMessages
  {
    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.ErrorMessages" /> class with the default <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    public ErrorMessages()
    {
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.ErrorMessages" /> class with the provided <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    /// <param name="dataProvider"><see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" /></param>
    public ErrorMessages(ResourceDataProvider dataProvider)
      : base(dataProvider)
    {
    }

    /// <summary>Error Messages</summary>
    [ResourceEntry("ErrorMessagesTitle", Description = "The title of this class.", LastModified = "2009/04/30", Value = "Error Messages")]
    public string ErrorMessagesTitle => this[nameof (ErrorMessagesTitle)];

    /// <summary>Error Messages Title plural</summary>
    [ResourceEntry("ErrorMessagesTitlePlural", Description = "The title plural of this class.", LastModified = "2009/04/30", Value = "Error Messages Title Plural")]
    public string ErrorMessagesTitlePlural => this[nameof (ErrorMessagesTitlePlural)];

    /// <summary>Contains localizable resources for Sitefinity errors.</summary>
    [ResourceEntry("ErrorMessagesDescription", Description = "The description of this class.", LastModified = "2009/04/30", Value = "Contains localizable resources for Sitefinity errors.")]
    public string ErrorMessagesDescription => this[nameof (ErrorMessagesDescription)];

    /// <summary>Message: DatabaseNotFound</summary>
    [ResourceEntry("DatabaseNotFound", Description = "The exception that is thrown when connection to the database cannot be established.", LastModified = "2010/09/20", Value = "Could not connect to database.")]
    public string DatabaseNotFound => this[nameof (DatabaseNotFound)];

    /// <summary>Message: Database server name cannot be empty</summary>
    [ResourceEntry("DatabaseServerNameCannotBeEmpty", Description = "The exception that is thrown when the name of the database server is not supplied.", LastModified = "2010/09/23", Value = "Database server name cannot be empty.")]
    public string DatabaseServerNameCannotBeEmpty => this[nameof (DatabaseServerNameCannotBeEmpty)];

    /// <summary>Message: Database name cannot be empty</summary>
    [ResourceEntry("DatabaseNameCannotBeEmpty", Description = "The exception that is thrown when the name of the database is not supplied.", LastModified = "2010/09/23", Value = "Database name cannot be empty.")]
    public string DatabaseNameCannotBeEmpty => this[nameof (DatabaseNameCannotBeEmpty)];

    /// <summary>Message: Database server not found</summary>
    [ResourceEntry("DatabaseServerNotFound", Description = "The exception that is thrown when connection to the database server cannot be found.", LastModified = "2010/09/20", Value = "Sitefinity was unable to connect to the specified database server. Check whether you have provided the correct server name and port (if applicable). Other possible causes might include network connectivity or database server availability.")]
    public string DatabaseServerNotFound => this[nameof (DatabaseServerNotFound)];

    /// <summary>
    /// Message: Configuration error while connecting to database on startup occured. For more details check the Error.log file.
    /// </summary>
    [ResourceEntry("StartupDatabaseConfigurationErrorMessage", Description = "The exception that is thrown when connecting to the database on startup fails due to configuration error.", LastModified = "2017/01/18", Value = "Configuration error while connecting to database on startup occured. For more details check the Error.log file.")]
    public string StartupDatabaseConfigurationErrorMessage => this[nameof (StartupDatabaseConfigurationErrorMessage)];

    /// <summary>Message: Unable to parse Oracle DataSource</summary>
    [ResourceEntry("UnableToParseOracleDataSource", Description = "The exception that is thrown when unable to parse Oracle DataSource.", LastModified = "2019/03/13", Value = "Sitefinity was unable to connect to the specified Oracle database server. Check whether you have provided the correct Oracle DataSource.")]
    public string UnableToParseOracleDataSource => this[nameof (UnableToParseOracleDataSource)];

    /// <summary>Message: ODAC not installed properly</summary>
    [ResourceEntry("OdacNotInstalledProperly", Description = "The exception that is thrown when ODAC is not installed properly.", LastModified = "2019/03/13", Value = "Sitefinity was unable to connect to the specified Oracle database server. Check whether you have installed ODAC.")]
    public string OdacNotInstalledProperly => this[nameof (OdacNotInstalledProperly)];

    /// <summary>
    /// Message: An error while connecting to database on startup occured. For more details check the Error.log file.
    /// </summary>
    [ResourceEntry("GenericStartupDatabaseConnectingErrorMessage", Description = "The exception that is thrown when connecting to the database on startup fails due to unspecified reason.", LastModified = "2017/01/18", Value = "An error while connecting to database on startup occured. For more details check the Error.log file.")]
    public string GenericStartupDatabaseConnectingErrorMessage => this[nameof (GenericStartupDatabaseConnectingErrorMessage)];

    /// <summary>
    /// Message: Cannot use SaveChanges or CancelChanges on instance manager that was specified to use global or distributed transaction. Instead, use TransactionManager static methods CommitTransaction(string transactionName) or RollbackTransaction(string transactionName).
    /// </summary>
    [ResourceEntry("GlobalTransactionUsed", Description = "The exception that is thrown when SaveChanges or CancelChanges is called on a manager that was specified to use global transaction.", LastModified = "2010/01/10", Value = "Cannot use SaveChanges or CancelChanges on instance manager that was specified to use global or distributed transaction. Instead, use TransactionManager static methods CommitTransaction(string transactionName) or RollbackTransaction(string transactionName).")]
    public string GlobalTransactionUsed => this[nameof (GlobalTransactionUsed)];

    /// <summary>Message: Authorization failed.</summary>
    [ResourceEntry("AuthorizationFailed", Description = "The exception that is thrown when the operating system denies access because of an I/O error or a specific type of security error.", LastModified = "2019/03/27", Value = "Authentication to the specified database server failed. Verify you have provided the correct credentials.")]
    public string AuthorizationFailed => this[nameof (AuthorizationFailed)];

    /// <summary>Message: Authorization failed.</summary>
    [ResourceEntry("AuthorizationFailedSqlWinAuth", Description = "The exception that is thrown when the operating system denies access because of an I/O error or a specific type of security error with SQL/SQLExpress and win auth.", LastModified = "2019/03/27", Value = "Authentication to the specified database server failed. Verify your website ApplicationPool Identity in IIS has a matching login created in SQL Server / SQL Express.")]
    public string AuthorizationFailedSqlWinAuth => this[nameof (AuthorizationFailedSqlWinAuth)];

    /// <summary>
    /// Translated message, similar to "You are trying to access {0} item with id {1} that no longer exists. The most probable reason is that it has been deleted by another user."
    /// </summary>
    /// <value>Error message show by Sitefinity's common web service when an item is searched by ID and is not found.</value>
    [ResourceEntry("ItemNotFound", Description = "Error message shown by Sitefinity's common web service when an item is searched by ID and is not found.", LastModified = "2017/03/14", Value = "You are trying to access {0} item with id {1} that no longer exists. The most probable reason is that it has been deleted by another user.")]
    public string ItemNotFound => this[nameof (ItemNotFound)];

    /// <summary>
    /// Translated message, similar to "You are trying to access {0} that no longer exists. The most probable reason is that it has been deleted by another user."
    /// </summary>
    /// <value>Error message show by Sitefinity's common web service when an item is searched by ID and is not found.</value>
    [ResourceEntry("ItemNotFoundFormated", Description = "Error message show by Sitefinity's common web service when an item is searched by ID and is not found.", LastModified = "2010/08/31", Value = "You are trying to access {0} that no longer exists. The most probable reason is that it has been deleted by another user.")]
    public string ItemNotFoundFormated => this[nameof (ItemNotFoundFormated)];

    /// <summary>
    /// Message: Authorization failed! Only system administrators are allowed to perform the operation: "{0}".
    /// </summary>
    [ResourceEntry("AuthorizationFailedOnlyAdminsAllowed", Description = "The exception that is thrown when the operating system denies access because of an I/O error or a specific type of security error.", LastModified = "2009/09/10", Value = "Authorization failed! Only system administrators are allowed to perform the operation: \"{0}\".")]
    public string AuthorizationFailedOnlyAdminsAllowed => this[nameof (AuthorizationFailedOnlyAdminsAllowed)];

    /// <summary>
    /// You are not authorized to {0}. Please contact your system administrator to grant you the necessary permissions to perform this operation.
    /// </summary>
    [ResourceEntry("YouAreNotAuthorizedTo", Description = "The exception that is thrown when an operation fails because of insufficient user privileges.", LastModified = "2009/09/10", Value = "You are not authorized to {0}. Please contact your system administrator to grant you the necessary permissions to perform this operation, if you believe you are eligible.")]
    public string YouAreNotAuthorizedTo => this[nameof (YouAreNotAuthorizedTo)];

    /// <summary>
    /// Message: The control (Type: {0}, ID: {1}) in template "{3}" specifies place holder with ID: "{4}", but such place holder does not exist in the current template hierarchy. Did you rename or remove the place holder?
    /// </summary>
    [ResourceEntry("InvalidPlaceHolderSpecified", Description = "This error is thrown when a Content control has ContentPlaceHolderID property set to place holder that does not exist in the template.", LastModified = "2009/09/10", Value = "The control (Type: {0}, ID: {1}) in template \"{2}\" specifies place holder with ID: \"{3}\", but such place holder does not exist in the current template hierarchy. Did you rename or remove the place holder?")]
    public string InvalidPlaceHolderSpecified => this[nameof (InvalidPlaceHolderSpecified)];

    /// <summary>
    /// Message: There is no action defined with the specified name "{0}" for permission set "{1}".
    /// </summary>
    [ResourceEntry("InvalidActionForPermissionSet", Description = "This error is thrown when a user tries to access security action that has not been defined.", LastModified = "2009/08/28", Value = "There is no action defined with the specified name \"{0}\" for permission set \"{1}\".")]
    public string InvalidActionForPermissionSet => this[nameof (InvalidActionForPermissionSet)];

    /// <summary>
    /// Message: There is no permission set defined with the specified name "{0}".
    /// </summary>
    [ResourceEntry("InvalidPermissionSet", Description = "This error is thrown when a user tries to access permission set that has not been defined.", LastModified = "2009/08/28", Value = "There is no permission set defined with the specified name \"{0}\".")]
    public string InvalidPermissionSet => this[nameof (InvalidPermissionSet)];

    /// <summary>
    /// Message: Object of type {0} does not support permission set "{1}".
    /// </summary>
    [ResourceEntry("ObjectDoesNotSupportPermissionSet", Description = "This error is thrown when a permission is demanded for an object with a permission set it does not support.", LastModified = "2010/05/12", Value = "Object of type {0} does not support permission set \"{1}\".")]
    public string ObjectDoesNotSupportPermissionSet => this[nameof (ObjectDoesNotSupportPermissionSet)];

    /// <summary>
    /// Message: Invalid root taxon configured for page taxonomy. No root taxon with the name of "{0}" for taxonomy "{1}", provider: "{2}".
    /// </summary>
    [ResourceEntry("InvalidRootSiteNode", Description = "Configuration error.", LastModified = "2009/08/22", Value = "Invalid root node configured for pages. No root node with the name of \"{0}\".")]
    public string InvalidRootSiteNode => this[nameof (InvalidRootSiteNode)];

    /// <summary>
    /// Message: Invalid page taxonomy configured. No taxonomy with the name of "{0}" for provider "{1}".
    /// </summary>
    [ResourceEntry("InvalidPageTaxonomy", Description = "Configuration error.", LastModified = "2009/08/22", Value = "Invalid page taxonomy configured. No taxonomy with the name of \"{0}\" for provider \"{1}\".")]
    public string InvalidPageTaxonomy => this[nameof (InvalidPageTaxonomy)];

    /// <summary>
    /// Message: This page was modified by someone else while you were editing it.
    /// </summary>
    [ResourceEntry("PageModifiedBySomeoneElse", Description = "This error is thrown when a user attempts to publish a page that was modified someone else while the user was editing it.", LastModified = "2009/08/22", Value = "This page was modified by someone else while you were editing it. Most likely it has been unlocked by a user with administrative permissions and then edited by another user.")]
    public string PageModifiedBySomeoneElse => this[nameof (PageModifiedBySomeoneElse)];

    /// <summary>Message: This page is locked by {0}.</summary>
    [ResourceEntry("PageIsLocked", Description = "This error is thrown when a user attempts to edit a page locked by someone else.", LastModified = "2009/08/22", Value = "This page is locked by {0}.")]
    public string PageIsLocked => this[nameof (PageIsLocked)];

    /// <summary>Message: This page is locked by {0}.</summary>
    [ResourceEntry("ItemIsLocked", Description = "This error is thrown when a user attempts to publish an item locked by someone else.", LastModified = "2014/11/28", Value = "This item is locked by '{0}'. Most likely it has been unlocked by a user with administrative permissions and then locked by the other user.")]
    public string ItemIsLocked => this[nameof (ItemIsLocked)];

    /// <summary>
    /// Message: The service "{0}" is disabled and cannot be started.
    /// </summary>
    [ResourceEntry("DisabledServiceCannotStart", Description = "This error is thrown when Start() method is called on disabled system service.", LastModified = "2009/07/20", Value = "The service \"{0}\" is disabled and cannot be started.")]
    public string DisabledServiceCannotStart => this[nameof (DisabledServiceCannotStart)];

    /// <summary>
    /// Message: No permission set is defined with the name of "{0}".
    /// </summary>
    [ResourceEntry("NoPermissionSet", Description = "This error is thrown when permission is requested for none existent permission set.", LastModified = "2009/07/20", Value = "No permission set is defined with the name of \"{0}\".")]
    public string NoPermissionSet => this[nameof (NoPermissionSet)];

    /// <summary>
    /// Message: "No action with the name of "{0}" is defined for permission set "{1}"."
    /// </summary>
    [ResourceEntry("NoSuchAction", Description = "This error is thrown when a none existent security action is requested for a permission set.", LastModified = "2009/07/20", Value = "No action with the name of \"{0}\" is defined for permission set \"{1}\".")]
    public string NoSuchAction => this[nameof (NoSuchAction)];

    /// <summary>
    /// Message: PersistentAttribute is required to create cache dependency.
    /// </summary>
    [ResourceEntry("PersistentAttributeRequired", Description = "This error is thrown if someone tries to create cache dependency for an object that doesn't have PersistentAttribute.", LastModified = "2009/06/05", Value = "PersistentAttribute is required to create cache dependency.")]
    public string PersistentAttributeRequired => this[nameof (PersistentAttributeRequired)];

    /// <summary>
    /// Message: Connection "{0}" could not be found in config files.
    /// </summary>
    [ResourceEntry("ConnectionNotFound", Description = "This error is thrown if the specified connection is not found in connection strings collection in web.config.", LastModified = "2009/06/05", Value = "Connection string \"{0}\" could not be found in config files.")]
    public string ConnectionNotFound => this[nameof (ConnectionNotFound)];

    /// <summary>
    /// Message: The type "{0}" does not have property with name "{1}".
    /// </summary>
    [ResourceEntry("PropertyNotFound", Description = "This error is thrown if the specified property is not found for a type.", LastModified = "2009/06/05", Value = "The type \"{0}\" does not have property with name \"{1}\".")]
    public string PropertyNotFound => this[nameof (PropertyNotFound)];

    /// <summary>
    /// Message: The control with pageId "{0}" of type "{1}" does not have property with name "{2}".
    /// </summary>
    [ResourceEntry("PropertyNotFoundInControl", Description = "This error is thrown if the specified property is not found in a control.", LastModified = "2010/01/01", Value = "The control with id \"{0}\" of type \"{1}\" does not have property with name \"{2}\".")]
    public string PropertyNotFoundInControl => this[nameof (PropertyNotFoundInControl)];

    /// <summary>
    /// Message: The type "{0}" was not registered as XML persistent.
    /// </summary>
    [ResourceEntry("NotRegisteredXmlPersistent", Description = "This error is thrown when a type that is not registered as XML persistent is attempted to be persisted by an XML data provider.", LastModified = "2009/06/05", Value = "The type \"{0}\" was not registered as XML persistent.")]
    public string NotRegisteredXmlPersistent => this[nameof (NotRegisteredXmlPersistent)];

    /// <summary>Message: The user "{0}" was not found.</summary>
    [ResourceEntry("UserNotFound", Description = "This error is thrown by Role data providers.", LastModified = "2009/05/15", Value = "The user \"{0}\" was not found.")]
    public string UserNotFound => this[nameof (UserNotFound)];

    /// <summary>Message: The user "{0}" is already in role "{1}".</summary>
    [ResourceEntry("UserAlreadyInRole", Description = "This error is thrown by Role data providers.", LastModified = "2009/05/15", Value = "The user \"{0}\" is already in role \"{1}\".")]
    public string UserAlreadyInRole => this[nameof (UserAlreadyInRole)];

    /// <summary>Message: The specified role "{0}" already exists.</summary>
    [ResourceEntry("RoleAlreadyExists", Description = "This error is thrown by Role data providers.", LastModified = "2009/05/15", Value = "The specified role \"{0}\" already exists.")]
    public string RoleAlreadyExists => this[nameof (RoleAlreadyExists)];

    /// <summary>Message: The specified role "{0}" is not empty.</summary>
    [ResourceEntry("RoleIsNotEmpty", Description = "This error is thrown by Role data providers.", LastModified = "2009/05/15", Value = "The specified role \"{0}\" is not empty.")]
    public string RoleIsNotEmpty => this[nameof (RoleIsNotEmpty)];

    /// <summary>
    /// Message: The specified role "{0}" was not found. Role provider: "{1}".
    /// </summary>
    [ResourceEntry("RoleNotFound", Description = "This error is thrown by Role data providers.", LastModified = "2009/05/15", Value = "The specified role \"{0}\" was not found. Role provider: \"{1}\".")]
    public string RoleNotFound => this[nameof (RoleNotFound)];

    /// <summary>
    /// Message: The key {0} already exists in the resource class {1}.
    /// </summary>
    [ResourceEntry("KeyAlreadyExistsInResource", Description = "This error is thrown when there are more then one resource entries with the same key in a resource class.", LastModified = "2009/05/12", Value = "The key \"{0}\" already exists in the resource class \"{1}\".")]
    public string KeyAlreadyExistsInResource => this[nameof (KeyAlreadyExistsInResource)];

    /// <summary>Message: Target and source types must be the same.</summary>
    [ResourceEntry("TargetAndSourceNotSame", Description = "This error is usually thrown when performing operations such as comparison of objects of different type.", LastModified = "2009/05/12", Value = "Target and source types must be the same.")]
    public string TargetAndSourceNotSame => this[nameof (TargetAndSourceNotSame)];

    /// <summary>Message: Permission denied.</summary>
    [ResourceEntry("PermissionDenied", Description = "This is security error thrown when certain permission is not granted or it is explicitly denied.", LastModified = "2009/05/12", Value = "Permission denied.")]
    public string PermissionDenied => this[nameof (PermissionDenied)];

    /// <summary>Message: Key not found.</summary>
    [ResourceEntry("KeyNotFound", Description = "This error is usually thrown by dictionaries if the specified key is not found.", LastModified = "2009/05/12", Value = "Key not found.")]
    public string KeyNotFound => this[nameof (KeyNotFound)];

    /// <summary>
    /// Message: The provided request context origins from different handler.
    /// </summary>
    [ResourceEntry("ContextOriginsFormDifferentHandler", Description = "This error is thrown by route handlers.", LastModified = "2011/3/11", Value = "The provided request context origins from different handler.")]
    public string ContextOriginsFormDifferentHandler => this[nameof (ContextOriginsFormDifferentHandler)];

    /// <summary>
    /// Message: The resource you are looking for was not found.
    /// </summary>
    [ResourceEntry("ResourceNotFound", Description = "This error is thrown when there is no resource for the requested URL (HTTP 404).", LastModified = "2009/05/04", Value = "The resource you are looking for was not found.")]
    public string ResourceNotFound => this[nameof (ResourceNotFound)];

    /// <summary>Message: ID or search type should be specified first.</summary>
    [ResourceEntry("IdOrTypeNotSepcified", Description = "This error is thrown by GenericContainer class if neither id or search type is specified when searching for child control.", LastModified = "2012/01/05", Value = "ID or search type should be specified first.")]
    public string IdOrTypeNotSpecified => this["IdOrTypeNotSepcified"];

    /// <summary>
    /// Message: Could not resolve type for tag "{0}". Make sure the proper namespace is registered..
    /// </summary>
    [ResourceEntry("CannotResolveTypeInTemplate", Description = "This error is thrown by TemplateParser class when a declared type cannot be resolved.", LastModified = "2009/04/26", Value = "Could not resolve type for tag \"{0}\". Make sure the proper namespace is registered.")]
    public string CannotResolveTypeInTemplate => this[nameof (CannotResolveTypeInTemplate)];

    /// <summary>Message: Cannot find template "{0}".</summary>
    [SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", MessageId = "Telerik.Sitefinity.Localization.Resource.get_Item(System.String)")]
    [ResourceEntry("CannotFindTemplate", Description = "This error is thrown by ControlUtilities class when trying to load external template but the specified virtual path is invalid.", LastModified = "2009/04/26", Value = "Cannot find template \"{0}\".")]
    public string CannotFindTemplate => this[nameof (CannotFindTemplate)];

    /// <summary>
    /// Message: Invalid resource name "{0}" for assembly "{1}" or empty template.
    /// </summary>
    [ResourceEntry("InvalidResourceNameOrEmtyTemplate", Description = "This error is thrown by ControlUtilities class when trying to load embedded template but the resource is not found or empty content is returned.", LastModified = "2009/04/26", Value = "Invalid resource name \"{0}\" for assembly \"{1}\" or empty template.")]
    public string InvalidResourceNameOrEmptyTemplate => this["InvalidResourceNameOrEmtyTemplate"];

    /// <summary>
    /// Message: The ViewMode "{0}" is not defined for this control.
    /// </summary>
    [ResourceEntry("ViewModeNotDefined", Description = "This error is thrown by ViewMode base control if there is an attempt to access a view that was not defined.", LastModified = "2009/04/26", Value = "The ViewMode \"{0}\" is not defined for this control.")]
    public string ViewModeNotDefined => this[nameof (ViewModeNotDefined)];

    /// <summary>Message: The property TemplateInfo must not be null.</summary>
    [ResourceEntry("NullTemplateInfo", Description = "This error is thrown if the propery TemplateInfo of SitefinityRouteHandler class is not set prior to invoking GetHttpHandler method.", LastModified = "2009/04/24", Value = "The property TemplateInfo must not be null.")]
    public string NullTemplateInfo => this[nameof (NullTemplateInfo)];

    /// <summary>
    /// Message: A required control was not found in the template for "{0}".
    /// The control must be assignable from type "{1}" and must have ID "{2}".
    /// </summary>
    [ResourceEntry("RequiredControlNotFound", Description = "This error is thrown if a required control is not found in a template.", LastModified = "2009/04/24", Value = "A required control was not found in the template for \"{0}\". The control must be assignable from type \"{1}\" and must have ID \"{2}\".")]
    public string RequiredControlNotFound => this[nameof (RequiredControlNotFound)];

    /// <summary>
    /// Message: No control with pageId "{0}" was found on the page.
    /// </summary>
    [ResourceEntry("ControlNotFoundOnPage", Description = "This error is thrown if a required control is not found on the page.", LastModified = "2010/01/04", Value = "No control with id \"{0}\" was found on this page.")]
    public string ControlNotFoundOnPage => this[nameof (ControlNotFoundOnPage)];

    /// <summary>
    /// Message: The site map provider "{0}" returned invalid root node or null.
    /// </summary>
    [ResourceEntry("InvalidSiteMapRootNode", Description = "This error is thrown a SiteMap provider does not return root node.", LastModified = "2009/04/22", Value = "The site map provider \"{0}\" returned invalid root node or null.")]
    public string InvalidSiteMapRootNode => this[nameof (InvalidSiteMapRootNode)];

    /// <summary>
    /// Message: Only one BackendSiteMapProvider is allowed.
    /// In the SiteMap configuration are defined multiple backend SiteMap providers.
    /// </summary>
    [ResourceEntry("OnlyOneBackendSiteMapProviderAllowed", Description = "This error is thrown when more than one BackendSiteMapProvider providers are defined.", LastModified = "2009/04/22", Value = "Only one BackendSiteMapProvider is allowed. In the SiteMap configuration are defined multiple backend SiteMap providers.")]
    public string OnlyOneBackendSiteMapProviderAllowed => this[nameof (OnlyOneBackendSiteMapProviderAllowed)];

    /// <summary>
    /// Message: Adding directly to the collection is not supported. You should assign this collection to the object instead.
    /// </summary>
    [ResourceEntry("AddingDirectlyNotSupported", Description = "This error is thrown by a collection that does not support adding items.", LastModified = "2009/04/07", Value = "Adding directly to the collection is not supported. You should assign this collection to the object instead.")]
    public string AddingDirectlyNotSupported => this[nameof (AddingDirectlyNotSupported)];

    /// <summary>
    /// Message: Template parser encountered unknown character sequence "&lt;%{0}".
    /// </summary>
    [ResourceEntry("KeyNotPresentInLocalResources", Description = "This error is thrown if requested local resource is missing.", LastModified = "2009/04/03", Value = "The key \"{0}\" was not present in the local resources.")]
    public string KeyNotPresentInLocalResources => this[nameof (KeyNotPresentInLocalResources)];

    /// <summary>
    /// Message: Template parser encountered unknown character sequence "&lt;%{0}".
    /// </summary>
    [ResourceEntry("ResourceIncorrectArgs", Description = "This error is thrown when the number formats in the string do not match the number of arguments passed.", LastModified = "2009/04/02", Value = "Incorrect number of formatting arguments.\r\nClassId: {0}\r\nKey: {1}\r\nValue: {2}\r\nArgs: {3}\r\n")]
    public string ResourceIncorrectArgs => this[nameof (ResourceIncorrectArgs)];

    /// <summary>
    /// Message: Template parser encountered unknown character sequence "&lt;%{0}".
    /// </summary>
    [ResourceEntry("TemplateParser_UnknownCharacterSequence", Description = "This error is thrown when template parser encounters unknown character sequence.", LastModified = "2009/03/31", Value = "Template parser encountered unknown character sequence \"<%{0}\".")]
    public string TemplateParser_UnknownCharacterSequence => this[nameof (TemplateParser_UnknownCharacterSequence)];

    /// <summary>
    /// Message: Invalid property name "{0}" for resource type "{1}".
    /// </summary>
    [ResourceEntry("Resource_InvalidPropertyName", Description = "This error is thrown when invalid property name is specified for a localizable resource.", LastModified = "2009/04/01", Value = "Invalid property name \"{0}\" for resource type \"{1}\".")]
    public string Resource_InvalidPropertyName => this[nameof (Resource_InvalidPropertyName)];

    /// <summary>
    /// Message: ResourceName property for the ResourceLinks has not been defined
    /// </summary>
    [ResourceEntry("ResourceLink_ResourceNameNotDefined", Description = "This error is thrown if the ResourceName for the ResourceLinks control has not been defined.", LastModified = "2009/04/01", Value = "ResourceName property for the ResourceLinks has not been defined")]
    public string ResourceLink_ResourceNameNotDefined => this[nameof (ResourceLink_ResourceNameNotDefined), CultureInfo.CurrentCulture];

    /// <summary>
    /// Message: ResourceName property of the ResourceLinks does not have defined extension which is not allowed
    /// </summary>
    [ResourceEntry("ResourceLink_ResourceNameMissingExtension", Description = "This error is thrown if the ResourceName property of the ResourceLinks control does not have a specified extension.", LastModified = "2009/04/01", Value = "ResourceName property of the ResourceLinks does not have defined extension which is not allowed")]
    public string ResourceLink_ResourceNameMissingExtension => this[nameof (ResourceLink_ResourceNameMissingExtension), CultureInfo.CurrentCulture];

    /// <summary>
    /// Message: ResourceName property of the ResourceLinks has specified a resource with an invalid extension.
    /// Allowed extensions are: .css, .jpg, .jpeg, .gif, .png and .js
    /// </summary>
    [ResourceEntry("ResourceLink_ResourceNameInvalidExtension", Description = "This error is thrown if the ResourceName has defined a resource with an invalid extension.", LastModified = "2009/04/01", Value = "ResourceName property of the ResourceLinks has specified a resource with an invalid extension. Allowed extensions are: .css, .jpg, .jpeg, .gif, .png and .js")]
    public string ResourceLink_ResourceNameInvalidExtension => this[nameof (ResourceLink_ResourceNameInvalidExtension), CultureInfo.CurrentCulture];

    /// <summary>
    /// Message: Local resources are not supported for embedded templates. Please use global resources instead.
    /// </summary>
    [ResourceEntry("LocalResourcesNotSupported", Description = "This error is thrown when local resource is specified in embedded template.", LastModified = "2009/04/01", Value = "Local resources are not supported for embedded templates. Please use global resources instead.")]
    public string LocalResourcesNotSupported => this[nameof (LocalResourcesNotSupported)];

    /// <summary>Message: Invalid class ID specified "{0}".</summary>
    [ResourceEntry("InvalidClassId", Description = "This error is thrown when global resource is requested by wrong class ID.", LastModified = "2009/04/01", Value = "A resource file with the specified class ID \"{0}\" was not found.")]
    public string InvalidClassId => this[nameof (InvalidClassId)];

    /// <summary>
    /// Message: Invalid class ID specified "{0}". This class ID declaration appears in {1}.
    /// </summary>
    [ResourceEntry("InvalidClassIdInTemplate", Description = "This error is thrown when global resource is requested by wrong class ID.", LastModified = "2009/04/02", Value = "A resource file with the specified class ID \"{0}\" was not found. This class ID declaration appears in {1}.")]
    public string InvalidClassIdInTemplate => this[nameof (InvalidClassIdInTemplate)];

    /// <summary>
    /// Message: Invalid data item type "{0}". Accepted item types are: {1}.
    /// </summary>
    [ResourceEntry("InvalidItemType", Description = "This error is thrown when invalid type of data item is passed as parameter to data provider.", LastModified = "2009/04/02", Value = "Invalid data item type \"{0}\". Accepted item types are: {1}.")]
    public string InvalidItemType => this[nameof (InvalidItemType)];

    /// <summary>
    /// Message: The key "{0}" is not defined in resource file with class ID "{1}". This key declaration appears in {2}.
    /// </summary>
    [ResourceEntry("InvalidResourceKey", Description = "This error is thrown when global resource is specified with wrong key in a template.", LastModified = "2009/04/02", Value = "The key \"{0}\" is not defined in resource file with class ID \"{1}\". This key declaration appears in {2}.")]
    public string InvalidResourceKey => this[nameof (InvalidResourceKey)];

    /// <summary>
    /// Message: The key "{0}" is not defined in resource file with class ID "{1}". This key declaration appears in {2}.
    /// </summary>
    [ResourceEntry("InvalidResourceKeyOrClassId", Description = "This error is thrown when global resource is specified with wrong key or class ID.", LastModified = "2009/04/07", Value = "Could not find the specified key \"{0}\" or class id \"{1}\".")]
    public string InvalidResourceKeyOrClassId => this[nameof (InvalidResourceKeyOrClassId)];

    /// <summary>
    /// Message: The key "{0}" is not defined in resource file with class ID "{1}". This key declaration appears in {2}.
    /// </summary>
    [ResourceEntry("NotJoinedToTransaction", Description = "This error is thrown when IDataItem.Save() method is called to a new item.", LastModified = "2009/04/07", Value = "Item must be joined to a transaction in order to be saved.")]
    public string NotJoinedToTransaction => this[nameof (NotJoinedToTransaction)];

    /// <summary>
    /// Message: Required control with the pageId 'pageContent' was not found on the backend page template.
    /// </summary>
    [ResourceEntry("PageContentPlaceholderNotFound", Description = "This error is thrown when required control with the id 'pageContent' does not exists on the backend page template.", LastModified = "2009/04/15", Value = "Required control with the id 'pageContent' was not found on the backend page template.")]
    public string PageContentPlaceholderNotFound => this[nameof (PageContentPlaceholderNotFound)];

    /// <summary>Message: Resource with the name '{0}' does not exist!</summary>
    /// <value>The name of the invalid resource.</value>
    [ResourceEntry("InvalidResourceName", Description = "This error is thrown when an embedded resource that does not exist is being used.", LastModified = "2009/04/24", Value = "Resource with the name '{0}' does not exist!")]
    public string InvalidResourceName => this[nameof (InvalidResourceName)];

    /// <summary>Message: Invalid redirect key "{0}" was specified.</summary>
    [ResourceEntry("InvalidRedirectKey", Description = "This error is thrown when an invalid key is specified for BackendRedirectRouteHandler.", LastModified = "2009/04/26", Value = "Invalid redirect key \"{0}\" was specified.")]
    public string InvalidRedirectKey => this[nameof (InvalidRedirectKey)];

    /// <summary>Message: Page object is not set to a reference.</summary>
    [ResourceEntry("PageIsNull", Description = "This error is thrown when the page object is required by the code, yet it is not set to a reference.", LastModified = "2009/05/20", Value = "Page object is not set to a reference.")]
    public string PageIsNull => this[nameof (PageIsNull)];

    /// <summary>
    /// Message: ScriptManager object is not set to a reference.
    /// </summary>
    [ResourceEntry("ScriptManagerIsNull", Description = "This error is thrown when the ScriptManager object is required by the code, yet it is not present on the page.", LastModified = "2009/05/20", Value = "ScriptManager object is not set to a reference.")]
    public string ScriptManagerIsNull => this[nameof (ScriptManagerIsNull)];

    /// <summary>
    /// Message: Required property '{0}' has not been defined on the object '{1}'.
    /// </summary>
    [ResourceEntry("RequiredPropertyNotDefined", Description = "This error is thrown when a required property has not been defined on an object.", LastModified = "2009/05/20", Value = "Required property '{0}' has not been defined on the object '{1}'.")]
    public string RequiredPropertyNotDefined => this[nameof (RequiredPropertyNotDefined)];

    /// <summary>
    /// Message: File set as a value of the property '{0}' on the object '{1}' cannot be found.
    /// </summary>
    [ResourceEntry("DefinedFileNotFound", Description = "This error is thrown when the value of a property is a file path, but the specified file cannot be found.", LastModified = "2009/05/20", Value = "File set as a value of the property '{0}' on the object '{1}' cannot be found.")]
    public string DefinedFileNotFound => this[nameof (DefinedFileNotFound)];

    /// <summary>
    /// Message: We were unable to access your information. Please try again.
    /// </summary>
    [ResourceEntry("PasswordRecoveryDefaultUserNameFailureText", Description = "PasswordRecovery controls - cannot retrieve information for the supplied Username.", LastModified = "2009/05/27", Value = "We were unable to access your information. Please try again.")]
    public string PasswordRecoveryDefaultUserNameFailureText => this[nameof (PasswordRecoveryDefaultUserNameFailureText)];

    /// <summary>
    /// Message: Your answer could not be verified. Please try again.
    /// </summary>
    [ResourceEntry("PasswordRecoveryDefaultQuestionFailureText", Description = "PasswordRecovery controls - Your answer could not be verified. Please try again.", LastModified = "2009/05/27", Value = "Your answer could not be verified. Please try again.")]
    public string PasswordRecoveryDefaultQuestionFailureText => this[nameof (PasswordRecoveryDefaultQuestionFailureText)];

    /// <summary>
    /// Message: Enter your User Name to receive your password.
    /// </summary>
    [ResourceEntry("PasswordRecoveryDefaultUserNameInstructionText", Description = "PasswordRecovery controls - Enter your User Name to receive your password.", LastModified = "2009/05/27", Value = "Enter your User Name to receive your password.")]
    public string PasswordRecoveryDefaultUserNameInstructionText => this[nameof (PasswordRecoveryDefaultUserNameInstructionText)];

    /// <summary>Message: Label for the username.</summary>
    [ResourceEntry("PasswordRecoveryDefaultUserNameLabelText", Description = "PasswordRecovery controls - Label for the username.", LastModified = "2009/05/27", Value = "User Name")]
    public string PasswordRecoveryDefaultUserNameLabelText => this[nameof (PasswordRecoveryDefaultUserNameLabelText)];

    /// <summary>Message: Question:</summary>
    [ResourceEntry("PasswordRecoveryDefaultQuestionLabelText", Description = "PasswordRecovery controls - Question", LastModified = "2009/05/27", Value = "Question")]
    public string PasswordRecoveryDefaultQuestionLabelText => this[nameof (PasswordRecoveryDefaultQuestionLabelText)];

    /// <summary>Message: Please enter a different password.</summary>
    [ResourceEntry("PasswordInvalidPasswordErrorMessage", Description = "PasswordRecovery controls - Please enter a different password.", LastModified = "2009/05/27", Value = "Please enter a different password.")]
    public string PasswordInvalidPasswordErrorMessage => this[nameof (PasswordInvalidPasswordErrorMessage)];

    /// <summary>Message: Answer label text</summary>
    [ResourceEntry("PasswordRecoveryDefaultAnswerLabelText", Description = "PasswordRecovery controls - Answer", LastModified = "2009/05/27", Value = "Answer")]
    public string PasswordRecoveryDefaultAnswerLabelText => this[nameof (PasswordRecoveryDefaultAnswerLabelText)];

    /// <summary>Message: Identity Confirmation</summary>
    [ResourceEntry("PasswordRecoveryIdentityConfirmation", Description = "PasswordRecovery controls - Identity Confirmation", LastModified = "2009/05/29", Value = "Identity Confirmation")]
    public string PasswordRecoveryIdentityConfirmation => this[nameof (PasswordRecoveryIdentityConfirmation)];

    /// <summary>
    /// Message: Answer the following question to receive your password.
    /// </summary>
    [ResourceEntry("PasswordRecoveryAnswerQuestionLabel", Description = "PasswordRecovery controls - Answer the following question to receive your password.", LastModified = "2009/05/29", Value = "Answer the following question to receive your password.")]
    public string PasswordRecoveryAnswerQuestionLabel => this[nameof (PasswordRecoveryAnswerQuestionLabel)];

    /// <summary>Message: Username cannot be empty</summary>
    [ResourceEntry("PasswordRecoveryDefaultUserNameRequiredErrorMessage", Description = "PasswordRecovery controls - Username cannot be empty", LastModified = "2009/05/27", Value = "Username cannot be empty")]
    public string PasswordRecoveryDefaultUserNameRequiredErrorMessage => this[nameof (PasswordRecoveryDefaultUserNameRequiredErrorMessage)];

    /// <summary>Message: Forgot your password?</summary>
    [ResourceEntry("PasswordRecoveryDefaultUserNameTitleText", Description = "PasswordRecovery controls - Forgot Your Password?", LastModified = "2009/05/27", Value = "Forgot your password?")]
    public string PasswordRecoveryDefaultUserNameTitleText => this[nameof (PasswordRecoveryDefaultUserNameTitleText)];

    /// <summary>
    /// Message: Membership provider does not support password retrieval or reset.
    /// </summary>
    [ResourceEntry("PasswordRecoveryRecoveryNotSupported", Description = "PasswordRecovery controls - Membership provider does not support password retrieval or reset.", LastModified = "2009/05/27", Value = "Membership provider does not support password retrieval or reset.")]
    public string PasswordRecoveryRecoveryNotSupported => this[nameof (PasswordRecoveryRecoveryNotSupported)];

    /// <summary>
    /// Message: Default subject of the mail for password recovery.
    /// </summary>
    [ResourceEntry("PasswordRecoveryDefaultSubject", Description = "PasswordRecovery controls - Default subject of the mail.", LastModified = "2009/05/27", Value = "Password")]
    public string PasswordRecoveryDefaultSubject => this[nameof (PasswordRecoveryDefaultSubject)];

    /// <summary>Message: The mail message body.</summary>
    [ResourceEntry("PasswordRecoveryDefaultBody", Description = "PasswordRecovery controls - the mail message body.", LastModified = "2012/01/05", Value = "Your password has been successfully changed.<br /><br />User Name: <%\\s*UserName\\s*%><br />Password: <%\\s*Password\\s*%>")]
    public string PasswordRecoveryDefaultBody => this[nameof (PasswordRecoveryDefaultBody)];

    /// <summary>Message: Answer can't be empty</summary>
    [ResourceEntry("PasswordRecoveryDefaultAnswerRequiredErrorMessage", Description = "Answer can't be empty", LastModified = "2009/05/27", Value = "Answer can't be empty")]
    public string PasswordRecoveryDefaultAnswerRequiredErrorMessage => this[nameof (PasswordRecoveryDefaultAnswerRequiredErrorMessage)];

    /// <summary>
    /// Message: Your attempt to retrieve your password was not successful. Please try again.
    /// </summary>
    [ResourceEntry("PasswordRecoveryDefaultGeneralFailureText", Description = "PasswordRecovery controls - Your attempt to retrieve your password was not successful. Please try again.", LastModified = "2009/05/27", Value = "Your attempt to retrieve your password was not successful. Please try again.")]
    public string PasswordRecoveryDefaultGeneralFailureText => this[nameof (PasswordRecoveryDefaultGeneralFailureText)];

    /// <summary>Message: Submit</summary>
    [ResourceEntry("PasswordRecoveryDefaultSubmitButtonText", Description = "PasswordRecovery controls - label of the Submit buttons.", LastModified = "2009/05/27", Value = "Submit")]
    public string PasswordRecoveryDefaultSubmitButtonText => this[nameof (PasswordRecoveryDefaultSubmitButtonText)];

    /// <summary>Message: Your password has been sent to you.</summary>
    [ResourceEntry("PasswordRecoveryDefaultSuccessText", Description = "PasswordRecovery controls - Your password has been sent to you.", LastModified = "2009/05/27", Value = "Your password has been sent to you.")]
    public string PasswordRecoveryDefaultSuccessText => this[nameof (PasswordRecoveryDefaultSuccessText)];

    /// <summary>Message: Password incorrect or New Password invalid</summary>
    [ResourceEntry("ChangePasswordDefaultChangePasswordFailureText", Description = "ChangePassword control - Password incorrect or New Password invalid.", LastModified = "2009/05/27", Value = "Username or/and Password incorrect or New Password invalid. The minimum length of the new password is {0} characters of which {1} must be non-alphanumeric.")]
    public string ChangePasswordDefaultChangePasswordFailureText => this[nameof (ChangePasswordDefaultChangePasswordFailureText)];

    /// <summary>Message: Password incorrect</summary>
    [ResourceEntry("ChangePasswordQuestionAndAnswerDefaultChangePasswordFailureText", Description = "ChangePasswordQuestionAndAnswer control - Password incorrect or New Question or/and Answer are invalid.", LastModified = "2015/03/17", Value = "Password incorrect or New Question or/and Answer are invalid.")]
    public string ChangePasswordQuestionAndAnswerDefaultChangePasswordFailureText => this[nameof (ChangePasswordQuestionAndAnswerDefaultChangePasswordFailureText)];

    /// <summary>Message: Confirm New Password is required</summary>
    [ResourceEntry("ChangePasswordDefaultConfirmPasswordRequiredErrorMessage", Description = "ChangePassword control - Confirm New Password is required", LastModified = "2009/05/27", Value = "Confirm New Password is required")]
    public string ChangePasswordDefaultConfirmPasswordRequiredErrorMessage => this[nameof (ChangePasswordDefaultConfirmPasswordRequiredErrorMessage)];

    /// <summary>Message: Change Your Password</summary>
    [ResourceEntry("ChangePasswordDefaultChangePasswordTitle", Description = "ChangePassword control - Change Your Password", LastModified = "2009/05/27", Value = "Change Your Password")]
    public string ChangePasswordDefaultChangePasswordTitle => this[nameof (ChangePasswordDefaultChangePasswordTitle)];

    /// <summary>Message: The new password entries do not match.</summary>
    [ResourceEntry("ConfirmPasswordCompareErrorMessage", Description = "Change password - The new password entries do not match.", LastModified = "2010/09/20", Value = "The new password entries do not match.")]
    public string ConfirmPasswordCompareErrorMessage => this[nameof (ConfirmPasswordCompareErrorMessage)];

    /// <summary>Message: Continue</summary>
    [ResourceEntry("ChangePasswordDefaultContinueButtonText", Description = "ChangePassword control - Continue", LastModified = "2009/05/27", Value = "Continue")]
    public string ChangePasswordDefaultContinueButtonText => this[nameof (ChangePasswordDefaultContinueButtonText)];

    /// <summary>Message: New password is required</summary>
    [ResourceEntry("ChangePasswordDefaultNewPasswordRequiredErrorMessage", Description = "ChangePassword control - New password is required", LastModified = "2009/05/27", Value = "New password is required")]
    public string ChangePasswordDefaultNewPasswordRequiredErrorMessage => this[nameof (ChangePasswordDefaultNewPasswordRequiredErrorMessage)];

    /// <summary>Message: Your password has been changed!</summary>
    [ResourceEntry("ChangePasswordDefaultSuccessText", Description = "ChangePassword control - Your password has been changed!", LastModified = "2009/05/27", Value = "Your password has been changed!")]
    public string ChangePasswordDefaultSuccessText => this[nameof (ChangePasswordDefaultSuccessText)];

    /// <summary>Message: Change Password Complete</summary>
    [ResourceEntry("ChangePasswordDefaultSuccessTitleText", Description = "ChangePassword control - Change Password Complete", LastModified = "2009/05/27", Value = "Change Password Complete")]
    public string ChangePasswordDefaultSuccessTitleText => this[nameof (ChangePasswordDefaultSuccessTitleText)];

    /// <summary>Message: User Name:</summary>
    [ResourceEntry("ChangePasswordDefaultUserNameLabelText", Description = "ChangePassword control - User Name", LastModified = "2009/05/27", Value = "User Name")]
    public string ChangePasswordDefaultUserNameLabelText => this[nameof (ChangePasswordDefaultUserNameLabelText)];

    /// <summary>Message: Username is required</summary>
    [ResourceEntry("ChangePasswordDefaultUserNameRequiredErrorMessage", Description = "ChangePassword control - Username is required", LastModified = "2009/05/27", Value = "Username is required")]
    public string ChangePasswordDefaultUserNameRequiredErrorMessage => this[nameof (ChangePasswordDefaultUserNameRequiredErrorMessage)];

    /// <summary>Message: Password is required</summary>
    [ResourceEntry("ChangePasswordDefaultPasswordRequiredErrorMessage", Description = "ChangePassword control - Password is required", LastModified = "2009/05/27", Value = "Password is required")]
    public string ChangePasswordDefaultPasswordRequiredErrorMessage => this[nameof (ChangePasswordDefaultPasswordRequiredErrorMessage)];

    /// <summary>Message: Password Retrieval Not Enabled.</summary>
    [ResourceEntry("PasswordRetrievalNotEnabled", Description = "EnablePasswordRetrieval is set to false in membership provider method GetPassword.", LastModified = "2009/05/28", Value = "Password Retrieval Not Enabled.")]
    public string PasswordRetrievalNotEnabled => this[nameof (PasswordRetrievalNotEnabled)];

    /// <summary>Message: Error changing password.</summary>
    [ResourceEntry("ChangePasswordFailureText", Description = "Error changing password", LastModified = "2010/09/23", Value = "Error changing password.")]
    public string ChangePasswordGeneralFailureText => this["ChangePasswordFailureText"];

    /// <summary>Message: Error changing password question and answer.</summary>
    [ResourceEntry("ChangePasswordQuestionAndAnswerFailureText", Description = "Error changing password question and answer", LastModified = "2015/03/19", Value = "Error changing password question and answer.")]
    public string ChangePasswordQuestionAndAnswerFailureText => this[nameof (ChangePasswordQuestionAndAnswerFailureText)];

    /// <summary>phrase: Invalid password</summary>
    [ResourceEntry("InvalidPassword", Description = "phrase: Invalid password", LastModified = "2010/09/23", Value = "Invalid password.")]
    public string InvalidPassword => this[nameof (InvalidPassword)];

    /// <summary>Message: Cannot retrieve Hashed passwords.</summary>
    [ResourceEntry("CannotRetrieveHashedPasswords", Description = "Cannot retrieve Hashed passwords.", LastModified = "2009/05/28", Value = "Cannot retrieve Hashed passwords.")]
    public string CannotRetrieveHashedPasswords => this[nameof (CannotRetrieveHashedPasswords)];

    /// <summary>Message: User not found.</summary>
    [ResourceEntry("MembershipUserNotFound", Description = "This error is thrown by the Membership provider.", LastModified = "2009/05/28", Value = "User not found.")]
    public string MembershipUserNotFound => this[nameof (MembershipUserNotFound)];

    /// <summary>Message: User locked.</summary>
    [ResourceEntry("MembershipUserLocked", Description = "Membership User is locked.", LastModified = "2009/05/28", Value = "User locked.")]
    public string MembershipUserLocked => this[nameof (MembershipUserLocked)];

    /// <summary>Message: Password answer required for password reset.</summary>
    [ResourceEntry("PasswordAnswerRequired", Description = "Password answer required for password reset.", LastModified = "2009/05/28", Value = "Password answer required for password reset.")]
    public string PasswordAnswerRequired => this[nameof (PasswordAnswerRequired)];

    /// <summary>Message: Wrong password answer.</summary>
    [ResourceEntry("WrongPasswordAnswer", Description = "Wrong password answer.", LastModified = "2009/05/28", Value = "Wrong password answer.")]
    public string WrongPasswordAnswer => this[nameof (WrongPasswordAnswer)];

    /// <summary>Message: Invalid provider name.</summary>
    [ResourceEntry("InvalidProviderName", Description = "Invalid provider name.", LastModified = "2009/10/14", Value = "Invalid provider name.")]
    public string InvalidProviderName => this[nameof (InvalidProviderName)];

    /// <summary>
    /// Message: Invalid providerUserKey type. Type must be GUID.
    /// </summary>
    [ResourceEntry("InvalidProviderUserKeyType", Description = "Invalid providerUserKey type. Type must be GUID.", LastModified = "2009/05/28", Value = "Invalid providerUserKey type. Type must be GUID.")]
    public string InvalidProviderUserKeyType => this[nameof (InvalidProviderUserKeyType)];

    /// <summary>Message: Password reset is not enabled.</summary>
    [ResourceEntry("PasswordResetIsNotEnabled", Description = "Password reset is not enabled.", LastModified = "2009/05/28", Value = "Password reset is not enabled. Set the EnablePasswordReset property of the membership provider to true.")]
    public string PasswordResetIsNotEnabled => this[nameof (PasswordResetIsNotEnabled)];

    /// <summary>
    /// Message: The mail message body for ChangePassword control.
    /// </summary>
    [ResourceEntry("ChangePasswordDefaultBody", Description = "ChangePassword controls - the mail message body.", LastModified = "2009/05/27", Value = "Please return to the site and log in using the following information.\nUser Name: <%UserName%>\nNew Password: <%Password%>\n\n")]
    public string ChangePasswordDefaultBody => this[nameof (ChangePasswordDefaultBody)];

    /// <summary>
    /// Message: CreateUserWizard - The text to be shown if the passwors answer textbox is missing from the template.
    /// </summary>
    [ResourceEntry("CreateUserWizardNoQuestionTextBox", Description = "CreateUserWizard - The text to be shown if the passwors answer textbox is missing from the template.", LastModified = "2009/05/30", Value = "{0}: CreateUserWizardStep.ContentTemplate does not contain an IEditableTextControl with ID {1} for the security question, this is required if your membership provider requires a question and answer.")]
    public string CreateUserWizardNoQuestionTextBox => this[nameof (CreateUserWizardNoQuestionTextBox)];

    /// <summary>
    /// Message: The error me to be shown if required textbox control is not presented in the template.
    /// </summary>
    [ResourceEntry("CreateUserWizardNoUserNameTextBox", Description = "CreateUserWizard - The error me to be shown if required textbox control is not presented in the template.", LastModified = "2009/05/30", Value = "{0}: CreateUserWizardStep.ContentTemplate does not contain an IEditableTextControl with ID {1} for the username.")]
    public string CreateUserWizardNoUserNameTextBox => this[nameof (CreateUserWizardNoUserNameTextBox)];

    /// <summary>
    /// Message: {0} control must contain a Label with ID {1} in its ItemTemplate.
    /// </summary>
    [ResourceEntry("CreateUserWizardSidebarLabelNotFound", Description = "CreateUserWizard - {0} control must contain a Label with ID {1} in its ItemTemplate.", LastModified = "2009/05/30", Value = "{0} control must contain a Label with ID {1} in its ItemTemplate.")]
    public string CreateUserWizardSidebarLabelNotFound => this[nameof (CreateUserWizardSidebarLabelNotFound)];

    /// <summary>
    /// Message: There can only be one CreateUserWizardStep in your WizardSteps.
    /// </summary>
    [ResourceEntry("CreateUserWizardDuplicateCreateUserWizardStep", Description = "CreateUserWizard - There can only be one CreateUserWizardStep in your WizardSteps.", LastModified = "2009/05/30", Value = "There can only be one CreateUserWizardStep in your WizardSteps.")]
    public string CreateUserWizardDuplicateCreateUserWizardStep => this[nameof (CreateUserWizardDuplicateCreateUserWizardStep)];

    /// <summary>
    /// Message: There can only be one CompleteWizardStep in your WizardSteps.
    /// </summary>
    /// <value>Message: There can only be one CompleteWizardStep in your WizardSteps.</value>
    [ResourceEntry("CreateUserWizardDuplicateCompleteWizardStep", Description = "CreateUserWizard - There can only be one CompleteWizardStep in your WizardSteps.", LastModified = "2009/05/30", Value = "There can only be one CompleteWizardStep in your WizardSteps.")]
    public string CreateUserWizardDuplicateCompleteWizardStep => this[nameof (CreateUserWizardDuplicateCompleteWizardStep)];

    /// <summary>Message: Invalid viewstate.</summary>
    [ResourceEntry("ViewStateInvalidViewState", Description = "CreateUserWizard - Invalid viewstate.", LastModified = "2009/05/30", Value = "Invalid viewstate.")]
    public string ViewStateInvalidViewState => this[nameof (ViewStateInvalidViewState)];

    /// <summary>
    /// Message: Could not find the specified membership provider.
    /// </summary>
    [ResourceEntry("WebControlCannotFindProvider", Description = "CreateUserWizard - Could not find the specified membership provider.", LastModified = "2009/05/30", Value = "Could not find the specified membership provider.")]
    public string WebControlCannotFindProvider => this[nameof (WebControlCannotFindProvider)];

    /// <summary>Message: The text that identifies the answer textbox.</summary>
    [ResourceEntry("CreateUserWizardDefaultAnswerLabelText", Description = "CreateUserWizard - The text that identifies the answer textbox.", LastModified = "2015/03/19", Value = "Security answer")]
    public string CreateUserWizardDefaultAnswerLabelText => this[nameof (CreateUserWizardDefaultAnswerLabelText)];

    /// <summary>
    /// Message: The text to be shown in the validation summary when the answer is empty.
    /// </summary>
    [ResourceEntry("CreateUserWizardDefaultAnswerRequiredErrorMessage", Description = "CreateUserWizard - The text to be shown in the validation summary when the answer is empty.", LastModified = "2009/05/30", Value = "Security answer cannot be empty")]
    public string CreateUserWizardDefaultAnswerRequiredErrorMessage => this[nameof (CreateUserWizardDefaultAnswerRequiredErrorMessage)];

    /// <summary>
    /// Message: The text to be shown after the user has been created.
    /// </summary>
    [ResourceEntry("CreateUserWizardDefaultCompleteSuccessText", Description = "CreateUserWizard - The text to be shown after the user has been created.", LastModified = "2009/05/30", Value = "Your account has been successfully created.")]
    public string CreateUserWizardDefaultCompleteSuccessText => this[nameof (CreateUserWizardDefaultCompleteSuccessText)];

    /// <summary>
    /// Message: The text to be shown in the validation summary when the password and confirm password do not match.
    /// </summary>
    [ResourceEntry("CreateUserWizardDefaultConfirmPasswordCompareErrorMessage", Description = "CreateUserWizard - The text to be shown in the validation summary when the password and confirm password do not match.", LastModified = "2009/05/30", Value = "The password and confirmation password must match")]
    public string CreateUserWizardDefaultConfirmPasswordCompareErrorMessage => this[nameof (CreateUserWizardDefaultConfirmPasswordCompareErrorMessage)];

    /// <summary>
    /// Message: The text that identifies the confirm password textbox.
    /// </summary>
    [ResourceEntry("CreateUserWizardDefaultConfirmPasswordLabelText", Description = "CreateUserWizard - The text that identifies the confirm password textbox.", LastModified = "2009/05/30", Value = "Confirm password")]
    public string CreateUserWizardDefaultConfirmPasswordLabelText => this[nameof (CreateUserWizardDefaultConfirmPasswordLabelText)];

    /// <summary>
    /// Message: The text to be shown in the validation summary when the confirm password is empty.
    /// </summary>
    [ResourceEntry("CreateUserWizardDefaultConfirmPasswordRequiredErrorMessage", Description = "CreateUserWizard - The text to be shown in the validation summary when the confirm password is empty.", LastModified = "2009/05/30", Value = "Confirm password cannot be empty")]
    public string CreateUserWizardDefaultConfirmPasswordRequiredErrorMessage => this[nameof (CreateUserWizardDefaultConfirmPasswordRequiredErrorMessage)];

    /// <summary>Message: The text of the continue button.</summary>
    [ResourceEntry("CreateUserWizardDefaultContinueButtonText", Description = "CreateUserWizard - The text of the continue button.", LastModified = "2009/05/30", Value = "Continue")]
    public string CreateUserWizardDefaultContinueButtonText => this[nameof (CreateUserWizardDefaultContinueButtonText)];

    /// <summary>Message: The text of the create user button.</summary>
    [ResourceEntry("CreateUserWizardDefaultCreateUserButtonText", Description = "CreateUserWizard - The text of the create user button.", LastModified = "2009/05/30", Value = "Create User")]
    public string CreateUserWizardDefaultCreateUserButtonText => this[nameof (CreateUserWizardDefaultCreateUserButtonText)];

    /// <summary>
    /// Message: Text to be shown when a duplicate email error is returned from create user.
    /// </summary>
    [ResourceEntry("CreateUserWizardDefaultDuplicateEmailErrorMessage", Description = "CreateUserWizard - Text to be shown when a duplicate email error is returned from create user.", LastModified = "2009/05/30", Value = "The email address that you entered is already in use. Please enter a different email address.")]
    public string CreateUserWizardDefaultDuplicateEmailErrorMessage => this[nameof (CreateUserWizardDefaultDuplicateEmailErrorMessage)];

    /// <summary>
    /// Message: Text to be shown when a duplicate username error is returned from create user.
    /// </summary>
    [ResourceEntry("CreateUserWizardDefaultDuplicateUserNameErrorMessage", Description = "CreateUserWizard - Text to be shown when a duplicate email error is returned from create user.", LastModified = "2016/12/07", Value = "Please enter a different email.")]
    public string CreateUserWizardDefaultDuplicateUserNameErrorMessage => this[nameof (CreateUserWizardDefaultDuplicateUserNameErrorMessage)];

    /// <summary>Message: The text that identifies the email textbox.</summary>
    [ResourceEntry("CreateUserWizardDefaultEmailLabelText", Description = "CreateUserWizard - The text that identifies the email textbox.", LastModified = "2009/05/30", Value = "Email")]
    public string CreateUserWizardDefaultEmailLabelText => this[nameof (CreateUserWizardDefaultEmailLabelText)];

    /// <summary>
    /// Message: The text to be shown in the validation summary when the email does not match the regular expression.
    /// </summary>
    [ResourceEntry("CreateUserWizardDefaultEmailRegularExpressionErrorMessage", Description = "CreateUserWizard - The text to be shown in the validation summary when the email does not match the regular expression.", LastModified = "2009/05/30", Value = "Please enter a valid email address")]
    public string CreateUserWizardDefaultEmailRegularExpressionErrorMessage => this[nameof (CreateUserWizardDefaultEmailRegularExpressionErrorMessage)];

    /// <summary>
    /// Message: The text to be shown in the validation summary when the email is empty.
    /// </summary>
    [ResourceEntry("CreateUserWizardDefaultEmailRequiredErrorMessage", Description = "CreateUserWizard - The text to be shown in the validation summary when the email is empty.", LastModified = "2009/05/30", Value = "Email cannot be empty")]
    public string CreateUserWizardDefaultEmailRequiredErrorMessage => this[nameof (CreateUserWizardDefaultEmailRequiredErrorMessage)];

    /// <summary>
    /// Message: Text to be shown when the security answer is invalid.
    /// </summary>
    [ResourceEntry("CreateUserWizardDefaultInvalidAnswerErrorMessage", Description = "CreateUserWizard - Text to be shown when the security answer is invalid.", LastModified = "2009/05/30", Value = "Please enter a different security answer.")]
    public string CreateUserWizardDefaultInvalidAnswerErrorMessage => this[nameof (CreateUserWizardDefaultInvalidAnswerErrorMessage)];

    /// <summary>
    /// Message: The text to be shown when the email is invalid.
    /// </summary>
    [ResourceEntry("CreateUserWizardDefaultInvalidEmailErrorMessage", Description = "CreateUserWizard - The text to be shown when the email is invalid.", LastModified = "2009/05/30", Value = "Please enter a valid email address.")]
    public string CreateUserWizardDefaultInvalidEmailErrorMessage => this[nameof (CreateUserWizardDefaultInvalidEmailErrorMessage)];

    /// <summary>
    /// Message: The text to be shown when the password is invalid.
    /// </summary>
    [ResourceEntry("CreateUserWizardDefaultInvalidPasswordErrorMessage", Description = "CreateUserWizard - The text to be shown when the password is invalid.", LastModified = "2009/05/30", Value = "Password length minimum: {0}. Non-alphanumeric characters required: {1}.")]
    public string CreateUserWizardDefaultInvalidPasswordErrorMessage => this[nameof (CreateUserWizardDefaultInvalidPasswordErrorMessage)];

    /// <summary>
    /// Message: Text to be shown when the security question is invalid.
    /// </summary>
    [ResourceEntry("CreateUserWizardDefaultInvalidQuestionErrorMessage", Description = "CreateUserWizard - Text to be shown when the security question is invalid.", LastModified = "2009/05/30", Value = "Please enter a different security question.")]
    public string CreateUserWizardDefaultInvalidQuestionErrorMessage => this[nameof (CreateUserWizardDefaultInvalidQuestionErrorMessage)];

    /// <summary>
    /// Message: The text that identifies the password textbox.
    /// </summary>
    [ResourceEntry("LoginControlsDefaultPasswordLabelText", Description = "CreateUserWizard - The text that identifies the password textbox.", LastModified = "2009/05/30", Value = "Password")]
    public string LoginControlsDefaultPasswordLabelText => this[nameof (LoginControlsDefaultPasswordLabelText)];

    /// <summary>
    /// Message: The text to be shown in the validation summary when the password is empty.
    /// </summary>
    [ResourceEntry("CreateUserWizardDefaultPasswordRequiredErrorMessage", Description = "CreateUserWizard - The text to be shown in the validation summary when the password is empty", LastModified = "2009/05/30", Value = "Password is required")]
    public string CreateUserWizardDefaultPasswordRequiredErrorMessage => this[nameof (CreateUserWizardDefaultPasswordRequiredErrorMessage)];

    /// <summary>
    /// Message: The text that identifies the question textbox.
    /// </summary>
    [ResourceEntry("CreateUserWizardDefaultQuestionLabelText", Description = "CreateUserWizard - The text that identifies the question textbox.", LastModified = "2015/03/19", Value = "Security question")]
    public string CreateUserWizardDefaultQuestionLabelText => this[nameof (CreateUserWizardDefaultQuestionLabelText)];

    /// <summary>
    /// Message: The text to be shown in the validation summary when the question is empty.
    /// </summary>
    [ResourceEntry("CreateUserWizardDefaultQuestionRequiredErrorMessage", Description = "CreateUserWizard - The text to be shown in the validation summary when the question is empty.", LastModified = "2009/05/30", Value = "Security question cannot be empty")]
    public string CreateUserWizardDefaultQuestionRequiredErrorMessage => this[nameof (CreateUserWizardDefaultQuestionRequiredErrorMessage)];

    /// <summary>Message: The label for Username textbox.</summary>
    [ResourceEntry("CreateUserWizardDefaultUserNameLabelText", Description = "CreateUserWizard - User Name", LastModified = "2009/05/30", Value = "User Name")]
    public string CreateUserWizardDefaultUserNameLabelText => this[nameof (CreateUserWizardDefaultUserNameLabelText)];

    /// <summary>Message: Sign Up for Your New Account</summary>
    [ResourceEntry("CreateUserWizardDefaultCreateUserTitleText", Description = "CreateUserWizard - Sign Up for Your New Account", LastModified = "2009/05/30", Value = "Sign Up for Your New Account")]
    public string CreateUserWizardDefaultCreateUserTitleText => this[nameof (CreateUserWizardDefaultCreateUserTitleText)];

    /// <summary>Message: Username is required</summary>
    [ResourceEntry("CreateUserWizardDefaultUserNameRequiredErrorMessage", Description = "CreateUserWizard - Username is required", LastModified = "2009/05/30", Value = "Username is required")]
    public string CreateUserWizardDefaultUserNameRequiredErrorMessage => this[nameof (CreateUserWizardDefaultUserNameRequiredErrorMessage)];

    /// <summary>Message: First name is required.</summary>
    [ResourceEntry("CreateUserWizardDefaultFirstNameRequiredErrorMessage", Description = "CreateUserWizard - First name is required", LastModified = "2010/05/12", Value = "First name is required")]
    public string CreateUserWizardDefaultFirstNameRequiredErrorMessage => this[nameof (CreateUserWizardDefaultFirstNameRequiredErrorMessage)];

    /// <summary>Message: Last name is required</summary>
    [ResourceEntry("CreateUserWizardDefaultLastNameRequiredErrorMessage", Description = "CreateUserWizard - Last name is required", LastModified = "2010/05/12", Value = "Last name is required")]
    public string CreateUserWizardDefaultLastNameRequiredErrorMessage => this[nameof (CreateUserWizardDefaultLastNameRequiredErrorMessage)];

    /// <summary>
    /// Message: {0}: CreateUserWizardStep.ContentTemplate does not contain an IEditableTextControl with ID {1} for the security answer, this is required if your membership provider requires a question and answer.
    /// </summary>
    [ResourceEntry("CreateUserWizardNoAnswerTextBox", Description = "CreateUserWizard - {0}: CreateUserWizardStep.ContentTemplate does not contain an IEditableTextControl with ID {1} for the security answer, this is required if your membership provider requires a question and answer.", LastModified = "2009/05/30", Value = "{0}: CreateUserWizardStep.ContentTemplate does not contain an IEditableTextControl with ID {1} for the security answer, this is required if your membership provider requires a question and answer.")]
    public string CreateUserWizardNoAnswerTextBox => this[nameof (CreateUserWizardNoAnswerTextBox)];

    /// <summary>Message: The Textbox for email is missing.</summary>
    [ResourceEntry("CreateUserWizardNoEmailTextBox", Description = "CreateUserWizard - {0}: CreateUserWizardStep.ContentTemplate does not contain an IEditableTextControl with ID {1} for the email, this is required if RequireEmail = true.", LastModified = "2009/05/30", Value = "{0}: CreateUserWizardStep.ContentTemplate does not contain an IEditableTextControl with ID {1} for the email, this is required if RequireEmail = true.")]
    public string CreateUserWizardNoEmailTextBox => this[nameof (CreateUserWizardNoEmailTextBox)];

    /// <summary>Message: The Textbox for password is missing.</summary>
    [ResourceEntry("CreateUserWizardNoPasswordTextBox", Description = "CreateUserWizard - The Textbox for password is missing.", LastModified = "2009/05/30", Value = "{0}: CreateUserWizardStep.ContentTemplate does not contain an IEditableTextControl with ID {1} for the new password, this is required if AutoGeneratePassword = true.")]
    public string CreateUserWizardNoPasswordTextBox => this[nameof (CreateUserWizardNoPasswordTextBox)];

    /// <summary>
    /// Message: The ActiveStepIndex must be less than WizardSteps.Count and at least -1.  For dynamically added steps, make sure they are added before or in Page_PreInit event.
    /// </summary>
    [ResourceEntry("WizardActiveStepIndexOutOfRange", Description = "CreateUserWizard - The ActiveStepIndex must be less than WizardSteps.Count and at least -1.  For dynamically added steps, make sure they are added before or in Page_PreInit event.", LastModified = "2009/05/30", Value = "The ActiveStepIndex must be less than WizardSteps.Count and at least -1.  For dynamically added steps, make sure they are added before or in Page_PreInit event.")]
    public string WizardActiveStepIndexOutOfRange => this[nameof (WizardActiveStepIndexOutOfRange)];

    /// <summary>Message: Complete</summary>
    [ResourceEntry("CreateUserWizardDefaultCompleteTitleText", Description = "Title text of Complete step in CreateUserWizard.", LastModified = "2009/05/30", Value = "Complete")]
    public string CreateUserWizardDefaultCompleteTitleText => this[nameof (CreateUserWizardDefaultCompleteTitleText)];

    /// <summary>
    /// The command '{0}' is not valid for the previous step, make sure the step type is not changed between postbacks.
    /// </summary>
    /// <value>The create user wizard default complete title text.</value>
    [ResourceEntry("WizardInvalidBubbleEvent", Description = "The command '{0}' is not valid for the previous step, make sure the step type is not changed between postbacks.", LastModified = "2009/05/30", Value = "The command '{0}' is not valid for the previous step, make sure the step type is not changed between postbacks.")]
    public string WizardInvalidBubbleEvent => this[nameof (WizardInvalidBubbleEvent)];

    /// <summary>Your account was not created. Please try again.</summary>
    /// <value>The create user wizard default complete title text.</value>
    [ResourceEntry("CreateUserWizardDefaultUnknownErrorMessage", Description = "Your account was not created. Please try again.", LastModified = "2009/05/30", Value = "Your account was not created. Please try again.")]
    public string CreateUserWizardDefaultUnknownErrorMessage => this[nameof (CreateUserWizardDefaultUnknownErrorMessage)];

    /// <summary>The label shown on previous button.</summary>
    /// <value>The label shown on previous button.</value>
    [ResourceEntry("CreateUserWizardDefaultPreviousButtonText", Description = "The label shown on previous button.", LastModified = "2009/05/30", Value = "Previous")]
    public string CreateUserWizardDefaultPreviousButtonText => this[nameof (CreateUserWizardDefaultPreviousButtonText)];

    /// <summary>The label shown on cancel button.</summary>
    /// <value>The label shown on cancel button.</value>
    [ResourceEntry("CreateUserWizardDefaultCancelButtonText", Description = "The label shown on cancel button.", LastModified = "2009/05/30", Value = "Cancel")]
    public string CreateUserWizardDefaultCancelButtonText => this[nameof (CreateUserWizardDefaultCancelButtonText)];

    /// <summary>AllowReturn cannot be set.</summary>
    /// <value>AllowReturn cannot be set.</value>
    [ResourceEntry("CreateUserWizardStepAllowReturnCannotBeSet", Description = "AllowReturn cannot be set.", LastModified = "2009/05/30", Value = "AllowReturn cannot be set.")]
    public string CreateUserWizardStepAllowReturnCannotBeSet => this[nameof (CreateUserWizardStepAllowReturnCannotBeSet)];

    /// <summary>StepType cannot be changed.</summary>
    /// <value>StepType cannot be changed.</value>
    [ResourceEntry("CreateUserWizardStepStepTypeCannotBeSet", Description = "StepType cannot be changed.", LastModified = "2009/05/30", Value = "StepType cannot be changed.")]
    public string CreateUserWizardStepStepTypeCannotBeSet => this[nameof (CreateUserWizardStepStepTypeCannotBeSet)];

    /// <summary>Default subject of the mail for creating new user.</summary>
    /// <value>StepType cannot be changed.</value>
    [ResourceEntry("CreateUserWizardDefaultSubject", Description = "Default subject of the mail for creating new user.", LastModified = "2009/06/05", Value = "New user created")]
    public string CreateUserWizardDefaultSubject => this[nameof (CreateUserWizardDefaultSubject)];

    /// <summary>Default body of the mail for creating new user.</summary>
    /// <value>Default body of the mail for creating new user.</value>
    [ResourceEntry("CreateUserWizardDefaultBody", Description = "Default body of the mail for creating new user.", LastModified = "2009/06/05", Value = "Your account have been created.<br /><br />User Name: <%\\s*UserName\\s*%><br />Password: <%\\s*Password\\s*%>")]
    public string CreateUserWizardDefaultBody => this[nameof (CreateUserWizardDefaultBody)];

    /// <summary>
    /// Error message: The confirmation page is invalid or not set.
    /// </summary>
    [ResourceEntry("NoConfirmationPageIsSet", Description = "Error message: The confirmation page is invalid or not set.", LastModified = "2011/03/31", Value = "The confirmation page is invalid or not set.")]
    public string NoConfirmationPageIsSet => this[nameof (NoConfirmationPageIsSet)];

    /// <summary>
    /// Default subject of the email sent to the client for registration confirmation.
    /// </summary>
    [ResourceEntry("ConfirmRegistrationMailSubject", Description = "Default subject of the email sent to the client for registration confirmation.", LastModified = "2011/03/31", Value = "Please confirm your registration")]
    public string ConfirmRegistrationMailSubject => this[nameof (ConfirmRegistrationMailSubject)];

    /// <summary>
    /// Default body of the email sent to the client for registration confirmation.
    /// </summary>
    [ResourceEntry("ConfirmRegistrationMailBody", Description = "Default body of the email sent to the client for registration confirmation.", LastModified = "2011/03/31", Value = "To complete your registration, follow this link: <%\\s*ConfirmationUrl\\s*%>.")]
    public string ConfirmRegistrationMailBody => this[nameof (ConfirmRegistrationMailBody)];

    /// <summary>
    /// Default subject of the email sent to the client after a successful registration.
    /// </summary>
    [ResourceEntry("SuccessfulRegistrationMailSubject", Description = "Default subject of the email sent to the client after a successful registration.", LastModified = "2011/03/31", Value = "Welcome")]
    public string SuccessfulRegistrationMailSubject => this[nameof (SuccessfulRegistrationMailSubject)];

    /// <summary>
    /// Default body of the email sent to the client after a successful registration.
    /// </summary>
    [ResourceEntry("SuccessfulRegistrationEmailBody", Description = "Default body of the email sent to the client after a successful registration.", LastModified = "2011/03/31", Value = "Your registration was successful.")]
    public string SuccessfulRegistrationEmailBody => this[nameof (SuccessfulRegistrationEmailBody)];

    /// <summary>Cannot send emails</summary>
    /// <value>SMTP settings are not set.</value>
    [ResourceEntry("CannotSendEmails", Description = "The error message title to be shown when SMTP settings are not set.", LastModified = "2011/04/11", Value = "Cannot send emails")]
    public string CannotSendEmails => this[nameof (CannotSendEmails)];

    /// <summary>SMTP settings are not set.</summary>
    /// <value>SMTP settings are not set.</value>
    [ResourceEntry("SmtpSettingsAreNotSet", Description = "The error message to be shown when SMTP settings are not set.", LastModified = "2011/04/11", Value = "The system has not been configured to send emails. Go to <a href='{0}' target='_blank'>Notifications</a> or ask your administrator to set them")]
    public string SmtpSettingsAreNotSet => this[nameof (SmtpSettingsAreNotSet)];

    /// <summary>Forms SMTP settings are not set.</summary>
    /// <value>Forms SMTP settings are not set.</value>
    [ResourceEntry("FormsSmtpSettingsAreNotSet", Description = "The error message to be shown when SMTP settings are not set.", LastModified = "2019/06/18", Value = "Forms are not configured to send emails. <a href='https://www.progress.com/documentation/sitefinity-cms/setup-email-notifications-for-form-responses' target='_blank'>Learn how to configure sending emails</a> or ask your administrator for assistance.")]
    public string FormsSmtpSettingsAreNotSet => this[nameof (FormsSmtpSettingsAreNotSet)];

    /// <summary>
    /// phrase: The system has not been configured to send email.
    /// </summary>
    [ResourceEntry("TheSystemHasNotBeenConfiguredToSendEmails", Description = "phrase: The system has not been configured to send email.", LastModified = "2010/09/27", Value = "The system has not been configured to send email.")]
    public string TheSystemHasNotBeenConfiguredToSendEmails => this[nameof (TheSystemHasNotBeenConfiguredToSendEmails)];

    /// <summary>
    /// phrase: The system is denied the permission(s) to send email.
    /// </summary>
    [ResourceEntry("TheSystemHasIsNotPermittedToSendEmails", Description = "phrase: The system is denied the permission(s) to send email.", LastModified = "2010/09/27", Value = "The system is denied the permission(s) to send email.")]
    public string TheSystemHasIsNotPermittedToSendEmails => this[nameof (TheSystemHasIsNotPermittedToSendEmails)];

    /// <summary>
    /// Contact an administrator to reset your password manually.
    /// </summary>
    /// <value>Contact an administrator to reset your password manually.</value>
    [ResourceEntry("ContactAnAdministratorToResetYourPasswordManually", Description = "Contact an administrator to reset your password manually.", LastModified = "2010/04/15", Value = "Contact an administrator to reset your password manually.")]
    public string ContactAnAdministratorToResetYourPasswordManually => this[nameof (ContactAnAdministratorToResetYourPasswordManually)];

    /// <summary>Or, ask an administrator to configure the system.</summary>
    /// <value>Or, ask an administrator to configure the system.</value>
    [ResourceEntry("OrAskAnAdministratorToConfigureThSystem", Description = "Or, ask an administrator to configure the system.", LastModified = "2010/04/15", Value = "Or, ask an administrator to configure the system.")]
    public string OrAskAnAdministratorToConfigureThSystem => this[nameof (OrAskAnAdministratorToConfigureThSystem)];

    /// <summary>Details</summary>
    /// <value>Details</value>
    [ResourceEntry("Details", Description = "Details", LastModified = "2010/04/15", Value = "Details")]
    public string Details => this[nameof (Details)];

    /// <summary>How to set SMTP?</summary>
    /// <value>How to set SMTP?</value>
    [ResourceEntry("HowToSetSMTP", Description = "How to set SMTP?", LastModified = "2010/04/15", Value = "How to set SMTP?")]
    public string HowToSetSMTP => this[nameof (HowToSetSMTP)];

    /// <summary>
    /// Once logged, go to <em>Settings</em> &gt; <em>Configuration</em>
    /// </summary>
    /// <value>Once logged, go to <em>Settings</em> &gt; <em>Configuration</em></value>
    [ResourceEntry("GoToSettingsConfiguration", Description = "Once logged, go to <em>Administration</em> &gt; <em>Settings</em> &gt; <em>Advanced</em>", LastModified = "2010/04/15", Value = "Once logged, go to <em>Administration</em> &gt; <em>Settings</em> &gt; <em>Advanced</em>")]
    public string GoToSettingsConfiguration => this[nameof (GoToSettingsConfiguration)];

    /// <summary>
    /// In that screen, select <em>System</em> &gt; <em>SMTP Settings</em>
    /// </summary>
    /// <value>In that screen, select <em>System</em> &gt; <em>SMTP Settings</em></value>
    [ResourceEntry("SelectSystemSMTPSettings", Description = "In that screen, select <em>System</em> &gt; <em>SMTP Settings</em>", LastModified = "2010/04/15", Value = "In that screen, select <em>System</em> &gt; <em>SMTP Settings</em>")]
    public string SelectSystemSMTPSettings => this[nameof (SelectSystemSMTPSettings)];

    /// <summary>
    /// The error message to be shown when no WebPermission is granted to specific external address.
    /// </summary>
    /// <value>The error message to be shown when no WebPermission is granted to specific external address.</value>
    [ResourceEntry("NoWebPermissionToConnectToHost", Description = "The error message to be shown when no WebPermission is granted to specific external address.", LastModified = "2009/06/10", Value = "You need to grant a WebPermission Connect to the host {0}.")]
    public string NoWebPermissionToConnectToHost => this[nameof (NoWebPermissionToConnectToHost)];

    /// <summary>No SmtpPermissions granted.</summary>
    /// <value>No SmtpPermissions granted.</value>
    [ResourceEntry("NoSmtpConnectPermissionGranted", Description = "No SmtpPermissions granted.", LastModified = "2009/06/10", Value = "You need to grant a SmtpPermissions in order to send emails.")]
    public string NoSmtpConnectPermissionGranted => this[nameof (NoSmtpConnectPermissionGranted)];

    /// <summary>
    /// phrase: SmtpPermission with Access = SmtpAccess.ConnectToUnrestrictedPort should be granted, when using non default SMTP port (different from 25).
    /// </summary>
    [ResourceEntry("NoSmtpPermissionGrantedForNonDefaultPort", Description = "phrase: SmtpPermission with Access = SmtpAccess.ConnectToUnrestrictedPort should be granted, when using non default SMTP port (different from 25).", LastModified = "2010/09/27", Value = "SmtpPermission with Access = SmtpAccess.ConnectToUnrestrictedPort should be granted, when using non default SMTP port (different from 25).")]
    public string NoSmtpPermissionGrantedForNonDefaultPort => this[nameof (NoSmtpPermissionGrantedForNonDefaultPort)];

    /// <summary>
    /// phrase: The new password should be different than the current one.
    /// </summary>
    [ResourceEntry("NewAndOldPasswordsAreEqual", Description = "phrase: The new password should be different than the current one.", LastModified = "2010/09/24", Value = "The new password should be different than the current one.")]
    public string NewAndOldPasswordsAreEqual => this[nameof (NewAndOldPasswordsAreEqual)];

    /// <summary>More than one user has the specified email address.</summary>
    /// <value>More than one user has the specified email address.</value>
    [ResourceEntry("MembershipMoreThanOneUserWithEmail", Description = "More than one user has the specified email address.", LastModified = "2009/06/17", Value = "More than one user has the specified email address.")]
    public string MembershipMoreThanOneUserWithEmail => this[nameof (MembershipMoreThanOneUserWithEmail)];

    /// <summary>The parameter '{0}' must not be empty.</summary>
    /// <value>The parameter '{0}' must not be empty.</value>
    [ResourceEntry("ParameterCanNotBeEmpty", Description = "The parameter '{0}' must not be empty.", LastModified = "2009/06/17", Value = "The parameter '{0}' must not be empty.")]
    public string ParameterCanNotBeEmpty => this[nameof (ParameterCanNotBeEmpty)];

    /// <summary>Message: Username {0} is not a valid email</summary>
    [ResourceEntry("InvalidEmailUsernameErrorMessage", Description = "Username {0} is not a valid email", LastModified = "2016/11/23", Value = "Username {0} is not a valid email")]
    public string InvalidEmailUsernameErrorMessage => this[nameof (InvalidEmailUsernameErrorMessage)];

    /// <summary>The parameter '{0}' must not contain commas.</summary>
    /// <value>The parameter '{0}' must not contain commas.</value>
    [ResourceEntry("ParameterCanNotContainComma", Description = "The parameter '{0}' must not contain commas.", LastModified = "2009/06/17", Value = "The parameter '{0}' must not contain commas.")]
    public string ParameterCanNotContainComma => this[nameof (ParameterCanNotContainComma)];

    /// <summary>
    /// The parameter '{0}' is too long: it must not exceed {1} chars in length.
    /// </summary>
    /// <value>The parameter '{0}' is too long: it must not exceed {1} chars in length.</value>
    [ResourceEntry("ParameterTooLong", Description = "The parameter '{0}' is too long: it must not exceed {1} chars in length.", LastModified = "2009/06/17", Value = "The parameter '{0}' is too long: it must not exceed {1} chars in length.")]
    public string ParameterTooLong => this[nameof (ParameterTooLong)];

    /// <summary>The role '{0}' already exists.</summary>
    /// <value>The role '{0}' already exists.</value>
    [ResourceEntry("ProviderRoleAlreadyExists", Description = "The role '{0}' already exists.", LastModified = "2009/06/17", Value = "The role '{0}' already exists.")]
    public string ProviderRoleAlreadyExists => this[nameof (ProviderRoleAlreadyExists)];

    /// <summary>
    /// Message: Error thrown when duplicate property is supplied in Save method of WCF service.
    /// </summary>
    /// <value>Error thrown when duplicate property is supplied in Save method of WCF service.</value>
    [ResourceEntry("WCFPropertyDuplicate", Description = "Error thrown when duplicate property is supplied in Save method of WCF service.", LastModified = "2009/06/25", Value = "ERROR: The property bag contains duplicate property '{0}', which is not allowed.")]
    public string WCFPropertyDuplicate => this[nameof (WCFPropertyDuplicate)];

    /// <summary>
    /// Message: Error thrown during saving resource of WCF service.
    /// </summary>
    [ResourceEntry("WCFErrorOnSave", Description = "Error thrown during saving resource of WCF service.", LastModified = "2010/09/21", Value = "ERROR: Item could not be saved.")]
    public string WCFErrorOnSave => this[nameof (WCFErrorOnSave)];

    /// <summary>
    /// Message: Error thrown when duplicate property is supplied in Save method of WCF service.
    /// </summary>
    /// <value>Error thrown when duplicate property is supplied in Save method of WCF service.</value>
    [ResourceEntry("WCFErrorOnDelete", Description = "Error thrown during deleting resource of WCF service.", LastModified = "2009/07/16", Value = "ERROR: Item could not have been deleted.")]
    public string WCFErrorOnDelete => this[nameof (WCFErrorOnDelete)];

    /// <summary>Message: Role name cannot be empty.</summary>
    /// <value>Role name cannot be empty.</value>
    [ResourceEntry("RoleNameRequiredErrorMessage", Description = "Message shown when role name is empty.", LastModified = "2009/07/07", Value = "Role name cannot be empty.")]
    public string RoleNameRequiredErrorMessage => this[nameof (RoleNameRequiredErrorMessage)];

    /// <summary>Message: Policy name cannot be empty.</summary>
    /// <value>Policy name cannot be empty.</value>
    [ResourceEntry("PolicyNameRequiredErrorMessage", Description = "Message shown when policy name is empty.", LastModified = "2009/07/14", Value = "Policy name cannot be empty.")]
    public string PolicyNameRequiredErrorMessage => this[nameof (PolicyNameRequiredErrorMessage)];

    /// <summary>
    /// Message: Cannot \"{0}\", because {1} does not contain a public property named \"{2}\"
    /// </summary>
    /// <value>Cannot \"{0}\", because {1} does not contain a public property named \"{2}\".</value>
    [ResourceEntry("CannotPerformOperationBecauseTypeDoesNotContainPropertyWithThatName", Description = "Message shown when policy name is empty.", LastModified = "2009/09/26", Value = "Cannot \"{0}\", because {1} does not contain a public property named \"{2}\".")]
    public string CannotPerformOperationBecauseTypeDoesNotContainPropertyWithThatName => this[nameof (CannotPerformOperationBecauseTypeDoesNotContainPropertyWithThatName)];

    /// <summary>Message: {0} does not implement {1}.</summary>
    /// <value>{0} does not implement {1}.</value>
    [ResourceEntry("TypeDoesNotImplementAnotherType", Description = "Message shown when Type(x).IsAssignableFrom(y) returns false and this is considered an error in that context.", LastModified = "2009/09/27", Value = "{0} does not implement {1}.")]
    public string TypeDoesNotImplementAnotherType => this[nameof (TypeDoesNotImplementAnotherType)];

    /// <summary>Message: {0} does not have the {1} attribute.</summary>
    /// <value>{0} does not have the {1} attribute.</value>
    [ResourceEntry("TypeDoesNotHaveAnAttribute", Description = "Message shown when a type is not decorated with a given attribute", LastModified = "2009/09/28", Value = "{0} does not have the {1} attribute.")]
    public string TypeDoesNotHaveAnAttribute => this[nameof (TypeDoesNotHaveAnAttribute)];

    /// <summary>
    /// Messages: Cannot infer item type from query string. Please check that you have an itemType query string parameter in your service call.
    /// </summary>
    [ResourceEntry("CannotInferItemTypeFromQueryString", Description = "Message shown when Sitefinity needs to generate a new type for the WCF pipeline", LastModified = "2009/10/88", Value = " Cannot infer item type from query string. Please check that you have an itemType query string parameter in your service call.")]
    public string CannotInferItemTypeFromQueryString => this[nameof (CannotInferItemTypeFromQueryString)];

    /// <summary>
    /// Message: Cannot resolve the provider that works with items of type {0}
    /// </summary>
    /// <value>Cannot resolve the provider that works with items of type {0}</value>
    [ResourceEntry("CannotResolveGenericProvider", Description = "Message shown when Sitefinity tries to determine the provider it has to use for the generic content manager", LastModified = "2009/10/08", Value = "Cannot resolve the provider that works with items of type {0}.")]
    public string CannotResolveGenericProvider => this[nameof (CannotResolveGenericProvider)];

    /// <summary>
    /// Message: Cannot resolve type {0}. Please check your spelling and if you have registered the type assembly with TypeResolutionService.
    /// </summary>
    [ResourceEntry("CannotResolveType", Description = "Message shown when Sitefinity cannot resolve a type.", LastModified = "2012/01/05", Value = "Cannot resolve type {0}. Please check your spelling and if you have registered the type assembly with TypeResolutionService.")]
    public string CannotResolveType => this[nameof (CannotResolveType)];

    /// <summary>
    /// Message: Cannot infer manager type, because content type `{0}` is not mapped to a manager type. You can do so in configuration, via the ManagerTypeAttribute, or in code subscribing to ManagerBase.NeedsManagerType.
    /// </summary>
    /// <value>
    /// Cannot infer manager type, because content type `{0}` is not mapped to a manager type. You can do so in configuration, via the ManagerTypeAttribute, or in code subscribing to ManagerBase.NeedsManagerType.
    /// </value>
    [ResourceEntry("CannotInferManagerTypeFromItemType", Description = "Message that is shown when the manager type cannot be infered from the item type.", LastModified = "2009/10/19", Value = "Cannot infer manager type, because content type `{0}` is not mapped to a manager type. You can do so in configuration, via the ManagerTypeAttribute, or in code subscribing to ManagerBase.NeedsManagerType.")]
    public string CannotInferManagerTypeFromItemType => this[nameof (CannotInferManagerTypeFromItemType)];

    /// <summary>
    /// Message: OpenAccess internal identity is not supported. Type: {0}
    /// </summary>
    /// <value>Open access internal identity is not supported. Type: {0}</value>
    [ResourceEntry("OpenAccesInternalIdentityIsNotSupported", Description = "Message shown when Sitefinity detects OpenAccess with internal identity in a context that does not allow it", LastModified = "2009/11/19", Value = "OpenAccess internal identity is not supported. Type: {0}")]
    public string OpenAccesInternalIdentityIsNotSupported => this[nameof (OpenAccesInternalIdentityIsNotSupported)];

    /// <summary>Message: Can not find identity field/property for {0}</summary>
    /// <value>Can not find identity field/property for {0}</value>
    [ResourceEntry("CannotFindIdentityForType", Description = "Message shown when Sitefinity can not determine the identity field or property used by a persistent type. Parameter is a Type.", LastModified = "2009/11/20", Value = "Can not find identity field/property for {0}")]
    public string CannotFindIdentityForType => this[nameof (CannotFindIdentityForType)];

    /// <summary>Message: View's Host is not defined.</summary>
    /// <value>View's Host is not defined.</value>
    [ResourceEntry("ViewHostUndefined", Description = "Message shown when a view host is undefined.", LastModified = "2009/12/21", Value = "View's Host is not defined.")]
    public string ViewHostUndefined => this[nameof (ViewHostUndefined)];

    /// <summary>Message: No Content Detail to show.</summary>
    /// <value>VNo Content Detail to show.</value>
    [ResourceEntry("ContentDetailIsNull", Description = "Message shown when view is in detail mode but the Content Detail is null.", LastModified = "2009/12/21", Value = "No Content Detail to show.")]
    public string ContentDetailIsNull => this[nameof (ContentDetailIsNull)];

    /// <summary>
    /// Message: {0} ('{1}'): cannot set attribute with name '{2}' to '{3}'
    /// </summary>
    /// <value>{TagName} ('{TypeName}'): cannot set attribute with name '{PropertyName}' to '{Value}'</value>
    [ResourceEntry("TemplateParserCannotSetAttributeValue", Description = "{TagName} ('{TypeName}'): cannot set attribute with name '{PropertyName}' to '{Value}'", LastModified = "2009/12/28", Value = "{0} ('{1}'): cannot set attribute with name '{2}' to '{3}'")]
    public string TemplateParserCannotSetAttributeValue => this[nameof (TemplateParserCannotSetAttributeValue)];

    /// <summary>
    /// Message: The currently logged on user is blocked by ID.
    /// </summary>
    /// <value>Typically used when a user that is blocked by ID tries to perform some action.</value>
    [ResourceEntry("CurrentUserIsBlockedById", Description = "Typically used when a user that is blocked by ID tries to perform some action.", LastModified = "2009/12/29", Value = "The currently logged on user is blocked by ID.")]
    public string CurrentUserIsBlockedById => this[nameof (CurrentUserIsBlockedById)];

    /// <summary>
    /// Message: The currently logged on user is blocked by IP.
    /// </summary>
    /// <value>Typically used when a user that is blocked by IP tries to perform some action.</value>
    [ResourceEntry("CurrentUserIsBlockedByIp", Description = "Typically used when a user that is blocked by IP tries to perform some action.", LastModified = "2009/12/29", Value = "The currently logged on user is blocked by IP.")]
    public string CurrentUserIsBlockedByIp => this[nameof (CurrentUserIsBlockedByIp)];

    /// <summary>Message: You must specify a content item to comment.</summary>
    /// <value>Typically used in CommentItemsService when trying to comment without specifying the item that has to be commented.</value>
    [ResourceEntry("NoCommentedItem", Description = "Typically used in CommentsService when trying to comment without specifying the item that has to be commented.", LastModified = "2009/12/29", Value = "You must specify a content item to comment.")]
    public string NoCommentedItem => this[nameof (NoCommentedItem)];

    /// <summary>
    /// Message: The specified regular expression: \"{0}\" is invalid.
    /// </summary>
    [ResourceEntry("InvalidRegularExpression", Description = "Message: The specified regular expression: \"{0}\" is invalid.", LastModified = "2010/02/16", Value = "The specified regular expression: \"{0}\" is invalid.")]
    public string InvalidRegularExpression => this[nameof (InvalidRegularExpression)];

    /// <summary>Message: You have specified an invalid minimum date.</summary>
    [ResourceEntry("InvalidMinDate", Description = "Message: You have specified an invalid minimum date.", LastModified = "2010/02/16", Value = "You have specified an invalid minimum date.")]
    public string InvalidMinDate => this[nameof (InvalidMinDate)];

    /// <summary>Message: You have specified an invalid maximum date.</summary>
    [ResourceEntry("InvalidMaxDate", Description = "Message: You have specified an invalid maximum date.", LastModified = "2010/02/16", Value = "You have specified an invalid maximum date.")]
    public string InvalidMaxDate => this[nameof (InvalidMaxDate)];

    /// <summary>
    /// Message: You have specified maximum date which is smaller than the minimum date.
    /// </summary>
    [ResourceEntry("MaxDateBeforeMinDate", Description = "Message: You have specified maximum date which is smaller than the minimum date.", LastModified = "2010/02/16", Value = "You have specified maximum date which is smaller than the minimum date.")]
    public string MaxDateBeforeMinDate => this[nameof (MaxDateBeforeMinDate)];

    /// <summary>phrase: invalid time zone standard name.</summary>
    [ResourceEntry("InvalidTimeZoneName", Description = "phrase: Invalid time zone standard name.", LastModified = "2010/11/03", Value = "Invalid time zone standard name.")]
    public string InvalidTimeZoneName => this[nameof (InvalidTimeZoneName)];

    /// <summary>phrase: Invalid time zone base UTC offset.</summary>
    [ResourceEntry("InvalidTimeZoneOffset", Description = "phrase: Invalid time zone base UTC offset.", LastModified = "2010/11/03", Value = "Invalid time zone base UTC offset.")]
    public string InvalidTimeZoneOffset => this[nameof (InvalidTimeZoneOffset)];

    /// <summary>Message: Non alphanumeric characters are not allowed.</summary>
    [ResourceEntry("AlphaNumericViolationMessage", Description = "Message: Non alphanumeric characters are not allowed.", LastModified = "2010/02/24", Value = "Non alphanumeric characters are not allowed.")]
    public string AlphaNumericViolationMessage => this[nameof (AlphaNumericViolationMessage)];

    /// <summary>Message: You have entered an invalid currency.</summary>
    [ResourceEntry("CurrencyViolationMessage", Description = "Message: You have entered an invalid currency.", LastModified = "2010/02/24", Value = "You have entered an invalid currency.")]
    public string CurrencyViolationMessage => this[nameof (CurrencyViolationMessage)];

    /// <summary>Message: You have entered an invalid email address.</summary>
    [ResourceEntry("EmailAddressViolationMessage", Description = "Message: You have entered an invalid email address.", LastModified = "2010/02/24", Value = "You have entered an invalid email address.")]
    public string EmailAddressViolationMessage => this[nameof (EmailAddressViolationMessage)];

    /// <summary>Message: You have entered an invalid integer.</summary>
    [ResourceEntry("IntegerViolationMessage", Description = "Message: You have entered an invalid integer.", LastModified = "2010/02/24", Value = "You have entered an invalid integer.")]
    public string IntegerViolationMessage => this[nameof (IntegerViolationMessage)];

    /// <summary>Message: You have entered an invalid URL.</summary>
    [ResourceEntry("InternetUrlViolationMessage", Description = "Message: You have entered an invalid URL.", LastModified = "2010/02/24", Value = "You have entered an invalid URL.")]
    public string InternetUrlViolationMessage => this[nameof (InternetUrlViolationMessage)];

    /// <summary>Message: Too long.</summary>
    [ResourceEntry("MaxLengthViolationMessage", Description = "Message: Too long", LastModified = "2010/02/24", Value = "Too long")]
    public string MaxLengthViolationMessage => this[nameof (MaxLengthViolationMessage)];

    /// <summary>Message: Too big.</summary>
    [ResourceEntry("MaxValueViolationMessage", Description = "Message: Too big", LastModified = "2010/02/24", Value = "Too big")]
    public string MaxValueViolationMessage => this[nameof (MaxValueViolationMessage)];

    /// <summary>Message: Too short.</summary>
    [ResourceEntry("MinLengthViolationMessage", Description = "Message: Too short.", LastModified = "2010/02/24", Value = "Too short.")]
    public string MinLengthViolationMessage => this[nameof (MinLengthViolationMessage)];

    /// <summary>Message: Too small.</summary>
    [ResourceEntry("MinValueViolationMessage", Description = "Message: Too small.", LastModified = "2010/02/24", Value = "Too small.")]
    public string MinValueViolationMessage => this[nameof (MinValueViolationMessage)];

    /// <summary>Message: Alphanumeric characters are not allowed.</summary>
    [ResourceEntry("NonAlphaNumericViolationMessage", Description = "Message: Alphanumeric characters are not allowed.", LastModified = "2010/02/24", Value = "Alphanumeric characters are not allowed.")]
    public string NonAlphaNumericViolationMessage => this[nameof (NonAlphaNumericViolationMessage)];

    /// <summary>Message: You have entered an invalid number.</summary>
    [ResourceEntry("NumericViolationMessage", Description = "Message: You have entered an invalid number.", LastModified = "2010/02/24", Value = "You have entered an invalid number.")]
    public string NumericViolationMessage => this[nameof (NumericViolationMessage)];

    /// <summary>Message: You have entered an invalid percentage.</summary>
    [ResourceEntry("PercentageViolationMessage", Description = "Message: You have entered an invalid percentage.", LastModified = "2010/02/24", Value = "You have entered an invalid percentage.")]
    public string PercentageViolationMessage => this[nameof (PercentageViolationMessage)];

    /// <summary>Message: Invalid format</summary>
    [ResourceEntry("RegularExpressionViolationMessage", Description = "Message: Invalid format", LastModified = "2010/02/24", Value = "Invalid format")]
    public string RegularExpressionViolationMessage => this[nameof (RegularExpressionViolationMessage)];

    /// <summary>Message: Required field.</summary>
    [ResourceEntry("RequiredViolationMessage", Description = "Message: Required field.", LastModified = "2010/02/24", Value = "Required field.")]
    public string RequiredViolationMessage => this[nameof (RequiredViolationMessage)];

    /// <summary>
    /// Message: You have entered an invalid US social security number.
    /// </summary>
    [ResourceEntry("USSocialSecurityNumberViolationMessage", Description = "Message: You have entered an invalid US social security number.", LastModified = "2010/02/24", Value = "You have entered an invalid US social security number.")]
    public string USSocialSecurityNumberViolationMessage => this[nameof (USSocialSecurityNumberViolationMessage)];

    /// <summary>Message: You have entered an invalid US ZIP code.</summary>
    [ResourceEntry("USZipCodeViolationMessage", Description = "Message: You have entered an invalid US ZIP code.", LastModified = "2010/02/24", Value = "You have entered an invalid US ZIP code.")]
    public string USZipCodeViolationMessage => this[nameof (USZipCodeViolationMessage)];

    /// <summary>
    /// Message: The expiration date must be after the publication date.
    /// </summary>
    [ResourceEntry("ExpirationDateShouldBeAfterPublicationDate", Description = "Message: The expiration date must be after the publication date.", LastModified = "2010/03/05", Value = "The expiration date must be after the publication date.")]
    public string ExpirationDateShouldBeAfterPublicationDate => this[nameof (ExpirationDateShouldBeAfterPublicationDate)];

    /// <summary>
    /// Message: Event's end date cannot be set earlier than its start date.
    /// </summary>
    [ResourceEntry("EndDateShouldNotBeEarlierThanStartDate", Description = "Message: Event's end date cannot be set earlier than its start date.", LastModified = "2011/01/25", Value = "Event's end date cannot be set earlier than its start date.")]
    public string EndDateShouldNotBeEarlierThanStartDate => this[nameof (EndDateShouldNotBeEarlierThanStartDate)];

    /// <summary>
    /// Message: Only one instance of FormManager is allowed per page.
    /// Please, use FormManager.GetCurrent() to get the FormManager instance or FormManager.Ensure() to automatically add one.
    /// </summary>
    [ResourceEntry("FormManagerOnlyOneInstance", Description = "Message: Only one instance of FormManager is allowed per page. Please, use FormManager.GetCurrent() to get the FormManager instance or FormManager.Ensure() to automatically add one.", LastModified = "2010/05/28", Value = "Only one instance of FormManager is allowed per page. Please, use FormManager.GetCurrent() to get the FormManager instance or FormManager.Ensure() to automatically add one.")]
    public string FormManagerOnlyOneInstance => this[nameof (FormManagerOnlyOneInstance)];

    /// <summary>
    /// Message: No FormManager found on the page. Use FormManager.Ensure() to add one.
    /// </summary>
    [ResourceEntry("FormManagerNotFound", Description = "Message: No FormManager found on the page. Use FormManager.Ensure() to add one.", LastModified = "2010/05/28", Value = "No FormManager found on the page. Use FormManager.Ensure() to add one.")]
    public string FormManagerNotFound => this[nameof (FormManagerNotFound)];

    /// <summary>Message: No provider is set.</summary>
    [ResourceEntry("MissingProvider", Description = "Error message shown when the provider cannot be retrieved.", LastModified = "2010/05/04", Value = "No provider is set.")]
    public string MissingProvider => this[nameof (MissingProvider)];

    /// <summary>
    /// Message: The parent item of the '{0}' ConfigElement is not the correct one.
    /// </summary>
    [ResourceEntry("InvalidConfigElementParent", Description = "Message: The parent item of the '{0}' ConfigElement is not the correct one.", LastModified = "2010/05/03", Value = "The parent item of the '{0}' ConfigElement is not the correct one.")]
    public string InvalidConfigElementParent => this[nameof (InvalidConfigElementParent)];

    /// <summary>Message: You have specified an invalid date range.</summary>
    [ResourceEntry("InvalidDateRange", Description = "Message: You have specified an invalid date range.", LastModified = "2010/06/16", Value = "You have specified an invalid date range.")]
    public string InvalidDateRange => this[nameof (InvalidDateRange)];

    /// <summary>phrase: FirstName cannot be empty</summary>
    [ResourceEntry("FirstNameCannotBeEmpty", Description = "phrase", LastModified = "2010/09/15", Value = "Enter your first name.")]
    public string FirstNameCannotBeEmpty => this[nameof (FirstNameCannotBeEmpty)];

    /// <summary>phrase: LastName cannot be empty</summary>
    [ResourceEntry("LastNameCannotBeEmpty", Description = "phrase", LastModified = "2010/09/15", Value = "Enter your last name.")]
    public string LastNameCannotBeEmpty => this[nameof (LastNameCannotBeEmpty)];

    /// <summary>phrase: Email cannot be empty</summary>
    [ResourceEntry("EmailCannotBeEmpty", Description = "phrase", LastModified = "2010/09/15", Value = "Enter your email.")]
    public string EmailCannotBeEmpty => this[nameof (EmailCannotBeEmpty)];

    /// <summary>phrase: Dynamic type object must be an array</summary>
    [ResourceEntry("DynamicTypeDefinitionIsNotArray", Description = "phrase", LastModified = "2010/09/15", Value = "Dynamic type object must be an array")]
    public string DynamicTypeDefinitionIsNotArray => this[nameof (DynamicTypeDefinitionIsNotArray)];

    /// <summary>
    /// Error thrown when the LinkedNodeId property of a page node is not set.
    /// </summary>
    [ResourceEntry("LinkedNodeIdNotSet", Description = "Error thrown when the LinkedNodeId property of a page node is not set.", LastModified = "2011/06/09", Value = "The LinkedNodeId property is not set.")]
    public string LinkedNodeIdNotSet => this[nameof (LinkedNodeIdNotSet)];

    /// <summary>User activation failed.</summary>
    [ResourceEntry("ActivationErrorMessage", Description = "User activation failed.", LastModified = "2011/04/04", Value = "User activation failed")]
    public string ActivationErrorMessage => this[nameof (ActivationErrorMessage)];

    /// <summary>User activation succeeded.</summary>
    [ResourceEntry("SuccessfulActivationMessage", Description = "User activation succeeded.", LastModified = "2011/04/04", Value = "User activation succeeded.")]
    public string SuccessfulActivationMessage => this[nameof (SuccessfulActivationMessage)];

    /// <summary>Error parsing the template</summary>
    [ResourceEntry("ErrorParsingTheTemplate", Description = "phrase: Error parsing the template", LastModified = "2011/05/04", Value = "Error parsing the template")]
    public string ErrorParsingTheTemplate => this[nameof (ErrorParsingTheTemplate)];

    /// <summary>Error creating the hash algorithm</summary>
    [ResourceEntry("CouldNotCreateHashAlgorithm", Description = "phrase: Error creating the hash algorithm", LastModified = "2011/10/13", Value = "Error creating the hash algorithm")]
    public string CouldNotCreateHashAlgorithm => this[nameof (CouldNotCreateHashAlgorithm)];

    /// <summary>Error. Password reset is not configured</summary>
    [ResourceEntry("PasswordResetNotConfigured", Description = "phrase: Password reset is not configured", LastModified = "2011/11/09", Value = "Password reset is not configured")]
    public string PasswordResetNotConfigured => this[nameof (PasswordResetNotConfigured)];

    /// <summary>phrase: A user with this nickname already exists.</summary>
    [ResourceEntry("NicknameAlreadyUsed", Description = "phrase: A user with this nickname already exists.", LastModified = "2012/01/27", Value = "A user with this nickname already exists.")]
    public string NicknameAlreadyUsed => this[nameof (NicknameAlreadyUsed)];

    /// <summary>phrase: A user with this nickname already exists.</summary>
    [ResourceEntry("NickNameGuidError", Description = "phrase: Nickname cannot be a Guid. Please change the nickname.", LastModified = "2015/06/15", Value = "Nickname cannot be a Guid. Please change the nickname.")]
    public string NickNameGuidError => this[nameof (NickNameGuidError)];

    /// <summary>phrase: The nickname should be at most 64 symbols.</summary>
    [ResourceEntry("NicknameMaxLength", Description = "phrase: The nickname should be at most 64 symbols.", LastModified = "2017/02/14", Value = "The nickname should be at most 64 symbols.")]
    public string NicknameMaxLength => this[nameof (NicknameMaxLength)];

    /// <summary>phrase: The name should be at most 250 symbols.</summary>
    [ResourceEntry("NameLength", Description = "phrase: The name should be at most 250 symbols.", LastModified = "2017/01/19", Value = "The name should be at most 250 symbols.")]
    public string NameLength => this[nameof (NameLength)];

    /// <summary>phrase: There is no user with this email.</summary>
    [ResourceEntry("EmailNotFound", Description = "phrase: There is no user with this email.", LastModified = "2012/02/15", Value = "There is no user with this email.")]
    public string EmailNotFound => this[nameof (EmailNotFound)];

    /// <summary>
    /// phrase: Basic templates can not be based on other templates.
    /// </summary>
    [ResourceEntry("BasicTemplatesCanNotBeBasedOnOtherTemplates", Description = "phrase: Basic templates can not be based on other templates.", LastModified = "2012/07/04", Value = "Basic templates can not be based on other templates.")]
    public string BasicTemplatesCanNotBeBasedOnOtherTemplates => this[nameof (BasicTemplatesCanNotBeBasedOnOtherTemplates)];

    /// <summary>
    /// phrase: The template cannot be based on itself or on a template related to this template.
    /// </summary>
    [ResourceEntry("TemplateCannotBeBasedOnItself", Description = "phrase: The template cannot be based on itself or on a template related to this template.", LastModified = "2012/10/25", Value = "The template cannot be based on itself or on a template related to this template.")]
    public string TemplateCannotBeBasedOnItself => this[nameof (TemplateCannotBeBasedOnItself)];

    [ResourceEntry("CannotChangeParentBecauseOfRecursiveRelation", Description = "Message: Parent cannot be changed. Changing the parent of {0} to {1} will cause a recursive relation since {0} is a predecessor of {1}.", LastModified = "2013/03/06", Value = "Parent cannot be changed. Changing the parent of \"{0}\" to \"{1}\" will cause a recursive relation since \"{0}\" is a predecessor of \"{1}\".")]
    public string CannotChangeParentBecauseOfRecursiveRelation => this[nameof (CannotChangeParentBecauseOfRecursiveRelation)];

    /// <summary>
    /// Error Message: An item with the URL '{0}' already exists. Please change the Url or delete the existing URL first.
    /// </summary>
    [ResourceEntry("DuplicateUrlException", Description = "Error message.", LastModified = "2013/04/12", Value = "An item with the URL '{0}' already exists.")]
    public string DuplicateUrlException => this[nameof (DuplicateUrlException)];

    /// <summary>Phrase: Title cannot be empty</summary>
    [ResourceEntry("TitleCannotBeEmpty", Description = "phrase: Title cannot be empty", LastModified = "2014/01/30", Value = "Title cannot be empty")]
    public string TitleCannotBeEmpty => this[nameof (TitleCannotBeEmpty)];

    /// <summary>phrase: The title should be at most 255 symbols.</summary>
    [ResourceEntry("TitleMaxLength", Description = "phrase: The title should be at most 255 symbols.", LastModified = "2014/01/30", Value = "The title should be at most 255 symbols.")]
    public string TitleMaxLength => this[nameof (TitleMaxLength)];

    /// <summary>
    /// phrase: The alternative text should be at most 255 symbols.
    /// </summary>
    [ResourceEntry("AltTextMaxLength", Description = "phrase: The alternative text should be at most 255 symbols.", LastModified = "2014/01/30", Value = "The alternative text should be at most 255 symbols.")]
    public string AltTextMaxLength => this[nameof (AltTextMaxLength)];

    /// <summary>
    /// phrase: An error occurred while rendering a control to the page. For details check the Error.log file.
    /// </summary>
    [ResourceEntry("ControlRenderingErrorMessage", Description = "phrase: An error occurred while rendering a control to the page. For details check the Error.log file.", LastModified = "2017/01/23", Value = "An error occurred while rendering a control to the page. For details check the Error.log file.")]
    public string ControlRenderingErrorMessage => this[nameof (ControlRenderingErrorMessage)];
  }
}
