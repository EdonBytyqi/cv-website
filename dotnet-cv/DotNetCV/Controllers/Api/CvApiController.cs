using DotNetCV.Models;
using DotNetCV.Services;
using DotNetCV.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCV.Controllers.Api;

/// <summary>
/// Read-only JSON access to the CV content.
/// </summary>
/// <remarks>
/// Unhandled errors go to the app's exception handler, so no internal details reach the client.
/// </remarks>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public sealed class CvApiController(ICvService cvService) : ControllerBase
{
    /// <summary>Gets the complete CV.</summary>
    [HttpGet]
    public ActionResult<CvViewModel> GetCompleteCv() => cvService.GetCv();

    /// <summary>Gets the personal information.</summary>
    [HttpGet("about")]
    public ActionResult<AboutMe> GetAboutMe() => cvService.GetCv().AboutMe;

    /// <summary>Gets the work history, newest first.</summary>
    [HttpGet("experiences")]
    public ActionResult<IReadOnlyList<Experience>> GetExperiences() => Ok(cvService.GetCv().Experiences);

    /// <summary>Gets degrees, certifications and courses.</summary>
    [HttpGet("education")]
    public ActionResult<IReadOnlyList<Education>> GetEducation() => Ok(cvService.GetCv().Education);

    /// <summary>Gets the skills, grouped by category.</summary>
    [HttpGet("skills")]
    public ActionResult<IReadOnlyList<SkillCategory>> GetSkills() => Ok(cvService.GetCv().SkillCategories);

    /// <summary>Gets the public contact details.</summary>
    [HttpGet("contact")]
    public ActionResult<Contact> GetContact() => cvService.GetCv().Contact;

    /// <summary>Gets the navigation and links shown in the layout.</summary>
    [HttpGet("sidebar")]
    public ActionResult<SidebarViewModel> GetSidebar() => cvService.GetCv().Sidebar;
}
