using CodeSmellsAndRefactoring.FeatureEnvy;
using Xunit;

namespace CodeSmellsAndRefactoring.Tests.FeatureEnvy
{
    public class PayrollShould
    {
        [Theory]
        [InlineData("Engineering", 50000, 5, 11500)]
        [InlineData("HR", 60000, 5, 10800)]
        [InlineData("Sales", 70000, 5, 14000)]
        [InlineData("Marketing", 80000, 5, 15200)]
        [InlineData("Unknown", 90000, 5, 18900)]
        public void calculate_the_correct_tax_for_each_department(string department, double salary, int yearsOfService, double expectedTaxAmount)
        {
            var employee = new Employee("John Doe", department, salary, 2000, 10, yearsOfService);
            var payroll = new Payroll(employee);

            var tax = payroll.CalculateTax();

            Assert.Equal(expectedTaxAmount, tax);
        }

        [Theory]
        [InlineData(5, 50000, 11500)]
        [InlineData(6, 50000, 11500)]
        public void adjusts_the_tax_rate_based_on_years_of_service(int yearsOfService, double salary, double expectedTaxAmount)
        {
            var employee = new Employee("John Doe", "Engineering", salary, 2000, 10, yearsOfService);
            var payroll = new Payroll(employee);

            var tax = payroll.CalculateTax();

            Assert.Equal(expectedTaxAmount, tax);
        }

        [Theory]
        [InlineData(-50000)]
        [InlineData(0)]
        public void handle_tax_calculations_with_a_negative_or_zero_salary(double salary)
        {
            var employee = new Employee("John Doe", "Engineering", salary, 2000, 10, 5);
            var payroll = new Payroll(employee);

            Assert.Throws<ArgumentOutOfRangeException>(() => payroll.CalculateTax());
        }

        [Fact]
        public void handle_tax_calculations_with_negative_years_of_service()
        {
            var employee = new Employee("Jane Doe", "Engineering", 50000, 2000, 10, -5);
            var payroll = new Payroll(employee);

            Assert.Throws<ArgumentOutOfRangeException>(() => payroll.CalculateTax());
        }
    }
}
