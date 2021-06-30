using AutoMapper;
using WebCrawler.Data;
using WebCrawler.Services.Models.Response;

namespace WebCrawler.Services.Mappers
{
    public class TestsMapperProfile : Profile
    {
        public TestsMapperProfile()
        {
            CreateMap<Test, TestModel>();
            CreateMap<PerformanceResult, PerformanceResultModel>();
            CreateMap<Test, TestDetailsModel>().ForMember(dest=>dest.Results, opt=>opt.MapFrom(src=>src.PerformanceResults));
        }
    }
}
