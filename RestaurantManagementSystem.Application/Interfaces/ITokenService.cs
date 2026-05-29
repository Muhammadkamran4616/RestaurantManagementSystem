namespace RestaurantManagementSystem.Application.Interfaces;

public interface ITokenService
{
    string CreateToken(int userId, string email, string role);
}