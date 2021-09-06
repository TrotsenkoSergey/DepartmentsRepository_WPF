using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace DepartmentsRepository_WPF
{
    /// <summary>
    /// Represents the essence of the Department.
    /// </summary>
    public class Department : INotifyPropertyChanged // the specified interface gives us the ability
                                                     // to change the values ​​of the TreeView.Item property       
    {

        private string departmentName;
        private ObservableCollection<Employe> employes;
        private ObservableCollection<Department> departments;
       
        public Department()
        {
            this.employes = new ObservableCollection<Employe>();
            this.Departments = new ObservableCollection<Department>();
        }

        /// <summary>
        /// Date the department was added to the repository.
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// Department name.
        /// </summary>
        public string DepartmentName
        {
            get => this.departmentName;
            set
            {
                this.departmentName = value;
                if (Employes.Count != 0)
                    foreach (Employe employe in Employes)
                    {
                        employe.DepName = DepartmentName;
                    }
                OnPropertyChanged(); // using Interface INotifyPropertyChanged
            }
        }

        /// <summary>
        /// Department employees.
        /// </summary>
        public ObservableCollection<Employe> Employes
        {
            get => this.employes;
            set
            {
                this.employes = value;
                OnPropertyChanged(); // using Interface INotifyPropertyChanged
            }
        }

        /// <summary>
        /// Subdivisions of the department (subdepartments).
        /// </summary>
        public ObservableCollection<Department> Departments
        {
            get => this.departments;
            set
            {
                this.departments = value;
                OnPropertyChanged(); // using Interface INotifyPropertyChanged
            }
        }

        /// <summary>
        /// Department constructor.
        /// </summary>
        /// <param name="departmentName"></param>
        public Department(string departmentName) : this()
        {
            this.CreationTime = DateTime.Now;
            this.departmentName = departmentName;
        }

        /// <summary>
        /// Adds a specific worker.
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="dateOfBirth"></param>
        /// <param name="attribute"></param>
        /// <param name="department"></param>
        /// <param name="firstDepartment"></param>
        public void AddEmploye(string firstName, string lastName, DateTime dateOfBirth,
            EmployeAttribute attribute, Department department, Department firstDepartment)
        {
            if ((int)attribute <= 2) this.Employes.Add(new Manager(firstName, lastName,
                dateOfBirth, attribute, department));
            else this.Employes.Add(new Worker(firstName, lastName, dateOfBirth,
                attribute, department));
        }

        /// <summary>
        /// Removes a specific worker from the list.
        /// </summary>
        /// <param name="concreteEmploye"></param>
        public void RemoveEmploye(Employe concreteEmploye)
        {
            Employes.Remove(concreteEmploye);
        }

        /// <summary>
        /// Get the Director of the Main Department.
        /// </summary>
        /// <param name="firstDepartment"></param>
        /// <returns></returns>
        public Manager GetDirector(Department firstDepartment)
        {
            foreach (Employe employe in firstDepartment.Employes)
            {
                if (employe.Attribute == EmployeAttribute.Director)
                { return employe as Manager; }
            }
            return null;
        }

        /// <summary>
        /// Get the Deputy Director of the Main Department.
        /// </summary>
        /// <param name="firstDepartment"></param>
        /// <returns></returns>
        public Manager GetDeputyDirector(Department firstDepartment)
        {
            foreach (Employe employe in firstDepartment.Employes)
            {
                if (employe.Attribute == EmployeAttribute.Deputy_Director)
                { return employe as Manager; }
            }
            return null;
        }

        /// <summary>
        /// Get the head of a specific department.
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public Manager GetHeadOfDepartment(Department department)
        {
            foreach (Employe employe in department.Employes)
            {
                if (employe.Attribute == EmployeAttribute.Head_Of_Department)
                { return employe as Manager; }
            }
            return null;
        }

        /// <summary>
        /// Get a list of existing nested departments.
        /// </summary>
        /// <param name="concreteDepartment"></param>
        /// <returns></returns>
        public ObservableCollection<Department> GetListOfDepartments(Department concreteDepartment)
        {
            var listOfDepartments = new ObservableCollection<Department>();
            
            int i = concreteDepartment.Departments.Count;
            for (--i; i >= 0; i--)
            {
                var tempListOfDepartments = GetListOfDepartments(concreteDepartment.Departments[i]);
                foreach (Department department in tempListOfDepartments)
                {
                    listOfDepartments.Add(department);
                }
            }
            listOfDepartments.Add(concreteDepartment);

            return listOfDepartments;
        }

        /// <summary>
        /// Get the ancestor of a specific department.
        /// </summary>
        /// <param name="childDepartment">Specific department</param>
        /// <param name="parentDepartment">Main department.</param>
        /// <returns></returns>
        public Department GetDepartmentAncestor(Department childDepartment, Department parentDepartment)
        {
            Department searchDepartment = default;
            var departments = parentDepartment.Departments;

            foreach (var department in departments)
            {
                if (department.departmentName == childDepartment.departmentName)
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

        /// <summary>
        /// Sort by last name.
        /// </summary>
        /// <param name="department"></param>
        public void SortEmployeByLastName(Department department)
        {
            department.Employes = new ObservableCollection<Employe>(department.Employes.OrderBy(x => x.LastName));
        }

        /// <summary>
        /// Sort by salary.
        /// </summary>
        /// <param name="department"></param>
        public void SortEmployeBySalary(Department department)
        {
            department.Employes = new ObservableCollection<Employe>(department.Employes.OrderBy(x => x.Salary));
        }

        /// <summary>
        /// Sort by date of birth.
        /// </summary>
        /// <param name="department"></param>
        public void SortEmployeByDateOfBirth(Department department)
        {
            department.Employes = new ObservableCollection<Employe>(department.Employes.OrderBy(x => x.DateOfBirth));
        }

        /// <summary>
        /// Sort by date added to app.
        /// </summary>
        /// <param name="department"></param>
        public void SortEmployeByCreationTime(Department department)
        {
            department.Employes = new ObservableCollection<Employe>(department.Employes.OrderBy(x => x.CreationTime));
        }

        // below, interface INotifyPropertyChanged implementation 
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
