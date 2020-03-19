using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reface.NPI.DynamicProxy.AppOfSqlite.Entities
{
    [Table("T_USER")]
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }

        public static User New()
        {
            return new User()
            {
                Password = nameof(Password),
                Id = Guid.NewGuid().ToString(),
                LoginName = nameof(LoginName),
                Name = nameof(Name)
            };
        }
    }
}
