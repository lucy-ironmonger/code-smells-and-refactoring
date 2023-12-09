namespace CodeSmellsAndRefactoring.FeatureEnvy
{
    public class Employee
    {
        public string Name { get; set; }
        public string Department { get; set; }
        public double Salary { get; set; }
        public int WorkHours { get; set; }
        public int VacationDays { get; set; }
        public int YearsOfService { get; set; } 

        public Employee(string name, string department, double salary, int workHours, int vacationDays, int yearsOfService)
        {
            if (salary <= 0) throw new ArgumentOutOfRangeException(nameof(salary), "Salary must be greater than zero.");
            if (yearsOfService < 0) throw new ArgumentOutOfRangeException(nameof(yearsOfService), "Years of service cannot be negative.");
            if (workHours < 0) throw new ArgumentOutOfRangeException(nameof(workHours), "Work hours cannot be negative.");

            Name = name;
            Department = department;
            Salary = salary;
            WorkHours = workHours;
            VacationDays = vacationDays;
            YearsOfService = yearsOfService;
        }

        private double GetBaseTaxRate()
        {
            return Department switch
            {
                "Engineering" => 0.25,
                "HR" => 0.20,
                "Sales" => 0.22,
                "Marketing" => 0.21,
                _ => 0.23
            };
        }

        private double GetServiceYearsBonus()
        {
            return YearsOfService >= 5 ? 0.02 : 0.0;
        }

        public double CalculateTax()
        {
            var baseTaxRate = GetBaseTaxRate();
            var serviceYearsBonus = GetServiceYearsBonus();

            return double.Round(Salary * (baseTaxRate - serviceYearsBonus)); 
        }

        public string GeneratePaySlip()
        {
            return $"Name: {Name}\nDepartment: {Department}\nSalary: {Salary}\nTax: {CalculateTax()}\n";
        }

        public bool IsEligibleForPromotion()
        {
            var meetsWorkHourCriteria = WorkHours > 2000;
            var meetsVacationCriteria = VacationDays < 10;
            var meetsServiceCriteria = YearsOfService > 3;

            return meetsWorkHourCriteria && meetsVacationCriteria && meetsServiceCriteria;
        }

        public double CalculateOvertimePay()
        {
            if (WorkHours <= 40)
            {
                return 0.0;
            }
            return (WorkHours - 40) * (Salary / 40) * 1.5;
        }

        public double CalculateHolidayPay()
        {
            var vacationRate = VacationDays > 15 ? 0.8 : 1.0;

            return VacationDays * (Salary / 20) * vacationRate;
        }

        public string EvaluatePerformance()
        {
            var score = 0;

            score += WorkHours > 1800 ? 10 : 0;
            score -= VacationDays > 12 ? 5 : 0;
            score += YearsOfService > 3 ? 5 : 0;
            score += WorkHours > 2000 && Department == "Engineering" ? 10 : 0;
            score -= VacationDays > 15 ? 5 : 0;

            if (score > 15)
            {
                return "Outstanding";
            }

            if (score > 10)
            {
                return "Excellent";
            }

            if (score > 5)
            {
                return "Good";
            }

            return "Needs Improvement";
        }

        public string GeneratePerformanceReport()
        {
            return $"Employee: {Name}\nDepartment: {Department}\nPerformance: {EvaluatePerformance()}\nYears of Service: {YearsOfService}\n";
        }
    }
}
