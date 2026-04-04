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

namespace WpfApp227136.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddProductPage.xaml
    /// </summary>
    public partial class AddProductPage : Page
    {
        public products CurrentProduct { get; set; }
        public AddProductPage()
        {
            InitializeComponent();
            type_productComboBox.ItemsSource = Core.Context.type_products.ToList();
            DataContext = CurrentProduct;
        }
        public AddProductPage(products Product)
        {
            InitializeComponent();
            CurrentProduct = Product;
            type_productComboBox.ItemsSource = Core.Context.type_products.ToList();
            DataContext = CurrentProduct;
            LoadProduct();
        }
        public void LoadProduct()
        {
            name_productTextBox.Text = CurrentProduct.name_product;
            artTextBox.Text = Convert.ToString(CurrentProduct.art);
            min_priceTextBox.Text = Convert.ToString(CurrentProduct.min_price);
            kolvo_peopleTextBox.Text = Convert.ToString(CurrentProduct.kolvo_people);
            nomer_cexaTextBox.Text = Convert.ToString(CurrentProduct.nomer_cexa);
            type_productComboBox.SelectedIndex = Convert.ToInt32 (CurrentProduct.type_product) -1;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(name_productTextBox.Text) ||
                string.IsNullOrWhiteSpace(artTextBox.Text) ||
                string.IsNullOrWhiteSpace(min_priceTextBox.Text) ||
                string.IsNullOrWhiteSpace(kolvo_peopleTextBox.Text) ||
                string.IsNullOrWhiteSpace(nomer_cexaTextBox.Text) 
                || type_productComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Укажите все данные");
                return;
            }
            if (CurrentProduct == null)
            {
                CurrentProduct = new products
                {
                    name_product = name_productTextBox.Text,
                    art = Convert.ToInt32(artTextBox.Text),
                    min_price = Convert.ToInt32(min_priceTextBox.Text),
                    kolvo_people = Convert.ToInt32(kolvo_peopleTextBox.Text),
                    nomer_cexa = Convert.ToInt32(nomer_cexaTextBox.Text),
                    type_products = type_productComboBox.SelectedItem as type_products
                };
                Core.Context.products.Add(CurrentProduct);
                Core.Context.SaveChanges();
            }
            else
            {
                CurrentProduct.name_product = name_productTextBox.Text;
                CurrentProduct.art = Convert.ToInt32(artTextBox.Text);
                CurrentProduct.min_price = Convert.ToInt32(min_priceTextBox.Text);
                CurrentProduct.kolvo_people = Convert.ToInt32(kolvo_peopleTextBox.Text);
                CurrentProduct.nomer_cexa = Convert.ToInt32(nomer_cexaTextBox.Text);
                CurrentProduct.type_products = type_productComboBox.SelectedItem as type_products;
                Core.Context.SaveChanges();
                MessageBox.Show("Данные успешно обновлены");
            }
            NavigationService.GoBack();
        }
        
    }
}
