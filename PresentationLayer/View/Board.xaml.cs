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
            boardVM.AdvanceTask();
        }

        private void Move_Left_Button(object sender, RoutedEventArgs e)
        {
            boardVM.MoveLeft();
        }

        private void Move_Right_Button(object sender, RoutedEventArgs e)
        {
            boardVM.MoveRight();
        }
        
        private void Remove_Column_Button(object sender, RoutedEventArgs e)
        {
            boardVM.RemoveColumn();
        }

        private void Assign_Task_Button(object sender, RoutedEventArgs e)
        {
            boardVM.AssignTask();
        }

        private void Add_Column_Button(object sender, RoutedEventArgs e)
        {
            boardVM.AddColumn();
        }

        private void Limit_Column_Button(object sender, RoutedEventArgs e)
        {
            boardVM.LimitColumn();
        }

        private void StackPanel_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }

        private void Back_Button(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow(user);
            main.Show();
            this.Close();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Sort_DueDate_Button(object sender, RoutedEventArgs e)
        {
            boardVM.SortTasks();
        }

        private void Search_Task_Button(object sender, RoutedEventArgs e)
        {
            boardVM.SearchTasks();
        }

        private void Reset_Task_Filter_Button(object sender, RoutedEventArgs e)
        {
            boardVM.TaskKeyWord = "";
            boardVM.SearchTasks();
        }
    }
}
