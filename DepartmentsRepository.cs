using System;
using System.Collections.ObjectModel;
using System.IO;

namespace DepartmentsRepository_WPF
{
    public class DepartmentsRepository
    {
        
        private Department firstDepartment;

        public Department FirstDepartment { get { return this.firstDepartment; } set { this.firstDepartment = value; } }

        public ObservableCollection<Department> Departments { get; set; }

        public DepartmentsRepository()
        {
            Departments = new ObservableCollection<Department>();
        }

        public void CreateFirstDepartment(string departmentName)
        {
            this.firstDepartment = new Department(departmentName);
            this.Departments.Add(this.firstDepartment);
        }

        public void CreateNewDepartment(string departmentName, Department department)
        {
            department.Departments.Add(new Department(departmentName));
        }

        public int GetCountOfEmployes(Department department)
        {
            int count = default;
            int length = department.Departments.Count;

            for (int i = 0; i < length; i++)
            {
                count += GetCountOfEmployes(department.Departments[i]);
            }
            count += department.Employes.Count;

            return count;
        }

        public Department GetConcreteDepartmentByName(Department concreteDepartment, string departmentName)
        {
            int length = concreteDepartment.Departments.Count;
            for (int i = 0; i < length; i++)
            {
                GetConcreteDepartmentByName(concreteDepartment.Departments[i], departmentName);
            }
            if (concreteDepartment.DepartmentName.Contains(departmentName)) return concreteDepartment;
            else return null;
        }

        public const string INVALID_NAME = "Invalid value entered. The name must contain only letters, at least '4'. Try again.";

        public bool IsCorrectName(string name)
        {
            bool isVarCorrect = false;
            if (String.IsNullOrEmpty(name)) return false;
            
            char[] nameChar = name.ToCharArray();
            if (nameChar.Length < 4) return false;

            foreach (char element in nameChar)
            {
                isVarCorrect = Char.IsLetter(element);
                if (!isVarCorrect) break;
            }
            return isVarCorrect;
        }
    }
}
