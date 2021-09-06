using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using Microsoft.Win32;

namespace DepartmentsRepository_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DepartmentsRepository departmentsRepository;

        public MainWindow()
        {
            InitializeComponent();
            departmentsRepository = new DepartmentsRepository();
            trvDepartments.ItemsSource = departmentsRepository.Departments;
        }

        /// <summary>
        /// Calls up a window for create a specific department.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateDepartment_Click(object sender, RoutedEventArgs e)
        {
            var windowDepartmentCreation = new WindowDepartmentCreation(departmentsRepository);
            windowDepartmentCreation.Owner = this;

            if ((bool)windowDepartmentCreation.ShowDialog())
            {
                if (DepartmentsRepository.MainDepartment == null)
                {
                    departmentsRepository.CreateMainDepartment(windowDepartmentCreation.tbDepName.Text);
                }
                else
                {
                    departmentsRepository.CreateNewDepartment(windowDepartmentCreation.tbDepName.Text,
                        windowDepartmentCreation.cbWinDep.SelectedItem as Department);
                }
            }
        }

        /// <summary>
        /// Calls up a window for renaming a specific department.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RenameDepartment_Click(object sender, RoutedEventArgs e)
        {
            if (trvDepartments.SelectedItem is Department)
            {
                Window windowDepartmentRename = new WindowDepartmentRename(trvDepartments.SelectedItem as Department);
                windowDepartmentRename.Owner = this;
                windowDepartmentRename.ShowDialog();
            }
            else
            {
                MessageBox.Show("First, you must select the department to rename in the tree view box.");
            }
        }

        /// <summary>
        /// Calls a method to remove an employee from the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveDepartment_Click(object sender, RoutedEventArgs e)
        {
            if (trvDepartments.SelectedItem is Department && (trvDepartments.SelectedItem as Department != DepartmentsRepository.MainDepartment))
            {
                MessageBoxResult messageBoxResult = MessageBox.Show($"Are you sure to remove '{(trvDepartments.SelectedItem as Department).DepartmentName}' department?",
                    "Warning message", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    DepartmentsRepository.MainDepartment.GetDepartmentAncestor(trvDepartments.SelectedItem as Department, DepartmentsRepository.MainDepartment).
                        Departments.Remove(trvDepartments.SelectedItem as Department);
                }
            }
            else if (trvDepartments.SelectedItem is Department && (trvDepartments.SelectedItem as Department == DepartmentsRepository.MainDepartment))
            {
                MessageBox.Show("You cant remove Main department.");
            }
            else
            {
                MessageBox.Show("You must select the department to remove in the tree view box.");
            }
        }

        /// <summary>
        /// Calls a method to add an employee to the list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddEmploye_Click(object sender, RoutedEventArgs e)
        {
            if (this.departmentsRepository == null || DepartmentsRepository.MainDepartment == null)
            {
                MessageBox.Show("You must create a Main Department, then you can add an employee.");
            }
            else if (DepartmentsRepository.MainDepartment.GetDirector(DepartmentsRepository.MainDepartment) == null ||
                     DepartmentsRepository.MainDepartment.GetDeputyDirector(DepartmentsRepository.MainDepartment) == null ||
                     DepartmentsRepository.MainDepartment.Departments.Count != 0)
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

        /// <summary>
        /// Links the selected department to the list of its employees.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trvDepartments_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (trvDepartments.SelectedItem is Department)
            {
                lvEmployes.ItemsSource = (trvDepartments.SelectedItem as Department).Employes;
            }
        }

        /// <summary>
        /// Calls a method to sort employes by Last name.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridViewColumnHeaderName_Click(object sender, RoutedEventArgs e)
        {
            if (trvDepartments.SelectedItem is Department)
            {
                (trvDepartments.SelectedItem as Department).SortEmployeByLastName(trvDepartments.SelectedItem as Department);
                lvEmployes.ItemsSource = (trvDepartments.SelectedItem as Department).Employes;
            }
        }

        /// <summary>
        /// Calls a method to sort employes by Salary.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridViewColumnHeaderSalary_Click(object sender, RoutedEventArgs e)
        {
            if (trvDepartments.SelectedItem is Department)
            {
                (trvDepartments.SelectedItem as Department).SortEmployeBySalary(trvDepartments.SelectedItem as Department);
                lvEmployes.ItemsSource = (trvDepartments.SelectedItem as Department).Employes;
            }
        }

        /// <summary>
        /// Calls a method to sort employes by the date they were added to the list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridViewColumnHeaderCreationTime_Click(object sender, RoutedEventArgs e)
        {
            if (trvDepartments.SelectedItem is Department)
            {
                (trvDepartments.SelectedItem as Department).SortEmployeByCreationTime(trvDepartments.SelectedItem as Department);
                lvEmployes.ItemsSource = (trvDepartments.SelectedItem as Department).Employes;
            }
        }

        /// <summary>
        /// Calls a method to sort employes by the date of birth.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                Window windowEmployeRename = new WindowEmployeRename(lvEmployes.SelectedItem as Employe);
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
                    (trvDepartments.SelectedItem as Department).RemoveEmploye(lvEmployes.SelectedItem as Employe);
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
            var directInfo = new DirectoryInfo(Directory.GetCurrentDirectory());
            string combined = Path.Combine(directInfo.Parent.Parent.FullName, "HelpENG.txt");
            Process.Start("notepad.exe", combined);
        }

        /// <summary>
        /// Calls NotePad and opens the HelpRUS.txt file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HelpRUS_Click(object sender, RoutedEventArgs e)
        {
            var directInfo = new DirectoryInfo(Directory.GetCurrentDirectory());
            string combined = Path.Combine(directInfo.Parent.Parent.FullName, "HelpRUS.txt");
            Process.Start("notepad.exe", combined);
        }

        /// <summary>
        /// Calls IO tools to Load file departmentsRepository.json.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Load_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Dialog window Json repository";
            openFileDialog.Filter = "Json files (*.json)|*.json";
            openFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
            //openFileDialog.InitialDirectory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
            {
                string json = System.IO.File.ReadAllText(openFileDialog.FileName);

                //var options = new JsonSerializerOptions()
                //{
                //    ReferenceHandler = ReferenceHandler.Preserve,
                //    WriteIndented = true,
                //    Converters = {
                //        //new EmployeConverter(),
                //        //new BaseClassConverter(),
                //        new JsonStringEnumConverter() }
                //};
                //this.departmentsRepository = JsonSerializer.Deserialize<DepartmentsRepository>(json, options);

                var settings = new Newtonsoft.Json.JsonSerializerSettings
                {
                    TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Objects,
                    Formatting = Newtonsoft.Json.Formatting.Indented,
                    //ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize
                };
                departmentsRepository = Newtonsoft.Json.JsonConvert.DeserializeObject<DepartmentsRepository>(json, settings);
                if (departmentsRepository.Departments.Count != 0)
                {
                    DepartmentsRepository.MainDepartment = departmentsRepository.Departments[0];
                }

                trvDepartments.ItemsSource = departmentsRepository.Departments;
            }
        }

        /// <summary>
        /// Calls IO tools to Save departmentsRepository.json file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Dialog window Json repository";
            saveFileDialog.Filter = "Json files (*.json)|*.json";
            saveFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
            //openFileDialog.InitialDirectory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            if (saveFileDialog.ShowDialog() == true)
            {
                //var options = new JsonSerializerOptions()
                //{
                //    ReferenceHandler = ReferenceHandler.Preserve,
                //    WriteIndented = true,
                //    Converters = {
                //            //new EmployeConverter(),
                //            //new BaseClassConverter(),
                //            //new TypeDiscriminatorConverter<Employe>(),
                //            new JsonStringEnumConverter() }
                //};
                //string json = JsonSerializer.Serialize<object>(this.departmentsRepository, options);

                var settings = new Newtonsoft.Json.JsonSerializerSettings
                {
                    TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Objects,
                    Formatting = Newtonsoft.Json.Formatting.Indented,
                    //ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize
                };
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(departmentsRepository, settings);

                string fileName = Path.Combine(saveFileDialog.InitialDirectory, saveFileDialog.FileName);

                System.IO.File.WriteAllText(fileName, json);
            }
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
