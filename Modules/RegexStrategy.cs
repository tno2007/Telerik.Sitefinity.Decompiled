// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.RegexStrategy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Modules
{
  /// <summary>Base class for defining regex constants.</summary>
  public class RegexStrategy
  {
    /// <summary>Gets the unicode categories.</summary>
    /// <value>The unicode categories.</value>
    public virtual string UnicodeCategories => "\\p{L}";

    /// <summary>Gets the allowed special symbols.</summary>
    /// <value>The allowed special symbols.</value>
    public virtual string AllowedSpecialSymbols => "\\–\\'’…„“”\"\\:";

    /// <summary>Gets the URL regex base.</summary>
    /// <value>The URL regex base.</value>
    public virtual string UrlRegExBase => this.UnicodeCategories + "\\-\\!\\$\\(\\)\\=\\@\\d_\\'\\.";

    /// <summary>Gets the SEO regex base.</summary>
    /// <value>The SEO regex base.</value>
    public virtual string SeoRegExBase => this.UnicodeCategories + "\\p{S}\\™\\®\\©\\s\\-\\!\\$\\(\\)\\=\\@\\d_\\'\\.\\&\\|\\,\\/\\+\\#\\>\\<";

    /// <summary>Gets the default expression filter.</summary>
    /// <value>The default expression filter.</value>
    public virtual string DefaultExpressionFilter => "[^" + this.UnicodeCategories + "\\-\\!\\$\\'\\(\\)\\*\\=\\@\\d]+";

    /// <summary>Gets the default validation expression.</summary>
    /// <value>The default validation expression.</value>
    public virtual string DefaultValidationExpression => "^[" + this.UnicodeCategories + "\\-\\!\\$\\'\\(\\)\\=\\@\\d]+$";
  }
}
