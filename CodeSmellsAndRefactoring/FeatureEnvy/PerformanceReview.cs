namespace CodeSmellsAndRefactoring.FeatureEnvy
{
    public class PerformanceReview
    {
        private readonly Employee _employee;

        public PerformanceReview(Employee employee)
        {
            _employee = employee;
        }

        public string EvaluatePerformance()
        {
            var score = 0;

            score += _employee.WorkHours > 1800 ? 10 : 0;
            score -= _employee.VacationDays > 12 ? 5 : 0;
            score += _employee.YearsOfService > 3 ? 5 : 0;
            score += _employee.WorkHours > 2000 && _employee.Department == "Engineering" ? 10 : 0;
            score -= _employee.VacationDays > 15 ? 5 : 0;

            if (score > 15)
            {
                return "Outstanding";
            }
            else if (score > 10)
            {
                return "Excellent";
            }
            else if (score > 5)
            {
                return "Good";
            }
            else
            {
                return "Needs Improvement";
            }
        }

        public string GeneratePerformanceReport()
        {
            return $"Employee: {_employee.Name}\nDepartment: {_employee.Department}\nPerformance: {EvaluatePerformance()}\nYears of Service: {_employee.YearsOfService}\n";
        }
    }
}
