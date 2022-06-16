// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.FieldFactory
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// Used to create instances of field controls depending on the supplied type or definition
  /// </summary>
  public class FieldFactory : IFieldFactory
  {
    /// <summary>Gets the field control by the provided definition.</summary>
    /// <param name="definition">The definition.</param>
    /// <returns></returns>
    public virtual IField GetFieldControl(IFieldDefinition definition)
    {
      Control fieldControl;
      if (!string.IsNullOrEmpty(definition.FieldVirtualPath))
        fieldControl = (Control) ControlUtilities.LoadControl(definition.FieldVirtualPath);
      else
        fieldControl = definition.FieldType != (Type) null ? (Control) Activator.CreateInstance(definition.FieldType) : throw new InvalidOperationException("You must specify either virtual path or the type of the field.");
      if (!typeof (IField).IsAssignableFrom(fieldControl.GetType()))
        throw new InvalidOperationException(string.Format("The control of type '{0}' does not implement IField interface. All fields must implement IField interface.", (object) fieldControl.GetType().FullName));
      ((IField) fieldControl).Configure(definition);
      return (IField) fieldControl;
    }
  }
}
