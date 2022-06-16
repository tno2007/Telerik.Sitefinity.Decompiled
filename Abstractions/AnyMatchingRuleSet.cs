// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.AnyMatchingRuleSet
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Telerik.Microsoft.Practices.Unity.InterceptionExtension;

namespace Telerik.Sitefinity.Abstractions
{
  /// <summary>
  /// A <cref see="T:AnyMatchingRuleSet" /> is a matching rule that
  /// is a collection of other matching rules. Any of the contained
  /// rules must match for the set to match.
  /// </summary>
  [SuppressMessage("Microsoft.Naming", "CA1710", Justification = "Using alternative suffix 'Set'.")]
  public class AnyMatchingRuleSet : List<IMatchingRule>, IMatchingRule
  {
    /// <summary>
    /// Tests the given member against the ruleset. The member matches
    /// if all contained rules in the ruleset match against it.
    /// </summary>
    /// <remarks>If the ruleset is empty, then Matches passes since no rules failed.</remarks>
    /// <param name="member">MemberInfo to test.</param>
    /// <returns>true if all contained rules match, false if any fail.</returns>
    public bool Matches(MethodBase member)
    {
      foreach (IMatchingRule matchingRule in (List<IMatchingRule>) this)
      {
        if (matchingRule.Matches(member))
          return true;
      }
      return false;
    }
  }
}
