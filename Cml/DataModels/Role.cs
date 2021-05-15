using System;

namespace Cml.DataModels
{
    public static class Role
    {
        public const String ADMIN = "admin";
        public const String USER = "user";

        public static class Access
        {
            public const String MUST_BE_AUTHENTICATED = ADMIN + "," + USER;
            public const String MUST_BE_ADMIN = ADMIN;

        }
    }
}
