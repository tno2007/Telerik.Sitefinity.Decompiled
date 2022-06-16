// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.IgnoreSpecialMethodsMatchingRule
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Reflection;
using Telerik.Microsoft.Practices.Unity.InterceptionExtension;

namespace Telerik.Sitefinity.Abstractions
{
  /// <summary>
  /// Represents a matching rule that avoids special methods such as property getters and setters.
  /// </summary>
  public class IgnoreSpecialMethodsMatchingRule : IMatchingRule
  {
    /// <summary>
    /// Tests to see if this rule applies to the given member.
    /// </summary>
    /// <param name="member">Member to test.</param>
    /// <returns>true if the rule applies, false if it doesn't.</returns>
    public bool Matches(MethodBase member) => !member.IsSpecialName && member.GetCustomAttributes(typeof (ApplyNoPoliciesAttribute), true).Length == 0;
  }
}
