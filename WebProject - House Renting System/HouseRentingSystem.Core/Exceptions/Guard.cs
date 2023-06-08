namespace HouseRentingSystem.Core.Exceptions
{
    public class Guard : IGuard
    {
        public void AgainstNull<T>(T value, string? errorMessage = null)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(T), errorMessage ?? "");
            }
        }
    }
}
