// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Configuration.FieldControls.ToolConfigElementList
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Configuration;

namespace Telerik.Sitefinity.Web.Configuration.FieldControls
{
  /// <summary>
  /// 
  /// </summary>
  public class ToolConfigElementList : ConfigElementList<ToolConfigElement>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.Configuration.FieldControls.ToolConfigElementList" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public ToolConfigElementList(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Determines whether toollist contains  the specified tool name
    /// </summary>
    /// <param name="name">The name.</param>
    /// <returns>
    /// 	<c>true</c> if [contains] [the specified name]; otherwise, <c>false</c>.
    /// </returns>
    public new bool Contains(string name)
    {
      foreach (ToolConfigElement toolConfigElement in (ConfigElementList<ToolConfigElement>) this)
      {
        if (toolConfigElement.Name == name)
          return true;
      }
      return false;
    }

    /// <summary>Gets the specified name.</summary>
    /// <param name="name">The name.</param>
    /// <returns></returns>
    public ToolConfigElement Get(string name)
    {
      foreach (ToolConfigElement toolConfigElement in (ConfigElementList<ToolConfigElement>) this)
      {
        if (toolConfigElement.Name == name)
          return toolConfigElement;
      }
      return (ToolConfigElement) null;
    }
  }
}
