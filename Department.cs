using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace DepartmentsRepository_WPF
{
    public class Department : INotifyPropertyChanged // the specified interface gives us the ability
                                                     // to change the values ​​of the TreeView.Item property       
    {

        private string departmentName;
        private ObservableCollection<Employe> employes;
        private ObservableCollection<Department> departments;

        public DateTime CreationTime { get; private set; }

        public string DepartmentName
        {
            get
            {
                return this.departmentName;
            }
            set
            {
                this.departmentName = value;
                OnPropertyChanged(); // using Interface INotifyPropertyChanged
            }
        }

        public ObservableCollection<Employe> Employes
        {
            get { return this.employes; }
            set
            {
                this.employes = value;
                OnPropertyChanged(); // using Interface INotifyPropertyChanged
            }
        }
                
        public ObservableCollection<Department> Departments 
        {
            get
            {
                return this.departments;
            }
            set
            {
                this.departments = value;
                OnPropertyChanged(); // using Interface INotifyPropertyChanged
            }
        }
                
        public Department(string departmentName)
        {
            this.departmentName = departmentName;
            this.employes = new ObservableCollection<Employe>();
            this.Departments = new ObservableCollection<Department>();
            this.CreationTime = DateTime.Now;
        }

        public void AddEmploye(string firstName, string lastName, DateTime dateOfBirth, EmployeAttribute attribute, Department department, Department firstDepartment)
        {
            if ((int)attribute <= 2) this.Employes.Add(new Manager(firstName, lastName, dateOfBirth, attribute, department, firstDepartment));
            else this.Employes.Add(new Worker(firstName, lastName, dateOfBirth, attribute, department, firstDepartment));
        }

        public void RemoveEmploye(Employe concreteEmploye)
        {
            concreteEmploye.Department_.Employes.Remove(concreteEmploye);
        }

        public Manager GetDirector(Department firstDepartment)
        {
            foreach (Employe employe in firstDepartment.Employes)
            {
                if (employe.Attribute == EmployeAttribute.Director)
                { return employe as Manager; }
            }
            return null;
        }

        public Manager GetDeputyDirector(Department firstDepartment)
        {
            foreach (Employe employe in firstDepartment.Employes)
            {
                if (employe.Attribute == EmployeAttribute.Deputy_Director)
                { return employe as Manager; }
            }
            return null;
        }

        public Manager GetHeadOfDepartment(Department department)
        {
            foreach (Employe employe in department.Employes)
            {
                if (employe.Attribute == EmployeAttribute.Head_Of_Department)
                { return employe as Manager; }
            }
            return null;
        }

        public ObservableCollection<Department> GetListOfDepartments(Department concreteDepartment)
        {
            ObservableCollection<Department> listOfDepartments = new ObservableCollection<Department>();
            ObservableCollection<Department> tempListOfDepartments = new ObservableCollection<Department>();

            int i = concreteDepartment.Departments.Count;

            for (--i; i >= 0; i--)
            {
                tempListOfDepartments = GetListOfDepartments(concreteDepartment.Departments[i]);
                foreach (Department department in tempListOfDepartments)
                {
                    listOfDepartments.Add(department);
                }
            }
            listOfDepartments.Add(concreteDepartment);

            return listOfDepartments;
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

        public void SortEmployeByLastName(Department department)
        {
            department.Employes = new ObservableCollection<Employe>(department.Employes.OrderBy(x => x.LastName));
        }

        public void SortEmployeBySalary(Department department)
        {
            department.Employes = new ObservableCollection<Employe>(department.Employes.OrderBy(x => x.Salary));
        }

        public void SortEmployeByDateOfBirth(Department department)
        {
            department.Employes = new ObservableCollection<Employe>(department.Employes.OrderBy(x => x.DateOfBirth));
        }

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
