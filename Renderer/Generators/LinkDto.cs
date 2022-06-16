// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Renderer.Generators.LinkDto
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Renderer.Generators
{
  [DataContract]
  internal class LinkDto
  {
    /// <summary>Gets or sets the href of the link.</summary>
    [DataMember(Name = "href")]
    public string Href { get; set; }

    /// <summary>Gets or sets the sfRef for the link.</summary>
    /// <remarks>This is used for resolving the item in Sitefinity if its URL has been changed.</remarks>
    [DataMember(Name = "sfref")]
    public string Sfref { get; set; }
  }
}
