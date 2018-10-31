using DotNetCoreAwsDockerSwaggerWebpi.Client;

namespace BasketApi.Specs
{
    public class ApiContext
    {
        public ApiClient Client { get; set; }
        public string BasketId { get; set; }
        public string StringResponse { get; set; }
        public Basket BasketResponse { get; set; }
    }
}