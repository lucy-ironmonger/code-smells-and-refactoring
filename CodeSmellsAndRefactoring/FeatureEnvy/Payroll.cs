namespace CodeSmellsAndRefactoring.FeatureEnvy
{
    public class Payroll
    {
        private readonly Employee _employee;

        public Payroll(Employee employee)
        {
            this._employee = employee;
        }

        public double CalculateTax()
        {
            var baseTaxRate = GetBaseTaxRate(_employee.Department);
            var serviceYearsBonus = _employee.YearsOfService >= 5 ? 0.02 : 0.0;

            return double.Round(_employee.Salary * (baseTaxRate - serviceYearsBonus));
        }

        public double CalculateOvertimePay()
        {
            if (_employee.WorkHours <= 40)
            {
                return 0.0;
            }
            return (_employee.WorkHours - 40) * (_employee.Salary / 40) * 1.5;
        }

        public string GeneratePaySlip()
        {
            return $"Name: {_employee.Name}\nDepartment: {_employee.Department}\nSalary: {_employee.Salary}\nTax: {CalculateTax()}\n";
        }

        public double CalculateHolidayPay()
        {
            var vacationRate = _employee.VacationDays > 15 ? 0.8 : 1.0;

            return _employee.VacationDays * (_employee.Salary / 20) * vacationRate;
        }

        public bool IsEligibleForPromotion()
        {
            var meetsWorkHourCriteria = _employee.WorkHours > 2000;
            var meetsVacationCriteria = _employee.VacationDays < 10;
            var meetsServiceCriteria = _employee.YearsOfService > 3;

            return meetsWorkHourCriteria && meetsVacationCriteria && meetsServiceCriteria;
        }

        private static double GetBaseTaxRate(string department)
        {
            return department switch
            {
                "Engineering" => 0.25,
                "HR" => 0.20,
                "Sales" => 0.22,
                "Marketing" => 0.21,
                _ => 0.23
            };
        }
    }
}
