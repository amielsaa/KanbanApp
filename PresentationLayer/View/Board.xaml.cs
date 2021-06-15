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
    /// Interaction logic for Board.xaml
    /// </summary>
    public partial class Board : Window
    {

        private BoardVM boardVM;
        private UserModel user;
        private BoardModel boardModel;
        public Board(UserModel user, BoardModel boardModel) 
        {
            InitializeComponent();
            this.user = user;
            this.boardModel = boardModel;
            this.boardVM = new BoardVM(user,boardModel); 
            this.DataContext = boardVM;
        }

        /*
        private void Load()
        {
            var columns = boardVM.Load();

            foreach(var column in columns)
            {
                StackPanel stackPanel = new StackPanel();
                Name = $"column{column.ColumnOrdinal}column";

                foreach (var task in column.Tasks)
                {
                    GroupBox taskBox = new GroupBox();
                    taskBox.Header = task.Title;
                    taskBox.Margin = new Thickness(16);
                    taskBox.Content = new TextBlock() { Text = task.Description};
                    stackPanel.Children.Add(taskBox);
                }

                GroupBox groupBox = new GroupBox();
                groupBox.Header = column.Title;
                groupBox.Height = 505;
                groupBox.Width = 257;
                groupBox.Margin = new Thickness(16);
                groupBox.Content = stackPanel;

                this.ColumnStackPanel.Children.Add(groupBox);
            }
            
        }*/

        private void Button_Add_Task(object sender, RoutedEventArgs e)
        {
            Task task = new Task(boardVM.Board.Columns[0], user,"CREATE");
            task.Show();

        }

        private void Edit_Task_Button(object sender, RoutedEventArgs e)
        {
            TaskModel task_to_edit = boardVM.EditTask();
            Task task = new Task(task_to_edit,user,"CONFIRM",boardVM.Board.BoardName);
            task.Show();
        }


        private void PopupBox_OnOpened(object sender,RoutedEventArgs e)
        {

        }
        private void PopupBox_OnClosed(object sender, RoutedEventArgs e)
        {

        }

        private void Remove_Task_Button(object sender, RoutedEventArgs e)
        {
            //boardVM.RemoveTask(((Button)sender).Tag);
        }

        private void Advance_Task_Button(object sender, RoutedEventArgs e)
        {
            //boardVM.AdvanceTask();
        }

        private void Move_Left_Button(object sender, RoutedEventArgs e)
        {
            boardVM.MoveLeft();
        }

        private void Move_Right_Button(object sender, RoutedEventArgs e)
        {
            boardVM.MoveRight();
        }
        private void Button_Add_Column(object sender, RoutedEventArgs e)
        {
            //boardVM.AddColumn();
        }
        private void Remove_Column_Button(object sender, RoutedEventArgs e)
        {
            boardVM.RemoveColumn();
        }

        private void Assign_Task_Button(object sender, RoutedEventArgs e)
        {

        }

        private void Add_Column_Button(object sender, RoutedEventArgs e)
        {

        }

        private void Limit_Column_Button(object sender, RoutedEventArgs e)
        {

        }
    }
}
