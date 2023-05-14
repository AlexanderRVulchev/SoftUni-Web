namespace Library.Data
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

        public class Book
        {
            public const int TitleMaxLength = 50;
            public const int TitleMinLength = 10;

            public const int AuthorMaxLegnth = 50;
            public const int AuthorMinLegnth = 5;

            public const int DescriptionMaxLength = 5000;
            public const int DescriptionMinLength = 5;

            public const decimal RatingMaxValue = 10.00m;
            public const decimal RatingMinValue = 0.00m;
        }

        public class Category
        {
            public const int NameMaxLength = 50;
            public const int ManeMinLength = 5;
        }
    }
}
