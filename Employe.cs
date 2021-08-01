using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentsRepository_WPF
{
    public abstract class Employe
    {
        protected private DateTime creationTime;
        protected private string firstName;
        protected private string lastName;
        protected private DateTime dateOfBirth;
        protected private Department department;

        public DateTime CreationTime
        {
            get { return this.creationTime; }
        }

        public virtual string FirstName
        {
            get { return this.firstName; }
            set { this.firstName = value; }
        }

        public virtual string LastName
        {
            get { return this.lastName; }
            set { this.lastName = value; }
        }

        public DateTime DateOfBirth
        {
            get { return this.dateOfBirth; }
        }

        public virtual Department Department_ { get; }

        public virtual double Salary { get; set; }

        public virtual EmployeAttribute Attribute { get; set; }


        public Employe(string firstName, string lastName, DateTime dateOfBirth, EmployeAttribute attribute, Department department)
        {
            this.creationTime = DateTime.Now;
            this.firstName = firstName;
            this.lastName = lastName;
            this.dateOfBirth = dateOfBirth;
            this.Attribute = attribute;
            this.department = department;
        }
                
    }
}
