using System;

namespace DepartmentsRepository_WPF
{
    public class Manager : Employe
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
        public Manager(string firstName, string lastName, DateTime dateOfBirth, EmployeAttribute attribute, Department department, Department firstDepartment)
             : base(firstName, lastName, dateOfBirth, attribute, department, firstDepartment)
        {
            if (attribute == EmployeAttribute.Head_Of_Department)
            {
                Department parentDepartment = department;
                while (parentDepartment != firstDepartment)
                {
                    parentDepartment = parentDepartment.GetDepartmentAncestor(parentDepartment, firstDepartment);
                    if (parentDepartment != firstDepartment)
                    { parentDepartment.GetHeadOfDepartment(parentDepartment).Salary = default; }
                }

                department.GetDeputyDirector(firstDepartment).Salary = default;
                department.GetDirector(firstDepartment).Salary = default;
            }
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
                salary = (salary < minSalary) ? minSalary : salary;
                this.salary = Math.Round(salary, 2);
                
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
            int lengthOfEmloyes = department.Employes.Count;

            for (int i = 0; i < lengthOfDepartments; i++)
            {
                salaryOfEmployes += GetSalaryOfEmployes(department.Departments[i]);
            }

            for (int j = 0; j < lengthOfEmloyes; j++)
            {
                salaryOfEmployes += department.Employes[j].Salary; 
            }
            return salaryOfEmployes;
        }
    }
}
