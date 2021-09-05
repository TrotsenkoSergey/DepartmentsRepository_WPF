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

        /// <summary>
        /// Main (first) department.
        /// </summary>
        public Department FirstDepartment { get; set; }

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
            FirstDepartment = new Department(departmentName);
            Departments.Add(FirstDepartment);
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
        /// <param name="firstDepartment"></param>
        /// <param name="departmentName"></param>
        /// <returns></returns>
        public Department GetConcreteDepartmentByName(string departmentName, Department firstDepartment)
        {
            int length = firstDepartment.Departments.Count;
            for (int i = 0; i < length; i++)
            {
                GetConcreteDepartmentByName(departmentName, firstDepartment.Departments[i]);
            }

            return firstDepartment.DepartmentName.Contains(departmentName) ? firstDepartment : null;
        }

        /// <summary>
        /// Checking the name for correctness.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static bool IsCorrectName(string name, out string error)
        {
            if (String.IsNullOrEmpty(name))
            {
                error = "is empty";
                return false;
            }

            if (name.Length < 4)
            {
                error = "is invalid, less then four letters";
                return false;
            }

            char[] nameChar = name.ToCharArray();
            foreach (char element in nameChar)
            {
                if (!Char.IsLetter(element))
                {
                    error = "incorrect, consists not only of letters";
                    return false;
                }
            }

            error = "is correct";
            return true;
        }
    }
}
