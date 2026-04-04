using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Логика взаимодействия для AddMaterialPage.xaml
    /// </summary>
    public partial class AddMaterialPage : Page
    {
        private MessageHelper _messageHelper = new MessageHelper();
        public materials CurrentMaterial { get; set; }

        public AddMaterialPage()
        {
            InitializeComponent();
            DataContext = CurrentMaterial;
            try
            {
                type_materialComboBox.ItemsSource = Core.Context.type_materials.ToList();
                ed_izmComboBox.ItemsSource = Core.Context.ed_izms.ToList();
            }
            catch (Exception ex)
            {
                _messageHelper.ShowError($"Ошибка при загрузке данных: {ex.Message}");
            }
        }

        public AddMaterialPage(materials Material)
        {
            InitializeComponent();
            CurrentMaterial = Material;
            DataContext = CurrentMaterial;
            try
            {
                type_materialComboBox.ItemsSource = Core.Context.type_materials.ToList();
                ed_izmComboBox.ItemsSource = Core.Context.ed_izms.ToList();
            }
            catch (Exception ex)
            {
                _messageHelper.ShowError($"Ошибка при загрузке данных: {ex.Message}");
            }
            LoadMaterial();
        }

        public void LoadMaterial()
        {
            name_materialTextBox.Text = CurrentMaterial.name_material;
            type_materialComboBox.SelectedIndex = Convert.ToInt32(CurrentMaterial.type_material) - 1;
            kolvo_in_paketTextBox.Text = Convert.ToString(CurrentMaterial.kolvo_in_paket);
            ed_izmComboBox.SelectedIndex = Convert.ToInt32(CurrentMaterial.ed_izm) - 1;
            kolvo_on_skladTextBox.Text = Convert.ToString(CurrentMaterial.kolvo_on_sklad);
            min_ostatokTextBox.Text = Convert.ToString(CurrentMaterial.min_ostatok);
            priceTextBox.Text = Convert.ToString(CurrentMaterial.price);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(name_materialTextBox.Text) ||
                type_materialComboBox.SelectedIndex == -1 ||
                string.IsNullOrWhiteSpace(kolvo_in_paketTextBox.Text) ||
                string.IsNullOrWhiteSpace(kolvo_on_skladTextBox.Text) ||
                string.IsNullOrWhiteSpace(min_ostatokTextBox.Text) ||
                string.IsNullOrWhiteSpace(priceTextBox.Text) ||
                ed_izmComboBox.SelectedIndex == -1)
            {
                _messageHelper.ShowWarning("Укажите все данные");
                return;
            }

            try
            {
                if (CurrentMaterial == null)
                {
                    CurrentMaterial = new materials
                    {
                        name_material = name_materialTextBox.Text,
                        type_materials = type_materialComboBox.SelectedItem as type_materials,
                        kolvo_in_paket = Convert.ToInt32(kolvo_in_paketTextBox.Text),
                        ed_izms = ed_izmComboBox.SelectedItem as ed_izms,
                        kolvo_on_sklad = Convert.ToInt32(kolvo_on_skladTextBox.Text),
                        min_ostatok = Convert.ToInt32(min_ostatokTextBox.Text),
                        price = Convert.ToInt32(priceTextBox.Text),
                    };
                    Core.Context.materials.Add(CurrentMaterial);
                    Core.Context.SaveChanges();
                    _messageHelper.ShowInfo("Материал успешно добавлен");
                }
                else
                {
                    CurrentMaterial.name_material = name_materialTextBox.Text;
                    CurrentMaterial.type_materials = type_materialComboBox.SelectedItem as type_materials;
                    CurrentMaterial.kolvo_in_paket = Convert.ToInt32(kolvo_in_paketTextBox.Text);
                    CurrentMaterial.ed_izms = ed_izmComboBox.SelectedItem as ed_izms;
                    CurrentMaterial.kolvo_on_sklad = Convert.ToInt32(kolvo_on_skladTextBox.Text);
                    CurrentMaterial.min_ostatok = Convert.ToInt32(min_ostatokTextBox.Text);
                    CurrentMaterial.price = Convert.ToInt32(priceTextBox.Text);
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