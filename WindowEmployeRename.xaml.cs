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

        DepartmentsRepository departmentsRepository;
        Employe employe;

        public WindowEmployeRename(DepartmentsRepository departmentsRepository, Employe employe)
        {
            InitializeComponent();
            this.departmentsRepository = departmentsRepository;
            this.employe = employe;
        }

        private void Rename_Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.departmentsRepository.IsCorrectName(tbEmployeFirstName.Text))
            {
                this.employe.FirstName = tbEmployeFirstName.Text;
            }
            if (this.departmentsRepository.IsCorrectName(tbEmployeLastName.Text))
            {
                this.employe.LastName = tbEmployeLastName.Text;
            }
            Close();
        }
    }
}
