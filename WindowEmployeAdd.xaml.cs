using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace DepartmentsRepository_WPF
{
    /// <summary>
    /// Interaction logic for WindowEmployeAdd.xaml
    /// </summary>
    public partial class WindowEmployeAdd
    {
        private readonly DepartmentsRepository departmentsRepository;

        public WindowEmployeAdd(DepartmentsRepository departmentsRepository)
        {
            InitializeComponent();
            this.departmentsRepository = departmentsRepository;

            var departments = new ObservableCollection<Department>();
            var employeAttributes = new List<EmployeAttribute>();
            
            if (DepartmentsRepository.MainDepartment.GetDirector(DepartmentsRepository.MainDepartment) == null ||
                DepartmentsRepository.MainDepartment.GetDeputyDirector(DepartmentsRepository.MainDepartment) == null)
            {
                departments.Add(DepartmentsRepository.MainDepartment);
                cbDepartments.ItemsSource = departments;
                cbDepartments.SelectedValue = DepartmentsRepository.MainDepartment;

                if (DepartmentsRepository.MainDepartment.GetDirector(DepartmentsRepository.MainDepartment) == null)
                { employeAttributes.Add(EmployeAttribute.Director); }
                else
                { employeAttributes.Add(EmployeAttribute.Deputy_Director); }

                cbAttribute.ItemsSource = employeAttributes;
                cbAttribute.SelectedItem = employeAttributes[0];
            }
            else
            {
                departments = DepartmentsRepository.MainDepartment.GetListOfDepartments(DepartmentsRepository.MainDepartment);
                departments.Remove(DepartmentsRepository.MainDepartment);
                cbDepartments.ItemsSource = departments;
                cbDepartments.SelectedValue = departments[0];
                
                employeAttributes.Add(EmployeAttribute.Head_Of_Department);
                employeAttributes.Add(EmployeAttribute.Worker);
                employeAttributes.Add(EmployeAttribute.Intern);

                cbAttribute.ItemsSource = employeAttributes;
                cbAttribute.SelectedItem = EmployeAttribute.Worker;
            }
        }

        private void Button_AddEmploye(object sender, RoutedEventArgs e)
        {
            
            if (!DepartmentsRepository.IsCorrectName(tbEmployeFirstName.Text, out string error) | 
                !DepartmentsRepository.IsCorrectName(tbEmployeLastName.Text, out string error2))
            {
                MessageBox.Show($"Firstname {error}, lastname {error2}.");
            }
            else if (!(dpDatePicker.SelectedDate is DateTime))
            {
                MessageBox.Show($"invalid Date of birth");
            }
            else
            {
                if (cbDepartments.SelectedValue as Department != DepartmentsRepository.MainDepartment &&
                    (cbDepartments.SelectedValue as Department).GetHeadOfDepartment(cbDepartments.SelectedValue as Department) == null &&
                    (EmployeAttribute)cbAttribute.SelectedValue != EmployeAttribute.Head_Of_Department)
                {
                    MessageBox.Show("You cannot add an worker or intern to a department while there is no Head in the department.");
                }
                else if ((cbDepartments.SelectedValue as Department) != DepartmentsRepository.MainDepartment &&
                    (cbDepartments.SelectedValue as Department).GetHeadOfDepartment(cbDepartments.SelectedValue as Department) != null &&
                    (EmployeAttribute)cbAttribute.SelectedValue == EmployeAttribute.Head_Of_Department)
                {
                    MessageBox.Show("You cannot add second Head of department in one department.");
                }
                else
                {
                    (cbDepartments.SelectedValue as Department).AddEmploye(tbEmployeFirstName.Text,
                            tbEmployeLastName.Text, (DateTime)dpDatePicker.SelectedDate,
                            (EmployeAttribute)cbAttribute.SelectedValue, cbDepartments.SelectedValue as Department,
                            DepartmentsRepository.MainDepartment);
                    Close();
                }
            }
        }


    }
}
