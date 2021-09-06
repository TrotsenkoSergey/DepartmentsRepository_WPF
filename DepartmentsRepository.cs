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
        public static Department MainDepartment { get; set; }

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
        public void CreateMainDepartment(string departmentName)
        {
            MainDepartment = new Department(departmentName);
            Departments.Add(MainDepartment);
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
        /// <param name="ancestorDepartment"></param>
        /// <returns></returns>
        public int GetCountOfEmployes(Department ancestorDepartment)
        {
            int count = default;
            int length = ancestorDepartment.Departments.Count;

            for (int i = 0; i < length; i++)
            {
                count += GetCountOfEmployes(ancestorDepartment.Departments[i]);
            }
            count += ancestorDepartment.Employes.Count;

            return count;
        }

        /// <summary>
        /// Getting a specific department by its name.
        /// </summary>
        /// <param name="ancestorDepartment"></param>
        /// <param name="departmentName"></param>
        /// <returns></returns>
        public static Department GetConcreteDepartmentByName(string departmentName, Department ancestorDepartment)
        {
            Department department;
            int length = ancestorDepartment.Departments.Count;
            for (int i = 0; i < length; i++)
            {
                department = GetConcreteDepartmentByName(departmentName, ancestorDepartment.Departments[i]);
                if (department != null) return department;
            }

            return ancestorDepartment.DepartmentName.Contains(departmentName) ? ancestorDepartment : null;
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
