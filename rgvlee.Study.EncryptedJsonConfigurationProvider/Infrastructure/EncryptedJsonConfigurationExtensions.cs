using System;
using Microsoft.Extensions.FileProviders;
using rgvlee.Study.EncryptedJsonConfigurationProvider.Infrastructure;

namespace Microsoft.Extensions.Configuration
{
    public static class EncryptedJsonConfigurationExtensions
    {
        public static IConfigurationBuilder AddEncryptedJsonFile(this IConfigurationBuilder builder, string path)
        {
            return AddEncryptedJsonFile(builder, null, path, false, false);
        }

        public static IConfigurationBuilder AddEncryptedJsonFile(this IConfigurationBuilder builder, string path, bool optional)
        {
            return AddEncryptedJsonFile(builder, null, path, optional, false);
        }

        public static IConfigurationBuilder AddEncryptedJsonFile(this IConfigurationBuilder builder, string path, bool optional, bool reloadOnChange)
        {
            return AddEncryptedJsonFile(builder, null, path, optional, reloadOnChange);
        }

        public static IConfigurationBuilder AddEncryptedJsonFile(this IConfigurationBuilder builder, IFileProvider provider, string path, bool optional,
            bool reloadOnChange)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException(nameof(path));
                //throw new ArgumentException(Resources.Error_InvalidFilePath, nameof(path));
            }

            return builder.AddEncryptedJsonFile(s =>
            {
                s.FileProvider = provider;
                s.Path = path;
                s.Optional = optional;
                s.ReloadOnChange = reloadOnChange;
                s.ResolveFileProvider();
            });
        }

        public static IConfigurationBuilder AddEncryptedJsonFile(this IConfigurationBuilder builder, Action<EncryptedJsonConfigurationSource> configureSource)
        {
            return builder.Add(configureSource);
        }
    }
}