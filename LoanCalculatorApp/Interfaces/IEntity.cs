using System;

namespace LoanCalculator.Core.Interfaces
{
    public interface IEntity
    {
        long Id { get; set; }

        DateTime DateCreated { get; set; } 

        DateTime? DateLastModified { get; set; }
    }
}
