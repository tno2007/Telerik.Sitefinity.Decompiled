// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.BaseFacadeWithParent`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Fluent
{
  /// <summary>
  /// Base class for facades that can be hosted by other facades
  /// </summary>
  /// <typeparam name="TParentFacade">Type of the facade that hosts this facade</typeparam>
  public abstract class BaseFacadeWithParent<TParentFacade> : BaseFacadeWithManager
    where TParentFacade : BaseFacade
  {
    private TParentFacade parentFacade;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.BaseFacadeWithParent`1" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    public BaseFacadeWithParent(AppSettings settings)
      : base(settings)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.BaseFacadeWithParent`1" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <param name="parentFacade">The parent facade. Can be null</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    public BaseFacadeWithParent(AppSettings settings, TParentFacade parentFacade)
      : this(settings)
    {
      this.parentFacade = parentFacade;
    }

    /// <summary>Return to parent facade</summary>
    /// <returns>Parant facade</returns>
    /// <exception cref="T:System.InvalidOperationException">If no parent facade was specified in the constructor</exception>
    public virtual TParentFacade Done()
    {
      if ((object) this.parentFacade == null && typeof (TParentFacade) == typeof (BaseFacade))
        return Activator.CreateInstance<TParentFacade>();
      FacadeHelper.AssertNotNull<TParentFacade>(this.parentFacade, "Parrent facade can not be null when you call Done()");
      return this.parentFacade;
    }
  }
}
