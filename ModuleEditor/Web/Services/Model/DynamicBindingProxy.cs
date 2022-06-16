// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ModuleEditor.Web.Services.Model.DynamicBindingProxy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace Telerik.Sitefinity.ModuleEditor.Web.Services.Model
{
  /// <summary>
  /// Provides a non-generic proxy class for specifying dynamic behavior at run-time.
  /// </summary>
  public class DynamicBindingProxy : DynamicObject, INotifyPropertyChanged
  {
    private static readonly Dictionary<string, Dictionary<string, PropertyInfo>> properties = new Dictionary<string, Dictionary<string, PropertyInfo>>();
    private readonly object instance;
    private readonly string typeName;

    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.ModuleEditor.Web.Services.Model.DynamicBindingProxy" /> class.
    /// </summary>
    /// <param name="instance">The instance.</param>
    public DynamicBindingProxy(object instance)
    {
      this.instance = instance;
      Type type = instance.GetType();
      this.typeName = type.FullName;
      if (DynamicBindingProxy.properties.ContainsKey(this.typeName))
        return;
      DynamicBindingProxy.SetProperties(type, this.typeName);
    }

    private static void SetProperties(Type type, string typeName)
    {
      Dictionary<string, PropertyInfo> dictionary = ((IEnumerable<PropertyInfo>) type.GetProperties(BindingFlags.Instance | BindingFlags.Public)).ToDictionary<PropertyInfo, string>((Func<PropertyInfo, string>) (prop => prop.Name));
      DynamicBindingProxy.properties.Add(typeName, dictionary);
    }

    /// <summary>
    /// Provides the implementation for operations that get member values. Classes derived from the <see cref="T:System.Dynamic.DynamicObject" /> class can override this method to specify dynamic behavior for operations such as getting a value for a property.
    /// </summary>
    /// <param name="binder">Provides information about the object that called the dynamic operation. The binder.Name property provides the name of the member on which the dynamic operation is performed. For example, for the Console.WriteLine(sampleObject.SampleProperty) statement, where sampleObject is an instance of the class derived from the <see cref="T:System.Dynamic.DynamicObject" /> class, binder.Name returns "SampleProperty". The binder.IgnoreCase property specifies whether the member name is case-sensitive.</param>
    /// <param name="result">The result of the get operation. For example, if the method is called for a property, you can assign the property value to <paramref name="result" />.</param>
    /// <returns>
    /// true if the operation is successful; otherwise, false. If this method returns false, the run-time binder of the language determines the behavior. (In most cases, a run-time exception is thrown.)
    /// </returns>
    public override bool TryGetMember(GetMemberBinder binder, out object result)
    {
      result = !DynamicBindingProxy.properties[this.typeName].ContainsKey(binder.Name) ? (object) null : DynamicBindingProxy.properties[this.typeName][binder.Name].GetValue(this.instance, (object[]) null);
      return true;
    }

    /// <summary>
    /// Provides the implementation for operations that set member values. Classes derived from the <see cref="T:System.Dynamic.DynamicObject" /> class can override this method to specify dynamic behavior for operations such as setting a value for a property.
    /// </summary>
    /// <param name="binder">Provides information about the object that called the dynamic operation. The binder.Name property provides the name of the member to which the value is being assigned. For example, for the statement sampleObject.SampleProperty = "Test", where sampleObject is an instance of the class derived from the <see cref="T:System.Dynamic.DynamicObject" /> class, binder.Name returns "SampleProperty". The binder.IgnoreCase property specifies whether the member name is case-sensitive.</param>
    /// <param name="value">The value to set to the member. For example, for sampleObject.SampleProperty = "Test", where sampleObject is an instance of the class derived from the <see cref="T:System.Dynamic.DynamicObject" /> class, the <paramref name="value" /> is "Test".</param>
    /// <returns>
    /// true if the operation is successful; otherwise, false. If this method returns false, the run-time binder of the language determines the behavior. (In most cases, a language-specific run-time exception is thrown.)
    /// </returns>
    public override bool TrySetMember(SetMemberBinder binder, object value)
    {
      if (DynamicBindingProxy.properties[this.typeName].ContainsKey(binder.Name))
      {
        DynamicBindingProxy.properties[this.typeName][binder.Name].SetValue(this.instance, value, (object[]) null);
        if (this.PropertyChanged != null)
          this.PropertyChanged((object) this, new PropertyChangedEventArgs(binder.Name));
      }
      return true;
    }
  }
}
