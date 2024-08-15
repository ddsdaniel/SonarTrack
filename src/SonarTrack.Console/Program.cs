using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using SonarTrack.Infrastructure.DependencyInjections;
using SonarTrack.Application.Abstractions.UseCases;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var serviceCollection = new ServiceCollection();
serviceCollection.AddDependencyInjections(configuration);
var serviceProvider = serviceCollection.BuildServiceProvider();

var trackerUseCase = serviceProvider.GetService<ITrackerUseCase>();
await trackerUseCase.TrackAsync();
