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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        DepartmentsRepository departmentsRepository;

        public MainWindow()
        {
            InitializeComponent();
            
        }
                
        private void CreateDepartment_Click(object sender, RoutedEventArgs e)
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

        private void RenameDepartment_Click(object sender, RoutedEventArgs e)
        {
            if (trvDepartments.SelectedItem is Department)
            {
                Window windowDepartmentRename = new WindowDepartmentRename (this.departmentsRepository, trvDepartments.SelectedItem as Department);
                windowDepartmentRename.Owner = this;
                windowDepartmentRename.ShowDialog();
            }
            else
            {
                MessageBox.Show("First, you must select the department to rename in the tree view box.");
            }
        }

        private void RemoveDepartment_Click(object sender, RoutedEventArgs e)
        {
            if (trvDepartments.SelectedItem is Department && (trvDepartments.SelectedItem as Department != this.departmentsRepository.FirstDepartment))
        {
                MessageBoxResult messageBoxResult = MessageBox.Show($"Are you sure to remove '{(trvDepartments.SelectedItem as Department).DepartmentName}' department?", 
                    "Warning message", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (messageBoxResult == MessageBoxResult.Yes)
            {
                this.departmentsRepository.GetDepartmentAncestor(trvDepartments.SelectedItem as Department, this.departmentsRepository.FirstDepartment).
                    Departments.Remove(trvDepartments.SelectedItem as Department);
            }
            }
            else if (trvDepartments.SelectedItem is Department && (trvDepartments.SelectedItem as Department == this.departmentsRepository.FirstDepartment))
            {
                MessageBox.Show("You cant remove Main department.");
            }
            else
            {
                MessageBox.Show("You must select the department to remove in the tree view box.");
            }
        }

        private void AddEmploye_Click(object sender, RoutedEventArgs e)
        {
            if (this.departmentsRepository != null)
            {
                Window windowEmployeAdd = new WindowEmployeAdd(this.departmentsRepository);
                windowEmployeAdd.Owner = this;
                windowEmployeAdd.ShowDialog();
            }
            else
            {
                MessageBox.Show("You must create at least one department to which you can add an employee.");
            }
        }

        private void trvDepartments_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (trvDepartments.SelectedItem is Department)
            {
                lvEmployes.ItemsSource = (trvDepartments.SelectedItem as Department).Employes;
            }
        }
    }
}
