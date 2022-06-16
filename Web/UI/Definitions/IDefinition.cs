// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Definitions.IDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Web.UI.Definitions
{
  /// <summary>
  /// Interface that needs to be implemented by all definition classes that whish to provide
  /// the property resolving chain that includes the values stored in configurations.
  /// </summary>
  public interface IDefinition
  {
    DefinitionBase GetDefinition();

    TDefinition GetDefinition<TDefinition>() where TDefinition : DefinitionBase, new();
  }
}
