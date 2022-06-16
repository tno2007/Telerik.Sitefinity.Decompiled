// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Restriction.CommandWidgetRestrictionStrategyBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;

namespace Telerik.Sitefinity.Restriction
{
  internal abstract class CommandWidgetRestrictionStrategyBase : 
    ICommandWidgetRestrictionStrategy,
    IRestrictionStrategy
  {
    public Type ContentType { get; set; }

    protected ICommandWidgetDefinition GetDefinition(object item) => item is ICommandWidgetDefinition widgetDefinition ? widgetDefinition : throw new ArgumentException("Item must implement ICommandWidgetDefinition.");

    /// <summary>
    /// Determines whether the specified command widget is restricted.
    /// </summary>
    /// <param name="item">The command widget definition.</param>
    /// <returns>Whether item is restricted.</returns>
    /// <exception cref="T:System.ArgumentException">item is not of types CommandWidget.</exception>
    public bool IsRestricted(object item) => this.IsRestricted(this.GetDefinition(item), (Type) null);

    public abstract bool IsRestricted(ICommandWidgetDefinition item, Type contentType);
  }
}
