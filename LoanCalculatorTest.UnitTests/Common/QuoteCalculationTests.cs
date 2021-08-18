using FluentAssertions;
using LoanCalculator.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace LoanCalculatorTest.UnitTests.Common
{
    [TestClass]
    public class QuoteCalculationTests
    {
        [TestMethod]
        public void QuoteCalculation_TenYearTerm_ShouldReturnCorrectMonthlyPayment()
        {
            //Arrange
            var loanDate = new DateTime(2021, 12, 12);
            var loanAmount = 20000D;
            var term = 10;
            var interestRate = 5;

            var expectedMonthlyPayment = 212.13D;
            var expectedTotalInterestPaid = 5455.72D;
            var expectedLoanFinishDate = new DateTime(2031, 12, 12);

            //Act
            var calculation = new QuoteCalculation(loanDate, loanAmount, interestRate, term);
            var monthlyPayment = Math.Round(calculation.MonthlyRepayment, 2);
            var lastPayment = calculation.PaymentSchedules.LastOrDefault();
            var totalInterestPaid = lastPayment.TotalInterest;

            //Assert
            monthlyPayment.Should().Be(expectedMonthlyPayment);
            calculation.PaymentSchedules.Count.Should().Be(120);
            lastPayment.PaymentDate.Should().Be(expectedLoanFinishDate);
            totalInterestPaid.Should().Be(expectedTotalInterestPaid);

        }

        [TestMethod]
        public void QuoteCalculation_TwoYearTerm_ShouldReturnCorrectMonthlyPayment()
        {
            //Arrange
            var loanDate = new DateTime(2021, 08, 12);
            var loanAmount = 20000D;
            var term = 2;
            var interestRate = 5;

            var expectedMonthlyPayment = 877.43;
            var expectedTotalInterestPaid = 1058.27;
            var expectedLoanFinishDate = new DateTime(2023, 08, 12);

            //Act
            var calculation = new QuoteCalculation(loanDate, loanAmount, interestRate, term);
            var monthlyPayment = Math.Round(calculation.MonthlyRepayment, 2);
            var lastPayment  = calculation.PaymentSchedules.LastOrDefault();
            var totalInterestPaid = lastPayment.TotalInterest;

            //Assert
            monthlyPayment.Should().Be(expectedMonthlyPayment);
            calculation.PaymentSchedules.Count.Should().Be(24);
            lastPayment.PaymentDate.Should().Be(expectedLoanFinishDate);
            totalInterestPaid.Should().Be(expectedTotalInterestPaid);

        }
    }
}
