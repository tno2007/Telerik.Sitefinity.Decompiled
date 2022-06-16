// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.SecurityDemandFailException
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Security
{
  /// <summary>
  /// This is the exception that Sitefinity throws when
  /// <see cref="M:Telerik.Sitefinity.Security.SecurityExtensions.Demand(Telerik.Sitefinity.Security.Model.ISecuredObject, System.String, System.Guid[], System.Int32)" />
  /// is called.
  /// </summary>
  public sealed class SecurityDemandFailException : ApplicationException
  {
    /// <summary>
    /// Creates a new <see cref="T:Telerik.Sitefinity.Security.SecurityDemandFailException" /> with formatted message
    /// </summary>
    /// <param name="item">Security item that failed the check</param>
    /// <param name="permissionSet">Permission set for tha failed item</param>
    /// <param name="principals">Principals for which the permission failed</param>
    /// <param name="actions">Bit-mask of security actions for which the permission failed</param>
    /// <returns>Instance of <see cref="T:Telerik.Sitefinity.Security.SecurityDemandFailException" /> with formatted message.</returns>
    public static SecurityDemandFailException Create(
      ISecuredObject item,
      string permissionSet,
      Guid[] principals,
      int actions)
    {
      if (principals == null || principals.Length == 0)
        principals = new Guid[1]
        {
          SecurityManager.GetCurrentUserId()
        };
      return new SecurityDemandFailException(string.Format("{0} was not granted {1} in {2} for principals with IDs {3}", (object) (item.GetType().FullName + ", " + item.GetType().Assembly.GetName().Name), (object) string.Join(", ", ((IEnumerable<string>) SecurityDemandFailException.GetActionNames(permissionSet, actions)).Select<string, string>((Func<string, string>) (id => id.ToString())).ToArray<string>()), (object) permissionSet, (object) string.Join(", ", ((IEnumerable<Guid>) principals).Select<Guid, string>((Func<Guid, string>) (id => id.ToString())).ToArray<string>())));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.SecurityDemandFailException" /> class.
    /// </summary>
    public SecurityDemandFailException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.SecurityDemandFailException" /> class.
    /// </summary>
    /// <param name="message">A message that describes the error</param>
    public SecurityDemandFailException(string message)
      : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.SecurityDemandFailException" /> class.
    /// </summary>
    /// <param name="message">A message that describes the error</param>
    /// <param name="innerException">
    /// The exception that is the cause of the current exception. If the innerException
    /// parameter is not a null reference, the current exception is raised in a catch
    /// block that handles the inner exception.
    /// </param>
    public SecurityDemandFailException(string message, Exception innerException)
      : base(message, innerException)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.SecurityDemandFailException" /> class.
    /// </summary>
    /// <param name="info">The object that holds the serialized object data.</param>
    /// <param name="context">The contextual information about the source or destination.</param>
    public SecurityDemandFailException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    private static string[] GetActionNames(string permissionSetName, int actions)
    {
      Telerik.Sitefinity.Security.Configuration.Permission permission = Config.Get<SecurityConfig>().Permissions[permissionSetName];
      List<string> stringList = new List<string>();
      foreach (SecurityAction action in (ConfigElementCollection) permission.Actions)
      {
        if ((actions & action.Value) == action.Value)
          stringList.Add(action.Name);
      }
      return stringList.ToArray();
    }
  }
}
