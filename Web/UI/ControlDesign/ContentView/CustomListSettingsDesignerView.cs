// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ControlDesign.ContentView.CustomListSettingsDesignerView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Web.UI.ControlDesign.ContentView
{
  public class CustomListSettingsDesignerView : ListSettingsDesignerView
  {
    internal const string customListSettingsDesignerViewScript = "Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.CustomListSettingsDesignerView.js";
    private Dictionary<string, Dictionary<string, string>> sortableFields = new Dictionary<string, Dictionary<string, string>>();

    protected override void InitializeControls(GenericContainer container)
    {
      base.InitializeControls(container);
      this.SortExpressionControl.Choices.Clear();
      foreach (string userProfileTypeName in UserProfilesHelper.GetUserProfileTypeNames())
      {
        PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(TypeResolutionService.ResolveType(userProfileTypeName));
        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        foreach (PropertyDescriptor propertyDescriptor in properties)
        {
          if (propertyDescriptor.Attributes[typeof (SortableAttribute)] is SortableAttribute attribute)
          {
            string name = propertyDescriptor.Name;
            if (propertyDescriptor.PropertyType.Name == "DateTime")
            {
              dictionary[name + " ASC"] = string.Format("{0} {1}", (object) attribute.DisplayName, (object) Res.Get<UserProfilesResources>().AscendingDateTimeSuffix) ?? string.Format("{0} {1}", (object) propertyDescriptor.DisplayName, (object) Res.Get<UserProfilesResources>().AscendingDateTimeSuffix);
              dictionary[name + " DESC"] = string.Format("{0} {1}", (object) attribute.DisplayName, (object) Res.Get<UserProfilesResources>().DescendingDateTimeSuffix) ?? string.Format("{0} {1}", (object) propertyDescriptor.DisplayName, (object) Res.Get<UserProfilesResources>().DescendingDateTimeSuffix);
            }
            else
            {
              dictionary[name + " ASC"] = string.Format("{0} {1}", (object) attribute.DisplayName, (object) Res.Get<UserProfilesResources>().AscendingSuffix) ?? string.Format("{0} {1}", (object) propertyDescriptor.DisplayName, (object) Res.Get<UserProfilesResources>().AscendingSuffix);
              dictionary[name + " DESC"] = string.Format("{0} {1}", (object) attribute.DisplayName, (object) Res.Get<UserProfilesResources>().DescendingSuffix) ?? string.Format("{0} {1}", (object) propertyDescriptor.DisplayName, (object) Res.Get<UserProfilesResources>().DescendingSuffix);
            }
          }
        }
        this.sortableFields[userProfileTypeName] = dictionary;
      }
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = base.GetScriptDescriptors().Last<ScriptDescriptor>() as ScriptControlDescriptor;
      controlDescriptor.AddProperty("sortableFields", (object) this.sortableFields);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.CustomListSettingsDesignerView.js", this.GetType().Assembly.GetName().ToString())
    };
  }
}
