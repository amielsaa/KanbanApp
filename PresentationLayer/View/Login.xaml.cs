using IntroSE.Kanban.PresentationLayer.ViewModel;
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

namespace IntroSE.Kanban.PresentationLayer.View
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        LoginVM loginVM;
        public Login()
        {
            InitializeComponent();
            loginVM = new LoginVM();
            this.DataContext = loginVM;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var user = loginVM.Login();
            if (user != null) { 
                MainWindow main = new MainWindow(user);
                main.Show();
                this.Close();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            loginVM.Register();
        }
    }
}
