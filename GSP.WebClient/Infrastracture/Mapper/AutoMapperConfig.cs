namespace GSP.WebClient.Infrastracture.Mapper
{
    public class AutoMapperConfig
    {
        public static void Init()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<GameAutoMapperProfile>();
            });

            AutoMapper.Mapper.AssertConfigurationIsValid();
        }
    }
}
