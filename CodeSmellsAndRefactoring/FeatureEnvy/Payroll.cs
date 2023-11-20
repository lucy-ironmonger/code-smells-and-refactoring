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

        public string GeneratePaySlip()
        {
            return $"Name: {_employee.Name}\nDepartment: {_employee.Department}\nSalary: {_employee.Salary}\nTax: {CalculateTax()}\n";
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
