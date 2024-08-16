using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using SonarTrack.Infrastructure.DependencyInjections;
using SonarTrack.Application.Abstractions.UseCases;
using Serilog;
using Serilog.Filters;

Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .Filter.ByExcluding(Matching.FromSource("System.Net.Http.HttpClient"))
            .CreateLogger();

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var serviceCollection = new ServiceCollection();
serviceCollection.AddLogging(builder => builder.AddSerilog());
serviceCollection.AddDependencyInjections(configuration);
var serviceProvider = serviceCollection.BuildServiceProvider();

var trackerUseCase = serviceProvider.GetService<ITrackerUseCase>();
await trackerUseCase.TrackAsync();

Log.CloseAndFlush();