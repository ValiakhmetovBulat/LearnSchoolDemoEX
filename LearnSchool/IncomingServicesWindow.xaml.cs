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
using System.Windows.Threading;

namespace LearnSchool
{
    /// <summary>
    /// Логика взаимодействия для IncomingServicesWindow.xaml
    /// </summary>
    public partial class IncomingServicesWindow : Window
    {
        private LearnSchoolDbContext _context;
        private List<ClientService> _clientServices;
        private DispatcherTimer _timer;
        private int _tickCounter = 0;
        private int _refreshTime = 30;

        public IncomingServicesWindow()
        {
            InitializeComponent();

            GetClientServices();

            var timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Start();

            _timer = timer;
        }

        private void GetClientServices()
        {
            _context = LearnSchoolDbContext.GetContext();
            _clientServices = _context.ClientServices.ToList();

            _clientServices = _clientServices.OrderBy(c => c.MinutesToBegin).ToList();
            _clientServices = _clientServices.Where(c => c.MinutesToBegin >= 0).ToList();

            listViewClientServices.ItemsSource = _clientServices;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            _tickCounter++;
            if (_tickCounter % _refreshTime == 0) 
            {
                GetClientServices();
            }
        }

        private void ButtonGoBack_Click(object sender, RoutedEventArgs e)
        {
            ServiceWindow serviceWindow = new ServiceWindow(); 
            serviceWindow.Show();
            this.Close();
        }
    }
}
