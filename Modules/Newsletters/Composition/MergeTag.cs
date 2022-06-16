// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Composition.MergeTag
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Text.RegularExpressions;
using Telerik.Sitefinity.Newsletters.Model;

namespace Telerik.Sitefinity.Modules.Newsletters.Composition
{
  /// <summary>This class represents the merge tag.</summary>
  public class MergeTag
  {
    private static Regex regex = new Regex("\\{\\|([a-zA-Z0-9\\s_-]+?).([a-zA-Z0-9\\s_-]+?)\\|\\}", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Composition.MergeTag" /> type.
    /// </summary>
    /// <param name="title">Title of the merge tag that will be displayed to the users.</param>
    /// <param name="propertyName">Name of the property used to resolve the merge tag.</param>
    /// <param name="declaringTypeName">Name of the property declaring type used to resolve the merge tag.</param>
    public MergeTag(string title, string propertyName, string declaringTypeName)
    {
      this.Title = title;
      this.PropertyName = propertyName;
      this.DeclaringTypeName = declaringTypeName;
    }

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Composition.MergeTag" /> type.
    /// </summary>
    /// <param name="composedTag">The composed merge tag.</param>
    public MergeTag(string composedTag) => this.ParseMergeTag(composedTag);

    /// <summary>
    /// Gets the title of the merge tag that will be displayed to the users.
    /// </summary>
    public string Title { get; private set; }

    /// <summary>
    /// Gets the name of the property used to resolve the merge tag.
    /// </summary>
    public string PropertyName { get; private set; }

    /// <summary>
    /// Gets the name of the property declaring type used to resolve the merge tag.
    /// </summary>
    public string DeclaringTypeName { get; private set; }

    public string ComposedTag => "{|" + this.DeclaringTypeName + "." + this.PropertyName + "|}";

    public bool IsMapped(DynamicListSettings dynamicList) => dynamicList.FirstNameMappedField == this.ComposedTag || dynamicList.LastNameMappedField == this.ComposedTag || dynamicList.EmailMappedField == this.ComposedTag;

    private void ParseMergeTag(string composedTag)
    {
      Match match = MergeTag.regex.Match(composedTag);
      if (match.Success)
      {
        this.DeclaringTypeName = match.Groups[1].Value.Trim();
        this.PropertyName = match.Groups[2].Value.Trim();
      }
      else
      {
        this.PropertyName = composedTag;
        this.DeclaringTypeName = typeof (string).FullName;
      }
    }
  }
}
