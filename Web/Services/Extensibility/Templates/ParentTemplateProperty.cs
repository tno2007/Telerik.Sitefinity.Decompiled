// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Extensibility.Templates.ParentTemplatesProperty
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Web.Services.Extensibility.Templates.Models;

namespace Telerik.Sitefinity.Web.Services.Extensibility.Templates
{
  /// <summary>
  /// A property for retrieving page templates' ParentTemplate property.
  /// </summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  internal class ParentTemplatesProperty : CalculatedProperty
  {
    /// <inheritdoc />
    public override Type ReturnType => typeof (ParentTemplate);

    /// <inheritdoc />
    public override IDictionary<object, object> GetValues(
      IEnumerable items,
      IManager manager)
    {
      Dictionary<object, object> values = new Dictionary<object, object>();
      if (items == null)
        return (IDictionary<object, object>) values;
      string str1 = "\\Mvc\\Views\\Layouts\\";
      foreach (PageTemplate key in items)
      {
        ParentTemplate parentTemplate1;
        if (key.ParentTemplate == null)
        {
          parentTemplate1 = (ParentTemplate) null;
        }
        else
        {
          parentTemplate1 = new ParentTemplate();
          parentTemplate1.Id = new Guid?(key.ParentTemplate.Id);
          parentTemplate1.Title = (string) key.ParentTemplate.Title;
        }
        ParentTemplate parentTemplate2 = parentTemplate1;
        if (parentTemplate2 == null && key.Framework == PageTemplateFramework.Mvc)
        {
          string name = key.Name;
          if (name.IndexOf('.') == -1)
          {
            parentTemplate2 = new ParentTemplate()
            {
              Id = new Guid?(),
              Title = string.Format("{0}{1}.cshtml", (object) str1, (object) name)
            };
          }
          else
          {
            string str2 = ((IEnumerable<string>) name.Split('.')).First<string>();
            parentTemplate2 = new ParentTemplate()
            {
              Id = new Guid?(),
              Title = string.Format("\\ResourcePackages\\{0}{1}{2}.cshtml", (object) str2, (object) str1, (object) name)
            };
          }
        }
        else if (parentTemplate2 == null && !string.IsNullOrEmpty(key.MasterPage))
          parentTemplate2 = new ParentTemplate()
          {
            Id = new Guid?(),
            Title = key.MasterPage
          };
        values.Add((object) key, (object) parentTemplate2);
      }
      return (IDictionary<object, object>) values;
    }
  }
}
