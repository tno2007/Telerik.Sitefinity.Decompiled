// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Definitions.DefinitionExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Configuration;

namespace Telerik.Sitefinity.Web.UI.Definitions
{
  /// <summary>
  /// Contains extension methods for objects implementing the <see cref="T:Telerik.Sitefinity.Web.UI.Definitions.IDefinition" /> interface and inheriting the <see cref="T:Telerik.Sitefinity.Web.UI.DefinitionBase" /> class.
  /// </summary>
  public static class DefinitionExtensions
  {
    /// <summary>
    /// A method used in definitions to resolve it's property value as another definition object instead of configuration element.
    /// </summary>
    /// <typeparam name="TConfElRepresentation">The type of the configuration element that contains the default values
    /// of this definition also known as the type of this definition's underlying configuration element.</typeparam>
    /// <typeparam name="TDefinitionToResolve">The definition type that the returned object will be cast to.</typeparam>
    /// <param name="definition">The definition instance that this method extends.</param>
    /// <param name="definitionSelector">A function that will be executed to select the configuration element that
    /// will be wrapped in a definition and returned. In other words this function should return the configuration
    /// element that represents the definition that this method resolves.</param>
    /// <returns>The resolved definition object.</returns>
    internal static TDefinitionToResolve ResolveComplexPropertyAsDefinition<TConfElRepresentation, TDefinitionToResolve>(
      this DefinitionBase definition,
      Func<TConfElRepresentation, TDefinitionToResolve> definitionSelector)
      where TConfElRepresentation : ConfigElement
      where TDefinitionToResolve : class, IDefinition
    {
      TConfElRepresentation elRepresentation = definition != null ? (TConfElRepresentation) definition.ConfigDefinition : throw new ArgumentNullException(nameof (definition));
      TDefinitionToResolve definitionToResolve = definitionSelector(elRepresentation);
      return (object) definitionToResolve != null ? (TDefinitionToResolve) definitionToResolve.GetDefinition() : default (TDefinitionToResolve);
    }
  }
}
