// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.ConfigElementLink`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Configuration
{
  /// <summary>Declared a link to external element</summary>
  /// <typeparam name="TElement">The type of the element.</typeparam>
  public class ConfigElementLink<TElement> : 
    ConfigElementItem<TElement>,
    IConfigElementLink,
    IConfigElementItem
    where TElement : ConfigElement
  {
    private string path;

    /// <summary>
    /// Initializes a new instance of the <see cref="!:ConfigElementLink" /> class.
    /// </summary>
    /// <param name="path">The path.</param>
    public ConfigElementLink(string key, string path, string moduleName = null)
    {
      this.key = key;
      this.path = path;
      this.ModuleName = moduleName;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="!:ConfigElementLink" /> class.
    /// </summary>
    /// <param name="element">The element.</param>
    public ConfigElementLink(string key, ConfigElement element, string moduleName = null)
    {
      this.key = key;
      this.path = element.GetPath();
      this.ModuleName = moduleName;
    }

    /// <summary>The path to the target (real) element.</summary>
    /// <value>The path.</value>
    public string Path => this.path;

    /// <summary>
    /// The name of the module, that contains the target element. May be <c>null</c>.
    /// </summary>
    public string ModuleName { get; set; }

    /// <summary>Gets the linked element.</summary>
    /// <returns></returns>
    public override TElement Element
    {
      get
      {
        if (this.ModuleName != null)
        {
          switch (SystemManager.GetModule(this.ModuleName))
          {
            case null:
            case InactiveModule _:
              return default (TElement);
          }
        }
        return (TElement) Config.GetByPath<ConfigElement>(this.path);
      }
    }
  }
}
