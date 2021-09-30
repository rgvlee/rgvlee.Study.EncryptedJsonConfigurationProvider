using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace rgvlee.Study.EncryptedJsonConfigurationProvider.Infrastructure
{
    public class EncryptedJsonConfigurationSource : JsonConfigurationSource
    {
        public override IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            EnsureDefaults(builder);
            return new EncryptedJsonConfigurationProvider(this);
        }
    }
}