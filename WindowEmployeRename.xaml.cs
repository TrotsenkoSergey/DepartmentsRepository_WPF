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
    /// Interaction logic for WindowEmployeRename.xaml
    /// </summary>
    public partial class WindowEmployeRename : Window
    {

        private readonly Employee employee;

        public WindowEmployeRename(Employee employee)
        {
            InitializeComponent();
            this.employee = employee;
        }

        private void Rename_Button_Click(object sender, RoutedEventArgs e)
        {
            if (DepartmentsRepository.IsCorrectName(tbEmployeFirstName.Text, out string error) &
                DepartmentsRepository.IsCorrectName(tbEmployeLastName.Text, out string error2))
            {
                employee.FirstName = tbEmployeFirstName.Text;
                employee.LastName = tbEmployeLastName.Text;
                Close();
            }
            else MessageBox.Show($"Firstname {error}, lastname {error2}");
        }
    }
}
