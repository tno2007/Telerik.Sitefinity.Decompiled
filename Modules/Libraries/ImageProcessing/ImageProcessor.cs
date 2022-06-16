// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.ImageProcessing.ImageProcessor
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Libraries.Images;

namespace Telerik.Sitefinity.Modules.Libraries.ImageProcessing
{
  /// <summary>Sitefinity's default image generator.</summary>
  public class ImageProcessor : IImageProcessor
  {
    private readonly IDictionary<string, ImageProcessingMethod> methods = (IDictionary<string, ImageProcessingMethod>) new Dictionary<string, ImageProcessingMethod>();
    private bool initialized;

    /// <summary>Gets the supported methods for image processing.</summary>
    /// <value>The methods.</value>
    public IDictionary<string, ImageProcessingMethod> Methods
    {
      get
      {
        if (!this.initialized)
        {
          lock (this.methods)
          {
            if (!this.initialized)
            {
              MethodInfo[] methods = this.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public);
              string fullName = typeof (Image).FullName;
              foreach (MethodInfo method in methods)
              {
                if (((IEnumerable<object>) method.GetCustomAttributes(typeof (ImageProcessingMethodAttribute), true)).FirstOrDefault<object>() is ImageProcessingMethodAttribute methodAttr && method.ReturnType.FullName == fullName)
                {
                  ParameterInfo[] parameters = method.GetParameters();
                  int num = ((IEnumerable<ParameterInfo>) parameters).Count<ParameterInfo>();
                  if (num > 0 && num <= 2 && parameters[0].ParameterType.FullName == fullName)
                  {
                    Type optionsArgType = (Type) null;
                    if (num == 2)
                      optionsArgType = parameters[1].ParameterType;
                    ImageProcessingMethod processingMethod = this.CreateProcessingMethod(method, methodAttr, optionsArgType);
                    this.methods.Add(processingMethod.MethodKey, processingMethod);
                  }
                }
              }
              this.initialized = true;
            }
          }
        }
        return this.methods;
      }
    }

    /// <summary>
    /// Creates the processing method on ImageProcessor initialization.
    /// </summary>
    /// <param name="method">The method.</param>
    /// <param name="methodAttr">The method attr.</param>
    /// <param name="optionsArgType">Type of the options arg.</param>
    /// <returns></returns>
    protected virtual ImageProcessingMethod CreateProcessingMethod(
      MethodInfo method,
      ImageProcessingMethodAttribute methodAttr,
      Type optionsArgType)
    {
      return new ImageProcessingMethod((IImageProcessor) this, method, methodAttr, optionsArgType);
    }

    /// <summary>
    /// Generates a new image from the given Image using the given method and arguments.
    /// If no such method is found or the method fails - an exception is thrown.
    /// </summary>
    /// <param name="sourceImage">The source image.</param>
    /// <param name="methodName">The name of the used processing method that will be called.</param>
    /// <param name="methodArgument">If the image processing method supports argument you could pass it.</param>
    /// <returns>The newly generated image.</returns>
    /// <exception cref="T:Telerik.Sitefinity.Modules.Libraries.ImageProcessing.ImageProcessingException"></exception>
    public virtual Image ProcessImage(
      Image sourceImage,
      string methodName,
      object methodArgument)
    {
      ImageProcessingMethod processingMethod;
      if (this.Methods.TryGetValue(methodName, out processingMethod))
      {
        bool flag = false;
        Type[] types;
        if (processingMethod.ArgumentType != (Type) null)
        {
          types = new Type[2]
          {
            typeof (Image),
            processingMethod.ArgumentType
          };
          flag = true;
        }
        else
          types = new Type[1]{ typeof (Image) };
        MethodInfo method = this.GetType().GetMethod(processingMethod.MethodName, types);
        if (method != (MethodInfo) null)
        {
          object[] parameters;
          if (flag)
            parameters = new object[2]
            {
              (object) sourceImage,
              methodArgument
            };
          else
            parameters = new object[1]
            {
              (object) sourceImage
            };
          try
          {
            return (Image) method.Invoke((object) this, parameters);
          }
          catch (Exception ex)
          {
            throw new ImageProcessingException(ImageProcessingError.MethodGenerationError, string.Format("Method invocation failed: {0}", (object) ex.Message), ex);
          }
        }
      }
      throw new ImageProcessingException(ImageProcessingError.MethodNotFound, string.Format("No such method for image processing: {0}", (object) methodName));
    }

