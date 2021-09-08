using System;
using Newtonsoft.Json;

namespace DepartmentsRepository_WPF
{
    public class Manager : Employee
    {
        private const double DIRECTOR_SALARY_RATIO = 0.15;
        private const double DIRECTOR_MIN_SALARY = 3000;

        private const double DEPUTY_DIRECTOR_SALARY_RATIO = 0.05;
        private const double DEPUTY_DIRECTOR_MIN_SALARY = 2000;

        private const double HEAD_OF_DEPARTMENT_SALARY_RATIO = 0.15;
        private const double HEAD_OF_DEPARTMENT_MIN_SALARY = 1300;

        public Manager() { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="dateOfBirth"></param>
        /// <param name="attribute"></param>
        /// <param name="department"></param>
        /// <param name="firstDepartment"></param>
        public Manager(string firstName, string lastName, DateTime dateOfBirth, EmployeAttribute attribute, Department department)
             : base(firstName, lastName, dateOfBirth, attribute, department)
        {
            if (attribute == EmployeAttribute.Head_Of_Department)
            {
                Department parentDepartment = department;
                while (parentDepartment != DepartmentsRepository.MainDepartment)
                {
                    parentDepartment = parentDepartment.GetDepartmentAncestor(parentDepartment, DepartmentsRepository.MainDepartment);
                    if (parentDepartment != DepartmentsRepository.MainDepartment && parentDepartment.GetHeadOfDepartment(parentDepartment) != null)
                    { parentDepartment.GetHeadOfDepartment(parentDepartment).Salary = default; }
                }

                department.GetDeputyDirector(DepartmentsRepository.MainDepartment).Salary = default;
                department.GetDirector(DepartmentsRepository.MainDepartment).Salary = default;
            }
        }

        /// <summary>
        /// Salary.
        /// </summary>
        public override double Salary
        {
            get => salary;
            set
            {
                double salaryRatio = default;
                double minSalary = default;

                if (Attribute == EmployeAttribute.Director)
                {
                    salaryRatio = DIRECTOR_SALARY_RATIO;
                    minSalary = DIRECTOR_MIN_SALARY;
                }
                else if (Attribute == EmployeAttribute.Deputy_Director)
                {
                    salaryRatio = DEPUTY_DIRECTOR_SALARY_RATIO;
                    minSalary = DEPUTY_DIRECTOR_MIN_SALARY;
                }
                else if (Attribute == EmployeAttribute.Head_Of_Department)
                {
                    salaryRatio = HEAD_OF_DEPARTMENT_SALARY_RATIO;
                    minSalary = HEAD_OF_DEPARTMENT_MIN_SALARY;
                }

                if (value == default)
                {
                    Department department =
                        DepartmentsRepository.GetConcreteDepartmentByName(DepName,
                            DepartmentsRepository.MainDepartment);
                    double salary = GetSalaryOfEmployes(department) * salaryRatio;
                }

                if (value > 0) minSalary = value;
                salary = (salary < minSalary) ? minSalary : salary;
                salary = Math.Round(salary, 2);

                OnPropertyChanged(); // using Interface INotifyPropertyChanged
            }
        }

        /// <summary>
        /// Calculate the salaries of all employees of the current department and its descendants.
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        private double GetSalaryOfEmployes(Department department)
        {
            double salaryOfEmployes = default;
            int lengthOfDepartments = department.Departments.Count;
            int lengthOfEmployes = department.Employes.Count;

            for (int i = 0; i < lengthOfDepartments; i++)
            {
                salaryOfEmployes += GetSalaryOfEmployes(department.Departments[i]);
                //if (i == 0) salaryOfEmployes -= department.Departments[i].GetHeadOfDepartment(department.Departments[i]).Salary;
            }

            for (int j = 0; j < lengthOfEmployes; j++)
            {
                salaryOfEmployes += department.Employes[j].Salary;
            }
            return salaryOfEmployes;
        }
    }
}
