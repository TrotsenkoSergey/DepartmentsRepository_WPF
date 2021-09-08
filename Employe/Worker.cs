using System;

namespace DepartmentsRepository_WPF
{
    public class Worker : Employee
    {

        private const double SALARY_WORKER_PER_HOUR = 12;
        private const double SALARY_INTERN_PER_MONTH = 500;

        public Worker() { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="dateOfBirth"></param>
        /// <param name="attribute"></param>
        /// <param name="department"></param>
        /// <param name="firstDepartment"></param>
        public Worker(string firstName, string lastName, DateTime dateOfBirth, EmployeAttribute attribute, Department department)
            : base(firstName, lastName, dateOfBirth, attribute, department)
        {
            Department parentDepartment = department;
            while (parentDepartment != DepartmentsRepository.MainDepartment) 
            {
                parentDepartment.GetHeadOfDepartment(parentDepartment).Salary = default;
                parentDepartment = parentDepartment.GetDepartmentAncestor(parentDepartment, DepartmentsRepository.MainDepartment);
            }

            department.GetDeputyDirector(DepartmentsRepository.MainDepartment).Salary = default;
            department.GetDirector(DepartmentsRepository.MainDepartment).Salary = default;
        }

        /// <summary>
        /// Salary.
        /// </summary>
        public override double Salary
        {
            get
            {
                return this.salary;
            }
            set
            {
                if (this.Attribute == EmployeAttribute.Worker)
                {
                    byte workingDays = 25;
                    byte workingHours = 8;
                    this.salary = SALARY_WORKER_PER_HOUR * workingHours * workingDays;
                }
                else // (this.Attribute == EmployeAttribute.Intern)
                {
                    this.salary = SALARY_INTERN_PER_MONTH;
                }
            }
        }


    }
}
