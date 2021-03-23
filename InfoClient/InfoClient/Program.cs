using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace UserInfoClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // The port number(5001) must match the port of the gRPC server.
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            string nume, cnp;
            Console.WriteLine("Nume: ");
            nume = Console.ReadLine();
            Console.WriteLine("CNP: ");
            cnp = Console.ReadLine();

            var client = new UserInfo.UserInfoClient(channel);
            var reply = await client.GetUserInfoAsync(
                              new UserInfoRequest { Nume = nume, CNP = cnp });
            Console.WriteLine($"REPLY: Greetings {reply.Nume}. Varsta: {reply.Varsta}, gen: {reply.Gen}.");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
