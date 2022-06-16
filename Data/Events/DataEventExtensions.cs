// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Events.DataEventExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Globalization;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Data.Events
{
  public static class DataEventExtensions
  {
    /// <summary>
    /// Returns the value of the <c>Language</c> property of the event if it implements <see cref="T:Telerik.Sitefinity.Data.Events.IMultilingualEvent" /> and <c>null</c> otherwise.
    /// </summary>
    public static string GetLanguage(this IEvent @event) => @event is IMultilingualEvent multilingualEvent ? multilingualEvent.Language : (string) null;

    /// <summary>
    /// Returns the value of the <c>Language</c> property of the event if it implements <see cref="T:Telerik.Sitefinity.Data.Events.IMultilingualEvent" /> and the name of the current UI culture otherwise.
    /// </summary>
    public static string GetLanguageOrCurrent(this IEvent @event) => @event is IMultilingualEvent multilingualEvent && multilingualEvent.Language != null ? multilingualEvent.Language : SystemManager.CurrentContext.Culture.Name;

    /// <summary>
    /// Returns a <see cref="T:System.Globalization.CultureInfo" /> object corresponding to the value of the <c>Language</c> property of the event,
    /// if it implements <see cref="T:Telerik.Sitefinity.Data.Events.IMultilingualEvent" /> and the value is not <c>null</c> and <c>null</c> otherwise.
    /// </summary>
    public static CultureInfo GetCulture(this IEvent @event)
    {
      string language = @event.GetLanguage();
      return language != null ? CultureInfo.GetCultureInfo(language) : (CultureInfo) null;
    }

    /// <summary>
    /// Returns a <see cref="T:System.Globalization.CultureInfo" /> object corresponding to the value of the <c>Language</c> property of the event,
    /// if it implements <see cref="T:Telerik.Sitefinity.Data.Events.IMultilingualEvent" /> and the value is not <c>null</c> and the the current UI culture otherwise.
    /// </summary>
    public static CultureInfo GetCultureOrCurrent(this IEvent @event)
    {
      string language = @event.GetLanguage();
      return language != null ? CultureInfo.GetCultureInfo(language) : SystemManager.CurrentContext.Culture;
    }

    internal static void SetLifecylceStatus(this IEvent @event, string status)
    {
      if (!(@event is ILifecycleEvent lifecycleEvent))
        return;
      lifecycleEvent.Status = status;
    }
  }
}
