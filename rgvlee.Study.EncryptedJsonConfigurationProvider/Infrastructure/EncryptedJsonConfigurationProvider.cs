using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration.Json;

namespace rgvlee.Study.EncryptedJsonConfigurationProvider.Infrastructure
{
    public class EncryptedJsonConfigurationProvider : JsonConfigurationProvider
    {
        public static string ConfigurationDecryptionPasswordConfigurationKey = "Configuration:Decryption:Password";
        public static string EncryptedValueKeyNameSuffix = ":Encrypted";

        public EncryptedJsonConfigurationProvider(EncryptedJsonConfigurationSource source) : base(source) { }

        private static bool TryGetConfigurationDecryptionPassword(IDictionary<string, string> data, out string password)
        {
            password = data.SingleOrDefault(x => x.Key.Equals(ConfigurationDecryptionPasswordConfigurationKey)).Value; //default KVP value is null
            return data.ContainsKey(ConfigurationDecryptionPasswordConfigurationKey);
        }

        public override void Load(Stream stream)
        {
            Console.WriteLine($"Loading encrypted json configuration for '{Source.Path}'");

            base.Load(stream);

            if (!TryGetConfigurationDecryptionPassword(Data, out var password))
            {
                Console.WriteLine($"Configuration key '{ConfigurationDecryptionPasswordConfigurationKey}' not found.");
            }

            foreach (var kvp in Data.Where(x => x.Key.EndsWith(EncryptedValueKeyNameSuffix, StringComparison.OrdinalIgnoreCase)).ToList())
            {
                Console.WriteLine($"Decrypting '{kvp.Key}'");

                Data.Add(
                    kvp.Key.Substring(0, kvp.Key.Length - EncryptedValueKeyNameSuffix.Length),
                    password.Equals("Decrypt") ? string.Join(string.Empty, kvp.Value.Reverse()) : "Denied"
                );
                Data.Remove(kvp.Key);
            }
        }
    }
}