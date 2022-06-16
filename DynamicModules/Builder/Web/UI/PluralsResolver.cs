// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Web.UI.PluralsResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;

namespace Telerik.Sitefinity.DynamicModules.Builder.Web.UI
{
  /// <summary>Convert words to and from singulars/plurals</summary>
  public sealed class PluralsResolver : IPluralsResolver
  {
    /// <summary>Store irregular plurals in a dictionary</summary>
    private static readonly Dictionary<string, string> _dictionary = new Dictionary<string, string>();
    /// <summary>The singleton instance</summary>
    private static readonly PluralsResolver _instance = new PluralsResolver();

    /// <summary>
    /// Get an instance of the structure singleton. This effectively caches the dictionary
    /// </summary>
    public static PluralsResolver Instance => PluralsResolver._instance;

    /// <summary>Run initialization on this singleton class</summary>
    private PluralsResolver() => PluralsResolver.Initialize();

    private static void Initialize()
    {
      PluralsResolver._dictionary.Add("afterlife", "afterlives");
      PluralsResolver._dictionary.Add("alga", "algae");
      PluralsResolver._dictionary.Add("alumna", "alumnae");
      PluralsResolver._dictionary.Add("alumnus", "alumni");
      PluralsResolver._dictionary.Add("analysis", "analyses");
      PluralsResolver._dictionary.Add("antenna", "antennae");
      PluralsResolver._dictionary.Add("appendix", "appendices");
      PluralsResolver._dictionary.Add("axis", "axes");
      PluralsResolver._dictionary.Add("bacillus", "bacilli");
      PluralsResolver._dictionary.Add("basis", "bases");
      PluralsResolver._dictionary.Add("Bedouin", "Bedouin");
      PluralsResolver._dictionary.Add("cactus", "cacti");
      PluralsResolver._dictionary.Add("calf", "calves");
      PluralsResolver._dictionary.Add("cherub", "cherubim");
      PluralsResolver._dictionary.Add("child", "children");
      PluralsResolver._dictionary.Add("cod", "cod");
      PluralsResolver._dictionary.Add("cookie", "cookies");
      PluralsResolver._dictionary.Add("criterion", "criteria");
      PluralsResolver._dictionary.Add("curriculum", "curricula");
      PluralsResolver._dictionary.Add("datum", "data");
      PluralsResolver._dictionary.Add("deer", "deer");
      PluralsResolver._dictionary.Add("diagnosis", "diagnoses");
      PluralsResolver._dictionary.Add("die", "dice");
      PluralsResolver._dictionary.Add("dormouse", "dormice");
      PluralsResolver._dictionary.Add("elf", "elves");
      PluralsResolver._dictionary.Add("elk", "elk");
      PluralsResolver._dictionary.Add("erratum", "errata");
      PluralsResolver._dictionary.Add("esophagus", "esophagi");
      PluralsResolver._dictionary.Add("fauna", "faunae");
      PluralsResolver._dictionary.Add("fish", "fish");
      PluralsResolver._dictionary.Add("flora", "florae");
      PluralsResolver._dictionary.Add("focus", "foci");
      PluralsResolver._dictionary.Add("foot", "feet");
      PluralsResolver._dictionary.Add("formula", "formulae");
      PluralsResolver._dictionary.Add("fundus", "fundi");
      PluralsResolver._dictionary.Add("fungus", "fungi");
      PluralsResolver._dictionary.Add("genie", "genii");
      PluralsResolver._dictionary.Add("genus", "genera");
      PluralsResolver._dictionary.Add("goose", "geese");
      PluralsResolver._dictionary.Add("grouse", "grouse");
      PluralsResolver._dictionary.Add("hake", "hake");
      PluralsResolver._dictionary.Add("half", "halves");
      PluralsResolver._dictionary.Add("headquarters", "headquarters");
      PluralsResolver._dictionary.Add("hippo", "hippos");
      PluralsResolver._dictionary.Add("hippopotamus", "hippopotami");
      PluralsResolver._dictionary.Add("hoof", "hooves");
      PluralsResolver._dictionary.Add("housewife", "housewives");
      PluralsResolver._dictionary.Add("hypothesis", "hypotheses");
      PluralsResolver._dictionary.Add("index", "indices");
      PluralsResolver._dictionary.Add("jackknife", "jackknives");
      PluralsResolver._dictionary.Add("knife", "knives");
      PluralsResolver._dictionary.Add("labium", "labia");
      PluralsResolver._dictionary.Add("larva", "larvae");
      PluralsResolver._dictionary.Add("leaf", "leaves");
      PluralsResolver._dictionary.Add("life", "lives");
      PluralsResolver._dictionary.Add("loaf", "loaves");
      PluralsResolver._dictionary.Add("louse", "lice");
      PluralsResolver._dictionary.Add("magus", "magi");
      PluralsResolver._dictionary.Add("man", "men");
      PluralsResolver._dictionary.Add("memorandum", "memoranda");
      PluralsResolver._dictionary.Add("midwife", "midwives");
      PluralsResolver._dictionary.Add("millennium", "millennia");
      PluralsResolver._dictionary.Add("moose", "moose");
      PluralsResolver._dictionary.Add("mouse", "mice");
      PluralsResolver._dictionary.Add("nebula", "nebulae");
      PluralsResolver._dictionary.Add("neurosis", "neuroses");
      PluralsResolver._dictionary.Add("nova", "novas");
      PluralsResolver._dictionary.Add("nucleus", "nuclei");
      PluralsResolver._dictionary.Add("oesophagus", "oesophagi");
      PluralsResolver._dictionary.Add("offspring", "offspring");
      PluralsResolver._dictionary.Add("ovum", "ova");
      PluralsResolver._dictionary.Add("ox", "oxen");
      PluralsResolver._dictionary.Add("papyrus", "papyri");
      PluralsResolver._dictionary.Add("passerby", "passersby");
      PluralsResolver._dictionary.Add("penknife", "penknives");
      PluralsResolver._dictionary.Add("person", "people");
      PluralsResolver._dictionary.Add("phenomenon", "phenomena");
      PluralsResolver._dictionary.Add("placenta", "placentae");
      PluralsResolver._dictionary.Add("pocketknife", "pocketknives");
      PluralsResolver._dictionary.Add("pupa", "pupae");
      PluralsResolver._dictionary.Add("radius", "radii");
      PluralsResolver._dictionary.Add("reindeer", "reindeer");
      PluralsResolver._dictionary.Add("retina", "retinae");
      PluralsResolver._dictionary.Add("rhinoceros", "rhinoceros");
      PluralsResolver._dictionary.Add("roe", "roe");
      PluralsResolver._dictionary.Add("salmon", "salmon");
      PluralsResolver._dictionary.Add("scarf", "scarves");
      PluralsResolver._dictionary.Add("self", "selves");
      PluralsResolver._dictionary.Add("seraph", "seraphim");
      PluralsResolver._dictionary.Add("series", "series");
      PluralsResolver._dictionary.Add("sheaf", "sheaves");
      PluralsResolver._dictionary.Add("sheep", "sheep");
      PluralsResolver._dictionary.Add("shelf", "shelves");
      PluralsResolver._dictionary.Add("species", "species");
      PluralsResolver._dictionary.Add("spectrum", "spectra");
      PluralsResolver._dictionary.Add("stimulus", "stimuli");
      PluralsResolver._dictionary.Add("stratum", "strata");
      PluralsResolver._dictionary.Add("supernova", "supernovas");
      PluralsResolver._dictionary.Add("swine", "swine");
      PluralsResolver._dictionary.Add("terminus", "termini");
      PluralsResolver._dictionary.Add("thesaurus", "thesauri");
      PluralsResolver._dictionary.Add("thesis", "theses");
      PluralsResolver._dictionary.Add("thief", "thieves");
      PluralsResolver._dictionary.Add("trout", "trout");
      PluralsResolver._dictionary.Add("vulva", "vulvae");
      PluralsResolver._dictionary.Add("wife", "wives");
      PluralsResolver._dictionary.Add("wildebeest", "wildebeest");
      PluralsResolver._dictionary.Add("wolf", "wolves");
      PluralsResolver._dictionary.Add("woman", "women");
      PluralsResolver._dictionary.Add("yen", "yen");
    }

