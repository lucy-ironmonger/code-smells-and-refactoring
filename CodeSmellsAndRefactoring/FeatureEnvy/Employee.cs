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
    }
}
