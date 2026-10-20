using CleanArchitecture.Core.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Shared.Contract.UserImplemention
{
    public class UserContract
    {
        public string Name { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public Enums.UserRole Role { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}
