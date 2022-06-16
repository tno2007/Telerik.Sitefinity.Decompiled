// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.UserProfiles.Web.UI.UserProvidersFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Definitions;

namespace Telerik.Sitefinity.Modules.UserProfiles.Web.UI
{
  /// <summary>
  /// A definition class containing all information needed to construct an instance of a
  /// <see cref="!:PageTemplateField" />
  /// </summary>
  public class UserProvidersFieldDefinition : 
    FieldControlDefinition,
    IUserProvidersFieldDefinition,
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="!:ColumnDefinition" /> class.
    /// </summary>
    public UserProvidersFieldDefinition()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="!:ViewModeDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public UserProvidersFieldDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }
  }
}
