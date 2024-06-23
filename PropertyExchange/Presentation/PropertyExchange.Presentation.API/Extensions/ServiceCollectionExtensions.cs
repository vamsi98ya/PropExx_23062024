using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace PropertyExchange.Presentation.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterDependencies(this IServiceCollection services)
        {
            var assembly = Assembly.Load("PropertyExchange.Infrastructure.Data");
            var types = assembly.GetTypes().Where(x => x.IsClass && x.IsNotPublic && !x.IsAbstract && x.FullName.EndsWith("Repository"));
            foreach (var type in types)
            {
                services.AddScoped(type.GetInterface($"I{type.Name}"), type);
            }

            assembly = Assembly.Load("PropertyExchange.Core.Application");
            types = assembly.GetTypes().Where(x => x.IsClass && x.IsNotPublic && !x.IsAbstract && x.FullName.EndsWith("Usecase"));
            foreach (var type in types)
            {
                services.AddScoped(type.GetInterface($"I{type.Name}"), type);
            }
        }
    }
}