    /// <summary>
    /// Tries to resize the image by keeping the ratio between width and height and not overflowing the max limits specified.
    /// </summary>
    /// <param name="sourceImage"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    [ImageProcessingMethod(DescriptionImageResourceName = "Telerik.Sitefinity.Modules.Libraries.ImageProcessing.Resources.FitToAreaResize.png", DescriptionText = "Generated image will be resized to desired area", LabelFormat = "ResizeWithFitToAreaSizeFormat", ResourceClassId = "LibrariesResources", Title = "ResizeWithFitToAreaImageProcessorMethod", ValidateArgumentsMethodName = "ValidateFitToAreaArguments")]
    public virtual Image Resize(Image sourceImage, ImageProcessor.FitToAreaArguments args)
    {
      Image resizedImage;
      return ImagesHelper.TryResizeImage(sourceImage, args.MaxWidth, args.MaxHeight, !args.ScaleUp, out resizedImage, args.Quality) ? resizedImage : sourceImage;
    }

    /// <summary>
    /// Resize the side with smaller change and cut the other side from both sides (to preserve the center of the image).
    /// </summary>
    /// <param name="sourceImage">The source image.</param>
    /// <param name="args">The crop operation parameters.</param>
    /// <returns>The cropped image.</returns>
    [ImageProcessingMethod(DescriptionImageResourceName = "Telerik.Sitefinity.Modules.Libraries.ImageProcessing.Resources.CropToAreaResize.png", DescriptionText = "Generated image will be resized and cropped to desired area", LabelFormat = "CropToAreaSizeFormat", ResourceClassId = "LibrariesResources", Title = "CropToAreaImageProcessorMethod", ValidateArgumentsMethodName = "ValidateCropToAreaArguments")]
    public virtual Image Crop(Image sourceImage, ImageProcessor.CropArguments args)
    {
      Image resultImage;
      return ImagesHelper.TryCrop(sourceImage, args.Width, args.Height, out resultImage, !args.ScaleUp, args.Quality) ? resultImage : sourceImage;
    }

    /// <inheritdoc />
    [ImageProcessingMethod(LabelFormat = "ResizeWithFitToSideSizeFormat", ResourceClassId = "LibrariesResources", Title = "ResizeWithFitToSideImageProcessorMethod", ValidateArgumentsMethodName = "ValidateFitToSideArguments")]
    [Browsable(false)]
    public virtual Image Resize(Image sourceImage, FitToSideArguments args)
    {
      Image thumbnail;
      return ImagesHelper.TryResizeImage(sourceImage, args.Size, !args.ScaleUp, !args.ResizeBiggerSide, out thumbnail, args.Quality) ? thumbnail : sourceImage;
    }

    /// <summary>
    /// Validates arguments for Resize method that accepts parameter of type FitToAreaArguments
    /// </summary>
    /// <param name="argument"></param>
    protected virtual void ValidateFitToAreaArguments(object argument)
    {
      int num = argument is ImageProcessor.FitToAreaArguments fitToAreaArguments ? fitToAreaArguments.MaxWidth : throw new InvalidOperationException("Argument of type '{0}' is expected".Arrange((object) typeof (ImageProcessor.FitToAreaArguments).FullName));
      int maxHeight = fitToAreaArguments.MaxHeight;
      if (num <= 0 && maxHeight <= 0)
        throw new ArgumentException(Res.Get<LibrariesResources>().FitToAreaErrorMessage);
      if (num > 0)
        this.CheckParameterInRange(num, Res.Get<LibrariesResources>().MaxWidth);
      if (maxHeight <= 0)
        return;
      this.CheckParameterInRange(maxHeight, Res.Get<LibrariesResources>().MaxHeight);
    }

    /// <summary>
    /// Validates arguments for Resize method that accepts parameter of type FitToSideArguments
    /// </summary>
    /// <param name="argument"></param>
    protected virtual void ValidateFitToSideArguments(object argument)
    {
      if (!(argument is FitToSideArguments fitToSideArguments))
        throw new InvalidOperationException("Argument of type '{0}' is expected".Arrange((object) typeof (FitToSideArguments).FullName));
      this.CheckParameterInRange(fitToSideArguments.Size, Res.Get<LibrariesResources>().SizeLabel);
    }

