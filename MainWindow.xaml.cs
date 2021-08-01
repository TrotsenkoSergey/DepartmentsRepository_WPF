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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DepartmentsRepository_WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        DepartmentsRepository departmentsRepository;

        public MainWindow()
        {
            InitializeComponent();
            
        }
                
        private void Create_Click(object sender, RoutedEventArgs e)
        {
            if (this.departmentsRepository == null)
            { this.departmentsRepository = new DepartmentsRepository(); }
            
            Window windowDepartmentCreation = new WindowDepartmentCreation(this.departmentsRepository);
            windowDepartmentCreation.Owner = this;
            windowDepartmentCreation.ShowDialog();

            if (trvDepartments.ItemsSource == null)
            { 
                trvDepartments.ItemsSource = departmentsRepository.Departments; 
            }
        }

        private void Rename_Click(object sender, RoutedEventArgs e)
        {
            if (trvDepartments.SelectedItem is Department)
            {
                (trvDepartments.SelectedItem as Department).DepartmentName = "Nice";
                

                //int index = trvDepartments.Items.IndexOf(trvDepartments.SelectedItem);
                //(trvDepartments.Items[index] as Department).DepartmentName = "Nice";
            }
            else
            {
                MessageBox.Show("First, you must select the department to rename in the tree view box.");
            }
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            if (trvDepartments.SelectedItem is Department)
            {
                this.departmentsRepository.GetDepartmentAncestor(trvDepartments.SelectedItem as Department, this.departmentsRepository.FirstDepartment).
                    Departments.Remove(trvDepartments.SelectedItem as Department);
            }
            else
            {
                MessageBox.Show("First, you must select the department to remove in the tree view box.");
            }
        }
    }
}
