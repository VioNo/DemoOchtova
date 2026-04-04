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
        private MessageHelper _messageHelper = new MessageHelper();
        public products CurrentProduct { get; set; }
        public AddProductPage()
        {
            InitializeComponent();
            try
            {
                type_productComboBox.ItemsSource = Core.Context.type_products.ToList();
                Id_productTextBlock.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                _messageHelper.ShowError($"Ошибка при загрузке типов продуктов: {ex.Message}");
            }
            DataContext = CurrentProduct;
        }
        public AddProductPage(products Product)
        {
            InitializeComponent();
            CurrentProduct = Product;
            try
            {
                type_productComboBox.ItemsSource = Core.Context.type_products.ToList();
            }
            catch (Exception ex)
            {
                _messageHelper.ShowError($"Ошибка при загрузке типов продуктов: {ex.Message}");
            }
            DataContext = CurrentProduct;
            LoadProduct();
        }
        public void LoadProduct()
        {
            Id_productTextBlock.Text = Convert.ToString(CurrentProduct.id);
            name_productTextBox.Text = CurrentProduct.name_product;
            artTextBox.Text = Convert.ToString(CurrentProduct.art);
            min_priceTextBox.Text = Convert.ToString(CurrentProduct.min_price);
            kolvo_peopleTextBox.Text = Convert.ToString(CurrentProduct.kolvo_people);
            nomer_cexaTextBox.Text = Convert.ToString(CurrentProduct.nomer_cexa);
            type_productComboBox.SelectedIndex = Convert.ToInt32(CurrentProduct.type_product) - 1;
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
                _messageHelper.ShowWarning("Укажите все данные");
                return;
            }
            else if(Convert.ToDecimal(min_priceTextBox.Text) > 0 || Convert.ToInt32(kolvo_peopleTextBox.Text) > 0)
            {
                _messageHelper.ShowError("Стоимость продукции и/или количество человек для производства продукции должно быть больше 0!");
            }
            try
            {
                if (CurrentProduct == null)
                {
                    CurrentProduct = new products
                    {
                        name_product = name_productTextBox.Text,
                        art = Convert.ToInt32(artTextBox.Text),
                        min_price = Convert.ToDecimal(min_priceTextBox.Text),
                        kolvo_people = Convert.ToInt32(kolvo_peopleTextBox.Text),
                        nomer_cexa = Convert.ToInt32(nomer_cexaTextBox.Text),
                        type_products = type_productComboBox.SelectedItem as type_products
                    };
                    Core.Context.products.Add(CurrentProduct);
                    Core.Context.SaveChanges();
                    _messageHelper.ShowInfo("Продукт успешно добавлен");
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
                    _messageHelper.ShowInfo("Данные успешно обновлены");
                }
                NavigationService.GoBack();
            }
            catch (FormatException ex)
            {
                _messageHelper.ShowError($"Ошибка формата данных: {ex.Message}\nПроверьте правильность ввода числовых значений.");
            }
            catch (Exception ex)
            {
                _messageHelper.ShowError($"Ошибка при сохранении данных: {ex.Message}");
            }
        }
    }
}