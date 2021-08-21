using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DepartmentsRepository_WPF
{
    /// <summary>
    /// Employe entity abstract class.
    /// </summary>
    public class Employe : INotifyPropertyChanged // the specified interface gives us the ability
                                                           // to change the values ​​of the ListView.Item property       
    {

        protected private DateTime creationTime;
        protected private string firstName;
        protected private string lastName;
        protected private DateTime dateOfBirth;
        protected private Department department;
        protected private Department firstDepartment;
        protected private double salary;

        /// <summary>
        /// Time to add to the list.
        /// </summary>
        public DateTime CreationTime
        {
            get { return this.creationTime; }
        }

        /// <summary>
        /// First name.
        /// </summary>
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

        /// <summary>
        /// Last and First name.
        /// </summary>
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

        /// <summary>
        /// Last name.
        /// </summary>
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

        /// <summary>
        /// Date of birth.
        /// </summary>
        public DateTime DateOfBirth
        {
            get { return this.dateOfBirth.Date; }
        }

        //[JsonIgnore]
        /// <summary>
        /// Department to which the employee is attached.
        /// </summary>
        public virtual Department Department_ { get { return this.department; } }

        /// <summary>
        /// Salary.
        /// </summary>
        public virtual double Salary { get; set; }

        /// <summary>
        /// Position.
        /// </summary>
        public virtual EmployeAttribute Attribute { get; set; }

        public Employe() { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="dateOfBirth"></param>
        /// <param name="attribute"></param>
        /// <param name="department"></param>
        /// <param name="firstDepartment"></param>
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
