using System.Windows;

namespace DepartmentsRepository_WPF
{
    /// <summary>
    /// Interaction logic for WindowDepartmentCreation.xaml
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
                cbWinDep.SelectedValue = this.departmentsRepository.FirstDepartment; /* for the convenience of the user
                                                                                      * and avoiding the exclusion of the absence
                                                                                      * of a choice of department for creating a new */
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.departmentsRepository.IsCorrectName(tbDepName.Text))
            {
                if (this.departmentsRepository.FirstDepartment == null)
                {
                    departmentsRepository.CreateFirstDepartment(tbDepName.Text);
                }
                else
                {
                    departmentsRepository.CreateNewDepartment(tbDepName.Text, cbWinDep.SelectedItem as Department);
                }
                Close();
            }
            else
            {
                MessageBox.Show(DepartmentsRepository.INVALID_NAME);
            }
        }
    }
}
