
using System;

namespace EduHub.Domain.Entities
{
    public class Member
    {
        public Guid Id { get; private set; }

        public string Email { get; private set; }

        private Member(Guid id, string email)
        {
            Id = id;
            Email = email;
        }

        public static Member Create(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email không được để trống");
            }

            if (!email.Contains("@"))
            {
                throw new ArgumentException("Email phải có ký tự @");
            }

            return new Member(Guid.NewGuid(), email);
        }
    }
}
