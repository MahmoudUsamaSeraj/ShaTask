namespace ShaTask.Needs
{
    public static class Permission
    {
        public static List<string> GeneratePermissionsList(string module)
        {
            return new List<string>()
            {
                $"Permissions.{module}.View",
                $"Permissions.{module}.Create",
                $"Permissions.{module}.Edit",
                $"Permissions.{module}.Delete"
            };
        }
        public static List<string> GenerateAllPermissions()
        {
            var allPermissions = new List<string>();

            foreach (var module in Enum.GetValues(typeof(Modules)))
                allPermissions.AddRange(GeneratePermissionsList(module.ToString()));

            return allPermissions;
        }
        public static class Cashiers
        {
            public const string View = "Permissions.Cashiers.View";
            public const string Create = "Permissions.Cashiers.Create";
            public const string Edit = "Permissions.Cashiers.Edit";
            public const string Delete = "Permissions.Cashiers.Delete";
        }
        public static class Invoices
        {
            public const string View = "Permissions.Invoices.View";
            public const string Create = "Permissions.Invoices.Create";
            public const string Edit = "Permissions.Invoices.Edit";
            public const string Delete = "Permissions.Invoices.Delete";
        }
    }
}
