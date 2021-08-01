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

        public ObservableCollection<Employe> Employes { get; set; }

        public int CountOfEmployes 
        { 
            get 
            { 
                return Employes.Count; 
            } 
        }

        public Department(string departmentName)
        {
            this.DepartmentName = departmentName;
            this.Employes = new ObservableCollection<Employe>();
            this.Departments = new ObservableCollection<Department>();
            this.CreationTime = DateTime.Now;
        }

        // below, interface INotifyPropertyChanged implementation 
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void AddEmploye(string firstName, string lastName, DateTime dateOfBirth, EmployeAttribute attribute, Department department)
        {
            if ((int)attribute <= 2) this.Employes.Add(new Manager(firstName, lastName, dateOfBirth, attribute, department));
            else this.Employes.Add(new Worker(firstName, lastName, dateOfBirth, attribute, department));
        }

    }
}
