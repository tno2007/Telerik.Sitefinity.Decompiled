// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.CodeQuality.ApprovedByAttribute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Abstractions.CodeQuality
{
  /// <summary>
  /// This attribute is used internally in Sitefinity team to mark the public types and members
  /// that have been reviewed and approved by people with rights to do so.
  /// </summary>
  [LockedBy("Boyan Rabchev", "This is part of the team code quality policies. Do not alter.")]
  [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
  internal sealed class ApprovedByAttribute : Attribute
  {
    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Abstractions.CodeQuality.ApprovedByAttribute" />.
    /// </summary>
    /// <param name="approver">First and last name of the person that approved the code.</param>
    /// <param name="dateOfApproval">Date when the code was approved (YYYY/MM/DD).</param>
    public ApprovedByAttribute(string approver, string dateOfApproval)
      : this(approver, dateOfApproval, (string) null)
    {
    }

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Abstractions.CodeQuality.ApprovedByAttribute" />.
    /// </summary>
    /// <param name="approver">First and last name of the person that approved the code.</param>
    /// <param name="dateOfApproval">Date when the code was approved (YYYY/MM/DD).</param>
    /// <param name="comment">Additional comments on the approved code.</param>
    public ApprovedByAttribute(string approver, string dateOfApproval, string comment)
    {
      this.Approver = approver;
      this.DateOfApproval = dateOfApproval;
      this.Comment = comment;
    }

    /// <summary>Gets the name of the person that made the approval.</summary>
    public string Approver { get; private set; }

    /// <summary>Gets the date of the approval (YYYY/MM/DD)</summary>
    public string DateOfApproval { get; private set; }

    /// <summary>Gets the comment (optional) about the approved code.</summary>
    public string Comment { get; private set; }
  }
}
