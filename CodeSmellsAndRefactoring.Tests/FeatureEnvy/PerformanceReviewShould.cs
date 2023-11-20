using CodeSmellsAndRefactoring.FeatureEnvy;
using Xunit;

namespace CodeSmellsAndRefactoring.Tests.FeatureEnvy
{
    public class PerformanceReviewShould
    {
        [Theory]
        [InlineData(2100, 8, 5, "Engineering", "Outstanding")]
        [InlineData(1850, 10, 4, "HR", "Excellent")]
        [InlineData(1850, 12, 3, "Sales", "Good")]
        [InlineData(1600, 16, 2, "Engineering", "Needs Improvement")]
        public void calculate_the_correct_performance_rating(int workHours, int vacationDays, int yearsOfService, string department, string expectedRating)
        {
            var employee = new Employee("Jane Doe", department, 50000, workHours, vacationDays, yearsOfService);
            var review = new PerformanceReview(employee);

            Assert.Equal(expectedRating, review.Employee.EvaluatePerformance());
        }

        [Fact]
        public void generate_performance_reports()
        {
            var employee = new Employee("Jane Doe", "Engineering", 50000, 2100, 8, 4);
            var review = new PerformanceReview(employee);

            const string expectedReport = $"Employee: Jane Doe\nDepartment: Engineering\nPerformance: Outstanding\nYears of Service: 4\n";

            Assert.Equal(expectedReport, review.Employee.GeneratePerformanceReport(review));
        }
    }
}
