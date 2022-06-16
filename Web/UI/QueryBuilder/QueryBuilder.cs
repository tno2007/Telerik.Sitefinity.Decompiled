// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.QueryBuilder
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Web.UI
{
  public class QueryBuilder : SimpleScriptView
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Search.QueryBuilder.ascx");
    private string persistentTypeName;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.QueryBuilder" /> class.
    /// </summary>
    public QueryBuilder() => this.LayoutTemplatePath = QueryBuilder.layoutTemplatePath;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the name of the persistent type against which the query will be built.
    /// </summary>
    /// <value>The name of the persistent type.</value>
    public string PersistentTypeName
    {
      get => this.persistentTypeName;
      set => this.persistentTypeName = value;
    }

    /// <summary>Gets the control that contains the whole thing</summary>
    /// <value>The container control.</value>
    protected virtual Control ContainerControl => this.Container.GetControl<Control>("queryBuilderContainer", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      if (string.IsNullOrEmpty(this.persistentTypeName))
        throw new ArgumentNullException("PersistentTypeName");
    }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.
    /// </returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptBehaviorDescriptor behaviorDescriptor = new ScriptBehaviorDescriptor(this.GetType().FullName, this.ClientID);
      Type componentType = TypeResolutionService.ResolveType(this.PersistentTypeName);
      PropertyDescriptorCollection descriptorCollection = !(componentType == (Type) null) ? TypeDescriptor.GetProperties(componentType) : throw new ArgumentException(string.Format("Type \"{0}\" cannot be resolved.", (object) this.PersistentTypeName));
      IList<string> source = (IList<string>) new List<string>();
      foreach (PropertyDescriptor propertyDescriptor in descriptorCollection)
      {
        SearchableAttribute attribute = (SearchableAttribute) propertyDescriptor.Attributes[typeof (SearchableAttribute)];
        if (attribute == null || attribute.IsSearchable)
          source.Add(propertyDescriptor.DisplayName);
      }
      behaviorDescriptor.AddProperty("typeProperties", (object) source.ToArray<string>());
      behaviorDescriptor.AddProperty("containerId", (object) this.ContainerControl.ClientID);
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
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      string fullName = this.GetType().Assembly.FullName;
      return (IEnumerable<ScriptReference>) new ScriptReference[5]
      {
        new ScriptReference()
        {
          Assembly = fullName,
          Name = "Telerik.Sitefinity.Web.UI.QueryBuilder.Scripts.QueryBuilder.js"
        },
        new ScriptReference()
        {
          Assembly = fullName,
          Name = "Telerik.Sitefinity.Web.UI.QueryBuilder.Scripts.QueryData.js"
        },
        new ScriptReference()
        {
          Assembly = fullName,
          Name = "Telerik.Sitefinity.Web.UI.QueryBuilder.Scripts.QueryDataItem.js"
        },
        new ScriptReference()
        {
          Assembly = fullName,
          Name = "Telerik.Sitefinity.Web.UI.QueryBuilder.Scripts.QueryItem.js"
        },
        new ScriptReference()
        {
          Assembly = fullName,
          Name = "Telerik.Sitefinity.Web.UI.QueryBuilder.Scripts.QueryGroup.js"
        }
      };
    }

    /// <inheritdoc />
    protected override ScriptRef GetRequiredCoreScripts() => base.GetRequiredCoreScripts() | ScriptRef.MicrosoftAjaxTemplates;
  }
}
