using AutoMapper;
using AutoMapper.Configuration;
using Kaizen.Model;
using Kaizen.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kaizen.Web
{
    public class AutoMapperConfig
    {
        public void Initialize()
        {
            var cfg = new MapperConfigurationExpression();

            cfg.CreateMap<Employee, EmployeeViewModel>().ReverseMap();
            cfg.CreateMap<Company, CompanyViewModel>().ReverseMap();
            cfg.CreateMap<Branch, BranchViewModel>().ReverseMap();
            cfg.CreateMap<Suggestion, SuggestionViewModel>().ReverseMap();
            cfg.CreateMap<Country, CountryViewModel>().ReverseMap();
            cfg.CreateMap<City, CityViewModel>().ReverseMap();
            cfg.CreateMap<County, CountyViewModel>().ReverseMap();

            Mapper.Initialize(cfg);
        }
    }
}