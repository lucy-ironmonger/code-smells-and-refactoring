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
    }
}
