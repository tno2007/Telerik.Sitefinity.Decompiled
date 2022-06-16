// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.SitefinityControlPersister
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.IO;
using System.Web;
using System.Web.UI;
using Telerik.Sitefinity.Abstractions.VirtualPath;

namespace Telerik.Sitefinity.Web.UI
{
  internal static class SitefinityControlPersister
  {
    private static void PersistObject(
      HtmlTextWriter writer,
      object control,
      bool runAtServer,
      CursorCollection placeHolders)
    {
      Type type = control.GetType();
      string tagPrefix = placeHolders.GetTagPrefix(type.Namespace, type.Assembly.GetName().Name, "sf");
      writer.WriteBeginTag(tagPrefix + ":" + control.GetType().Name);
      foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(control))
        SitefinityControlPersister.ProcessAttribute(property, control, writer, string.Empty);
      if (runAtServer)
        writer.WriteAttribute("runat", "server");
      if (control is IComponent component && component.Site != null)
      {
        IEventBindingService service = (IEventBindingService) component.Site.GetService(typeof (IEventBindingService));
        if (service != null)
        {
          foreach (EventDescriptor e in TypeDescriptor.GetEvents((object) component))
            SitefinityControlPersister.ProcessEvent(e, component, writer, service);
        }
      }
      if (SitefinityControlPersister.HasInnerProperties(control))
      {
        writer.Write('>');
        ++writer.Indent;
        SitefinityControlPersister.PersistInnerProperties(writer, control, placeHolders);
        --writer.Indent;
        writer.WriteEndTag(tagPrefix + ":" + control.GetType().Name);
      }
      else
        writer.Write(" />");
      writer.WriteLine();
      writer.Flush();
    }

    private static void PersistInnerProperties(
      HtmlTextWriter writer,
      object component,
      CursorCollection placeHolders)
    {
      if (TypeDescriptor.GetAttributes(component)[typeof (PersistChildrenAttribute)] is PersistChildrenAttribute attribute1 && attribute1.Persist && component is Control)
      {
        if (((Control) component).Controls.Count <= 0)
          return;
        ++writer.Indent;
        foreach (Control control in ((Control) component).Controls)
          SitefinityControlPersister.PersistObject(writer, (object) control, true, placeHolders);
        --writer.Indent;
      }
      else
      {
        PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(component);
        bool flag = false;
        foreach (PropertyDescriptor prop in properties)
        {
          if (prop.SerializationVisibility != DesignerSerializationVisibility.Hidden && !prop.DesignTimeOnly && prop.Converter != null && prop.Attributes[typeof (PersistenceModeAttribute)] is PersistenceModeAttribute attribute && attribute.Mode != PersistenceMode.Attribute)
          {
            switch (attribute.Mode)
            {
              case PersistenceMode.InnerProperty:
                SitefinityControlPersister.PersistInnerProperty(prop, prop.GetValue(component), writer, false, placeHolders);
                flag = true;
                continue;
              case PersistenceMode.InnerDefaultProperty:
                if (flag)
                  throw new Exception("The Control has inner properties in addition to a default inner property");
                SitefinityControlPersister.PersistInnerProperty(prop, prop.GetValue(component), writer, true, placeHolders);
                return;
              case PersistenceMode.EncodedInnerDefaultProperty:
                if (flag)
                  throw new Exception("The Control has inner properties in addition to a default inner property");
                if (prop.Converter.CanConvertTo(typeof (string)))
                {
                  writer.Write(HttpUtility.HtmlEncode(prop.Converter.ConvertToString(prop.GetValue(component))));
                  return;
                }
                continue;
              default:
                continue;
            }
          }
        }
        writer.WriteLine();
      }
    }

