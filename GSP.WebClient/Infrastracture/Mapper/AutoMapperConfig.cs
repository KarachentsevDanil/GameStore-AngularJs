namespace GSP.WebClient.Infrastracture.Mapper
{
    public class AutoMapperConfig
    {
        public static void Init()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<GameAutoMapperProfile>();
                cfg.AddProfile<RateAutoMapperConfig>();
                cfg.AddProfile<OrderAutoMapperProfile>();
            });

            AutoMapper.Mapper.AssertConfigurationIsValid();
        }
    }
}
