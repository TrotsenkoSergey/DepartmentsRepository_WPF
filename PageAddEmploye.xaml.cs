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
            cbDepartments.ItemsSource = this.departmentsRepository.GetListOfDepartments(this.departmentsRepository.FirstDepartment);
            cbDepartments.SelectedValue = this.departmentsRepository.FirstDepartment;
            
            ObservableCollection<EmployeAttribute> employeAttributes = new ObservableCollection<EmployeAttribute>() // don't know how to get Enum easier ("Enum.GetValues(EmployeAttribute)" didnt work)
            { 
                EmployeAttribute.Deputy_Director, EmployeAttribute.Director,EmployeAttribute.Head_Of_Department, EmployeAttribute.Intern, EmployeAttribute.Worker
            };

            cbAttribute.ItemsSource = employeAttributes;
            cbAttribute.SelectedItem = EmployeAttribute.Worker;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PageCalendar pageCalendar = new PageCalendar(this.windowEmployeAdd, this);
            this.pageCalendar = pageCalendar;
            this.windowEmployeAdd.Frame.Content = pageCalendar;
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            (cbDepartments.SelectedValue as Department).AddEmploye(tbEmployeFirstName.Text, tbEmployeLastName.Text, (DateTime)pageCalendar.Calendar.SelectedDate, (EmployeAttribute)cbAttribute.SelectedValue, cbDepartments.SelectedValue as Department);
            windowEmployeAdd.Close();
        }
    }
}
