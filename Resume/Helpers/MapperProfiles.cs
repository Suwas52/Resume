using AutoMapper;
using Resume.DTOs.ContactDTOs;
using Resume.DTOs.EducationDTOs;
using Resume.DTOs.ExperienceDTOs;
using Resume.DTOs.InfomationDTOs;
using Resume.DTOs.ProjectDTOs;
using Resume.DTOs.SkillDTOs;
using Resume.Models;

namespace Resume.Helpers
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles() {

          //InformationDTOs
            CreateMap<Information, InformationReadDTOs>();
            CreateMap<InformationCreateDTOs, Information>();
            CreateMap<InformationUpdateDTOs, Information>();
            CreateMap<Information, InformationUpdateDTOs>();

            //EducationDTOs
            CreateMap<Education, EducationReadDTOs>();
            CreateMap<EducationCreateDTOs, Education>();
            CreateMap<EducationUpdateDTOs, Education>();
            CreateMap<Education, EducationUpdateDTOs>();

            //ExperienceDTOs
            CreateMap<Experience, ExperienceReadDTOs>();
            CreateMap<ExperienceCreateDTOs, Experience>();
            CreateMap<ExperienceUpdateDTOs, Experience>();
            CreateMap<Experience, ExperienceUpdateDTOs>();

            //ContactDTOs
            CreateMap<Contact, ContactReadDTOs>();
            CreateMap<ContactCreateDTOs, Contact>();
            CreateMap<ContactUpdateDTOs, Contact>();
            CreateMap<Contact, ContactUpdateDTOs>();

            //SkillDTOs
            CreateMap<Skill, SkillReadDTOs>();
            CreateMap<SkillCreateDTOs, Skill>();
            CreateMap<SkillUpdateDTOs, Skill>();
            CreateMap<Skill, SkillUpdateDTOs>();

            //ProjectDTOs
            CreateMap<Project, ProjectReadDTOs>();
            CreateMap<ProjectCreateDTOs, Project>();
            CreateMap<ProjectUpdateDTOs, Project>();
            CreateMap<Project, ProjectUpdateDTOs>();

        }
    }
}
