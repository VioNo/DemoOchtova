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
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
        }

        private void AuthButton_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(LoginTextBox.Text))
            {
                MessageBox.Show("введите логин");
                return;
            }
            if (string.IsNullOrWhiteSpace(PassBox.Password))
            {
                MessageBox.Show("введите пароль");
                return;
            }
            var User = Core.Context.users.FirstOrDefault(u =>
            u.login == LoginTextBox.Text && u.password == PassBox.Password);

            if (User == null){
                MessageBox.Show("пользователь не найден");
                return;
            }
            Core.AuthUser = User;
            NavigationService.Navigate(new ProductsPage());
        } 
        
        private void ProductButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProductsPage());
        }
    }
}
