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
            Name = name;
            Department = department;
            Salary = salary;
            WorkHours = workHours;
            VacationDays = vacationDays;
            YearsOfService = yearsOfService;
            
            if (this.Salary <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(this.Salary), "Salary must be greater than zero.");
            }

            if (this.YearsOfService < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(this.YearsOfService), "Years of service cannot be negative.");
            }
            
            if (this.WorkHours < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(this.WorkHours), "Work hours cannot be negative.");
            }
        }

        public bool IsEligibleForPromotion()
        {
            var meetsWorkHourCriteria = this.WorkHours > 2000;
            var meetsVacationCriteria = this.VacationDays < 10;
            var meetsServiceCriteria = this.YearsOfService > 3;

            return meetsWorkHourCriteria && meetsVacationCriteria && meetsServiceCriteria;
        }

        public double CalculateOvertimePay()
        {
            if (this.WorkHours <= 40)
            {
                return 0.0;
            }
            return (this.WorkHours - 40) * (this.Salary / 40) * 1.5;
        }

        public double CalculateHolidayPay()
        {
            var vacationRate = this.VacationDays > 15 ? 0.8 : 1.0;

            return this.VacationDays * (this.Salary / 20) * vacationRate;
        }
    }
}
