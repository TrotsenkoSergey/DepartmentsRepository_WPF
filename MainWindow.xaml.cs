using System.Windows;

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
            this.departmentsRepository = new DepartmentsRepository();
            trvDepartments.ItemsSource = this.departmentsRepository.Departments;
        }

        private void CreateDepartment_Click(object sender, RoutedEventArgs e)
        {
            //if (this.departmentsRepository == null)
            //{ this.departmentsRepository = new DepartmentsRepository(); }

            Window windowDepartmentCreation = new WindowDepartmentCreation(this.departmentsRepository);
            windowDepartmentCreation.Owner = this;
            windowDepartmentCreation.ShowDialog();

            //if (trvDepartments.ItemsSource == null)
            //{
            //    trvDepartments.ItemsSource = departmentsRepository.Departments;
            //}
        }

        private void RenameDepartment_Click(object sender, RoutedEventArgs e)
        {
            if (trvDepartments.SelectedItem is Department)
            {
                Window windowDepartmentRename = new WindowDepartmentRename(this.departmentsRepository, trvDepartments.SelectedItem as Department);
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
                    this.departmentsRepository.FirstDepartment.GetDepartmentAncestor(trvDepartments.SelectedItem as Department, this.departmentsRepository.FirstDepartment).
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
            if (this.departmentsRepository == null || this.departmentsRepository.FirstDepartment == null)
            {
                MessageBox.Show("You must create a Main Department, then you can add an employee.");
            }
            else if (this.departmentsRepository.FirstDepartment.GetDirector(departmentsRepository.FirstDepartment) == null ||
                this.departmentsRepository.FirstDepartment.GetDeputyDirector(departmentsRepository.FirstDepartment) == null ||
                    this.departmentsRepository.FirstDepartment.Departments.Count != 0)
            {
                Window windowEmployeAdd = new WindowEmployeAdd(this.departmentsRepository);
                windowEmployeAdd.Owner = this;
                windowEmployeAdd.ShowDialog();
            }
            else
            {
                MessageBox.Show("You must create the next department dimension to which you can add an employee " +
                    "(excluding the director and his deputy).");
            }
        }

        private void trvDepartments_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (trvDepartments.SelectedItem is Department)
            {
                lvEmployes.ItemsSource = (trvDepartments.SelectedItem as Department).Employes;
            }
        }

        private void GridViewColumnHeaderName_Click(object sender, RoutedEventArgs e)
        {
            if (trvDepartments.SelectedItem is Department)
            {
                (trvDepartments.SelectedItem as Department).SortEmployeByLastName(trvDepartments.SelectedItem as Department);
                lvEmployes.ItemsSource = (trvDepartments.SelectedItem as Department).Employes;
            }
        }

        private void GridViewColumnHeaderSalary_Click(object sender, RoutedEventArgs e)
        {
            if (trvDepartments.SelectedItem is Department)
            {
                (trvDepartments.SelectedItem as Department).SortEmployeBySalary(trvDepartments.SelectedItem as Department);
                lvEmployes.ItemsSource = (trvDepartments.SelectedItem as Department).Employes;
            }
        }

        private void GridViewColumnHeaderCreationTime_Click(object sender, RoutedEventArgs e)
        {
            if (trvDepartments.SelectedItem is Department)
            {
                (trvDepartments.SelectedItem as Department).SortEmployeByCreationTime(trvDepartments.SelectedItem as Department);
                lvEmployes.ItemsSource = (trvDepartments.SelectedItem as Department).Employes;
            }
        }

        private void GridViewColumnHeaderDateOfBirth_Click(object sender, RoutedEventArgs e)
        {
            if (trvDepartments.SelectedItem is Department)
            {
                (trvDepartments.SelectedItem as Department).SortEmployeByDateOfBirth(trvDepartments.SelectedItem as Department);
                lvEmployes.ItemsSource = (trvDepartments.SelectedItem as Department).Employes;
            }
        }

        private void RenameEmploye_Click(object sender, RoutedEventArgs e)
        {
            if (lvEmployes.SelectedItem is Employe)
            {
                Window windowEmployeRename = new WindowEmployeRename(this.departmentsRepository, lvEmployes.SelectedItem as Employe);
                windowEmployeRename.Owner = this;
                windowEmployeRename.ShowDialog();
            }
            else
            {
                MessageBox.Show("First, you must select the employe to rename in the list view box.");
            }
        }

        private void RemoveEmploye_Click(object sender, RoutedEventArgs e)
        {
            if (lvEmployes.SelectedItem is Employe)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show($"Are you sure to remove '{(lvEmployes.SelectedItem as Employe).FullName}' employe?",
                    "Warning message", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    (lvEmployes.SelectedItem as Employe).Department_.RemoveEmploye(lvEmployes.SelectedItem as Employe);
                }
            }
            else
            {
                MessageBox.Show("You must select the employe to remove in the list view box.");
            }
        }
    }
}
