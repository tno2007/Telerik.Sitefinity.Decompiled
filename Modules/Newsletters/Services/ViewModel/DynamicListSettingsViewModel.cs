// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.DynamicListSettingsViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel
{
  /// <summary>
  /// View model class for the dynamic list settings persistent class.
  /// </summary>
  [DataContract]
  public class DynamicListSettingsViewModel
  {
    /// <summary>Gets or sets the name of the connection.</summary>
    [DataMember]
    public string ConnectionName { get; set; }

    /// <summary>Gets or sets the name of the dynamic list provider.</summary>
    [DataMember]
    public string DynamicListProviderName { get; set; }

    /// <summary>Gets or sets the key of the dynamic list.</summary>
    [DataMember]
    public string ListKey { get; set; }

    /// <summary>
    /// Gets or sets the name of the field that was mapped as the first name field.
    /// </summary>
    [DataMember]
    public string FirstNameMappedField { get; set; }

    /// <summary>
    /// Gets or sets the name of the field that was mapped as the last name field.
    /// </summary>
    [DataMember]
    public string LastNameMappedField { get; set; }

    /// <summary>
    /// Gets or sets the name of the field that was mapped as the email field.
    /// </summary>
    [DataMember]
    public string EmailMappedField { get; set; }
  }
}
