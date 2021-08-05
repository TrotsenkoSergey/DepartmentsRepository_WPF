using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DepartmentsRepository_WPF
{
    /// <summary>
    /// Interaction logic for PageAddEmploye.xaml
    /// </summary>
    public partial class PageAddEmploye : Page
    {
        DepartmentsRepository departmentsRepository;
        WindowEmployeAdd windowEmployeAdd;
        PageCalendar pageCalendar;

        public PageAddEmploye(DepartmentsRepository departmentsRepository, WindowEmployeAdd windowEmployeAdd)
        {
            InitializeComponent();
            this.departmentsRepository = departmentsRepository;
            this.windowEmployeAdd = windowEmployeAdd;
            ObservableCollection<Department> departments = new ObservableCollection<Department>();
            ObservableCollection<EmployeAttribute> employeAttributes = new ObservableCollection<EmployeAttribute>();
            // don't know how to get Enum easier ("Enum.GetValues(EmployeAttribute)" didnt work)

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
                departments = this.departmentsRepository.GetListOfDepartments(this.departmentsRepository.FirstDepartment);
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

        private void Button_Calendar(object sender, RoutedEventArgs e)
        {
            PageCalendar pageCalendar = new PageCalendar(this.windowEmployeAdd, this);
            this.pageCalendar = pageCalendar;
            this.windowEmployeAdd.Frame.Content = pageCalendar;
        }

        private void Button_AddEmploye(object sender, RoutedEventArgs e)
        {
            if (pageCalendar == null || pageCalendar.Calendar.SelectedDate == null)
            {
                MessageBox.Show("First you must fill form.");
            }
            else
            {
                if ((cbDepartments.SelectedValue as Department).GetHeadOfDepartment(cbDepartments.SelectedValue as Department) == null &&
                    (EmployeAttribute)cbAttribute.SelectedValue != EmployeAttribute.Head_Of_Department)
                {
                    MessageBox.Show("You cannot add an employee or intern to a department while there is no Head in the department.");
                }
                else
                {
                    (cbDepartments.SelectedValue as Department).AddEmploye(tbEmployeFirstName.Text,
                        tbEmployeLastName.Text, (DateTime)pageCalendar.Calendar.SelectedDate,
                        (EmployeAttribute)cbAttribute.SelectedValue, cbDepartments.SelectedValue as Department, 
                        this.departmentsRepository.FirstDepartment);
                    windowEmployeAdd.Close();
                }
            }
        }
    }
}
