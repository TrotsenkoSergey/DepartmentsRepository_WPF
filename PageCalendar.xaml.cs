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
    /// Interaction logic for PageCalendar.xaml
    /// </summary>
    public partial class PageCalendar : Page
    {
        WindowEmployeAdd windowEmployeAdd;
        PageAddEmploye pageAddEmploye;

        public PageCalendar(WindowEmployeAdd windowEmployeAdd, PageAddEmploye pageAddEmploye)
        {
            InitializeComponent();
            this.windowEmployeAdd = windowEmployeAdd;
            this.pageAddEmploye = pageAddEmploye;
            Calendar.SelectedDate = DateTime.Now;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Calendar.SelectedDate != null)
            {
                this.windowEmployeAdd.Frame.Content = pageAddEmploye;
            }
            else
            {
                MessageBox.Show("You must select Date of birth.");
            }
        }

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            Button.IsDefault = true;
        }
    }
}
