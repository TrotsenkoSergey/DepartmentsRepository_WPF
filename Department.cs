using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentsRepository_WPF
{
    public class Department : INotifyPropertyChanged // the specified interface gives us the ability to change the values ​​of the TreeView.Item property       
    {

        private string departmentName;
        private ObservableCollection<Employe> employes;

        public DateTime CreationTime { get; private set; }

        public ObservableCollection<Department> Departments { get; set; }

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

        public int CountOfEmployes
        {
            get
            {
                return Employes.Count;
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

        public void SortEmployeByLastName(Department department)
        {
            department.Employes = new ObservableCollection<Employe>(department.Employes.OrderBy(x => x.LastName));
        }

        public void SortEmployeBySalary(Department department)
        {
            department.Employes = new ObservableCollection<Employe>(department.Employes.OrderBy(x => x.Salary));
        }

        // below, interface INotifyPropertyChanged implementation 
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
