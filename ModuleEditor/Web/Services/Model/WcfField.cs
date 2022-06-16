// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ModuleEditor.Web.Services.Model.WcfField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.Serialization;

namespace Telerik.Sitefinity.ModuleEditor.Web.Services.Model
{
  /// <summary>Wcf class for transfering data for field object.</summary>
  [DataContract]
  public class WcfField : FieldViewModel
  {
    /// <summary>Gets or sets the type of the field.</summary>
    /// <value>The type.</value>
    [DataMember]
    public string FieldTypeKey { get; set; }

    /// <summary>Gets or sets the field info.</summary>
    /// <value>The field info.</value>
    [DataMember]
    public WcfFieldDefinition Definition { get; set; }

    /// <summary>Gets or sets the database mapping.</summary>
    /// <value>The database mapping.</value>
    [DataMember]
    public WcfDatabaseMapping DatabaseMapping { get; set; }
  }
}
