using System;
using System.Collections.ObjectModel;
using System.IO;

namespace DepartmentsRepository_WPF
{
    /// <summary>
    /// Repository of departments and organization of their work.
    /// </summary>
    public class DepartmentsRepository
    {
        
        private Department firstDepartment;

        /// <summary>
        /// Main (first) department.
        /// </summary>
        public Department FirstDepartment { get { return this.firstDepartment; } set { this.firstDepartment = value; } }

        /// <summary>
        /// A collection with a link to the main (first) department (for binding to a TreeView).
        /// </summary>
        public ObservableCollection<Department> Departments { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public DepartmentsRepository() 
        {
            Departments = new ObservableCollection<Department>();
        }

        /// <summary>
        /// Creation of the first (main) department.
        /// </summary>
        /// <param name="departmentName"></param>
        public void CreateFirstDepartment(string departmentName)
        {
            this.firstDepartment = new Department(departmentName);
            this.Departments.Add(this.firstDepartment);
        }

        /// <summary>
        /// Creation of a new department (except for the first-main).
        /// </summary>
        /// <param name="departmentName"></param>
        /// <param name="department"></param>
        public void CreateNewDepartment(string departmentName, Department department)
        {
            department.Departments.Add(new Department(departmentName));
        }

        /// <summary>
        /// Obtaining the number of employees in a specific department and its heirs.
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Getting a specific department by its name.
        /// </summary>
        /// <param name="concreteDepartment"></param>
        /// <param name="departmentName"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Checking the name for correctness.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
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
