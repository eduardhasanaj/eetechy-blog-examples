using AzureRestApi.Models;
using AzureRestApi.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: FunctionsStartup(typeof(AzureRestApi.Startup))]
namespace AzureRestApi
{
  class Startup : FunctionsStartup
  {
    public override void Configure(IFunctionsHostBuilder builder)
    {
      var connectionStr = Environment.GetEnvironmentVariable("SqlConnectionString");
      builder.Services.AddEntityFrameworkNpgsql()
        .AddDbContext<RepositoryContext>(
        options => options.UseNpgsql(connectionStr));

      builder.Services.AddTransient<ITaskService, TaskService>();
    }
  }
}
