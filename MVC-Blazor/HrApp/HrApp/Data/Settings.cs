namespace HrApp.Data
{
    public static class Settings
    {
        public static bool IsDevelopmentEnvironment { get; set; }

        public static class AdminUser
        {
            public static string UserName = "Admin";
            public static string Password = "T3st123!";
            public static string Email = "admin@pxl.be";
        }

        public static class Roles
        {
            public static string AdminRole = "Admin";
            public static string UserRole = "NormalUser";
        }
    }
}
