// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ClientLabelManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// This control is used to provide client side code with the localized messages
  /// </summary>
  [ParseChildren(true)]
  public class ClientLabelManager : CompositeControl, IScriptControl
  {
    private Collection<ClientLableBase> labels;
    private Dictionary<string, string> programaticallySetLabels = new Dictionary<string, string>();
    private ResourceCulture clientLabelManagerCulture;

    /// <summary>
    /// Gets the collection of labels that will be available on the client
    /// </summary>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public Collection<ClientLableBase> Labels
    {
      get
      {
        if (this.labels == null)
          this.labels = new Collection<ClientLableBase>();
        return this.labels;
      }
    }

    /// <summary>Gets or sets the client label manager culture.</summary>
    /// <value>The client label manager culture.</value>
    public ResourceCulture ClientLabelManagerCulture
    {
      get => this.clientLabelManagerCulture;
      set => this.clientLabelManagerCulture = value;
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      if (this.Page == null)
        throw new HttpException(Res.Get<ErrorMessages>().PageIsNull);
      ScriptManager.GetCurrent(this.Page).RegisterScriptControl<ClientLabelManager>(this);
    }

    /// <summary>
    /// Writes the <see cref="T:System.Web.UI.WebControls.CompositeControl" /> content to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object, for display on the client.
    /// </summary>
    /// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
    protected override void Render(HtmlTextWriter writer)
    {
      if (!this.DesignMode && this.Page != null)
        ScriptManager.GetCurrent(this.Page)?.RegisterScriptDescriptors((IScriptControl) this);
      base.Render(writer);
    }

    /// <summary>
    /// Adds a label to the client label manager. Replaces existing programatically set resources. Merges with Labels property set via UI
    /// </summary>
    /// <param name="resClass">Resource class name</param>
    /// <param name="resID">Resource label ID</param>
    /// <exception cref="T:System.ArgumentException">When either <paramref name="resClass" /> or <paramref name="resID" /> is null or empty</exception>
    public void AddClientLabel(string resClass, string resID) => this.programaticallySetLabels[resClass + resID] = !string.IsNullOrEmpty(resClass) && !string.IsNullOrEmpty(resID) ? Res.Get(resClass, resID) : throw new ArgumentException("resClass or resID is null or empty string");

    /// <summary>
    /// Adds a label to the client label manager. Replaces existing programatically set resources. Merges with Labels property set via UI
    /// </summary>
    /// <param name="name">constant name</param>
    /// <param name="value">constant value</param>
    /// <exception cref="T:System.ArgumentException">When either <paramref name="name" /> or <paramref name="value" /> is null or empty</exception>
    public void AddConstant(string name, string value)
    {
      if (string.IsNullOrEmpty(name))
        throw new ArgumentException("name is null or empty string");
      this.programaticallySetLabels[name] = value;
    }

    /// <summary>
    /// Adds a dictionary of (resClass, resId) to the client label manager
    /// </summary>
    /// <param name="resources">A list of resource strings (resClass, resID)</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="resources" /> is null</exception>
    public void AddDictionaryOfClientLabels(
      IEnumerable<KeyValuePair<string, string>> resources)
    {
      if (resources == null)
        throw new ArgumentNullException(nameof (resources));
      foreach (KeyValuePair<string, string> resource in resources)
        this.AddClientLabel(resource.Key, resource.Value);
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      foreach (ClientLableBase label in this.Labels)
      {
        if (label is ClientLabel)
        {
          ClientLabel clientLabel = (ClientLabel) label;
          switch (this.clientLabelManagerCulture)
          {
            case ResourceCulture.FrontendDefultCulture:
              dictionary.Add(clientLabel.ClassId + clientLabel.Key, Res.Get(clientLabel.ClassId, clientLabel.Key, SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage));
              continue;
            case ResourceCulture.BackendDefaultCulture:
              dictionary.Add(clientLabel.ClassId + clientLabel.Key, Res.Get(clientLabel.ClassId, clientLabel.Key, Res.CurrentBackendCulture));
              continue;
            default:
              dictionary.Add(clientLabel.ClassId + clientLabel.Key, Res.Get(clientLabel.ClassId, clientLabel.Key));
              continue;
          }
        }
        else if (label is ClientMesasge)
        {
          ClientMesasge clientMesasge = (ClientMesasge) label;
          dictionary.Add(clientMesasge.Key, clientMesasge.Mesasge);
        }
      }
      foreach (string key in this.programaticallySetLabels.Keys)
      {
        if (!dictionary.ContainsKey(key))
          dictionary.Add(key, this.programaticallySetLabels[key]);
      }
      JavaScriptSerializer scriptSerializer = new JavaScriptSerializer();
      ScriptBehaviorDescriptor behaviorDescriptor = new ScriptBehaviorDescriptor(this.GetType().FullName, this.ClientID);
      behaviorDescriptor.AddProperty("_dictionary", (object) scriptSerializer.Serialize((object) dictionary));
      return (IEnumerable<ScriptDescriptor>) new ScriptBehaviorDescriptor[1]
      {
        behaviorDescriptor
      };
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new ScriptReference[1]
    {
      new ScriptReference("Telerik.Sitefinity.Web.UI.Components.Scripts.ClientLabelManager.js", this.GetType().Assembly.FullName)
    };
  }
}
