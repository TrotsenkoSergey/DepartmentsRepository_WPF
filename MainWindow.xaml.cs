using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DepartmentsRepository_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DepartmentsRepository departmentsRepository;
        const string JSON_FILE_NAME = "departmentsRepository.json";

        public MainWindow()
        {
            InitializeComponent();
            this.departmentsRepository = new DepartmentsRepository();
            trvDepartments.ItemsSource = this.departmentsRepository.Departments;
        }

        private void CreateDepartment_Click(object sender, RoutedEventArgs e)
        {
            Window windowDepartmentCreation = new WindowDepartmentCreation(this.departmentsRepository);
            windowDepartmentCreation.Owner = this;
            windowDepartmentCreation.ShowDialog();
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

        /// <summary>
        /// Calls a window to rename a specific department.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Calls a method to remove a specific Employe.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Calls NotePad and opens the HelpENG.txt file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HelpENG_Click(object sender, RoutedEventArgs e)
        {
            string s = Directory.GetCurrentDirectory();
            Process.Start("notepad.exe", "HelpENG.txt");
        }

        /// <summary>
        /// Calls NotePad and opens the HelpRUS.txt file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HelpRUS_Click(object sender, RoutedEventArgs e)
        {
            string s = Directory.GetCurrentDirectory();
            Process.Start("notepad.exe", "HelpRUS.txt");
        }

        /// <summary>
        /// Calls IO tools to Load file departmentsRepository.json.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Load_Click(object sender, RoutedEventArgs e)
        {
            if (!System.IO.File.Exists(JSON_FILE_NAME)) MessageBox.Show("You havnt file to download, first - save something.");
            else
            {
                //var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };
                string json = System.IO.File.ReadAllText(JSON_FILE_NAME);
                //this.departmentsRepository.Departments = JsonConvert.DeserializeObject<ObservableCollection<Department>>(json, settings);


                JsonSerializerOptions options = new JsonSerializerOptions()
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    WriteIndented = true
                };


                this.departmentsRepository.Departments = JsonSerializer.Deserialize<ObservableCollection<Department>>(json, options);
                trvDepartments.ItemsSource = this.departmentsRepository.Departments;
                if (departmentsRepository.Departments.Count != 0)
                { this.departmentsRepository.FirstDepartment = departmentsRepository.Departments[0]; }
            }
        }

        /// <summary>
        /// Calls IO tools to Save departmentsRepository.json file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            //var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };
            //string json = JsonConvert.SerializeObject(this.departmentsRepository.Departments, typeof(ObservableCollection<Department>), settings);


            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(this.departmentsRepository.Departments, typeof(ObservableCollection<Department>), options);
            System.IO.File.WriteAllText(JSON_FILE_NAME, json);
        }

        /// <summary>
        /// Hint where the file with the saved message history is stored.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PathData_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"If you want to do something with the saved file you must follow\n" +
                $"{Path.Combine(Directory.GetCurrentDirectory(), JSON_FILE_NAME)}");
        }

        /// <summary>
        /// Shows copyright. And allows you to follow the link.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Reference_Click(object sender, RoutedEventArgs e)
        {
            string link = @"https://github.com/TrotsenkoSergey/DepartmentsRepository_WPF";
            var result = MessageBox.Show(
                "Copyright (c) Sergey Trotsenko. All rights reserved.\n\n" +
                $"{link}\n\n" +
                "Click OK button if you want to link...",
                Reference.Name,
                MessageBoxButton.OKCancel,
                MessageBoxImage.Information
                );
            if (result == MessageBoxResult.OK)
            {
                Process.Start($"{link}");
            }
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
