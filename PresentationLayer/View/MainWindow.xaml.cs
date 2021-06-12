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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        MainWindowVM mainWindowVM;
        UserModel user;

        public MainWindow(UserModel user)
        {
            InitializeComponent();
            this.user = user;
            mainWindowVM = new MainWindowVM(user);
            this.DataContext = mainWindowVM;
            LoadBoards();
        }

        private void LoadBoards()
        {
            var boardNames = mainWindowVM.boardNames;
            foreach(string boardName in boardNames)
            {
                AddBoardToView(boardName);
            }
        }


        private void Enter_Button(object sender, RoutedEventArgs e)
        {
            var myValue = ((Button)sender).Tag;
            var boardModel = mainWindowVM.LoadBoard(myValue.ToString());
            Board board = new Board(user, boardModel,this);
            board.Show();
            this.Hide();
        }

        private void Remove_Button(object sender, RoutedEventArgs e)
        {
            var boardName = ((StackPanel)((Button)sender).Parent).Name;
            if (mainWindowVM.RemoveBoard(boardName))
            {
                this.BoardStackPanel.Children.RemoveAt(Int32.Parse(((Button)sender).Tag.ToString()));
                
            }
        }
        
        private void Add_Board_Button(object sender, RoutedEventArgs e)
        {
            string boardName = mainWindowVM.AddBoard();
            this.text_box.Text = "";
            if(boardName != null)
            {
                AddBoardToView(boardName);
            }
            
        }
        /* GroupBox Example
         <WrapPanel Orientation="Vertical" VerticalAlignment="Center" Width="200">
                        <GroupBox Header="BoardName" Height="200"  Margin="10,0,10,5" Content="fsdafasd"/>
                        <Button  Content="Enter Board" Tag="BoardName" Width="200" Margin="0,0,0,5" Click="Enter_Button"/>
                        <Button  Content="Remove Board" Width="200" Click="Remove_Button" />
                    </WrapPanel>
         */
        private void AddBoardToView(string boardName)
        {
            StackPanel stackPanel = new StackPanel();
            stackPanel.VerticalAlignment = VerticalAlignment.Center;
            stackPanel.Orientation = Orientation.Vertical;
            //stackPanel.Tag = $"{this.BoardStackPanel.Children.Count}";
            //stackPanel.Name = boardName.Replace(" ","");

            GroupBox groupBox = new GroupBox();
            groupBox.Header = boardName;
            groupBox.Height = 200;
            groupBox.Width = 200;
            groupBox.Margin = new Thickness(5);
            stackPanel.Children.Add(groupBox);

            Button enterButton = new Button();
            enterButton.Content = "Enter Board";
            enterButton.Tag = boardName;
            enterButton.Width = 200;
            enterButton.Margin = new Thickness(5);
            enterButton.Click += Enter_Button;
            stackPanel.Children.Add(enterButton);

            Button removeButton = new Button();
            removeButton.Content = "Remove Board";
            removeButton.Tag = this.BoardStackPanel.Children.Count;
            removeButton.Width = 200;
            removeButton.Margin = new Thickness(5);
            removeButton.Click += Remove_Button;
            stackPanel.Children.Add(removeButton);

            this.BoardStackPanel.Children.Add(stackPanel);
            
        }
    }
}
