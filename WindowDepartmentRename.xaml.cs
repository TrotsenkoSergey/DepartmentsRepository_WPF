using System.Windows;

namespace DepartmentsRepository_WPF
{
    /// <summary>
    /// Interaction logic for WindowDepartmentRename.xaml
    /// </summary>
    public partial class WindowDepartmentRename : Window
    {
        DepartmentsRepository departmentsRepository;
        Department concreteDepartment;

        public WindowDepartmentRename(DepartmentsRepository departmentsRepository, Department concreteDepartment)
        {
            InitializeComponent();
            this.departmentsRepository = departmentsRepository;
            this.concreteDepartment = concreteDepartment;
        }

        private void Rename_Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.departmentsRepository.IsCorrectName(tbDepName.Text))
            {
                concreteDepartment.DepartmentName = tbDepName.Text;
                Close();
            }
            else
            {
                MessageBox.Show(DepartmentsRepository.INVALID_NAME);
            }
        }
    }
}
