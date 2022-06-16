// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.UpgradeMethod
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Reflection;

namespace Telerik.Sitefinity.Abstractions
{
  internal class UpgradeMethod
  {
    private readonly MethodInfo method;
    private readonly object instance;
    private readonly UpgradeInfoAttribute upgradeInfo;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Abstractions.UpgradeMethod" /> class.
    /// </summary>
    /// <param name="instance">The instance.</param>
    /// <param name="method">The method.</param>
    /// <param name="upgradeAttribute">The upgrade attribute.</param>
    public UpgradeMethod(object instance, MethodInfo method, UpgradeInfoAttribute upgradeAttribute)
    {
      this.instance = instance;
      this.method = method;
      this.upgradeInfo = upgradeAttribute;
    }

    /// <summary>
    /// Executes the current method with the specified upgrade context parameter.
    /// </summary>
    /// <param name="upgradeContext">The upgrade context.</param>
    public void Execute(object upgradeContext)
    {
      object[] parameters;
      if (upgradeContext != null && upgradeContext != this.instance)
        parameters = new object[1]{ upgradeContext };
      else
        parameters = new object[0];
      this.method.Invoke(this.instance, parameters);
    }

    /// <summary>Gets the ID.</summary>
    /// <value>The ID.</value>
    public string Id => this.upgradeInfo.Id;

    /// <summary>Gets the name of the method.</summary>
    /// <value>The name of the method.</value>
    public string MethodName => this.method.Name;

    /// <summary>
    /// Gets the version of the build related to the upgrade method.
    /// </summary>
    /// <value>The version of the build.</value>
    public int UpgradeTo => this.UpgradeInfo.UpgradeTo;

    /// <summary>Gets the upgrade info.</summary>
    /// <value>The upgrade info.</value>
    public UpgradeInfoAttribute UpgradeInfo => this.upgradeInfo;

    /// <summary>Gets the instance.</summary>
    /// <value>The instance.</value>
    public object Instance => this.instance;
  }
}
