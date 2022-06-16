// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Impersonation
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace Telerik.Sitefinity.Security
{
  public class Impersonation : IDisposable
  {
    private const int LOGON32_LOGON_INTERACTIVE = 2;
    private const int LOGON32_PROVIDER_DEFAULT = 0;
    private WindowsImpersonationContext impersonationContext;
    private readonly string domain;
    private readonly string username;
    private readonly string password;

    public Impersonation(string domain, string username, string password)
    {
      this.domain = domain;
      this.username = username;
      this.password = password;
    }

    [DllImport("advapi32.dll")]
    private static extern int LogonUserA(
      string lpszUserName,
      string lpszDomain,
      string lpszPassword,
      int dwLogonType,
      int dwLogonProvider,
      ref IntPtr phToken);

    [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern int DuplicateToken(
      IntPtr hToken,
      int impersonationLevel,
      ref IntPtr hNewToken);

    [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern bool RevertToSelf();

    [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
    private static extern bool CloseHandle(IntPtr handle);

    public bool Impersonate()
    {
      if (!string.IsNullOrEmpty(this.username))
        this.impersonationContext = Impersonation.Impersonate(this.domain, this.username, this.password);
      return this.impersonationContext != null;
    }

    /// <summary>This method is used for impersonating an user.</summary>
    /// <param name="domain">Domain of the user being impersonated.</param>
    /// <param name="userName">User being impersonated.</param>
    /// <param name="password">Password of the user being impersonated.</param>
    /// <returns>A Boolean indicating if the user could successfully impersonate.</returns>
    public static WindowsImpersonationContext Impersonate(
      string domain,
      string userName,
      string password)
    {
      IntPtr zero1 = IntPtr.Zero;
      IntPtr zero2 = IntPtr.Zero;
      if (Impersonation.RevertToSelf() && Impersonation.LogonUserA(userName, domain, password, 2, 0, ref zero1) != 0 && Impersonation.DuplicateToken(zero1, 2, ref zero2) != 0)
      {
        WindowsImpersonationContext impersonationContext = new WindowsIdentity(zero2).Impersonate();
        if (impersonationContext != null)
        {
          Impersonation.CloseHandle(zero1);
          Impersonation.CloseHandle(zero2);
          return impersonationContext;
        }
      }
      if (zero1 != IntPtr.Zero)
        Impersonation.CloseHandle(zero1);
      if (zero2 != IntPtr.Zero)
        Impersonation.CloseHandle(zero2);
      return (WindowsImpersonationContext) null;
    }

    /// <summary>
    /// This method undo the impersonation done in the ImpersonateValidUser method.
    /// </summary>
    public void UndoImpersonation()
    {
      if (this.impersonationContext == null)
        return;
      this.impersonationContext.Undo();
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
      if (this.impersonationContext == null)
        return;
      this.impersonationContext.Dispose();
    }
  }
}
