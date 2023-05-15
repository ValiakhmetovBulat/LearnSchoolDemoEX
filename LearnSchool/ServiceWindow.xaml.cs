using LearnSchool.Models;
using LearnSchool.Models.Entities;
using Microsoft.EntityFrameworkCore.Query.Internal;
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
    /// Логика взаимодействия для ServiceWindow.xaml
    /// </summary>
    public partial class ServiceWindow : Window
    {
        private LearnSchoolDbContext _db;
        private List<Service> _services;

        public ServiceWindow()
        {
            InitializeComponent();
            _db = LearnSchoolDbContext.GetContext();
            _services = _db.Services.ToList();           

            ComboBoxServiceCostSort.ItemsSource = new List<string>
            {
                "Все",
                "По возрастанию",
                "По убыванию"
            };

            ComboBoxServiceDiscountFilter.ItemsSource = new List<string>
            {
                "Все",
                "от 0% до 5%",
                "от 5% до 15%",
                "от 15% до 30%",
                "от 30% до 70%",
                "от 70% до 100%"
            };         

            ComboBoxServiceCostSort.SelectedIndex = 0;
            ComboBoxServiceDiscountFilter.SelectedIndex = 0;
        }

        private void ButtonDeleteService_Click(object sender, RoutedEventArgs e)
        {
            var serviceToDelete = (sender as Button).DataContext as Service;

            if (serviceToDelete.ClientServices.Count > 0)
            {
                MessageBox.Show("Удаление услуги невозможно, так как есть записи на нее", "Ошибка");
                return;
            }

            if (MessageBox.Show(
                "Вы действительно желаете удалить данную услугу ?", 
                "Внимание", 
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _db.Remove(serviceToDelete);
                _services.Remove(serviceToDelete);
                _db.SaveChanges();
                
                SortServices();
            }            
        }

        private void ButtonEditService_Click(object sender, RoutedEventArgs e)
        {
            AddEditServiceWindow addEditServiceWindow = new AddEditServiceWindow((sender as Button).DataContext as Service);
            addEditServiceWindow.Show();
            this.Close();
        }

        private void ButtonAddService_Click(object sender, RoutedEventArgs e)
        {
            AddEditServiceWindow addEditServiceWindow = new AddEditServiceWindow(new Service());
            addEditServiceWindow.Show();
            this.Close();
        }

        private void ComboBoxServiceDiscountFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SortServices();
        }

        private void ComboBoxServiceCostSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SortServices();
        }

        private void TextBoxSearchServices_TextChanged(object sender, TextChangedEventArgs e)
        {
            SortServices();
        }

        private void SortServices()
        {
            var sorted = _services;

            sorted = SortServicesByDiscount(sorted);
            sorted = SortServicesByName(sorted);
            sorted = SortServicesByPrice(sorted);
            SetServicesCount(sorted);

            listViewServices.ItemsSource = sorted;
        }

        private List<Service> SortServicesByPrice(List<Service> services)
        {
            switch (ComboBoxServiceCostSort.SelectedIndex)
            {
                case 0:
                    {
                        break;
                    }
                case 1:
                    {
                        services = services.OrderBy(s => s.ServiceCostWithDiscount).ToList();
                        break;
                    }
                case 2:
                    {
                        services = services.OrderByDescending(s => s.ServiceCostWithDiscount).ToList(); 
                        break;
                    }
            }

            return services;
        }

        private List<Service> SortServicesByDiscount(List<Service> services)
        {
            switch (ComboBoxServiceDiscountFilter.SelectedIndex)
            {
                case 0:
                    {
                        break;
                    }                    
                case 1:
                    {
                        services = services.Where(s => s.ServiceDiscount >= 0 &&  s.ServiceDiscount < 5).ToList();
                        break;
                    }
                case 2:
                    {
                        services = services.Where(s => s.ServiceDiscount >= 5 && s.ServiceDiscount < 15).ToList();
                        break;
                    }
                case 3:
                    {
                        services = services.Where(s => s.ServiceDiscount >= 15 && s.ServiceDiscount < 30).ToList();
                        break;
                    }
                case 4:
                    {
                        services = services.Where(s => s.ServiceDiscount >= 30 && s.ServiceDiscount < 70).ToList();
                        break;
                    }
                case 5:
                    {
                        services = services.Where(s => s.ServiceDiscount >= 70 && s.ServiceDiscount < 100).ToList();
                        break;
                    }
            }

            return services;
        }

        private List<Service> SortServicesByName(List<Service> services)
        {
            return services.Where(s => s.ServiceName.ToLower().Contains(TextBoxSearchServices.Text.ToLower())).ToList();
        }

        private void SetServicesCount(List<Service> services)
        {
            TextBlockCountServices.Text = services.Count.ToString() + " из " + _services.Count.ToString();
        }

        private void ButtonGoToClientServiceWindow_Click(object sender, RoutedEventArgs e)
        {
            if (listViewServices.SelectedItems.Count == 0) 
            {
                MessageBox.Show("Выберете услугу для записи нажамием на карточку");
                return;
            }
            if (listViewServices.SelectedItems.Count > 1) 
            {
                MessageBox.Show("Одновременно можно записывать только на одну услугу");
                return;
            }

            var selectedService = listViewServices.SelectedItems.Cast<Service>().FirstOrDefault();

            ClientServiceWindow clientServiceWindow = new ClientServiceWindow(selectedService);
            clientServiceWindow.Show();
            this.Close();
        }

        private void ButtonGoToIncomingServicesWindow_Click(object sender, RoutedEventArgs e)
        {
            IncomingServicesWindow incomingServicesWindow = new IncomingServicesWindow();
            incomingServicesWindow.Show();
            this.Close();
        }
    }
}
