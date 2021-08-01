using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentsRepository_WPF
{
    public class DepartmentsRepository
    {
        private readonly string path;
        private Department firstDepartment;
        public Department FirstDepartment { get { return this.firstDepartment; } private set { this.firstDepartment = value; } }

        //public ObservableCollection<Employe> Employes { get; private set; }

        public ObservableCollection<Department> Departments { get; private set; }

        public DepartmentsRepository()
        {
            //Employes = new ObservableCollection<Employe>();
            //Departments = new ObservableCollection<Department>();
        }

        public DepartmentsRepository(string path) : this()
        {
            this.path = path;
            this.Load(path);
        }

        private void Load(string path)
        {
            string json = File.ReadAllText(path);
            var departmentsRepository = JsonConvert.DeserializeObject<DepartmentsRepository>(path);
            //this.Departments = departmentsRepository.Departments;
            //this.Employes = departmentsRepository.Employes;
        }

        public void Save(string path, DepartmentsRepository departmentsRepository)
        {
            string json = JsonConvert.SerializeObject(departmentsRepository);
            File.WriteAllText(path, json);
        }

        public void CreateNewEmploye(string firstName, string lastName, DateTime dateOfBirth, EmployeAttribute attribute, Department department)
        {
            //int count = Employes.Count;

            //if ((int)attribute <= 2) this.Employes.Add(new Manager(firstName, lastName, dateOfBirth, attribute, department));
            //else this.Employes.Add(new Worker(firstName, lastName, dateOfBirth, attribute, department));

            //department.Employes.Add(Employes[count]);
        }

        public void CreateFirstDepartment(string departmentName)
        {
            //this.Departments.Add(new Department(departmentName));
            this.firstDepartment = new Department(departmentName);
            Departments = new ObservableCollection<Department>();
            Departments.Add(this.firstDepartment);
        }

        public void CreateNewDepartment(string departmentName, Department department)
        {
            //int count = Departments.Count;

            //this.Departments.Add(new Department(departmentName));

            //department.Departments.Add(Departments[count]);

            department.Departments.Add(new Department(departmentName));
        }

        public int GetCountOfEmployes(Department department)
        {
            int count = default;
            int length = department.Departments.Count;

            for (int i = 0; i < length; i++)
            {
                count = +GetCountOfEmployes(department.Departments[i]);
            }
            count = +department.Employes.Count;

            return count;
        }

        public ObservableCollection<Department> GetListOfDepartments(Department concreteDepartment)
        {
            ObservableCollection<Department> listOfDepartments = new ObservableCollection<Department>();
            ObservableCollection<Department> tempList = new ObservableCollection<Department>();

            int length = concreteDepartment.Departments.Count;

            for (int i = 0; i < length; i++)
            {
                tempList = GetListOfDepartments(concreteDepartment.Departments[i]);
                foreach (var item in tempList)
                {
                    listOfDepartments.Add(item);
                }
            }
            listOfDepartments.Add(concreteDepartment);

            return listOfDepartments;
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

        public Department GetDepartmentAncestor(Department childDepartment, Department parentDepartment)
        {
            Department searchDepartment = default;
            var departments = parentDepartment.Departments;

            foreach (var department in departments)
            {
                if (department == childDepartment)
                {
                    return parentDepartment;
                }
                else if (department.Departments.Count != 0)
                {
                    searchDepartment = GetDepartmentAncestor(childDepartment, department);
                }
            }


            return searchDepartment;
        }

    }
}
