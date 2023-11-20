namespace CodeSmellsAndRefactoring.FeatureEnvy
{
    public class PerformanceReview
    {
        private readonly Employee _employee;

        public PerformanceReview(Employee employee)
        {
            _employee = employee;
        }

        public Employee Employee
        {
            get { return _employee; }
        }
    }
}
