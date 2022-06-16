// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.DefinitionBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Principal;
using System.Web;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>A base class for all definition classes.</summary>
  [DataContract]
  [ParseChildren(true)]
  public abstract class DefinitionBase : IDefinition, IModuleDependentItem
  {
    /// <summary>
    /// Fired before resolving the property. The method can be canceled.
    /// </summary>
    public EventHandler<EventArgs> PropertyResolving;
    /// <summary>Fired after resolving the property.</summary>
    public EventHandler<EventArgs> PropertyResolved;
    protected bool createdFromConfig;
    private string configDefinitionPath;
    private ConfigElement configDefinition;
    private string policyName;
    private string moduleName;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.DefinitionBase" /> class.
    /// </summary>
    public DefinitionBase()
      : this((ConfigElement) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.DefinitionBase" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public DefinitionBase(ConfigElement configDefinition)
    {
      if (configDefinition == null)
        return;
      this.createdFromConfig = true;
      this.configDefinition = configDefinition;
      this.ConfigDefinitionPath = this.configDefinition.GetPath();
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public virtual DefinitionBase GetDefinition() => this;

    /// <summary>Gets the definition.</summary>
    /// <typeparam name="TDefinition">The type of the definition.</typeparam>
    /// <returns></returns>
    public TDefinition GetDefinition<TDefinition>() where TDefinition : DefinitionBase, new() => (TDefinition) this.GetDefinition();

    /// <summary>
    /// Gets or sets the config definition path, used to find the coresponding definition configuration element.
    /// </summary>
    /// <value>The config definition path.</value>
    public string ConfigDefinitionPath
    {
      get => this.configDefinitionPath;
      set => this.configDefinitionPath = value;
    }

    /// <summary>Gets the name of the policy.</summary>
    /// <value>The name of the policy.</value>
    public virtual string PolicyName
    {
      get
      {
        if (string.IsNullOrEmpty(this.policyName))
        {
          HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
          if (currentHttpContext != null && currentHttpContext.User != null)
          {
            IIdentity identity = currentHttpContext.User.Identity;
            if (identity.IsAuthenticated)
              this.policyName = identity.Name;
          }
        }
        return this.policyName;
      }
    }

    /// <summary>
    /// Gets or sets an instance of configuration element that represents the definition
    /// object in configuration.
    /// </summary>
    /// <value>Configuration element representing the current definition.</value>
    public virtual ConfigElement ConfigDefinition
    {
      get
      {
        if (this.configDefinition == null)
          this.configDefinition = this.GetConfigurationDefinition();
        return this.configDefinition;
      }
    }

    protected virtual bool IsRequired(string fieldName) => TypeDescriptor.GetProperties(this.GetType()).Find(fieldName, false).Attributes.OfType<RequiredDefinitionPropertyAttribute>().Count<RequiredDefinitionPropertyAttribute>() > 0;

    /// <summary>Gets the configuration definition.</summary>
    /// <returns></returns>
    protected virtual ConfigElement GetConfigurationDefinition() => this.ConfigDefinitionPath != null ? Config.GetByPath<ConfigElement>(this.ConfigDefinitionPath) : (ConfigElement) null;

    /// <summary>
    /// Resolves the property. When returning the properites of any type that inherits from DefinitionBase you should
    /// always use this method.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="propertyName">Name of the property.</param>
    /// <param name="instanceValue">The instance value that will be returned if it is set.</param>
    /// <param name="defaultValue">The default value that will be returned if the configuration and the instance values are not set.</param>
    /// <returns>If it is set - the instanceValue or if it is set - the value from the ConfigDefinition (with the specified propertyName) or the deafaultValue</returns>
    protected internal virtual T ResolveProperty<T>(
      string propertyName,
      T instanceValue,
      T defaultValue)
    {
      this.OnPropertyResolving<T>((object) this, new PropertyResolvingEventArgs<T>(propertyName, instanceValue));
      ConfigManager manager = ConfigManager.GetManager();
      T obj1 = default (T);
      if (this.IsGenericValueSet<T>(instanceValue))
        obj1 = instanceValue;
      else if (this.ConfigDefinition != null)
      {
        PropertyDescriptor property = TypeDescriptor.GetProperties((object) this.ConfigDefinition)[propertyName];
        object obj2 = (object) null;
        if (property != null)
          obj2 = property.GetValue((object) this.ConfigDefinition);
        if (obj2 != null)
        {
          if ((object) instanceValue != null && obj2 is IEnumerable && (object) instanceValue is IList)
          {
            if ((object) obj1 == null)
              obj1 = instanceValue;
            IEnumerable enumerable = (IEnumerable) obj2;
            IList list = (IList) (object) obj1;
            foreach (object obj3 in enumerable)
              list.Add(obj3);
          }
          else
            obj1 = (T) obj2;
          return obj1;
        }
        if (property != null)
          obj1 = this.GetConfigurationValue<T>(manager, propertyName);
      }
      if (!this.IsGenericValueSet<T>(obj1))
        obj1 = defaultValue;
      this.OnPropertyResolved<T>((object) this, new PropertyResolvedEventArgs<T>(propertyName, instanceValue, obj1));
      if ((object) obj1 == null)
        obj1 = instanceValue;
      return obj1;
    }

    /// <summary>
    /// Resolves the property. When returning the properties of any type that inherits from DefinitionBase you should
    /// always use this method.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="propertyName">Name of the property.</param>
    /// <param name="instanceValue">The instance value. It will be returned if it is set.</param>
    /// <returns>If it is set - the instanceValue or if it is set - the value from the ConfigDefinition (with the specified propertyName)</returns>
    protected internal virtual T ResolveProperty<T>(string propertyName, T instanceValue) => this.ResolveProperty<T>(propertyName, instanceValue, default (T));

    /// <summary>
    /// Raises the <see cref="E:PropertyResolving" /> event.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sender">The sender.</param>
    /// <param name="args">The <see cref="T:Telerik.Sitefinity.Web.UI.PropertyResolvingEventArgs`1" /> instance containing the event data.</param>
    protected virtual void OnPropertyResolving<T>(object sender, PropertyResolvingEventArgs<T> args)
    {
      if (args == null)
        throw new ArgumentNullException(nameof (args));
      if (this.PropertyResolving != null)
        this.PropertyResolving((object) this, (EventArgs) args);
      int num = args.Cancel ? 1 : 0;
    }

    /// <summary>
    /// Raises the <see cref="E:PropertyResolved" /> event.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sender">The sender.</param>
    /// <param name="args">The <see cref="T:Telerik.Sitefinity.Web.UI.PropertyResolvedEventArgs`1" /> instance containing the event data.</param>
    protected virtual void OnPropertyResolved<T>(object sender, PropertyResolvedEventArgs<T> args)
    {
      if (args == null)
        throw new ArgumentNullException(nameof (args));
      if (this.PropertyResolved == null)
        return;
      this.PropertyResolved((object) this, (EventArgs) args);
    }

    internal virtual bool IsGenericValueSet<T>(T genericValue)
    {
      if ((object) genericValue != null)
      {
        if (typeof (ICollection).IsAssignableFrom(typeof (T)))
          return (uint) ((ICollection) (object) genericValue).Count > 0U;
        if (typeof (IList).IsAssignableFrom(typeof (T)))
          return (uint) ((ICollection) (object) genericValue).Count > 0U;
      }
      return !EqualityComparer<T>.Default.Equals(genericValue, default (T));
    }

    /// <summary>Gets the policy value.</summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="configManager">The config manager.</param>
    /// <param name="propertyName">Name of the property.</param>
    /// <returns></returns>
    public T GetPolicyValue<T>(ConfigManager configManager, string propertyName) => throw new NotImplementedException();

    /// <summary>Gets the configuration value.</summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="configManager">The config manager.</param>
    /// <param name="propertyName">Name of the property.</param>
    /// <returns></returns>
    public T GetConfigurationValue<T>(ConfigManager configManager, string propertyName)
    {
      if (this.ConfigDefinition == null)
        return default (T);
      T x = (T) TypeDescriptor.GetProperties((object) this.ConfigDefinition)[propertyName].GetValue((object) this.ConfigDefinition);
      if (EqualityComparer<T>.Default.Equals(x, default (T)) && this.IsRequired(propertyName))
        throw new ArgumentNullException(string.Format("The property {0} is required, and must be set", (object) propertyName));
      return x;
    }

    /// <summary>Gets or sets the name of the module.</summary>
    public string ModuleName
    {
      get => this.ResolveProperty<string>(nameof (ModuleName), this.moduleName);
      set => this.moduleName = value;
    }
  }
}
