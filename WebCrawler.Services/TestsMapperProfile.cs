using AutoMapper;
using WebCrawler.Data;
using WebCrawler.Services.Models.Response;

namespace WebCrawler.Services
{
    public class TestsMapperProfile : Profile
    {
        public TestsMapperProfile()
        {
            CreateMap<Test, TestDto>();
            CreateMap<PerformanceResult, PerformanceResultModel>();
            CreateMap<Test, TestDetailsDto>().ForMember(dest=>dest.Results, opt=>opt.MapFrom(src=>src.PerformanceResults));
        }
    }
}
