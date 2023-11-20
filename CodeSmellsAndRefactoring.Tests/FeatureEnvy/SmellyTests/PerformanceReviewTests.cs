using CodeSmellsAndRefactoring.FeatureEnvy;
using Xunit;

namespace CodeSmellsAndRefactoring.Tests.FeatureEnvy.SmellyTests
{
    // What impact does the name of the test class have on the name of the test methods?
    // What happens if EvaluatePerformance or GeneratePerformanceReport are renamed?
    // https://connascence.io/name.html
    public class PerformanceReviewTests
    {
        [Theory]
        [InlineData(2100, 8, 5, "Engineering", "Outstanding")]
        [InlineData(1850, 10, 4, "HR", "Excellent")]
        [InlineData(1850, 12, 3, "Sales", "Good")]
        [InlineData(1600, 16, 2, "Engineering", "Needs Improvement")]
        public void EvaluatePerformance_Returns_Correct_Rating(int workHours, int vacationDays, int yearsOfService, string department, string expectedRating)
        {
            var employee = new Employee("Jane Doe", department, 50000, workHours, vacationDays, yearsOfService);
            var review = new PerformanceReview(employee);

            var rating = review.Employee.EvaluatePerformance();

            Assert.Equal(expectedRating, rating);
        }

        [Fact]
        public void GeneratePerformanceReport_Returns_Correct_Report()
        {
            var employee = new Employee("Jane Doe", "Engineering", 50000, 2100, 8, 4);
            var review = new PerformanceReview(employee);

            const string expectedReport = $"Employee: Jane Doe\nDepartment: Engineering\nPerformance: Outstanding\nYears of Service: 4\n";

            var report = review.Employee.GeneratePerformanceReport(review);

            Assert.Equal(expectedReport, report);
        }
    }
}
