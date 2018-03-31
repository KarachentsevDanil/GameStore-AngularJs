namespace GSP.Games.BLL.Mapper
{
    public class AutoMapperConfig
    {
        public static void Init()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<CategoryAutoMapperProfile>();
                cfg.AddProfile<GameAutoMapperProfile>();
                cfg.AddProfile<RateAutoMapperConfig>();
            });

            AutoMapper.Mapper.AssertConfigurationIsValid();
        }
    }
}