using DotNetCV.Models;

namespace DotNetCV.Services;

public interface ICvService
{
    Task<AboutMe> GetAboutMeAsync();
    Task<List<Experience>> GetExperiencesAsync();
    Task<List<Education>> GetEducationAsync();
    Task<List<SkillCategory>> GetSkillCategoriesAsync();
    Task<Contact> GetContactAsync();
    Task<CvViewModel> GetCompleteCvAsync();
}
