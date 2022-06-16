// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.SitefinityFileDependency
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching.Properties;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Configuration
{
  /// <summary>
  /// This class tracks a file cache dependency while caching the expiration checks in the current context.
  /// </summary>
  [ComVisible(false)]
  [Serializable]
  public class SitefinityFileDependency : ICacheItemExpiration
  {
    private readonly string dependencyFileName;
    private DateTime lastModifiedTime;
    private const string fileDependencyContextKey = "Sf_FileDependencyChecks";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Configuration.SitefinityFileDependency" /> class.
    /// </summary>
    /// <param name="fullFileName">Full name of the file.</param>
    public SitefinityFileDependency(string fullFileName)
    {
      this.dependencyFileName = !string.IsNullOrEmpty(fullFileName) ? Path.GetFullPath(fullFileName) : throw new ArgumentException(nameof (fullFileName), Resources.ExceptionNullFileName);
      this.EnsureTargetFileAccessible();
      if (!File.Exists(this.dependencyFileName))
        throw new ArgumentException(Resources.ExceptionInvalidFileName, nameof (fullFileName));
      this.lastModifiedTime = File.GetLastWriteTime(fullFileName);
    }

    /// <summary>Gets the name of the dependent file.</summary>
    /// <value>The name of the dependent file.</value>
    public string FileName => this.dependencyFileName;

    /// <summary>Gets the last modified time of the file.</summary>
    /// <value>The last modified time of the file</value>
    public DateTime LastModifiedTime => this.lastModifiedTime;

    /// <summary>Specifies if the item has expired or not.</summary>
    /// <returns>Returns true if the item has expired, otherwise false.</returns>
    public virtual bool HasExpired()
    {
      if (!this.IsToCheckExpiration())
        return false;
      this.EnsureTargetFileAccessible();
      if (!File.Exists(this.dependencyFileName) || DateTime.Compare(this.lastModifiedTime, File.GetLastWriteTime(this.dependencyFileName)) != 0)
        return true;
      if (SystemManager.CurrentHttpContext != null)
        this.GetDependencyExpirationChecks().Add(this.FileName);
      return false;
    }

    /// <summary>Notifies that the item was recently used.</summary>
    public virtual void Notify()
    {
    }

    /// <summary>Not used</summary>
    /// <param name="owningCacheItem">Not used</param>
    public virtual void Initialize(CacheItem owningCacheItem)
    {
    }

    private void EnsureTargetFileAccessible() => new FileIOPermission(FileIOPermissionAccess.Read, this.dependencyFileName).Demand();

    private bool IsToCheckExpiration() => SystemManager.CurrentHttpContext == null || !this.GetDependencyExpirationChecks().Contains(this.FileName);

    private HashSet<string> GetDependencyExpirationChecks()
    {
      HashSet<string> expirationChecks = (HashSet<string>) SystemManager.CurrentHttpContext.Items[(object) "Sf_FileDependencyChecks"];
      if (expirationChecks == null)
      {
        expirationChecks = new HashSet<string>();
        SystemManager.CurrentHttpContext.Items[(object) "Sf_FileDependencyChecks"] = (object) expirationChecks;
      }
      return expirationChecks;
    }
  }
}
