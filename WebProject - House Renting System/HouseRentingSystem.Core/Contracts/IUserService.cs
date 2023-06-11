namespace HouseRentingSystem.Core.Contracts
{
    public interface IUserService
    {
        Task<string> UserFullName(string userId);
    }
}
