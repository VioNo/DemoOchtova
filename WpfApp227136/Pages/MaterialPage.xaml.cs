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
    /// Логика взаимодействия для MaterialPage.xaml
    /// </summary>
    public partial class MaterialPage : Page
    {
        private MessageHelper _messageHelper = new MessageHelper();

        public MaterialPage()
        {
            InitializeComponent();

            try
            {
                MaterialListBox.ItemsSource = Core.Context.materials.ToList();
            }
            catch (Exception ex)
            {
                _messageHelper.ShowError($"Ошибка при загрузке материалов: {ex.Message}");
            }

            FIOTextBlock.Text = Core.AuthUser.fio;

            switch (Core.AuthUser.role)
            {
                case 2: //Мененджер 
                    MaterialStackPanel.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddMaterialPage());
        }

        private void MaterialListBox_MouseClick(object sender, SelectionChangedEventArgs e)
        {
            // Редактировать может только админ (role = 1)
            if (Core.AuthUser != null && Core.AuthUser.role == 1)
            {
                if (MaterialListBox.SelectedItem is materials selectedMaterial)
                    NavigationService.Navigate(new AddMaterialPage(selectedMaterial));
            }
            else
            {
                _messageHelper.ShowInfo("Вы не можете редактировать данные");
            }


            
        }

        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            if (MaterialListBox.SelectedItem is materials selectedMaterial)
            {
                var MessageBoxResult = MessageBox.Show("Вы точно хотите удалить материал?", "Удалить", MessageBoxButton.YesNo);
                if (MessageBoxResult == MessageBoxResult.Yes)
                {
                    try
                    {
                        Core.Context.materials.Remove(selectedMaterial);
                        Core.Context.SaveChanges();
                        MaterialListBox.ItemsSource = Core.Context.materials.ToList();
                    }
                    catch (Exception ex)
                    {
                        _messageHelper.ShowError($"Ошибка при удалении материала: {ex.Message}");
                    }
                }
            }
        }
        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.Visibility == Visibility.Visible)
            {
                try
                {
                    MaterialListBox.ItemsSource = Core.Context.products.ToList();
                }
                catch (Exception ex)
                {
                    _messageHelper.ShowError($"Ошибка при обновлении данных: {ex.Message}");
                }
            }
        }
    }
}