// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Extensibility.Templates.TemplateRenderersProperty
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Renderer.Editor.Specifics;

namespace Telerik.Sitefinity.Web.Services.Extensibility.Templates
{
  /// <summary>
  /// A property for retrieving page templates' renderer property.
  /// </summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  internal class TemplateRenderersProperty : CalculatedProperty
  {
    /// <inheritdoc />
    public override Type ReturnType => typeof (string);

    /// <inheritdoc />
    public override IDictionary<object, object> GetValues(
      IEnumerable items,
      IManager manager)
    {
      Dictionary<object, object> values = new Dictionary<object, object>();
      if (items == null)
        return (IDictionary<object, object>) values;
      foreach (PageTemplate pageTemplate in items)
      {
        IRendererCommonData lastContainer = pageTemplate.GetLastContainer<IRendererCommonData>();
        values.Add((object) pageTemplate, (object) lastContainer?.Renderer);
      }
      return (IDictionary<object, object>) values;
    }
  }
}
