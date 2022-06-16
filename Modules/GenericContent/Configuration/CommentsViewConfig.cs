// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.Configuration.CommentsConfig
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data.Configuration;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Modules.GenericContent.Configuration
{
  /// <summary>Represents the configuration section for Comments.</summary>
  [Obsolete("CommentsModuleConfig configures the comments.")]
  [ObjectInfo(typeof (ConfigDescriptions), Description = "CommentsViewConfigDescription", Title = "CommentsViewConfig")]
  public class CommentsConfig : ConfigSection
  {
    /// <summary>
    /// Gets the configured comments settings for this module.
    /// </summary>
    /// <value>The comments settings.</value>
    [ConfigurationProperty("commentsSettings")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "CommentsSettingsDescription", Title = "CommentsSettingsTitle")]
    public GlobalCommentsSettings CommentsSettings
    {
      get => (GlobalCommentsSettings) this["commentsSettings"];
      set => this["commentsSettings"] = (object) value;
    }

    /// <summary>Gets the configured DateFormat settings.</summary>
    /// <value>The SortExpression settings.</value>
    [ConfigurationProperty("dateFormatSettings")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DateFormatSettingsDescription", Title = "DateFormatSettingsTitle")]
    public ConfigElementList<DateFormatElement> DateFormatSettings => (ConfigElementList<DateFormatElement>) this["dateFormatSettings"];

    /// <summary>Gets the configured SortingExpression settings.</summary>
    /// <value>The SortExpression settings.</value>
    [ConfigurationProperty("sortingExpressionSettings")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "SortingExpressionSettingsDescription", Title = "SortingExpressionSettingsTitle")]
    public ConfigElementList<SortingExpressionEnabledElement> SortingExpressionSettings => (ConfigElementList<SortingExpressionEnabledElement>) this["sortingExpressionSettings"];

    /// <summary>
    /// Called when the corresponding XML element is read and properties loaded.
    /// </summary>
    protected override void OnPropertiesInitialized()
    {
      this.InitializeSortingExpressionSettings();
      this.InitilizeDateFormatSettings();
    }

    /// <summary>Initializes the sorting expression settings.</summary>
    private void InitializeSortingExpressionSettings()
    {
      Type type = typeof (Comment);
      ConfigElementList<SortingExpressionEnabledElement> expressionSettings1 = this.SortingExpressionSettings;
      SortingExpressionEnabledElement element1 = new SortingExpressionEnabledElement((ConfigElement) this.SortingExpressionSettings);
      element1.ContentType = type.FullName;
      element1.SortingExpressionTitle = "NewCreatedFirst";
      element1.SortingExpression = "DateCreated DESC";
      element1.Enabled = true;
      expressionSettings1.Add(element1);
      ConfigElementList<SortingExpressionEnabledElement> expressionSettings2 = this.SortingExpressionSettings;
      SortingExpressionEnabledElement element2 = new SortingExpressionEnabledElement((ConfigElement) this.SortingExpressionSettings);
      element2.ContentType = type.FullName;
      element2.SortingExpressionTitle = "NewModifiedFirst";
      element2.SortingExpression = "LastModified DESC";
      expressionSettings2.Add(element2);
    }

    /// <summary>Initilizes the date format settings.</summary>
    private void InitilizeDateFormatSettings()
    {
      this.DateFormatSettings.Add(new DateFormatElement((ConfigElement) this.DateFormatSettings)
      {
        DateFormat = "dd/MM/yyyy",
        Enabled = true
      });
      this.DateFormatSettings.Add(new DateFormatElement((ConfigElement) this.DateFormatSettings)
      {
        DateFormat = "dd MM yy, hh:mm"
      });
    }

    /// <summary>
    /// Gets a value indicating whether display the section in the configuration UI tree.
    /// </summary>
    /// <value>
    ///   <c>true</c> if [visible in UI]; otherwise, <c>false</c>.
    /// </value>
    public override bool VisibleInUI => false;
  }
}