    /// <summary>
    /// Call this method to get the properly pluralized
    /// English version of the word.
    /// </summary>
    /// <param name="word">The word needing conditional pluralization.</param>
    /// <param name="count">The number of items the word refers to.</param>
    /// <returns>The pluralized word</returns>
    public string ToPlural(string word)
    {
      if (this.IsPlural(word))
        return word;
      if (PluralsResolver._dictionary.ContainsKey(word.ToLower()))
      {
        string plural = PluralsResolver._dictionary[word.ToLower()];
        if (word.Length > 0 && char.IsUpper(word[0]))
          plural = plural.UpperFirstLetter();
        return plural;
      }
      if (word.Length <= 2)
        return word;
      switch (word.Substring(word.Length - 2))
      {
        case "by":
        case "cy":
        case "dy":
        case "fy":
        case "gy":
        case "hy":
        case "jy":
        case "ky":
        case "ly":
        case "my":
        case "ny":
        case "py":
        case "ry":
        case "sy":
        case "ty":
        case "vy":
        case "wy":
        case "xy":
        case "zy":
          return word.Substring(0, word.Length - 1) + "ies";
        case "ch":
        case "sh":
          return word + "es";
        case "is":
          return word.Substring(0, word.Length - 1) + "es";
        default:
          string str = word.Substring(word.Length - 1);
          return str == "s" || str == "z" || str == "x" ? word + "es" : word + "s";
      }
    }

