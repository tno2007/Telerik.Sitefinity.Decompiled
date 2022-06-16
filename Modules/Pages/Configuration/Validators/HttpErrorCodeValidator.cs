// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Configuration.Validators.HttpErrorCodeValidator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.CustomErrorPages;

namespace Telerik.Sitefinity.Modules.Pages.Configuration.Validators
{
  internal class HttpErrorCodeValidator : ConfigurationValidatorBase
  {
    public override bool CanValidate(Type type) => type.IsAssignableFrom(typeof (string));

    public override void Validate(object value)
    {
      int result = -1;
      if ((!int.TryParse(value.ToString(), out result) || !Enum.IsDefined(typeof (HttpErrorCode), (object) result)) && !value.ToString().Equals("Default", StringComparison.OrdinalIgnoreCase))
        throw new ArgumentException(Res.Get<ConfigDescriptions>().InvalidErrorCode);
    }
  }
}
