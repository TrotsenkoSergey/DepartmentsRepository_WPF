using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace DepartmentsRepository_WPF
{
    /// <summary>
    ///     Employe entity abstract class.
    /// </summary>
    public abstract class Employe : INotifyPropertyChanged // the specified interface gives us the ability
        // to change the values ​​of the ListView.Item property       
    {
        private protected DateTime dateOfBirth;
        private protected Department firstDepartment;
        private protected string firstName;
        private protected string lastName;
        private protected double salary;

        private protected Employe()
        {
        }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="dateOfBirth"></param>
        /// <param name="attribute"></param>
        /// <param name="department"></param>
        /// <param name="firstDepartment"></param>
        private protected Employe(string firstName, string lastName, DateTime dateOfBirth, EmployeAttribute attribute,
            Department department, Department firstDepartment)
        {
            CreationTime = DateTime.Now;
            this.firstName = firstName;
            this.lastName = lastName;
            this.dateOfBirth = dateOfBirth.Date;
            Attribute = attribute;
            this.Department = department;
            this.firstDepartment = firstDepartment;
            Salary = default;
        }

        /// <summary>
        ///     Time to add to the list.
        /// </summary>
        public virtual DateTime CreationTime
        { get ; set ; }

        /// <summary>
        ///     First name.
        /// </summary>
        public virtual string FirstName
        {
            get => firstName;
            set
            {
                firstName = value;
                FullName = default; // using Interface INotifyPropertyChanged
                OnPropertyChanged(); // using Interface INotifyPropertyChanged
            }
        }

        [JsonIgnore]
        /// <summary>
        /// Last and First name.
        /// </summary>
        public virtual string FullName
        {
            get => $"{lastName} {firstName}";
            set => OnPropertyChanged(); // using Interface INotifyPropertyChanged
        }

        /// <summary>
        ///     Last name.
        /// </summary>
        public virtual string LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                FullName = default; // using Interface INotifyPropertyChanged
                OnPropertyChanged(); // using Interface INotifyPropertyChanged
            }
        }

        /// <summary>
        ///     Date of birth.
        /// </summary>
        public DateTime DateOfBirth
        {
            get => dateOfBirth;
            set => dateOfBirth = value;
        }

        /// <summary>
        ///     Department to which the employee is attached.
        /// </summary>
        public virtual Department Department
        { get ; set ;}

        /// <summary>
        ///     Salary.
        /// </summary>
        public virtual double Salary { get; set; }

        /// <summary>
        ///     Position.
        /// </summary>
        public virtual EmployeAttribute Attribute { get; set; }

        // below, interface INotifyPropertyChanged implementation 
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}