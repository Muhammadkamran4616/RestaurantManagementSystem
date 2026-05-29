using RestaurantManagementSystem.Application.DTOs;
using RestaurantManagementSystem.Application.Interfaces;
using RestaurantManagementSystem.Domain.Entities;

namespace RestaurantManagementSystem.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

    public AuthService(
        IUserRepository userRepository,
        ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public async Task<string> RegisterAsync(RegisterDto dto)
    {
        var existingUser = await _userRepository.GetByEmailAsync(dto.Email);

        if (existingUser != null)
            throw new Exception("User already exists");

        var user = new User
        {
            FullName = dto.FullName,
            Email = dto.Email,
            PasswordHash = dto.Password,
            Role = "User"
        };

        await _userRepository.AddAsync(user);

        return _tokenService.CreateToken(
            user.Id,
            user.Email,
            user.Role);
    }

    public async Task<string> LoginAsync(LoginDto dto)
    {
        var user = await _userRepository.GetByEmailAsync(dto.Email);

        if (user == null)
            throw new Exception("Invalid email");

        if (user.PasswordHash != dto.Password)
            throw new Exception("Invalid password");

        return _tokenService.CreateToken(
            user.Id,
            user.Email,
            user.Role);
    }
}