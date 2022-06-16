// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormMetaFieldLabelViewBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;

namespace Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views
{
  /// <summary>
  /// Base class for the form "label" views for IFormMetaField controls
  /// </summary>
  public abstract class FormMetaFieldLabelViewBase : ContentViewDesignerView
  {
    /// <summary>Control that represents the meta field name</summary>
    public MetaFieldNameTextBox MetaFieldNameTextBox => this.Container.GetControl<MetaFieldNameTextBox>("metaFieldNameTextBox", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container) => this.ConfigureMetaFieldName();

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.ScriptDescriptorTypeName, this.ClientID);
      controlDescriptor.AddComponentProperty("metaFieldNameTextBox", this.MetaFieldNameTextBox.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    private void ConfigureMetaFieldName()
    {
      IFormFieldControl control = this.ParentDesigner.PropertyEditor.Control as IFormFieldControl;
      FormDraftControl controlData = this.ParentDesigner.PropertyEditor.ControlData as FormDraftControl;
      FormDraft form = controlData.Form;
      if (string.IsNullOrEmpty(control.MetaField.FieldName))
      {
        this.MetaFieldNameTextBox.FieldName = control.GetType().Name + "_" + ((Control) control).ID.ToString();
        this.MetaFieldNameTextBox.ReadOnly = false;
      }
      else
      {
        this.MetaFieldNameTextBox.FieldName = control.MetaField.FieldName;
        if (!controlData.Published)
          return;
        string fieldName = control.MetaField.FieldName;
        if (!form.GetMetaFields().Any<MetaField>((Func<MetaField, bool>) (m => m.FieldName == fieldName)))
          return;
        this.MetaFieldNameTextBox.ReadOnly = true;
      }
    }
  }
}
