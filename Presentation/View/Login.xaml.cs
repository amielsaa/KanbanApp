using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using IntroSE.Kanban.Presentation.ViewModel;


using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace IntroSE.Kanban.Presentation.View
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window

    {
        LoginVM loginVM = new LoginVM();
        public Login()
        {
            InitializeComponent();
            this.DataContext = loginVM;
        }

        private void InitializeComponent()
        {
            throw new NotImplementedException();
        }

    }
}
