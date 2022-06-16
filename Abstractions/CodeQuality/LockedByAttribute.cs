// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.CodeQuality.LockedByAttribute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Abstractions.CodeQuality
{
  /// <summary>
  /// This attribute is used internally in Sitefinity team to mark the piece of code locked. This means
  /// that the code marked with this attribute MUST not be modified without prior approval / consultation
  /// with the owner of the code. If owner of the code is not available, consult the most senior architect.
  /// </summary>
  [LockedBy("Boyan Rabchev", "This is part of the team code quality policies. Do not alter.")]
  [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
  internal sealed class LockedByAttribute : Attribute
  {
    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Abstractions.CodeQuality.LockedByAttribute" />.
    /// </summary>
    /// <param name="owner">First and last name of the person that owns the code.</param>
    /// <param name="reason">Reason for which the code has been locked.</param>
    public LockedByAttribute(string owner, string reason)
    {
      this.Owner = owner;
      this.Reason = reason;
    }

    /// <summary>Gets the name of the person that locked the code.</summary>
    public string Owner { get; private set; }

    /// <summary>Gets the comment (optional) about the approved code.</summary>
    public string Reason { get; private set; }
  }
}
