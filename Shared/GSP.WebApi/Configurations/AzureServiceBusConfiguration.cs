namespace GSP.WebApi.Configurations
{
    public class AzureServiceBusConfiguration
    {
        public string ConnectionString { get; set; }

        public string TopicName { get; set; }

        public string SubscriptionClientName { get; set; }
    }
}
