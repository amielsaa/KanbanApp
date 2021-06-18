using IntroSE.Kanban.PresentationLayer.Model;
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
    /// Interaction logic for Task.xaml
    /// </summary>
    public partial class Task : Window
    {
        private Board board;
        private TaskVM taskVM;
        public Task(TaskModel task, UserModel user ,string content, string boardName)
        {
            InitializeComponent();
            taskVM = new TaskVM(task, user, content,boardName);
            this.DataContext = taskVM;
        }
        public Task(ColumnModel columnModel, UserModel user ,string content )
        {
            InitializeComponent();
            taskVM = new TaskVM(columnModel, user,content);
            this.DataContext = taskVM;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if(((Button)sender).Content == "CREATE")
            {
                var res = taskVM.AddTask();
                if (res)
                {
                    this.Close();
                }
                
            }
            else
            {
                var res = taskVM.EditTask();
                if (res)
                {
                    this.Close();
                }
                
            }
                
        }
    }
}
