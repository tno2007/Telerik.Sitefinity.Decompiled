// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Versioning.ColumnProviders.MainVersionHistoryColumnProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.Versioning.ColumnProviders
{
  internal class MainVersionHistoryColumnProvider : IVersionHistoryColumnProvider
  {
    public IEnumerable<VersionHistoryColumn> GetColumns(
      Type itemType,
      object item)
    {
      List<VersionHistoryColumn> columns = new List<VersionHistoryColumn>();
      string str1 = "Version";
      string str2 = "ChangeDescription";
      columns.Add(new VersionHistoryColumn()
      {
        Field = str1,
        Title = Res.Get<Labels>().Version,
        Template = "<div class=\"sfItemVersionStatus\r\n                                #if (IsLastPublishedVersion) {# sfpublished #}#\">\r\n                                <a href=\"\" data-command-name=\"edit\" class=\"sfItemTitle\">#: " + str1 + " #</a>\r\n                                <div class=\"sfStatusLocation\">#: " + str2 + " #</div>\r\n                                </div>",
        CssClass = "sfTitleCol",
        HeaderCssClass = "sfTitleCol",
        Ordinal = 1f
      });
      string str3 = "DateCreated";
      columns.Add(new VersionHistoryColumn()
      {
        Field = str3,
        Title = Res.Get<Labels>().CreatedOn,
        Template = "<span>#: " + str3 + ".sitefinityLocaleFormat('dd MMM, yyyy')# <br> #:" + str3 + ".sitefinityLocaleFormat('hh:mm tt') #</span>",
        CssClass = "sfShort",
        HeaderCssClass = "sfShort",
        Ordinal = 2f
      });
      string str4 = "CreatedByUserName";
      columns.Add(new VersionHistoryColumn()
      {
        Field = str4,
        Title = Res.Get<Labels>().CreatedBy,
        Template = "<span>#: " + str4 + " #</span>",
        CssClass = "sfRegular",
        HeaderCssClass = "sfRegular",
        Ordinal = 3f
      });
      string str5 = "Comment";
      columns.Add(new VersionHistoryColumn()
      {
        Field = str5,
        Title = Res.Get<Labels>().Notes,
        Template = "<div data-field=\"noteContent\">#: " + str5 + " #</div>\r\n                            #if (isCommentButtonVisible(" + str5 + ", \"write\")) { # <a href=\" id=\"btnNoteWrite\" data-command-name=\"editNote\" class=\"sfEditItemNote\">" + Res.Get<VersionResources>().WriteNoteLabel + "</a> #}#\r\n                            #if (isCommentButtonVisible(" + str5 + ", \"edit\")) { # <a href=\" id=\"btnNoteEdit\" data-command-name=\"editNote\" class=\"sfEditItemNote\">" + Res.Get<VersionResources>().EditNoteLabel + "</a> #}#\r\n                            #if (isCommentButtonVisible(" + str5 + ", \"delete\")) { # <a href=\" id=\"btnNoteDelete\" data-command-name=\"deleteNote\" class=\"sfDeleteItemNote\">" + Res.Get<VersionResources>().DeleteNoteLabel + "</a> #}#",
        CssClass = "sfRegular sfItemNote",
        HeaderCssClass = "sfRegular sfItemNote",
        Ordinal = 4f
      });
      return (IEnumerable<VersionHistoryColumn>) columns;
    }
  }
}
