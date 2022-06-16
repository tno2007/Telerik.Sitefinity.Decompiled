// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.Web.ViewModels.SystemEmailsPlaceholderViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Services.Notifications;

namespace Telerik.Sitefinity.Configuration.Web.ViewModels
{
  /// <summary>
  /// Represents a view model for the system emails placeholder.
  /// </summary>
  [DataContract]
  public class SystemEmailsPlaceholderViewModel
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Configuration.Web.ViewModels.SystemEmailsPlaceholderViewModel" /> class.
    /// </summary>
    public SystemEmailsPlaceholderViewModel()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Configuration.Web.ViewModels.SystemEmailsPlaceholderViewModel" /> class.
    /// </summary>
    /// <param name="placeholderField">The <see cref="T:Telerik.Sitefinity.Services.Notifications.PlaceholderField" /> used to initialize the view model</param>
    public SystemEmailsPlaceholderViewModel(PlaceholderField placeholderField)
    {
      this.DisplayName = placeholderField != null ? placeholderField.DisplayName : throw new ArgumentNullException(nameof (placeholderField));
      this.FieldName = placeholderField.FieldName;
    }

    /// <summary>
    /// Gets or sets the system email placeholders field name.
    /// </summary>
    [DataMember]
    public string FieldName { get; set; }

    /// <summary>
    /// Gets or sets the system email placeholders display name.
    /// </summary>
    [DataMember]
    public string DisplayName { get; set; }
  }
}
