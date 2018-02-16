namespace GSP.BLL.Mapper
{
    public class AutoMapperConfig
    {
        public static void Init()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<CategoryAutoMapperProfile>();
                cfg.AddProfile<CustomerAutoMapperProfile>();
                cfg.AddProfile<GameAutoMapperProfile>();
                cfg.AddProfile<RateAutoMapperConfig>();
                cfg.AddProfile<OrderAutoMapperProfile>();
            });

            AutoMapper.Mapper.AssertConfigurationIsValid();
        }
    }
}