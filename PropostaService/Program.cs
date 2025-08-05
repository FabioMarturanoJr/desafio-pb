using CommonLibrary.Configs;
using CommonLibrary.Rabbit;
using MassTransit;
using PropostaService.Consumer;
using PropostaService.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Config
var massTransitConfigsSection = builder.Configuration.GetSection(nameof(MassTransitConfigs));
builder.Services.Configure<MassTransitConfigs>(massTransitConfigsSection);

// Service
var massTransitConfigs = massTransitConfigsSection.Get<MassTransitConfigs>();

builder.Services.AddScoped<IPropostaBusService, PropostaBusService>();
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<PropostaConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(massTransitConfigs?.host, h =>
        {
            h.Username(massTransitConfigs?.User);
            h.Password(massTransitConfigs?.Pwd);
        });
        cfg.UseMessageRetry(r => r.Interval(2, TimeSpan.FromSeconds(10)));
        cfg.UseDelayedRedelivery(r => r.Interval(2, TimeSpan.FromSeconds(10)));

        cfg.ReceiveEndpoint(Queues.Proposta, ep =>
        {
            ep.ConcurrentMessageLimit = 2;
            ep.PrefetchCount = 10;
            //ep.UseMessageRetry(r => r.Interval(2, TimeSpan.FromSeconds(10)));
            ep.ConfigureConsumer<PropostaConsumer>(context);
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
