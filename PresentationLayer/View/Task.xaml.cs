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
        private ColumnModel columnModel;
        public Task()
        {
            InitializeComponent();
        }
        internal Task(BoardVM boardVM, Board board)
        {
            InitializeComponent();
            //this.columnModel = boardVM.columns[0];
            taskVM = new TaskVM(boardVM.user);
            this.DataContext = taskVM;
            this.board = board;
            

        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            TaskModel task = taskVM.AddTask(new ColumnModel(null,null));
            GroupBox taskGroupBox = new GroupBox();
            taskGroupBox.Header = task.Title;
            taskGroupBox.Margin = new Thickness(16);
            taskGroupBox.Content = task.Description;
            board.TasksPanel.Children.Add(taskGroupBox);
            this.Close();
        }
    }
}