    /// <summary>
    /// Call this method to get the singular
    /// version of a plural English word.
    /// </summary>
    /// <param name="word">The word to turn into a singular</param>
    /// <returns>The singular word</returns>
    public string ToSingular(string word)
    {
      word = word.ToLower();
      if (PluralsResolver._dictionary.ContainsValue(word))
      {
        foreach (KeyValuePair<string, string> keyValuePair in PluralsResolver._dictionary)
        {
          if (keyValuePair.Value.ToLower() == word)
            return keyValuePair.Key;
        }
      }
      if (word.Substring(word.Length - 1) != "s" || word.Length <= 2)
        return word;
      if (word.Length >= 4)
      {
        switch (word.Substring(word.Length - 4))
        {
          case "bies":
          case "cies":
          case "dies":
          case "fies":
          case "gies":
          case "hies":
          case "jies":
          case "kies":
          case "lies":
          case "mies":
          case "nies":
          case "pies":
          case "ries":
          case "sies":
          case "ties":
          case "vies":
          case "wies":
          case "xies":
          case "zies":
            return word.Substring(0, word.Length - 3) + "y";
          case "ches":
          case "shes":
            return word.Substring(0, word.Length - 2);
        }
      }
      if (word.Length >= 3)
      {
        string str = word.Substring(word.Length - 3);
        if (str == "ses" || str == "zes" || str == "xes")
          return word.Substring(0, word.Length - 2);
      }
      if (word.Length < 3)
        return word;
      return word.Substring(word.Length - 2) == "es" ? word.Substring(0, word.Length - 1) + "is" : word.Substring(0, word.Length - 1);
    }

    /// <summary>test if a word is plural</summary>
    /// <param name="word">word to test</param>
    /// <returns>true if a word is plural</returns>
    public bool IsPlural(string word)
    {
      word = word.ToLower();
      if (word.Length <= 2)
        return false;
      if (PluralsResolver._dictionary.ContainsValue(word.ToLower()))
        return true;
      if (word.Length >= 4)
      {
        switch (word.Substring(word.Length - 4))
        {
          case "bies":
          case "ches":
          case "cies":
          case "dies":
          case "fies":
          case "gies":
          case "hies":
          case "jies":
          case "kies":
          case "lies":
          case "mies":
          case "nies":
          case "pies":
          case "ries":
          case "shes":
          case "sies":
          case "ties":
          case "vies":
          case "wies":
          case "xies":
          case "zies":
            return true;
        }
      }
      if (word.Length >= 3)
      {
        string str = word.Substring(word.Length - 3);
        if (str == "ses" || str == "zes" || str == "xes")
          return true;
      }
      return word.Length >= 3 && word.Substring(word.Length - 2) == "es" || !(word.Substring(word.Length - 1) != "s");
    }
  }
}
