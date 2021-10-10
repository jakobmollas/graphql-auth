namespace Server.Security
{
    //public enum UserRole
    //{
    //    /// <summary>
    //    /// Undefined - no access (fallback if no role is specified to avoid falling back to a defined role)
    //    /// </summary>
    //    Undefined = 0,
    //    Read,
    //    Write
    //}

    internal static class UserRoles
    {
        public const string Read = "Read";
        public const string Write = "Write";
    }
}
