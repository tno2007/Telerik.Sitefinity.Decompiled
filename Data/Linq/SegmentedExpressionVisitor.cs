// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Linq.SegmentedExpressionVisitor
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Text;

namespace Telerik.Sitefinity.Data.Linq
{
  /// <summary>Base class for segmented visitors</summary>
  public abstract class SegmentedExpressionVisitor : ExpressionVisitor
  {
    private OrderedDictionary segments;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.Linq.SegmentedExpressionVisitor" /> class.
    /// </summary>
    public SegmentedExpressionVisitor() => this.segments = new OrderedDictionary((IEqualityComparer) StringComparer.OrdinalIgnoreCase);

    /// <summary>Returns the current segment's string builder</summary>
    protected override StringBuilder ExpressionBuilder => this[this.CurrentSegmentName][this.CurrentSegmentOrientation != SegmentOrientation.Start ? this[this.CurrentSegmentName].Count - this.CurrentSegmentIndex : this.CurrentSegmentIndex];

    /// <summary>Visits the specified expression.</summary>
    /// <param name="exp">The expression to be visited.</param>
    /// <returns>
    /// The expression that was returned by the specific visitor.
    /// </returns>
    protected override Expression Visit(Expression exp)
    {
      if (!this.BeforeVisit(exp))
        return exp;
      this.PrevuousExpression = this.CurrentExpression;
      this.CurrentExpression = exp;
      return base.Visit(exp);
    }

    protected virtual IList<StringBuilder> this[string segmentName]
    {
      get
      {
        if (string.IsNullOrEmpty(segmentName))
          segmentName = this.DefaultSegmentName;
        IList<StringBuilder> stringBuilderList;
        if (!this.segments.Contains((object) segmentName))
        {
          stringBuilderList = (IList<StringBuilder>) new List<StringBuilder>();
          stringBuilderList.Add(new StringBuilder());
          this.segments.Add((object) segmentName, (object) stringBuilderList);
        }
        else
          stringBuilderList = (IList<StringBuilder>) this.segments[(object) segmentName];
        return stringBuilderList;
      }
    }

    protected virtual Expression CurrentExpression { get; private set; }

    protected virtual Expression PrevuousExpression { get; private set; }

    protected virtual string DefaultSegmentName => "@Default@";

    protected virtual string CurrentSegmentName { get; set; }

    /// <summary>Index of the current segment's string builder</summary>
    protected virtual int CurrentSegmentIndex { get; set; }

    /// <summary>
    /// Determines the current segment's string builder index orientation
    /// </summary>
    protected virtual SegmentOrientation CurrentSegmentOrientation { get; set; }

    /// <summary>For testing: call the base implementation of Visit</summary>
    /// <param name="expression">Expression to visit</param>
    /// <returns>Result of the visit</returns>
    protected internal virtual Expression CallBaseVisit(Expression expression) => base.Visit(expression);

    /// <summary>
    /// For testing: call <paramref name="builder" /><c>.ToString()</c>
    /// </summary>
    /// <param name="builder">Builder whose text to return</param>
    /// <returns>Text of the builder</returns>
    protected internal virtual string GetBuilderText(StringBuilder builder) => builder.ToString();

    /// <summary>
    /// Change current segment name to <paramref name="newSegmentName" />,
    /// and if put in a <c>using</c>, will switch back to the previous mode
    /// </summary>
    /// <param name="newSegmentName">Segment name to switch to</param>
    /// <returns>
    /// Object, whose <c>Dispose</c> method switches to the mode that was
    /// prior to this method call
    /// </returns>
    protected virtual IDisposable PushSegment(string newSegmentName) => (IDisposable) new SegmentedExpressionVisitor.SegmentSwitch(this, newSegmentName);

    /// <summary>
    /// Optional callback, that, when overridden, can cancel a visit
    /// </summary>
    /// <param name="expression">Expression that is about to be visited</param>
    /// <returns><c>true</c> to proceed with the visit, <c>valse</c> to cancel it</returns>
    protected virtual bool BeforeVisit(Expression expression) => true;

