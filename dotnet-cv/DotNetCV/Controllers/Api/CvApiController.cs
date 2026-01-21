using DotNetCV.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCV.Controllers.Api;

/// <summary>
/// API controller for CV data
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public sealed class CvApiController : ControllerBase
{
    private readonly ICvService _cvService;
    private readonly ILogger<CvApiController> _logger;

    public CvApiController(ICvService cvService, ILogger<CvApiController> logger)
    {
        ArgumentNullException.ThrowIfNull(cvService);
        ArgumentNullException.ThrowIfNull(logger);

        _cvService = cvService;
        _logger = logger;
    }

    /// <summary>
    /// Gets complete CV data
    /// </summary>
    /// <response code="200">Returns the complete CV data</response>
    /// <response code="500">Internal server error</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCompleteCv()
    {
        try
        {
            var cv = await _cvService.GetCompleteCvAsync();
            return Ok(cv);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving complete CV data");
            return Problem(
                title: "An error occurred while retrieving CV data",
                detail: ex.Message,
                statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Gets about me information
    /// </summary>
    /// <response code="200">Returns the about me data</response>
    /// <response code="500">Internal server error</response>
    [HttpGet("about")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAboutMe()
    {
        try
        {
            var aboutMe = await _cvService.GetAboutMeAsync();
            return Ok(aboutMe);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving about me data");
            return Problem(
                title: "An error occurred while retrieving about me data",
                detail: ex.Message,
                statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Gets work experiences
    /// </summary>
    /// <response code="200">Returns the experiences list</response>
    /// <response code="500">Internal server error</response>
    [HttpGet("experiences")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetExperiences()
    {
        try
        {
            var experiences = await _cvService.GetExperiencesAsync();
            return Ok(experiences);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving experiences data");
            return Problem(
                title: "An error occurred while retrieving experiences data",
                detail: ex.Message,
                statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Gets education history
    /// </summary>
    /// <response code="200">Returns the education list</response>
    /// <response code="500">Internal server error</response>
    [HttpGet("education")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetEducation()
    {
        try
        {
            var education = await _cvService.GetEducationAsync();
            return Ok(education);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving education data");
            return Problem(
                title: "An error occurred while retrieving education data",
                detail: ex.Message,
                statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Gets skill categories
    /// </summary>
    /// <response code="200">Returns the skills categorized</response>
    /// <response code="500">Internal server error</response>
    [HttpGet("skills")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetSkills()
    {
        try
        {
            var skills = await _cvService.GetSkillCategoriesAsync();
            return Ok(skills);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving skills data");
            return Problem(
                title: "An error occurred while retrieving skills data",
                detail: ex.Message,
                statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Gets contact information
    /// </summary>
    /// <response code="200">Returns the contact data</response>
    /// <response code="500">Internal server error</response>
    [HttpGet("contact")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetContact()
    {
        try
        {
            var contact = await _cvService.GetContactAsync();
            return Ok(contact);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving contact data");
            return Problem(
                title: "An error occurred while retrieving contact data",
                detail: ex.Message,
                statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Gets sidebar data
    /// </summary>
    /// <response code="200">Returns the sidebar data</response>
    /// <response code="500">Internal server error</response>
    [HttpGet("sidebar")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetSidebar()
    {
        try
        {
            var sidebar = await _cvService.GetSidebarAsync();
            return Ok(sidebar);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving sidebar data");
            return Problem(
                title: "An error occurred while retrieving sidebar data",
                detail: ex.Message,
                statusCode: StatusCodes.Status500InternalServerError);
        }
    }
}
