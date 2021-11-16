using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Filuet.Infrastructure.Abstractions.Models
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEventMediation(this IServiceCollection serviceCollection, Action<IServiceProvider, EventBroker> setupAction)
            => serviceCollection.AddSingleton<IEventConsumer, EventConsumer>()
                .AddSingleton<IEventBroker>(sp =>
                {
                    EventBroker broker = new EventBroker(sp.GetRequiredService<IEventConsumer>());
                    setupAction?.Invoke(sp, broker);
                    return broker;
                });

        public static IServiceCollection AddEventMediation(this IServiceCollection serviceCollection)
            => serviceCollection.AddSingleton<IEventConsumer, EventConsumer>()
                .AddSingleton<IEventBroker>(sp =>
                {
                    EventBroker broker = new EventBroker(sp.GetRequiredService<IEventConsumer>());

                    Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
                    IEnumerable<TypeInfo> typesFromAssemblies = assemblies.SelectMany(x =>
                        x.DefinedTypes.Where(t => t.IsClass && t.GetInterfaces().Contains(typeof(IEventProducer)))); // IKioskEventProducer

                    foreach (var producer in typesFromAssemblies)
                    {
                        foreach (var i in producer.GetInterfaces())
                        {
                            if (i.GetInterfaces().Any(x => x == typeof(IEventProducer)))
                            {
                                broker.AppendProducer((IEventProducer)sp.GetRequiredService(i));
                                break;
                            }
                        }
                    }

                    return broker;
                });
    }
}