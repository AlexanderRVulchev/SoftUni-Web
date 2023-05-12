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
        }
    }
}
