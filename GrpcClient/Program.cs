using Grpc.Net.Client;
using GrpcSharedProtoFile;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Create a channel to the gRPC server
            using var channel = GrpcChannel.ForAddress("https://localhost:7089");
            var client = new UserService.UserServiceClient(channel);

            var getAllUsers = await client.ListUsersAsync(new Google.Protobuf.WellKnownTypes.Empty());
            getAllUsers.Users.ToList().ForEach(x => Console.WriteLine($"firstname is {x.FirstName} and lastname is {x.LastName}"));

            var createUser = await client.CreateUserAsync(new UserRequest()
            {
                Id = Guid.NewGuid().ToString(),
                BirthDate = DateTime.Now.ToString(),
                FirstName = "Test",
                LastName = "Grpc",
                NationalCode = "1234567890",
            });
            Console.WriteLine("UserService: " + createUser.Message);

            var updateUser = await client.UpdateUserAsync(new UserRequest()
            {
                Id = "99300AB2-76BC-436F-A66B-BF8FA42278D2",
                BirthDate = DateTime.Now.ToString(),
                FirstName = "Test2",
                LastName = "Grpc2",
                NationalCode = "0987654321",
            });
            Console.WriteLine("UserService: " + updateUser.Message);

            var deleteUser = await client.DeleteUserAsync(new UserIdRequest()
            {
                Id = "99300AB2-76BC-436F-A66B-BF8FA42278D2"
            });
            Console.WriteLine("UserService: " + deleteUser.Message);
        }
    }
}
