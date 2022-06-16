// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.ILessCompiler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.IO;

namespace Telerik.Sitefinity.Web
{
  /// <summary>LESS to CSS compiler.</summary>
  public interface ILessCompiler
  {
    /// <summary>Compiles a LESS file to CSS.</summary>
    /// <param name="filePath">Path to the LESS file.</param>
    /// <returns>Compiled CSS.</returns>
    string CompileFile(string filePath, LessCompilerSettings settings);

    /// <summary>Compiles a LESS stream to CSS.</summary>
    /// <param name="less">Input LESS stream.</param>
    /// <returns>Compiled CSS.</returns>
    string Compile(Stream less, LessCompilerSettings settings);
  }
}
