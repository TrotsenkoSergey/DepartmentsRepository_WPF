﻿using System;

namespace DepartmentsRepository_WPF
{
    public class Worker : Employe
    {

        private const double SALARY_WORKER_PER_HOUR = 12;
        private const double SALARY_INTERN_PER_MONTH = 500;

        public Worker(string firstName, string lastName, DateTime dateOfBirth, EmployeAttribute attribute, Department department, Department firstDepartment)
            : base(firstName, lastName, dateOfBirth, attribute, department, firstDepartment)
        {
            Department parentDepartment = department;
            while (parentDepartment != firstDepartment) 
            {
                parentDepartment.GetHeadOfDepartment(parentDepartment).Salary = default;
                parentDepartment = parentDepartment.GetDepartmentAncestor(parentDepartment, firstDepartment);
            }

            department.GetDeputyDirector(firstDepartment).Salary = default;
            department.GetDirector(firstDepartment).Salary = default;
        }

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
