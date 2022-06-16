// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.ContentOutPipeDescriptionProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Publishing.Model;

namespace Telerik.Sitefinity.Publishing
{
  internal class ContentOutPipeDescriptionProvider : IPipeDescriptionProvider
  {
    public bool GetPipeSettingsShortDescription(PipeSettings pipeSettings, out string description) => this.BuildPipeDescription(pipeSettings, out description);

    private bool BuildPipeDescription(PipeSettings pipeSettings, out string description)
    {
      if (!(pipeSettings is SitefinityContentPipeSettings contentPipeSettings) || contentPipeSettings.ContentTypeName != typeof (ContentItem).FullName)
      {
        description = (string) null;
        return false;
      }
      PublishingMessages publishingMessages = Res.Get<PublishingMessages>();
      string str = string.Format("<strong>{0}</strong> ", (object) publishingMessages.Content);
      description = publishingMessages.PublishAs + " " + str;
      return true;
    }
  }
}
