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
        [InlineData(39, 50000, 0)]
        [InlineData(40, 50000, 0)]
        [InlineData(41, 50000, 1875)]
        public void calculate_the_correct_amount_of_overtime_pay(int workHours, double salary, double expectedOvertimePay)
        {
            var employee = new Employee("John Doe", "Engineering", salary, workHours, 10, 5);
            var payroll = new Payroll(employee);

            var overtimePay = payroll.CalculateOvertimePay();

            Assert.Equal(expectedOvertimePay, overtimePay);
        }

        [Fact]
        public void generate_payslips()
        {
            var employee = new Employee("John Doe", "Engineering", 50000, 2000, 10, 5);
            var payroll = new Payroll(employee);
            var expectedPaySlip = $"Name: John Doe\nDepartment: Engineering\nSalary: 50000\nTax: {payroll.CalculateTax()}\n";

            var paySlip = payroll.GeneratePaySlip();

            Assert.Equal(expectedPaySlip, paySlip);
        }

        [Theory]
        [InlineData(10, 50000, 25000)]
        [InlineData(20, 50000, 40000)]
        public void calculate_the_correct_amount_of_holiday_pay(int vacationDays, double salary, double expectedVacationPay)
        {
            var employee = new Employee("John Doe", "Engineering", salary, 2000, vacationDays, 5);
            var payroll = new Payroll(employee);

            var holidayPay = payroll.CalculateHolidayPay();

            Assert.Equal(expectedVacationPay, holidayPay);
        }

        [Theory]
        [InlineData(2050, 9, 4, true)]
        [InlineData(2000, 9, 4, false)]
        [InlineData(2050, 10, 4, false)]
        [InlineData(2050, 9, 3, false)]
        public void determine_whether_an_employee_is_eligible_for_promotion(int workHours, int vacationDays, int yearsOfService, bool expectedEligibility)
        {
            var employee = new Employee("John Doe", "Engineering", 50000, workHours, vacationDays, yearsOfService);
            var payroll = new Payroll(employee);

            var isEligible = payroll.IsEligibleForPromotion();

            Assert.Equal(expectedEligibility, isEligible);
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

        [Fact]
        public void handle_overtime_payment_calculations_with_negative_work_hours()
        {
            var employee = new Employee("Jane Doe", "Engineering", 50000, -2000, 10, 5);
            var payroll = new Payroll(employee);

            Assert.Throws<ArgumentOutOfRangeException>(() => payroll.CalculateOvertimePay());
        }
    }
}
