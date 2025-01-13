using Grpc.Core;
using HelioHub.UserService.ConsoleApp;
using HelioHub.UserService.DAL.Repository.UserRepository;
using MongoDB.Driver;
using Npgsql;
using static System.Net.Mime.MediaTypeNames;

Console.WriteLine("Starting gRPC User Service...");

var mongoClient = new MongoClient("mongodb://localhost:27017");
var mongoDatabase = mongoClient.GetDatabase("UserService");
var postgresConnection = new NpgsqlConnection("Host=localhost;Username=postgres;Password=password;Database=UserService");
await postgresConnection.OpenAsync();

var userRepository = new UserRepository(mongoDatabase, postgresConnection, mongoClient);
var userService = new Application.UserService(userRepository);

var server = new Server
{
    Services = { UserService.BindService(new UserServiceGrpc(userService)) },
    Ports = { new ServerPort("localhost", 5000, ServerCredentials.Insecure) }
};

server.Start();

Console.WriteLine("User Service is running on port 5000");
Console.WriteLine("Press any key to stop...");
Console.ReadKey();

await server.ShutdownAsync();