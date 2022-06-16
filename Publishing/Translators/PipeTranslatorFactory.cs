// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Translators.PipeTranslatorFactory
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Publishing.Translators
{
  /// <summary>Lightweight factory for managing translators</summary>
  public static class PipeTranslatorFactory
  {
    /// <summary>Register a translator by its name</summary>
    /// <param name="translator">Translator to register</param>
    /// <exception cref="T:System.ArgumentNullException">if <paramref name="translator" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">If <paramref name="translator" />'s name is null or empty</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">IF there is another translator register with the same name.</exception>
    public static void RegisterTranslator(TranslatorBase translator) => PublishingSystemFactory.RegisterTranslator(translator);

    /// <summary>Look up a translator by its name</summary>
    /// <param name="name">Name of the translator</param>
    /// <returns>Translator, if it was registered by this name, or null if it weren't</returns>
    public static TranslatorBase ResolveTranslator(string name) => PublishingSystemFactory.ResolveTranslator(name);
  }
}
