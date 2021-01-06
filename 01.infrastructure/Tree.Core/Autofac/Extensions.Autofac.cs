using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using AspectCore.Extensions.Autofac;
using Autofac;
using Microsoft.Extensions.PlatformAbstractions;
using Treebank.Core.Autofac;

namespace Tree.Core.Autofac
{
    public static partial class Extensions
    {
        /// <summary>
        /// the assembly should be skipped to injection
        /// </summary>
        private const string SkipAssemblies =
            "^System|^Mscorlib|^msvcr120|^Netstandard|^Microsoft|^Autofac|^AutoMapper|^EntityFramework|^Newtonsoft|^Castle|^NLog|^Pomelo|^AspectCore|^Xunit|^Nito|^Npgsql|^Exceptionless|^MySqlConnector|^Anonymously Hosted|^libuv|^api-ms|^clrcompression|^clretwrc|^clrjit|^coreclr|^dbgshim|^e_sqlite3|^hostfxr|^hostpolicy|^MessagePack|^mscordaccore|^mscordbi|^mscorrc|sni|sos|SOS.NETCore|^sos_amd64|^SQLitePCLRaw|^StackExchange|^Swashbuckle|WindowsBase|ucrtbase|^DotNetCore.CAP|^MongoDB|^Confluent.Kafka|^librdkafka|^EasyCaching|^RabbitMQ|^Consul|^Dapper|^EnyimMemcachedCore|^Pipelines|^DnsClient|^IdentityModel|^zlib";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public static void Register(this ContainerBuilder builder)
        {
            var basePath = PlatformServices.Default.Application.ApplicationBasePath;
            var files = Directory.GetFiles(basePath, "*.dll");
            foreach (var file in files)
            {
                if (!NotThirdPartyAssembly(Path.GetFileName(file)))
                    continue;
                LoadAssemblyToDomain(file);
            }

            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(NotThirdPartyAssembly).Distinct().ToList();
            foreach (var assembly in assemblies)
            {
                builder.RegisterAssemblyTypes(assembly)
                    .Where(t => typeof(ISingletonDependency).IsAssignableFrom(t) && t != typeof(ISingletonDependency))
                    .AsImplementedInterfaces().SingleInstance();
                builder.RegisterAssemblyTypes(assembly)
                    .Where(t => typeof(IScopeDependency).IsAssignableFrom(t) && t != typeof(IScopeDependency))
                    .AsImplementedInterfaces().InstancePerLifetimeScope();
                builder.RegisterAssemblyTypes(assembly)
                    .Where(t => typeof(ITransientDependency).IsAssignableFrom(t) && t != typeof(ITransientDependency))
                    .AsImplementedInterfaces().InstancePerDependency();
            }

            //注册aop
            builder.RegisterDynamicProxy();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        private static bool NotThirdPartyAssembly(string assembly)
        {
            return !assembly.StartsWith($"{PlatformServices.Default.Application.ApplicationName}.Views") && !Regex.IsMatch(assembly, SkipAssemblies, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }

        /// <summary>
        ///
        /// </summary>
        private static bool NotThirdPartyAssembly(Assembly assembly)
        {
            return !Regex.IsMatch(assembly.FullName, SkipAssemblies, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assemblyFullName"></param>
        private static void LoadAssemblyToDomain(string assemblyFullName)
        {
            try
            {
                var assemblyName = AssemblyName.GetAssemblyName(assemblyFullName);
                AppDomain.CurrentDomain.Load(assemblyName);
            }
            catch (BadImageFormatException)
            {
                //ignore exception to make program continue
            }
        }
    }
}