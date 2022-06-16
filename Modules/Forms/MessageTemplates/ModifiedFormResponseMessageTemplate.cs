// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.MessageTemplates.ModifiedFormResponseMessageTemplate
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Reflection;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services.Notifications;

namespace Telerik.Sitefinity.Modules.Forms.MessageTemplates
{
  internal class ModifiedFormResponseMessageTemplate : FormActionMessageTemplate
  {
    public override IMessageTemplateRequest GetDefaultMessageTemplate() => (IMessageTemplateRequest) new MessageTemplateRequestProxy()
    {
      Subject = ("Modified form response - {|" + FormActionMessageTemplate.PlaceholderFields.FormTitle.FieldName + "|}"),
      BodyHtml = ControlTemplateResolver.ResolveTemplate("Telerik.Sitefinity.Modules.Forms.MessageTemplates.ModifiedFormResponseMessageTemplate.htm", Assembly.GetExecutingAssembly())
    };
  }
}
