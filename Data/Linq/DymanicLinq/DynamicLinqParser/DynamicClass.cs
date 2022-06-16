// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Linq.Dynamic.DynamicClass
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Reflection;
using System.Text;

namespace Telerik.Sitefinity.Data.Linq.Dynamic
{
  /// <summary>
  /// 
  /// </summary>
  public abstract class DynamicClass
  {
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
      PropertyInfo[] properties = this.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("{");
      for (int index = 0; index < properties.Length; ++index)
      {
        if (index > 0)
          stringBuilder.Append(", ");
        stringBuilder.Append(properties[index].Name);
        stringBuilder.Append("=");
        stringBuilder.Append(properties[index].GetValue((object) this, (object[]) null));
      }
      stringBuilder.Append("}");
      return stringBuilder.ToString();
    }
  }
}
