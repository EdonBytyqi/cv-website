using DotNetCV.ViewModels;

namespace DotNetCV.Services;

/// <summary>
/// Provides the CV content.
/// </summary>
public interface ICvService
{
    /// <summary>
    /// Gets the complete CV, with every list already in display order.
    /// </summary>
    CvViewModel GetCv();
}
