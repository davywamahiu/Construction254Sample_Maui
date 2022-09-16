using System.ComponentModel.DataAnnotations;

namespace Construction_Ke.Model
{
    public class SysLogin
    {
        public SysLogin(bool isAdmin, DateTime lastLogin, string password, string username)
        {
            IsAdmin = isAdmin;
            LastLogin = lastLogin;
            Password = password;
            Username = username;
        }

        public SysLogin(int id, string name, long userId, DateTime createdAt, bool isAdmin, DateTime lastLogin, string password, long phone, string username)
        {
            Id = id;
            Name = name;
            UserId = userId;
            CreatedAt = createdAt;
            IsAdmin = isAdmin;
            LastLogin = lastLogin;
            Password = password;
            Phone = phone;
            Username = username;
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public long UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime? LastLogin { get; set; }
        public string Password { get; set; } = null!;
        public long Phone { get; set; }
        public string Username { get; set; } = null!;
    }
}
