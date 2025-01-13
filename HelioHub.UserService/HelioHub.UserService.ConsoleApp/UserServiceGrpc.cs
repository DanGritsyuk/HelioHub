using Grpc.Core;

namespace HelioHub.UserService.ConsoleApp
{
    public class UserServiceGrpc : UserService.UserServiceBase
    {
        private readonly IUserService _userService;

        public UserServiceGrpc(IUserService userService)
        {
            _userService = userService;
        }

        public override async Task<GetUserResponse> GetUserById(GetUserRequest request, ServerCallContext context)
        {
            var user = await _userService.GetUserByIdAsync(Guid.Parse(request.UserId));
            return new GetUserResponse
            {
                Id = user.Id.ToString(),
                FullName = user.FullName,
                DateOfBirth = Timestamp.FromDateTime(user.DateOfBirth.ToUniversalTime()),
                Email = user.Email,
                Bio = user.Bio,
                ProfilePictureUrl = user.ProfilePictureUrl
            };
        }

        public override async Task<GetAllUsersResponse> GetAllUsers(Empty request, ServerCallContext context)
        {
            var users = await _userService.GetAllUsersAsync();
            var response = new GetAllUsersResponse();
            response.Users.AddRange(users.Select(u => new GetUserResponse
            {
                Id = u.Id.ToString(),
                FullName = u.FullName,
                DateOfBirth = Timestamp.FromDateTime(u.DateOfBirth.ToUniversalTime()),
                Email = u.Email,
                Bio = u.Bio,
                ProfilePictureUrl = u.ProfilePictureUrl
            }));
            return response;
        }

        public override async Task<CreateUserResponse> CreateUser(CreateUserRequest request, ServerCallContext context)
        {
            var userId = await _userService.CreateUserAsync(new CreateUserDto
            {
                FullName = request.FullName,
                DateOfBirth = request.DateOfBirth.ToDateTime(),
                Email = request.Email,
                Bio = request.Bio
            });

            return new CreateUserResponse { UserId = userId.ToString() };
        }

        public override async Task<Empty> UpdateUser(UpdateUserRequest request, ServerCallContext context)
        {
            await _userService.UpdateUserAsync(Guid.Parse(request.UserId), new UpdateUserDto
            {
                FullName = request.FullName,
                DateOfBirth = request.DateOfBirth.ToDateTime(),
                Bio = request.Bio,
                ProfilePictureUrl = request.ProfilePictureUrl
            });
            return new Empty();
        }

        public override async Task<Empty> DeleteUser(DeleteUserRequest request, ServerCallContext context)
        {
            await _userService.DeleteUserAsync(Guid.Parse(request.UserId));
            return new Empty();
        }
    }
}
