using System.Windows;

namespace DepartmentsRepository_WPF
{
    /// <summary>
    /// Interaction logic for WindowDepartmentRename.xaml
    /// </summary>
    public partial class WindowDepartmentRename : Window
    {
        private readonly Department concreteDepartment;

        public WindowDepartmentRename(Department concreteDepartment)
        {
            InitializeComponent();
            this.concreteDepartment = concreteDepartment;
        }

        private void Rename_Button_Click(object sender, RoutedEventArgs e)
        {
            if (DepartmentsRepository.IsCorrectName(tbDepName.Text, out string error))
            {
                concreteDepartment.DepartmentName = tbDepName.Text;
                Close();
            }
            else
            {
                MessageBox.Show($"Department name {error}");
            }
        }
    }
}
