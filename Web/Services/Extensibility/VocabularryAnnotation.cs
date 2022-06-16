// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Extensibility.VocabularyAnnotation
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Web.Services.Extensibility
{
  /// <summary>A DTO that holds the vocabulary annotation value.</summary>
  public class VocabularyAnnotation : Annotation
  {
    private string name;
    private string nameSpace;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.Services.Extensibility.VocabularyAnnotation" /> class.
    /// </summary>
    /// <param name="nameSpace">The namespace of the annotation.</param>
    /// <param name="name">The name of the annotation.</param>
    /// <param name="val">The value.</param>
    public VocabularyAnnotation(string nameSpace, string name, object val)
      : base(val)
    {
      this.name = name;
      this.nameSpace = nameSpace;
    }

    /// <summary>Gets the namespace.</summary>
    public string Namespace => this.nameSpace;

    /// <summary>Gets the name.</summary>
    public string Name => this.name;
  }
}
