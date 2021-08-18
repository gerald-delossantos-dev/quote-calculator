using LoanCalculator.Core.Interfaces;
using System;

namespace LoanCalculator.Core.Common
{
    public class BaseEntity : IEntity
    {
        public long Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateLastModified { get; set; }
    }
}
