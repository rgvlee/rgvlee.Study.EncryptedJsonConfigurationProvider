using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration.Json;

namespace rgvlee.Study.EncryptedJsonConfigurationProvider.Infrastructure
{
    public class EncryptedJsonConfigurationProvider : JsonConfigurationProvider
    {
        private const string encryptedValueKeyNameSuffix = ":Encrypted";

        public EncryptedJsonConfigurationProvider(JsonConfigurationSource source) : base(source) { }

        public override void Load(Stream stream)
        {
            base.Load(stream);

            foreach (var kvp in Data.Where(x => x.Key.EndsWith(encryptedValueKeyNameSuffix, StringComparison.OrdinalIgnoreCase)).ToList())
            {
                Data.Add(kvp.Key.Substring(0, kvp.Key.Length - encryptedValueKeyNameSuffix.Length), string.Join(string.Empty, kvp.Value.Reverse()));
                Data.Remove(kvp.Key);
            }
        }
    }
}