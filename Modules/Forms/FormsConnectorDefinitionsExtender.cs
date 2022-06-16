// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.FormsConnectorDefinitionsExtender
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Fields.Config;

namespace Telerik.Sitefinity.Modules.Forms
{
  /// <summary>Extends forms definitions</summary>
  public abstract class FormsConnectorDefinitionsExtender
  {
    /// <summary>Gets the ordinal</summary>
    public abstract int Ordinal { get; }

    /// <summary>Add connector settings to form views</summary>
    /// <param name="sectionFields">The section fields</param>
    public virtual void AddConnectorSettings(
      ConfigElementDictionary<string, FieldDefinitionElement> sectionFields)
    {
    }
  }
}
