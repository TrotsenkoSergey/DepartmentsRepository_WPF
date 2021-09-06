using System.Windows;

namespace DepartmentsRepository_WPF
{
    /// <summary>
    /// Interaction logic for WindowDepartmentCreation.xaml
    /// </summary>
    public partial class WindowDepartmentCreation
    {

        public WindowDepartmentCreation(DepartmentsRepository departmentsRepository)
        {
            InitializeComponent();
            if (DepartmentsRepository.MainDepartment != null)
            {
                cbWinDep.ItemsSource = DepartmentsRepository.MainDepartment.GetListOfDepartments(DepartmentsRepository.MainDepartment);
                cbWinDep.SelectedValue = DepartmentsRepository.MainDepartment; /* for the convenience of the user
                                                                                      * and avoiding the exclusion of the absence
                                                                                      * of a choice of department for creating a new */
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (DepartmentsRepository.IsCorrectName(tbDepName.Text, out string error))
            {
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show($"Department name {error}");
            }
        }
    }
}
