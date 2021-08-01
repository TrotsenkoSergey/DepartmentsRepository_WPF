using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentsRepository_WPF
{
    class Manager : Employe
    {
        private const double DIRECTOR_SALARY_RATIO = 0.15;
        private const double DIRECTOR_MIN_SALARY = 3000;

        private const double DEPUTY_DIRECTOR_SALARY_RATIO = 0.05;
        private const double DEPUTY_DIRECTOR_MIN_SALARY = 2000;

        private const double HEAD_OF_DEPARTMENT_SALARY_RATIO = 0.15;
        private const double HEAD_OF_DEPARTMENT_MIN_SALARY = 1300;

        public Manager(string firstName, string lastName, DateTime dateOfBirth, EmployeAttribute attribute, Department department)
             : base(firstName, lastName, dateOfBirth, attribute, department)
        {
        }

        public override double Salary
        {
            get
            {
                double salaryRatio = default;
                double minSalary = default;

                if (this.Attribute == EmployeAttribute.Director)
                {
                    salaryRatio = DIRECTOR_SALARY_RATIO;
                    minSalary = DIRECTOR_MIN_SALARY;
                }
                else if (this.Attribute == EmployeAttribute.Deputy_Director)
                {
                    salaryRatio = DEPUTY_DIRECTOR_SALARY_RATIO;
                    minSalary = DEPUTY_DIRECTOR_MIN_SALARY;
                }
                else if (this.Attribute == EmployeAttribute.Head_Of_Department)
                {
                    salaryRatio = HEAD_OF_DEPARTMENT_SALARY_RATIO;
                    minSalary = HEAD_OF_DEPARTMENT_MIN_SALARY;
                }

                double salary = GetSalaryOfEmployes(this.department) * salaryRatio;
                return (salary < minSalary) ? minSalary : salary;
            }
        }

        private double GetSalaryOfEmployes(Department department)
        {
            double salaryOfEmployes = default;
            int lengthOfDepartments = department.Departments.Count;
            int lengthOfEmloyes = department.Employes.Count;

            for (int i = 0; i < lengthOfDepartments; i++)
            {
                salaryOfEmployes = +GetSalaryOfEmployes(department.Departments[i]);
            }

            for (int j = 0; j < lengthOfEmloyes; j++)
            {
                salaryOfEmployes = +department.Employes[j].Salary;
            }
            return salaryOfEmployes;
        }

        

    }
}
