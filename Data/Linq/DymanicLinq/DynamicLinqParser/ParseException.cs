// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Linq.Dynamic.ParseException
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Data.Linq.Dynamic
{
  /// <summary>
  /// 
  /// </summary>
  public sealed class ParseException : Exception
  {
    private int position;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <param name="position"></param>
    public ParseException(string message, int position)
      : base(message)
    {
      this.position = position;
    }

    /// <summary>
    /// 
    /// </summary>
    public int Position => this.position;

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override string ToString() => string.Format("{0} (at index {1})", (object) this.Message, (object) this.position);
  }
}
