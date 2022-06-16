// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit.PropertyEditorBrowseAndEditStrategy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;

namespace Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit
{
  public class PropertyEditorBrowseAndEditStrategy : IBrowseAndEditStrategy
  {
    public void Configure(object instance, Guid pageId)
    {
      Control control = (Control) null;
      if (instance is Control)
        control = instance as Control;
      else if (instance is Telerik.Sitefinity.Modules.Pages.ControlBuilder)
        control = (instance as Telerik.Sitefinity.Modules.Pages.ControlBuilder).Control;
      if (!(control is IBrowseAndEditable browseAndEditable))
        return;
      BrowseAndEditableInfo browseAndEditableInfo = browseAndEditable.BrowseAndEditableInfo;
      Guid guid = pageId;
      if (guid == Guid.Empty)
        guid = browseAndEditableInfo.PageId;
      IDialogDefinition dialogDefinition = Telerik.Sitefinity.Configuration.Config.Get<ContentViewConfig>().ContentViewControls["FrontendPages"].Dialogs.Where<IDialogDefinition>((Func<IDialogDefinition, bool>) (d => d.Name == "PropertyEditor")).Single<IDialogDefinition>();
      string str = string.Empty;
      if (SystemManager.CurrentContext.AppSettings.Multilingual)
        str = SystemManager.CurrentContext.Culture.Name;
      BrowseAndEditCommand browseAndEditCommand = new BrowseAndEditCommand()
      {
        CommandName = "editControl",
        CommandTitle = Res.Get<Labels>().Edit,
        UsesPagePermissions = true,
        Arguments = new BrowseAndEditCommandArgs()
        {
          DialogName = dialogDefinition.Name,
          DialogDefinition = dialogDefinition,
          ItemId = new Guid?(browseAndEditableInfo.ControlDataId),
          ItemType = browseAndEditableInfo.ControlType,
          DialogUrlParameters = new List<KeyValuePair<string, string>>()
          {
            new KeyValuePair<string, string>("Id", browseAndEditableInfo.ControlDataId.ToString()),
            new KeyValuePair<string, string>("propertyValueCulture", str),
            new KeyValuePair<string, string>("PageId", guid.ToString()),
            new KeyValuePair<string, string>("checkLiveVersion", "true"),
            new KeyValuePair<string, string>("upgradePageVersion", "true"),
            new KeyValuePair<string, string>("MediaType", 0.ToString()),
            new KeyValuePair<string, string>("isOpenedByBrowseAndEdit", "true")
          }
        }
      };
      browseAndEditable.AddCommands((IList<BrowseAndEditCommand>) new BrowseAndEditCommand[1]
      {
        browseAndEditCommand
      });
    }
  }
}
