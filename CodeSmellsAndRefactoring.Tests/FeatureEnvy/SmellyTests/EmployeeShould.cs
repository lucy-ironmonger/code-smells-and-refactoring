using CodeSmellsAndRefactoring.FeatureEnvy;
using Xunit;

namespace CodeSmellsAndRefactoring.Tests.FeatureEnvy.SmellyTests
{
    // Do we need this test?
    // How else could we tell if an Employee was constructed correctly?
    public class EmployeeShould
    {
        [Fact]
        public void be_constructed_correctly()
        {
            var employee = new Employee("John Doe", "Engineering", 50000, 2000, 10, 5);

            Assert.Equal("John Doe", employee.Name);
            Assert.Equal("Engineering", employee.Department);
            Assert.Equal(50000, employee.Salary);
            Assert.Equal(2000, employee.WorkHours);
            Assert.Equal(10, employee.VacationDays);
            Assert.Equal(5, employee.YearsOfService);
        }

        [Theory]
        [InlineData(-50000)]
        [InlineData(0)]
        public void not_be_constructed_with_zero_or_negative_salary(double salary)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                new Employee("John Doe", "Engineering", salary, 2000, 10, 5));
        }

        [Fact]
        public void not_be_constructed_with_negative_years_of_service()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                new Employee("Jane Doe", "Engineering", 50000, 2000, 10, -5));
        }

        [Fact]
        public void not_be_constructed_with_negative_work_hours()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                new Employee("Jane Doe", "Engineering", 50000, -2000, 10, 5));
        }

        [Theory]
        [InlineData(5, 50000, 11500)]
        [InlineData(6, 50000, 11500)]
        public void adjusts_the_tax_rate_based_on_years_of_service(
            int yearsOfService, 
            double salary,
            double expectedTaxAmount)
        {
            var employee = new Employee("John Doe", "Engineering", salary, 2000, 10, yearsOfService);
            Assert.Equal(expectedTaxAmount, employee.CalculateTax());
        }
        
        [Fact]
        public void generate_payslips()
        {
            var employee = new Employee("John Doe", "Engineering", 50000, 2000, 10, 5);
            var expectedPaySlip =
                $"Name: John Doe\nDepartment: Engineering\nSalary: 50000\nTax: 11500\n";
            var paySlip = employee.GeneratePaySlip();
            
            Assert.Equal(expectedPaySlip, paySlip);
        }

        [Theory]
        [InlineData(2050, 9, 4, true)]
        [InlineData(2000, 9, 4, false)]
        [InlineData(2050, 10, 4, false)]
        [InlineData(2050, 9, 3, false)]
        public void determine_whether_an_employee_is_eligible_for_promotion(
            int workHours, 
            int vacationDays,
            int yearsOfService, 
            bool expectedEligibility)
        {
            var employee = new Employee("John Doe", "Engineering", 50000, workHours, vacationDays, yearsOfService);
            Assert.Equal(expectedEligibility, employee.IsEligibleForPromotion());
        }
        
        [Theory]
        [InlineData("Engineering", 50000, 5, 11500)]
        [InlineData("HR", 60000, 5, 10800)]
        [InlineData("Sales", 70000, 5, 14000)]
        [InlineData("Marketing", 80000, 5, 15200)]
        [InlineData("Unknown", 90000, 5, 18900)]
        public void calculate_the_correct_tax_for_each_department(
            string department, 
            double salary, 
            int yearsOfService,
            double expectedTaxAmount)
        {
            var employee = new Employee("John Doe", department, salary, 2000, 10, yearsOfService);
            Assert.Equal(expectedTaxAmount, employee.CalculateTax());
        }

        [Theory]
        [InlineData(39, 50000, 0)]
        [InlineData(40, 50000, 0)]
        [InlineData(41, 50000, 1875)]
        public void calculate_the_correct_amount_of_overtime_pay(
            int workHours, 
            double salary,
            double expectedOvertimePay)
        {
            var employee = new Employee("John Doe", "Engineering", salary, workHours, 10, 5);
            Assert.Equal(expectedOvertimePay, employee.CalculateOvertimePay());
        }
        
        [Theory]
        [InlineData(10, 50000, 25000)]
        [InlineData(20, 50000, 40000)]
        public void calculate_the_correct_amount_of_holiday_pay(
            int vacationDays, 
            double salary,
            double expectedVacationPay)
        {
            var employee = new Employee("John Doe", "Engineering", salary, 2000, vacationDays, 5);
            Assert.Equal(expectedVacationPay, employee.CalculateHolidayPay());
        }
        
        [Theory]
        [InlineData(2100, 8, 5, "Engineering", "Outstanding")]
        [InlineData(1850, 10, 4, "HR", "Excellent")]
        [InlineData(1850, 12, 3, "Sales", "Good")]
        [InlineData(1600, 16, 2, "Engineering", "Needs Improvement")]
        public void EvaluatePerformance_Returns_Correct_Rating(
            int workHours, 
            int vacationDays, 
            int yearsOfService, 
            string department, 
            string expectedRating)
        {
            var employee = new Employee("Jane Doe", department, 50000, workHours, vacationDays, yearsOfService);
            Assert.Equal(expectedRating, employee.EvaluatePerformance());
        }

        [Fact]
        public void GeneratePerformanceReport_Returns_Correct_Report()
        {
            var employee = new Employee("Jane Doe", "Engineering", 50000, 2100, 8, 4);
            const string expectedReport =
                $"Employee: Jane Doe\nDepartment: Engineering\nPerformance: Outstanding\nYears of Service: 4\n";
            Assert.Equal(expectedReport, employee.GeneratePerformanceReport());
        }
    }
}