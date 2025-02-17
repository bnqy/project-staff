﻿using AutoMapper;
using project_staff.Entities.Models;
using project_staff.Shared.DTOs;

namespace project_staff
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Project, ProjectDto>();
			CreateMap<ProjectTask, ProjectTaskDto>();
			CreateMap<ApplicationUser, ApplicationUserDto>();
		}
	}
}
