// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.BlobStorage.BlobPathExtender
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;

namespace Telerik.Sitefinity.Modules.Libraries.BlobStorage
{
  /// <summary>Represents a blob path extender class</summary>
  public class BlobPathExtender
  {
    /// <summary>
    /// Extends the current blob path including a hash key after the file name
    /// </summary>
    /// <param name="blobPath">The current file path</param>
    /// <param name="fileExtension">The current file extension</param>
    /// <returns>The new path</returns>
    public virtual string ExtendCurrentPathToUnique(string blobPath, string fileExtension)
    {
      string[] strArray = blobPath.TrimEnd('/').Split('/');
      if (strArray.Length > 1)
      {
        bool flag = !fileExtension.Contains<char>('.');
        string str = strArray[strArray.Length - 1].Replace(fileExtension, string.Empty).TrimEnd('.');
        if (flag)
          fileExtension = "." + fileExtension;
        strArray[strArray.Length - 1] = str + this.GetUniqueKey() + fileExtension;
        blobPath = string.Join("/", strArray);
      }
      return blobPath;
    }

    /// <summary>This method builds an unique key based on new Guid</summary>
    /// <returns>The generated key</returns>
    protected virtual string GetUniqueKey() => Guid.NewGuid().GetHashCode().ToString();
  }
}
