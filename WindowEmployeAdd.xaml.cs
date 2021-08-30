using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace DepartmentsRepository_WPF
{
    /// <summary>
    /// Interaction logic for WindowEmployeAdd.xaml
    /// </summary>
    public partial class WindowEmployeAdd : Window
    {
        DepartmentsRepository departmentsRepository;
        public WindowEmployeAdd(DepartmentsRepository departmentsRepository)
        {
            InitializeComponent();
            this.departmentsRepository = departmentsRepository;
            ObservableCollection<Department> departments = new ObservableCollection<Department>();
            ObservableCollection<EmployeAttribute> employeAttributes = new ObservableCollection<EmployeAttribute>();
            
            if (departmentsRepository.FirstDepartment.GetDirector(departmentsRepository.FirstDepartment) == null ||
                departmentsRepository.FirstDepartment.GetDeputyDirector(departmentsRepository.FirstDepartment) == null)
            {
                departments.Add(this.departmentsRepository.FirstDepartment);
                cbDepartments.ItemsSource = departments;
                cbDepartments.SelectedValue = this.departmentsRepository.FirstDepartment;

                if (departmentsRepository.FirstDepartment.GetDirector(departmentsRepository.FirstDepartment) == null)
                { employeAttributes.Add(EmployeAttribute.Director); }
                else
                { employeAttributes.Add(EmployeAttribute.Deputy_Director); }

                cbAttribute.ItemsSource = employeAttributes;
                cbAttribute.SelectedItem = employeAttributes[0];
            }
            else
            {
                departments = this.departmentsRepository.FirstDepartment.GetListOfDepartments(this.departmentsRepository.FirstDepartment);
                departments.Remove(this.departmentsRepository.FirstDepartment);
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
            if (!(dpDatePicker.SelectedDate is DateTime) || 
                !this.departmentsRepository.IsCorrectName(tbEmployeFirstName.Text) || !this.departmentsRepository.IsCorrectName(tbEmployeLastName.Text))
            {
                MessageBox.Show($"You must first fill out the form. \nOR \n{DepartmentsRepository.INVALID_NAME}");
            }
            else
            {
                if ((cbDepartments.SelectedValue as Department) != this.departmentsRepository.FirstDepartment &&
                    (cbDepartments.SelectedValue as Department).GetHeadOfDepartment(cbDepartments.SelectedValue as Department) == null &&
                    (EmployeAttribute)cbAttribute.SelectedValue != EmployeAttribute.Head_Of_Department)
                {
                    MessageBox.Show("You cannot add an worker or intern to a department while there is no Head in the department.");
                }
                else if ((cbDepartments.SelectedValue as Department) != this.departmentsRepository.FirstDepartment &&
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
                            this.departmentsRepository.FirstDepartment);
                    Close();
                }
            }
        }


    }
}
