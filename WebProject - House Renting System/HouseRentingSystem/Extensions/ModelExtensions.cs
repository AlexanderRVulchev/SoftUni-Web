
namespace HouseRentingSystem.Extensions
{
    using Core.Contracts;
    using System.Text.RegularExpressions;

    public static class ModelExtensions
    {
        public static string GetInformation(this IHouseModel house)
            => house.Title.Replace(" ", "-") + "-" + GetAddress(house.Address);

        private static string GetAddress(string address)
        {
            address = string.Join("-", address.Split().Take(3));
            return Regex.Replace(address, @"[^a-zA-Z0-9\-]", String.Empty);
        }        
    }
}
