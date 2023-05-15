using LearnSchool.Models;
using LearnSchool.Models.Entities;
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

namespace LearnSchool
{
    /// <summary>
    /// Логика взаимодействия для ClientServiceWindow.xaml
    /// </summary>
    public partial class ClientServiceWindow : Window
    {
        private Service _service;
        private LearnSchoolDbContext _context;
        private ClientService _clientService = new ClientService();

        public ClientServiceWindow(Service service)
        {
            InitializeComponent();

            _service = service;
            _context = LearnSchoolDbContext.GetContext();
            _clientService.Service = _service;

            DataContext = _clientService;

            _clientService.DateOfService = DateTime.Today;

            TextBlockService.Text = _service.ServiceName + ":" + _service.ServiceDuration.ToString() + " минут";

            ComboBoxClients.ItemsSource = _context.Clients.ToList();
        }

        private void TextBoxBeginningTime_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (
                !Char.IsDigit(e.Text, 0) &&
                e.Text[0] != ':'
                )
            {
                e.Handled = true;
            }
        }

        private void TextBoxBeginningTime_TextChanged(object sender, TextChangedEventArgs e)
        {
            var IsParsed = TimeOnly.TryParse(TextBoxBeginningTime.Text.ToString(), out var result);

            if (IsParsed)
            {
                _clientService.DateOfService = _clientService.DateOfService.Date; 
                _clientService.DateOfService += result.ToTimeSpan();
                var endingTime = _clientService.DateOfService.AddMinutes(_service.ServiceDuration);
                TextBoxEndingTime.Text = endingTime.ToShortTimeString();
            }
            else
            {
                TextBoxEndingTime.Text = "";
            }
        }

        private void ButtonGoBack_Click(object sender, RoutedEventArgs e)
        {
                if (MessageBox.Show("Несохраненные данные будут утеряны. Вернуться?",
                "Внимание",
                MessageBoxButton.YesNo) ==
                MessageBoxResult.Yes)
            {
                ServiceWindow serviceWindow = new ServiceWindow();
                serviceWindow.Show();
                this.Close();
            }
        }

        private void ButtonSaveClientService_Click(object sender, RoutedEventArgs e)
        {
            _context.ClientServices.Add(_clientService);

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
    }
}
