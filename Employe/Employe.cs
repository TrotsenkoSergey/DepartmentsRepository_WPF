using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DepartmentsRepository_WPF
{
    public abstract class Employe : INotifyPropertyChanged // the specified interface gives us the ability
                                                           // to change the values ​​of the TreeView.Item property       
    {

        protected private DateTime creationTime;
        protected private string firstName;
        protected private string lastName;
        protected private DateTime dateOfBirth;
        protected private Department department;
        protected private Department firstDepartment;
        protected private double salary;
        
        public string CreationTime
        {
            get { return $"{this.creationTime.ToShortDateString()}  {this.creationTime.ToShortTimeString()}"; }
        }

        public virtual string FirstName
        {
            get { return this.firstName; }
            set 
            { 
                this.firstName = value;
                FullName = default; // using Interface INotifyPropertyChanged
                OnPropertyChanged(); // using Interface INotifyPropertyChanged
            }
        }

        public virtual string FullName
        {
            get 
            { 
                return $"{this.lastName} {this.firstName}";
            }
            set 
            { 
                OnPropertyChanged(); // using Interface INotifyPropertyChanged
            }
        }

        public virtual string LastName
        {
            get { return this.lastName; }
            set 
            { 
                this.lastName = value;
                FullName = default; // using Interface INotifyPropertyChanged
                OnPropertyChanged(); // using Interface INotifyPropertyChanged
            }
        }

        public string DateOfBirth
        {
            get { return this.dateOfBirth.ToShortDateString(); }
        }

        public virtual Department Department_ { get { return this.department; } }

        public virtual double Salary { get; set; }

        public virtual EmployeAttribute Attribute { get; set; }

        public Employe(string firstName, string lastName, DateTime dateOfBirth, EmployeAttribute attribute, Department department, Department firstDepartment)
        {
            this.creationTime = DateTime.Now;
            this.firstName = firstName;
            this.lastName = lastName;
            this.dateOfBirth = dateOfBirth;
            this.Attribute = attribute;
            this.department = department;
            this.firstDepartment = firstDepartment;
            this.Salary = default;
        }

        // below, interface INotifyPropertyChanged implementation 
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
