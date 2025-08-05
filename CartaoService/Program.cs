using CartaoService.Consumer;
using CartaoService.Service;
using CommonLibrary.Configs;
using CommonLibrary.Rabbit;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Config
var massTransitConfigsSection = builder.Configuration.GetSection(nameof(MassTransitConfigs));
builder.Services.Configure<MassTransitConfigs>(massTransitConfigsSection);

// Service
var massTransitConfigs = massTransitConfigsSection.Get<MassTransitConfigs>();

builder.Services.AddScoped<ICartaoBusService, CartaoBusService>();
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<CartaoConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(massTransitConfigs?.host, h =>
        {
            h.Username(massTransitConfigs?.User);
            h.Password(massTransitConfigs?.Pwd);
        });
        cfg.ReceiveEndpoint(Queues.Cartao, ep =>
        {
            ep.ConcurrentMessageLimit = 2;
            ep.PrefetchCount = 10;
            ep.UseMessageRetry(r => r.Interval(2, 10000));
            ep.UseDelayedRedelivery(r => r.Interval(2, 10000));
            ep.ConfigureConsumer<CartaoConsumer>(context);
        });
    });
});

var app = builder.Build();


//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

//app.MapControllers();

app.Run();