    /// <summary>
    /// Append <paramref name="expression" /> to the current segment's string builder
    /// </summary>
    /// <param name="expression">String to append</param>
    protected virtual void Append(string expression) => this.ExpressionBuilder.Append(expression);

    /// <summary>
    /// Appends all builders of a segment to the current string builder
    /// </summary>
    /// <param name="segmentName">Name of the segment whose builders to append</param>
    protected virtual void AppendSegment(string segmentName)
    {
      foreach (StringBuilder builder in (IEnumerable<StringBuilder>) this[segmentName])
        this.Append(this.GetBuilderText(builder));
    }

    /// <summary>
    /// Append an <paramref name="expression" />, and optionally, <paramref name="more" />
    /// expressions, to the current segment's string builder
    /// </summary>
    /// <param name="expression">Expression to append</param>
    /// <param name="more">More optional expressions to append</param>
    protected virtual void Append(string expression, params string[] more)
    {
      this.Append(expression);
      foreach (string expression1 in more)
        this.Append(expression1);
    }

    /// <summary>
    /// Append several expressions to the current segment's string builder
    /// and put a space between them
    /// </summary>
    /// <param name="expressions">List of expressions to add</param>
    protected virtual void AppendWithSpace(params string[] expressions)
    {
      for (int index = 0; index < expressions.Length; ++index)
      {
        this.Append(expressions[index]);
        if (index + 1 == expressions.Length)
          this.Append(" ");
      }
    }

    /// <summary>
    /// Append several expressions to the current segment's string builder
    /// and put a space before, between and after them
    /// </summary>
    /// <param name="expressions">List of expressions to append</param>
    protected virtual void AppendAndWrapWithSpace(params string[] expressions)
    {
      if (expressions.Length != 0)
        this.Append(" ");
      this.AppendAndSuffixWithSpace(expressions);
    }

    /// <summary>
    /// Append several expressions to the current segment's string builder
    /// and put a space between and after them
    /// </summary>
    /// <param name="expressions">List of expressions to add</param>
    protected virtual void AppendAndSuffixWithSpace(params string[] expressions)
    {
      foreach (string expression in expressions)
      {
        this.Append(expression);
        this.Append(" ");
      }
    }

    /// <summary>Remove a substring from the current string builder</summary>
    /// <param name="startIndex">Start removing from this index</param>
    /// <param name="length">Remove that much characters</param>
    /// <returns>Reference to the string builder after the removal</returns>
    protected virtual StringBuilder Remove(int startIndex, int length) => this.ExpressionBuilder.Remove(startIndex, length);

    /// <summary>
    /// Switches a visitor from one segment to another.
    /// When put in a <c>using</c> statement, it will automatically switch back to the
    /// previous segment.
    /// </summary>
    [DebuggerDisplay("SegmentSwitch: Current={visitor.CurrentSegmentName}, Previous={previousSegmentName}")]
    public class SegmentSwitch : IDisposable
    {
      private SegmentedExpressionVisitor visitor;
      private string previousSegmentName;

      /// <summary>
      /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.Linq.SegmentedExpressionVisitor.SegmentSwitch" /> class.
      /// </summary>
      /// <param name="visitor">Segmented visitor whose segments to switch.</param>
      /// <param name="segment">Segment name to temorarily switch to.</param>
      public SegmentSwitch(SegmentedExpressionVisitor visitor, string segment) => this.Initialize(visitor, segment);

      /// <summary>Initializes the switched</summary>
      /// <param name="visitor">Segmented visitor whose segments to switch.</param>
      /// <param name="segment">Segment name to temorarily switch to.</param>
      protected internal virtual void Initialize(SegmentedExpressionVisitor visitor, string segment)
      {
        this.visitor = visitor;
        this.previousSegmentName = visitor.CurrentSegmentName;
        this.visitor.CurrentSegmentName = segment;
      }

      /// <summary>Switches back to the previous segment</summary>
      public virtual void Dispose() => this.visitor.CurrentSegmentName = this.previousSegmentName;
    }
  }
}
