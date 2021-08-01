using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentsRepository_WPF
{
    class Worker : Employe
    {

        private double salary;
        private const double SALARY_WORKER_PER_HOUR = 12;
        private const double SALARY_INTERN_PER_MONTH = 500;

        public Worker(string firstName, string lastName, DateTime dateOfBirth, EmployeAttribute attribute, Department department) 
            : base(firstName, lastName, dateOfBirth, attribute, department)
        {
            this.salary = Salary;
        }

        public override double Salary
        {
           get
            {
                if (this.salary != default) return this.salary;
                else if (this.Attribute == EmployeAttribute.Worker)
                {
                    byte workingDays = 25;
                    byte workingHours = 8;
                    return SALARY_WORKER_PER_HOUR * workingHours * workingDays;
                }
                else // (this.Attribute == EmployeAttribute.Intern)
                {
                    return SALARY_INTERN_PER_MONTH;
                }
            }

            set { this.salary = value; }
        }


    }
}
