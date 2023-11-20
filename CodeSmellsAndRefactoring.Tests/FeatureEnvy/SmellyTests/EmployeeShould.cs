using CodeSmellsAndRefactoring.FeatureEnvy;
using Xunit;

namespace CodeSmellsAndRefactoring.Tests.FeatureEnvy.SmellyTests
{
    // Do we need this test?
    // How else could we tell if an Employee was constructed correctly?
    // A: Could we put in invariance checks into the constructor to establish the rules, and then have the tests monitor for any 
    // exceptions thrown?
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
        
        [Fact]
        public void handle_overtime_payment_calculations_with_negative_work_hours()
        {
            var employee = new Employee("Jane Doe", "Engineering", 50000, -2000, 10, 5);
            var payroll = new Payroll(employee);

            Assert.Throws<ArgumentOutOfRangeException>(() => employee.CalculateOvertimePay());
        }
        
        [Theory]
        [InlineData(2050, 9, 4, true)]
        [InlineData(2000, 9, 4, false)]
        [InlineData(2050, 10, 4, false)]
        [InlineData(2050, 9, 3, false)]
        public void determine_whether_an_employee_is_eligible_for_promotion(int workHours, int vacationDays, int yearsOfService, bool expectedEligibility)
        {
            var employee = new Employee("John Doe", "Engineering", 50000, workHours, vacationDays, yearsOfService);

            var isEligible = employee.IsEligibleForPromotion();

            Assert.Equal(expectedEligibility, isEligible);
        }
        
        [Theory]
        [InlineData(10, 50000, 25000)]
        [InlineData(20, 50000, 40000)]
        public void calculate_the_correct_amount_of_holiday_pay(int vacationDays, double salary, double expectedVacationPay)
        {
            var employee = new Employee("John Doe", "Engineering", salary, 2000, vacationDays, 5);

            var holidayPay = employee.CalculateHolidayPay();

            Assert.Equal(expectedVacationPay, holidayPay);
        }
        
        [Fact]
        public void generate_payslips()
        {
            var employee = new Employee("John Doe", "Engineering", 50000, 2000, 10, 5);
            var payroll = new Payroll(employee);
            var expectedPaySlip = $"Name: John Doe\nDepartment: Engineering\nSalary: 50000\nTax: {payroll.CalculateTax()}\n";

            var paySlip = employee.GeneratePaySlip(payroll);

            Assert.Equal(expectedPaySlip, paySlip);
        }
        
        [Theory]
        [InlineData(39, 50000, 0)]
        [InlineData(40, 50000, 0)]
        [InlineData(41, 50000, 1875)]
        public void calculate_the_correct_amount_of_overtime_pay(int workHours, double salary, double expectedOvertimePay)
        {
            var employee = new Employee("John Doe", "Engineering", salary, workHours, 10, 5);

            var overtimePay = employee.CalculateOvertimePay();

            Assert.Equal(expectedOvertimePay, overtimePay);
        }
    }
}
