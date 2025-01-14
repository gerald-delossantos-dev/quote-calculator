﻿using LoanCalculator.Core.Common;

namespace LoanCalculator.Core.Entities
{
    public class Customer : BaseEntity
    {
        public string Title { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public string UserName { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }
    }
}
