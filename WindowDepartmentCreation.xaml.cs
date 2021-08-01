using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace DepartmentsRepository_WPF
{
    /// <summary>
    /// Логика взаимодействия для windowDepartmentCreation.xaml
    /// </summary>
    public partial class WindowDepartmentCreation : Window
    {
        DepartmentsRepository departmentsRepository;
        public WindowDepartmentCreation(DepartmentsRepository departmentsRepository)
        {
            InitializeComponent();
            this.departmentsRepository = departmentsRepository;
            if (this.departmentsRepository.FirstDepartment != null)
            { 
                cbWinDep.ItemsSource = this.departmentsRepository.GetListOfDepartments(this.departmentsRepository.FirstDepartment);
                cbWinDep.SelectedValue = this.departmentsRepository.FirstDepartment;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.departmentsRepository.FirstDepartment == null)
            { 
                departmentsRepository.CreateFirstDepartment(tbWinDepName.Text); 
            }
            else
            {
                departmentsRepository.CreateNewDepartment(tbWinDepName.Text, cbWinDep.SelectedItem as Department);
                 //this.departmentsRepository.Departments[cbWinDep.SelectedIndex]);
            }
            Close();
        }
    }
}
