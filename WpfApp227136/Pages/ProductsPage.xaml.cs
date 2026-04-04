using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp227136.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductsPage.xaml
    /// </summary>
    public partial class ProductsPage : Page
    {
        public ProductsPage()
        {
            InitializeComponent();
            ProductsListBox.ItemsSource = Core.Context.products.ToList();

            var ProductsTypes = Core.Context.type_products.ToList();
            ProductsTypes.Insert(0, new type_products { type_product = "Все типы продукции" });
            ProductTypeComboBox.ItemsSource = ProductsTypes;

            if(Core.AuthUser == null)
            {
                ProductStackPanel.Visibility = Visibility.Collapsed;
                MaterialButton.Visibility = Visibility.Collapsed;
                AddButton.Visibility = Visibility.Collapsed;
                UpdateButton.Visibility = Visibility.Collapsed;
                DelButton.Visibility = Visibility.Collapsed;
                return;
            }
            switch(Core.AuthUser.role)
            {
                case 1: //Админ

                    break;
                case 2: //Мененджер 
                    AddButton.Visibility = Visibility.Collapsed;
                    UpdateButton.Visibility = Visibility.Collapsed;
                    DelButton.Visibility = Visibility.Collapsed;
                    break;
                case 3: // Агент
                    AddButton.Visibility = Visibility.Collapsed;
                    UpdateButton.Visibility = Visibility.Collapsed;
                    DelButton.Visibility = Visibility.Collapsed;
                    MaterialButton.Visibility = Visibility.Collapsed;
                    break;

            }
        }

        private void Filter()
        {
            var filteredProducts = Core.Context.products.ToList();

            if (!string.IsNullOrWhiteSpace(SearchTextBox?.Text))
            {
                filteredProducts = filteredProducts
                    .Where(p => p.name_product.ToLower().Contains(SearchTextBox.Text.ToLower()))
                .ToList();
            }
            if (PriceSortComboBox.SelectedIndex == 1) 
            {
                filteredProducts = filteredProducts.OrderBy(p => p.min_price).ToList();
            }
            else if (PriceSortComboBox.SelectedIndex == 2)
            {
                filteredProducts = filteredProducts.OrderByDescending(p => p.min_price).ToList();
            }

            if (ProductTypeComboBox != null && ProductTypeComboBox.SelectedIndex != 0)
            {
                filteredProducts = filteredProducts
                    .Where(p => p.type_products == ProductTypeComboBox.SelectedItem as type_products)
                    .ToList();
            }

            if(ProductsListBox != null)
            {
                ProductsListBox.ItemsSource = filteredProducts;
            }

        }


        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void PriceSortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void ProductTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void MaterialButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MaterialPage());

        }
   
        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsListBox.SelectedItem is products SelectedProduct)
            {

                var MessageBoxResult = MessageBox.Show("Вы точно хотите удалить товар?", "Удалить", MessageBoxButton.YesNo);
                if (MessageBoxResult == MessageBoxResult.Yes)
                {
                    Core.Context.products.Remove(SelectedProduct);
                    Core.Context.SaveChanges();
                    ProductsListBox.ItemsSource = Core.Context.products.ToList();
                }
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddProductPage());
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsListBox.SelectedItem is products selectedProduct)
            NavigationService.Navigate(new AddProductPage(selectedProduct));
        }
        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.Visibility == Visibility.Visible)
            {
                ProductsListBox.ItemsSource = Core.Context.products.ToList();
            }
        }
    }
}
