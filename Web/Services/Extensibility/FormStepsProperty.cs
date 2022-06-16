// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Extensibility.FormStepsProperty
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Web.Services.Extensibility
{
  [EditorBrowsable(EditorBrowsableState.Never)]
  internal class FormStepsProperty : CalculatedProperty
  {
    public override Type ReturnType => typeof (List<Step>);

    /// <inheritdoc />
    public override IDictionary<object, object> GetValues(
      IEnumerable items,
      IManager manager)
    {
      Dictionary<object, object> values = new Dictionary<object, object>();
      foreach (FormDraft key in items ?? (IEnumerable) Enumerable.Empty<ControlData>())
      {
        if (key != null)
        {
          List<Step> stepList = new List<Step>();
          IEnumerable<FormDraftControl> source1 = key.Controls.Where<FormDraftControl>((Func<FormDraftControl, bool>) (c => PropertyHelpers.IsFormPageBreak(manager, (ControlData) c)));
          if (source1.Any<FormDraftControl>())
          {
            IEnumerable<FormDraftControl> source2 = key.Controls.Where<FormDraftControl>((Func<FormDraftControl, bool>) (c => PropertyHelpers.IsFormNavigation(manager, (ControlData) c)));
            if (source2.Any<FormDraftControl>())
            {
              IEnumerable<ControlProperty> persistedProperties = ControlUtilities.BehaviorResolver.GetPersistedProperties((ControlData) source2.FirstOrDefault<FormDraftControl>());
              if (persistedProperties != null)
              {
                ControlProperty controlProperty1 = persistedProperties.FirstOrDefault<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name == "Model"));
                if (controlProperty1 != null)
                {
                  ControlProperty controlProperty2 = controlProperty1.ChildProperties.FirstOrDefault<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name == "SerializedPages"));
                  if (controlProperty2 != null)
                  {
                    foreach (object obj in JsonConvert.DeserializeObject<List<object>>(controlProperty2.Value))
                    {
                      JObject jobject = obj as JObject;
                      string str1 = jobject.SelectToken("Title").ToString();
                      string str2 = jobject.SelectToken("Index").ToString();
                      stepList.Add(new Step()
                      {
                        Key = str2,
                        Value = str1
                      });
                    }
                  }
                }
              }
            }
            else
            {
              for (int index = 0; index <= source1.Count<FormDraftControl>(); ++index)
                stepList.Add(new Step()
                {
                  Key = index.ToString(),
                  Value = string.Format(Res.Get<Labels>("FormStep"), (object) (index + 1))
                });
            }
          }
          values.Add((object) key, (object) stepList);
        }
      }
      return (IDictionary<object, object>) values;
    }
  }
}
