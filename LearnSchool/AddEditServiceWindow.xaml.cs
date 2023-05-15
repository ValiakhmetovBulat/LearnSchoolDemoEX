using LearnSchool.Models;
using LearnSchool.Models.Entities;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Permissions;
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
using Path = System.IO.Path;

namespace LearnSchool
{
    /// <summary>
    /// Логика взаимодействия для AddEditServiceWindow.xaml
    /// </summary>
    public partial class AddEditServiceWindow : Window
    {
        private List<Service> _services;
        private LearnSchoolDbContext _context;
        private Service _service;
        public AddEditServiceWindow(Service service)
        {
            InitializeComponent();

            _service = service;
            DataContext = _service;
            _context = LearnSchoolDbContext.GetContext();
            _services = _context.Services.ToList();

            if (_service.ServiceId == 0 )
            {
                TextBoxServiceId.Visibility = Visibility.Hidden;
            }
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (_service.ServiceDuration / 60 >= 4)
            {
                errors.AppendLine("Длительность услуги не может превышать 4 часа");
            }
            if (_service.ServiceDuration < 0)
            {
                errors.AppendLine("Длительность услуги не может быть отрицательной");
            }
            if (_services.Find(s => s.ServiceName == _service.ServiceName) != null && _service.ServiceId == 0)
            {
                errors.AppendLine("Услуга с таким названием уже существует в базе");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_service.ServiceId == 0)
            {
                _context.Services.Add(_service);
            }

            try
            {
                _context.SaveChanges();
                MessageBox.Show("Информация сохранена");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ButtonGoBack_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(
                "Вы действительно хотите вернуться ? Несохраненные данные будут потеряны.",
                "Внимание",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                ServiceWindow serviceWindow = new ServiceWindow();
                serviceWindow.Show();
                this.Close();
            }                
        }

        private void ButtonChangeServiceImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "images | *.png;*.jpg;*.jpeg";
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            dialog.Multiselect = false;
            if (dialog.ShowDialog() == true)
            {
                File.Copy(dialog.FileName, @$"Resources\{_service.ServiceName}.jpg");
            }
        }
    }
}
