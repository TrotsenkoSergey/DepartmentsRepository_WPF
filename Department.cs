using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentsRepository_WPF
{
    public class Department
    {
        public DateTime CreationTime { get; private set; }

        public ObservableCollection<Department> Departments { get; set; }

        public string DepartmentName { get; set; }

        public ObservableCollection<Employe> Employes { get; set; }

        public int CountOfEmployes { get { return Employes.Count; } }

        public Department(string departmentName)
        {
            this.DepartmentName = departmentName;
            this.Employes = new ObservableCollection<Employe>();
            this.Departments = new ObservableCollection<Department>();
            this.CreationTime = DateTime.Now;
        }
    }
}
