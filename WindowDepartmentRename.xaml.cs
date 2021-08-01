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
