using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
            Id_productTextBlock.Visibility = Visibility.Collapsed;
            Id_productTextBlock2.Visibility = Visibility.Collapsed;
            type_productComboBox.ItemsSource = Core.Context.type_products.ToList();
            CurrentProduct = new products();
            Core.Context.products.Add(CurrentProduct);
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
            if (!string.IsNullOrWhiteSpace(CurrentProduct.picture))
            {
                try
                {
                    string imagePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, CurrentProduct.picture);
                    if (File.Exists(imagePath))
                    {
                        pictureImage.Source = new BitmapImage(new Uri(imagePath, UriKind.Absolute));
                    }
                }
                catch (Exception ex)
                {
                    _messageHelper.ShowError($"Ошибка при загрузке изображения: {ex.Message}");
                }
            }
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
            else if (Convert.ToDouble(min_priceTextBox.Text) <= 0 || Convert.ToInt32(kolvo_peopleTextBox.Text) <= 0)
            {
                _messageHelper.ShowError("Стоимость продукции и количество человек для производства продукции должны быть больше 0!");
                return;
            }

            CurrentProduct.name_product = name_productTextBox.Text;
            CurrentProduct.art = Convert.ToInt32(artTextBox.Text);
            CurrentProduct.min_price = Convert.ToInt32(min_priceTextBox.Text);
            CurrentProduct.kolvo_people = Convert.ToInt32(kolvo_peopleTextBox.Text);
            CurrentProduct.nomer_cexa = Convert.ToInt32(nomer_cexaTextBox.Text);
            CurrentProduct.type_products = type_productComboBox.SelectedItem as type_products;          
            Core.Context.SaveChanges();
            _messageHelper.ShowInfo("Данные успешно обновлены");
            NavigationService.GoBack();
            
        }

        private void imageButton_Click(object sender, RoutedEventArgs e)
        {
            string MainPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "product");

            OpenFileDialog NewImage = new OpenFileDialog();
            NewImage.Filter = "(*.png, *.jpg)|*.png;*.jpg";
            if (NewImage.ShowDialog() == true)
            {
                CurrentProduct.ImagePath = NewImage.FileName;
                _messageHelper.ShowInfo("Картинка загружена успешно");
            }
        }
    }
}