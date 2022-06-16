// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Extensibility.FormAvailableActionsProperty
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Forms.Configuration;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Web.Services.Extensibility
{
  [EditorBrowsable(EditorBrowsableState.Never)]
  internal class FormAvailableActionsProperty : CalculatedProperty
  {
    public override Type ReturnType => typeof (List<AvailableAction>);

    /// <inheritdoc />
    public override IDictionary<object, object> GetValues(
      IEnumerable items,
      IManager manager)
    {
      Dictionary<object, object> values = new Dictionary<object, object>();
      foreach (FormDraft key in items)
      {
        if (key != null)
        {
          List<AvailableAction> availableActionList = new List<AvailableAction>()
          {
            new AvailableAction()
            {
              Key = FormRuleAction.Show,
              Value = Res.Get<Labels>().ShowField
            },
            new AvailableAction()
            {
              Key = FormRuleAction.Hide,
              Value = Res.Get<Labels>().HideField
            },
            new AvailableAction()
            {
              Key = FormRuleAction.GoTo,
              Value = Res.Get<Labels>().GoToPageAfterSubmit
            },
            new AvailableAction()
            {
              Key = FormRuleAction.ShowMessage,
              Value = Res.Get<Labels>().ShowMessage
            }
          };
          if (key.Controls.Where<FormDraftControl>((Func<FormDraftControl, bool>) (c => PropertyHelpers.IsFormPageBreak(manager, (ControlData) c))).Any<FormDraftControl>())
            availableActionList.Insert(2, new AvailableAction()
            {
              Key = FormRuleAction.Skip,
              Value = Res.Get<Labels>().SkipToStep
            });
          if (Config.Get<FormsConfig>().Notifications.Enabled)
            availableActionList.Add(new AvailableAction()
            {
              Key = FormRuleAction.SendNotification,
              Value = Res.Get<Labels>().SendEmailNotificationsTo
            });
          values.Add((object) key, (object) availableActionList);
        }
      }
      return (IDictionary<object, object>) values;
    }
  }
}
