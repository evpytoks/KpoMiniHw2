
using Aplication.Interfaces;
using Aplication.Services;
using Domain.Events;
using Infrastructure.Interfacses;
using Infrastructure.Repositories;
using Aplication.Handlers;
using MediatR;
using Application.Handlers;

namespace Application;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

        builder.Services.AddSingleton<IAnimalsRepository, AnimalRepository>();
        builder.Services.AddSingleton<IEnclosuresRepository, EnclosuresRepository>();
        builder.Services.AddSingleton<IFeedingSchedulesRepository, FeedingSchedulesRepository>();

        builder.Services.AddScoped<IAnimalTransferSetvice, AnimalTransferService>();
        builder.Services.AddScoped<IFeedingOrganizationService, FeedingOrganizationService>();
        builder.Services.AddScoped<IZooStatisticsService, ZooStatisticsService>();

        builder.Services.AddTransient<INotificationHandler<AnimalMovedEvent>, AnimalMovedHandler>();
        builder.Services.AddTransient<INotificationHandler<FeedingTimeEvent>, FeedingTimeHandler>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
