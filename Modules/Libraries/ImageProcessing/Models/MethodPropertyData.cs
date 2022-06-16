// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.ImageProcessing.Models.MethodPropertyData
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;

namespace Telerik.Sitefinity.Modules.Libraries.ImageProcessing.Models
{
  public class MethodPropertyData
  {
    public string Name { get; set; }

    public string Title { get; set; }

    public string PropertyType { get; set; }

    public string PropertyBaseType { get; set; }

    public List<string> Choices { get; set; }

    public string RegularExpression { get; set; }

    public string RegularExpressionValidationMessage { get; set; }

    public bool IsRequired { get; set; }
  }
}
