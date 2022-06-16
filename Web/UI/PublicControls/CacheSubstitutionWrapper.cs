// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.PublicControls.CacheSubstitutionWrapper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.OutputCache;

namespace Telerik.Sitefinity.Web.UI.PublicControls
{
  /// <summary>
  /// Wrapper for cache substitution, used when the OutputCache is enabled on a page.
  /// </summary>
  public class CacheSubstitutionWrapper
  {
    /// <summary>The method name parameter</summary>
    private const string CacheSubstMethodNameParam = "SF_CSW_Method_Name";
    private const string RegistrationIndexKey = "OC_CSW_Registration_Order";
    private const string ProcessedMethodIndexKey = "OC_CSW_Processed_Method";
    private const string MethodTypeMethodNameDelimiter = "->";
    private const string MethodParametersPrefix = ":";

    /// <summary>
    /// Parameters for the substitution
    /// passed to the method called by the delegate
    /// </summary>
    public Dictionary<string, string> Parameters { get; private set; }

    /// <summary>
    /// Creates a new instance of <typeparamref name="CacheSubstitutionWrapper" />
    /// </summary>
    /// <param name="parameters"></param>
    /// <param name="renderMarkup"></param>
    public CacheSubstitutionWrapper(
      Dictionary<string, string> parameters,
      CacheSubstitutionWrapper.RenderMarkupDelegate renderMarkup)
    {
      if (parameters == null)
        throw new ArgumentNullException(nameof (parameters));
      if (renderMarkup == null)
        throw new ArgumentNullException(nameof (renderMarkup));
      int nextContextCounter = CacheSubstitutionWrapper.GetNextContextCounter("OC_CSW_Registration_Order");
      Dictionary<string, string> prefixedParameters = CacheSubstitutionWrapper.GeneratePrefixedParameters(parameters, nextContextCounter);
      prefixedParameters.Add(CacheSubstitutionWrapper.GetNextMethodNameKey(nextContextCounter), CacheSubstitutionWrapper.GetNextMethodNameValue(renderMarkup.Method));
      SitefinityOutputCacheProvider.RegisterCacheSubstitutionCallbackParameters(prefixedParameters);
      this.Parameters = parameters;
    }

    /// <summary>
    /// Register the callback method, that is going to be called when using cache substitution
    /// </summary>
    /// <param name="context"></param>
    public void RegisterPostCacheCallBack(HttpContext context)
    {
      HttpResponseSubstitutionCallback callback = new HttpResponseSubstitutionCallback(CacheSubstitutionWrapper.Render);
      context.Response.WriteSubstitution(callback);
    }

    /// <summary>
    /// Invoking the method, used for cache substitution, this actually renders the markup for the substituted control
    /// </summary>
    public static string Render(HttpContext context)
    {
      if (context == null)
        return string.Empty;
      try
      {
        Dictionary<string, string> callbackParameters = SitefinityOutputCacheProvider.GetSubstitutionCallbackParameters(context);
        if (callbackParameters != null)
        {
          int nextContextCounter = CacheSubstitutionWrapper.GetNextContextCounter("OC_CSW_Processed_Method");
          string methodFullName;
          if (callbackParameters.TryGetValue(CacheSubstitutionWrapper.GetNextMethodNameKey(nextContextCounter), out methodFullName))
          {
            MethodInfo nextMethod = CacheSubstitutionWrapper.GetNextMethod(methodFullName);
            if (nextMethod != (MethodInfo) null)
            {
              Dictionary<string, string> methodParameters = CacheSubstitutionWrapper.GetCurrentMethodParameters(callbackParameters, nextContextCounter);
              return nextMethod.Invoke((object) null, new object[1]
              {
                (object) methodParameters
              }).ToString();
            }
          }
        }
      }
      catch (Exception ex)
      {
        Log.Trace("Failed to execute a cache substitution: " + ex.Message);
        if (Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
          throw;
      }
      return string.Empty;
    }

    private static int GetNextContextCounter(string key)
    {
      int nextContextCounter = 0;
      HttpContext current = HttpContext.Current;
      if (current != null)
      {
        if (current.Items.Contains((object) key))
          nextContextCounter = (int) current.Items[(object) key];
        ++nextContextCounter;
        current.Items[(object) key] = (object) nextContextCounter;
      }
      return nextContextCounter;
    }

    private static string GetNextMethodNameKey(int index) => index.ToString((IFormatProvider) CultureInfo.InvariantCulture) + "SF_CSW_Method_Name";

    private static string GetNextMethodNameValue(MethodInfo methodInfo) => string.Format((IFormatProvider) CultureInfo.InvariantCulture, "{0}{1}{2}", (object) methodInfo.DeclaringType.FullName, (object) "->", (object) methodInfo.Name);

    private static MethodInfo GetNextMethod(string methodFullName)
    {
      MethodInfo nextMethod = (MethodInfo) null;
      if (string.IsNullOrWhiteSpace(methodFullName))
        return (MethodInfo) null;
      try
      {
        string[] source = methodFullName.Split(new string[1]
        {
          "->"
        }, 2, StringSplitOptions.RemoveEmptyEntries);
        if (((IEnumerable<string>) source).Count<string>() == 2)
        {
          string name = source[0];
          string methodName = source[1];
          Type type = TypeResolutionService.ResolveType(name);
          if (type != (Type) null)
            nextMethod = ((IEnumerable<MethodInfo>) type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)).FirstOrDefault<MethodInfo>((Func<MethodInfo, bool>) (x => x.Name == methodName && x.ReturnType == typeof (string) && ((IEnumerable<ParameterInfo>) x.GetParameters()).Any<ParameterInfo>() && typeof (Dictionary<string, string>).Equals(((IEnumerable<ParameterInfo>) x.GetParameters()).First<ParameterInfo>().ParameterType)));
        }
      }
      catch (Exception ex)
      {
        Log.Trace("Failed to find a cache substitution method information: " + ex.Message);
        if (Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
          throw;
      }
      return nextMethod;
    }

    private static Dictionary<string, string> GeneratePrefixedParameters(
      Dictionary<string, string> parameters,
      int nextSubstitutionIndex)
    {
      Dictionary<string, string> prefixedParameters = (Dictionary<string, string>) null;
      if (parameters != null && parameters.Count > 0)
      {
        string str = nextSubstitutionIndex.ToString((IFormatProvider) CultureInfo.InvariantCulture);
        prefixedParameters = new Dictionary<string, string>();
        foreach (KeyValuePair<string, string> parameter in parameters)
        {
          string key = string.Format((IFormatProvider) CultureInfo.InvariantCulture, "{0}{1}{2}", (object) str, (object) ":", (object) parameter.Key);
          prefixedParameters.Add(key, parameter.Value);
        }
      }
      return prefixedParameters;
    }

    private static Dictionary<string, string> GetCurrentMethodParameters(
      Dictionary<string, string> parameters,
      int index)
    {
      Dictionary<string, string> methodParameters = new Dictionary<string, string>();
      string str = index.ToString((IFormatProvider) CultureInfo.InvariantCulture) + ":";
      foreach (KeyValuePair<string, string> parameter in parameters)
      {
        if (parameter.Key.StartsWith(str))
        {
          string key = parameter.Key.Substring(str.Length);
          methodParameters.Add(key, parameter.Value);
        }
      }
      return methodParameters;
    }

    /// <summary>
    /// Delegate that should return string to be rendered when a control is substituted
    /// </summary>
    /// <param name="parameters">Parameters for the substitution</param>
    /// <returns>Html markup string</returns>
    public delegate string RenderMarkupDelegate(Dictionary<string, string> parameters);
  }
}
