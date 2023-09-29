using Application.InterfaceServices;
using Application.Repositories;
using Application.Services;
using Domain.Entities;
using Infrastucture;
using Microsoft.EntityFrameworkCore;
using Infrastucture.Mapping;
using Application.RabbitMQ;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Assuming GenericRepository is the implementation of IGenericRepository
builder.Services.AddScoped<IGenericRepository<Account>, GenericRepository<Account>>();
builder.Services.AddScoped<IGenericRepository<Covenant>, GenericRepository<Covenant>>();
builder.Services.AddScoped<IGenericRepository<CovenantCondition>, GenericRepository<CovenantCondition>>();
builder.Services.AddScoped<IGenericRepository<CovenantResult>, GenericRepository<CovenantResult>>();
builder.Services.AddScoped<IGenericRepository<ResultNote>, GenericRepository<ResultNote>>();
builder.Services.AddScoped<IRabbitMQ, Application.RabbitMQ.RabbitMQ>();
builder.Services.AddScoped <IGenericRepository<Counterparty>, GenericRepository<Counterparty>>();


// Assuming Service is the implementation of IService
builder.Services.AddTransient<IAccountService, AccountService>();
builder.Services.AddTransient<ICovenantService, CovenantService>();
builder.Services.AddTransient<ICovenantConditionService, CovenantConditionService>();
builder.Services.AddTransient<ILinkedLineItemValueCalculatorService, LinkedLineItemValueCalculatorService>();
builder.Services.AddTransient<ICovenantResultService, CovenantResultService>();
builder.Services.AddTransient<IResultNoteService, ResultNoteService>();
builder.Services.AddTransient<ICounterpartyService, CounterpartyService>();


// Add DataBase Connection
builder.Services.AddDbContext<DBContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add mapping
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Add Cors
builder.Services.AddCors(
    option => option.AddPolicy(name: "CovenantManagement", builder =>
    {
        builder.AllowAnyOrigin().
                AllowAnyHeader().
                AllowAnyMethod();
    })
);

// Add lazy loading
builder.Services.AddDbContext<DBContext>(options =>
{
    options.UseLazyLoadingProxies();
});


//Here we specify the Rabbit MQ Server. we use rabbitmq docker image and use it
var factory = new ConnectionFactory
{
    HostName = "localhost"
};
//Create the RabbitMQ connection using connection factory details as i mentioned above
var connection = factory.CreateConnection();
//Here we create channel with session and model
using
var channel = connection.CreateModel();
//declare the queue after mentioning name and a few property related to that
channel.QueueDeclare("consumer", exclusive: false);
//Set Event object which listen message from chanel which is sent by producer
var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, eventArgs) => {
    var body = eventArgs.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"Consumer message received: {message}");
};
//read the message
channel.BasicConsume(queue: "consumer", autoAck: true, consumer: consumer);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("CovenantManagement");

app.Run();
Console.ReadKey();