    /// <summary>
    /// Validates arguments for Crop method that accepts parameter of type CropArguments
    /// </summary>
    /// <param name="argument"></param>
    protected virtual void ValidateCropToAreaArguments(object argument)
    {
      int num = argument is ImageProcessor.CropArguments cropArguments ? cropArguments.Width : throw new InvalidOperationException("Argument of type '{0}' is expected".Arrange((object) typeof (ImageProcessor.CropArguments).FullName));
      int height = cropArguments.Height;
      this.CheckParameterInRange(num, Res.Get<LibrariesResources>().Width);
      this.CheckParameterInRange(height, Res.Get<LibrariesResources>().Height);
    }

    private void CheckParameterInRange(int value, string parameterName)
    {
      if (value < 1 || value > 9999)
        throw new ArgumentException(Res.Get<LibrariesResources>().MethodParameterErrorMessage.Arrange((object) parameterName));
    }

    /// <summary>
    /// Defines the Crop method argument for specifying the desired cropping options.
    /// </summary>
    public class CropArguments
    {
      /// <summary>
      /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.ImageProcessing.ImageProcessor.CropArguments" /> class.
      /// </summary>
      public CropArguments() => this.Quality = ImageQuality.Medium;

      /// <summary>
      /// Gets or sets the desired width of the transformed image.
      /// </summary>
      /// <value>The desired width.</value>
      [ImageProcessingProperty(IsRequired = true, RegularExpression = "^[1-9][0-9]{0,3}$", RegularExpressionViolationMessage = "ValueMustBeInteger", ResourceClassId = "LibrariesResources", Units = "Pixels")]
      public int Width { get; set; }

      /// <summary>
      /// Gets or sets the desired height of the transformed image.
      /// </summary>
      /// <value>The desired height.</value>
      [ImageProcessingProperty(IsRequired = true, RegularExpression = "^[1-9][0-9]{0,3}$", RegularExpressionViolationMessage = "ValueMustBeInteger", ResourceClassId = "LibrariesResources", Units = "Pixels")]
      public int Height { get; set; }

      /// <summary>Determines if the method could return bigger images.</summary>
      /// <value>If set to true the method could return bigger images.</value>
      [ImageProcessingProperty(ResourceClassId = "LibrariesResources", Title = "ScaleUp")]
      public bool ScaleUp { get; set; }

      /// <summary>Gets or sets the quality of the transformation.</summary>
      /// <value>The quality.</value>
      [ImageProcessingProperty]
      public ImageQuality Quality { get; set; }
    }

    /// <summary>
    /// Defines the Resize (fit-to-area) method argument for specifying the desired resizing options.
    /// </summary>
    public class FitToAreaArguments
    {
      /// <summary>
      /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.ImageProcessing.ImageProcessor.FitToAreaArguments" /> class.
      /// </summary>
      public FitToAreaArguments() => this.Quality = ImageQuality.Medium;

      /// <summary>
      /// Gets or sets the maximum allowed width of the transformed image.
      /// </summary>
      /// <value>The maximum width.</value>
      [ImageProcessingProperty(RegularExpression = "^[1-9]?[0-9]{0,3}$", RegularExpressionViolationMessage = "ValueMustBeInteger", ResourceClassId = "LibrariesResources", Title = "MaxWidth", Units = "Pixels")]
      public int MaxWidth { get; set; }

      /// <summary>
      /// Gets or sets the maximum allowed height of the transformed image.
      /// </summary>
      /// <value>The maximum height.</value>
      [ImageProcessingProperty(RegularExpression = "^[1-9]?[0-9]{0,3}$", RegularExpressionViolationMessage = "ValueMustBeInteger", ResourceClassId = "LibrariesResources", Title = "MaxHeight", Units = "Pixels")]
      public int MaxHeight { get; set; }

      /// <summary>Determines if the method could return bigger images.</summary>
      /// <value>If set to true the method could return bigger images.</value>
      [ImageProcessingProperty(ResourceClassId = "LibrariesResources", Title = "ScaleUp")]
      public bool ScaleUp { get; set; }

      /// <summary>Gets or sets the quality of the transformation.</summary>
      /// <value>The quality.</value>
      [ImageProcessingProperty]
      public ImageQuality Quality { get; set; }
    }
  }
}