    private static void PersistInnerProperty(
      PropertyDescriptor prop,
      object value,
      HtmlTextWriter writer,
      bool isDefault,
      CursorCollection placeHolders)
    {
      writer.WriteLine();
      if (value == null)
      {
        if (isDefault)
          return;
        writer.WriteBeginTag(prop.Name);
        writer.Write(" />");
      }
      else if (value is ICollection)
      {
        if (((ICollection) value).Count <= 0)
          return;
        if (!isDefault)
        {
          writer.WriteFullBeginTag(prop.Name);
          ++writer.Indent;
        }
        foreach (object control in (IEnumerable) value)
          SitefinityControlPersister.PersistObject(writer, control, false, placeHolders);
        if (isDefault)
          return;
        --writer.Indent;
        writer.WriteEndTag(prop.Name);
      }
      else if (isDefault)
      {
        if (!prop.Converter.CanConvertTo(typeof (string)))
          return;
        writer.Write(prop.Converter.ConvertToString(value));
      }
      else
      {
        writer.WriteBeginTag(prop.Name);
        foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(value))
          SitefinityControlPersister.ProcessAttribute(property, value, writer, string.Empty);
        writer.Write(" />");
      }
    }

    private static void ProcessEvent(
      EventDescriptor e,
      IComponent comp,
      HtmlTextWriter writer,
      IEventBindingService evtBind)
    {
      PropertyDescriptor eventProperty = evtBind.GetEventProperty(e);
      string str;
      try
      {
        str = eventProperty.GetValue((object) comp) as string;
      }
      catch (Exception ex)
      {
        if (ex is NotImplementedException || ex.InnerException is NotImplementedException)
          return;
        throw;
      }
      if (eventProperty.SerializationVisibility != DesignerSerializationVisibility.Visible || str == null || eventProperty.DesignTimeOnly || eventProperty.IsReadOnly || !eventProperty.ShouldSerializeValue((object) comp))
        return;
      writer.WriteAttribute("On" + eventProperty.Name, str);
    }

    /// <summary>
    /// Writes an attribute to an HtmlTextWriter if it needs serializing
    /// </summary>
    /// <returns>True if it does any writing</returns>
    private static bool ProcessAttribute(
      PropertyDescriptor prop,
      object o,
      HtmlTextWriter writer,
      string prefix)
    {
      try
      {
        prop.GetValue(o);
      }
      catch (Exception ex)
      {
        if (ex is NotImplementedException || ex.InnerException is NotImplementedException)
          return false;
        throw;
      }
      if (prop.SerializationVisibility == DesignerSerializationVisibility.Hidden || prop.DesignTimeOnly || prop.IsReadOnly || !prop.ShouldSerializeValue(o) || prop.Converter == null || !prop.Converter.CanConvertTo(typeof (string)))
        return false;
      bool flag = false;
      if (!(prop.Attributes[typeof (PersistenceModeAttribute)] is PersistenceModeAttribute attribute) || attribute.Mode == PersistenceMode.Attribute)
      {
        if (prop.SerializationVisibility == DesignerSerializationVisibility.Visible)
        {
          if (prefix == string.Empty)
            writer.WriteAttribute(prop.Name, prop.Converter.ConvertToString(prop.GetValue(o)));
          else
            writer.WriteAttribute(prefix + "-" + prop.Name, prop.Converter.ConvertToString(prop.GetValue(o)));
          flag = true;
        }
        else if (prop.SerializationVisibility == DesignerSerializationVisibility.Content)
        {
          object obj = prop.GetValue(o);
          foreach (PropertyDescriptor childProperty in prop.GetChildProperties(obj))
          {
            if (SitefinityControlPersister.ProcessAttribute(childProperty, obj, writer, prop.Name))
              flag = true;
          }
        }
      }
      return flag;
    }

    private static bool HasInnerProperties(object component)
    {
      if (component == null)
        throw new ArgumentNullException(nameof (component));
      if (TypeDescriptor.GetAttributes(component)[typeof (PersistChildrenAttribute)] is PersistChildrenAttribute attribute1 && attribute1.Persist && component is Control)
        return true;
      foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(component))
      {
        if (property.SerializationVisibility != DesignerSerializationVisibility.Hidden && !property.DesignTimeOnly && property.Converter != null && property.Attributes[typeof (PersistenceModeAttribute)] is PersistenceModeAttribute attribute2 && attribute2.Mode != PersistenceMode.Attribute)
          return true;
      }
      return false;
    }

    internal static string PersistObject(object component, CursorCollection placeHolders)
    {
      StringWriter sw = new StringWriter();
      SitefinityControlPersister.PersistObject((TextWriter) sw, component, placeHolders);
      sw.Flush();
      return sw.ToString();
    }

    internal static void PersistObject(
      TextWriter sw,
      object component,
      CursorCollection placeHolders)
    {
      if (component == null)
        throw new ArgumentNullException("control");
      if (sw == null)
        throw new ArgumentNullException(nameof (sw));
      SitefinityControlPersister.PersistObject(!(sw is HtmlTextWriter) ? new HtmlTextWriter(sw) : (HtmlTextWriter) sw, component, true, placeHolders);
    }
  }
}
