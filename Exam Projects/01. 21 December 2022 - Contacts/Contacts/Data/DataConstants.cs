namespace Contacts.Data
{
    public class DataConstants
    {
        public class User
        {
            public const int UserNameMaxLength = 20;
            public const int UserNameMinLength = 5;

            public const int EmailMaxLength = 60;
            public const int EmailMinLength = 10;

            public const int PasswordMaxLength = 20;
            public const int PasswordMinLength = 5;
        }

        public class Contact
        {
            public const int FirstNameMaxLength = 50;
            public const int FirstNameMinLength = 2;
            
            public const int LastNameMaxLength = 50;
            public const int LastNameMinLength = 5;
            
            public const int EmailMaxLength = 60;
            public const int EmailMinLength = 10;

            public const int PhoneNumberMaxLength = 13;
            public const string PhoneNumberRegex = @"^(\+359|0)([ -]?)\d{3}\2\d{2}\2\d{2}\2\d{2}$";

            public const string WebsiteRegex = @"^www\.[A-Za-z0-9]+\.bg$";
        }
    }
}
