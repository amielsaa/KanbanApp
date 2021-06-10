using System;
using introSE.KanbanBoard.Backend.BuisnessLayer;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using IntroSE.Kanban.Backend.DataAccessLayer.DalObjects;
using IntroSE.Kanban.Backend.BuisnessLayer;
using IntroSE.Kanban.Backend.DataAccessLayer;

namespace introSE.KanbanBoard.Backend.BuisnessLayer
{

    public class User
    {
        // feilds
        private string password;
        private List<string> oldPassword;
        public List<Task> myAssignments;
        public string email;
        private Boards boards;
        public Boolean login;
        public UserDTO dtoUser;
        public readonly int passMinLen = 4;
        public readonly int passMaxLen = 20;
        private BoardController boardController;
       
        //constructor

        //create new user constructor
        public User(string em, string pw )
        {
            oldPassword = new List<string>();
            if (!validatePasswordRules(pw))
                throw new ArgumentException("this password doesn't stand in the password rules");
            email = em;
            password = pw;
            this.boardController = BoardController.getInstance();
            List<Board> boardList = new List<Board>() ;
            boards = new Boards(boardList ,0);
            boardController.AddBoardsToBC(email, boards);
            login = false;
            myAssignments = new List<Task>();
            
        }

        // pull info from dal constructor
        public User(string em, string pw, List<string> oldPw, List<Task> myAssignments, int boardsId  )
        {
            email = em;
            password = pw;
            oldPassword = oldPw;
            this.myAssignments = myAssignments;
            this.boardController = BoardController.getInstance();
            List<Board> boardList = this.boardController.getAllUserBoards(email);
            this.boards = new Boards(boardList, boardsId);
            boardController.AddBoardsToBC(email, boards);
            login = false;
        }
//---------------------------------------------------------methods----------------------------------------------------------------------------------------------------------------
      
//--------------------------------------------Login methods ----------------------------------------------------------------------------------------------------------------------

         public void logout()
        {
            if (!login)
                throw new ArgumentException("User isn't logged in");
            login = false;
        }
        public void checkIfLogedIn()
        {
            if (!login)
                throw new ArgumentException("you can't do this action if the user is'nt log in");
        }
//--------------------------------------------board methods ----------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Add a new board.
        /// </summary>
        /// <param name="name"> the name of the new Board, it must not be used by this user before. The user must be logged in.</param>
        /// <returns>A board with the name it was given and 3 empty columns, in a case where the name was already used it shoulf return an error message </returns>
        public Board newBoard(string name)
        {
            //checkIfLogedIn();
            name = boards.getValidatename(name);
            Board board = new Board(name, email,boards.id);
            boards.addboard( board);
            boardController.addBoard(board);
            DUserController dUser = new DUserController();
            dUser.updateBoardsIdNum(email, boards.id + 1);
            return board;
        }
        /// <summary>
        /// remove a board 
        /// </summary>
        /// <param name="board"> the borad the user want to delete. the user must be logged in to delete it.</param>
        /// <returns>it returns nothing, instead it calls Boards class function to delete it.</returns>
        public void removeBoard(Board board)
        {
            checkIfLogedIn();
            if (email != board.creatorEmail)
                throw new ArgumentException("only the creator can delete his board");
            boards.removeBoard(board);
            boardController.deleteBoard(board);
        }

        /// <summary>
        /// get one of the user's board by its name
        /// </summary>
        /// <param name="name"> the name of the board the user is looking for .</param>
        /// <returns> returns a board with the defines name.</returns>
        public Board getBoardByName(string name)
        {
            return boards.getBoardByName(email, name);
        }

        /// <summary>
        /// joining user to other users board
        /// </summary>
        /// <param name="boardCreator"> the creator of the board this user wants to join .</param>
        /// <param name="boardName"> the name of the board the user wants to join to .</param>
        /// <returns> return nothing, only adding board to boards of this user.</returns>
        public void joinBoard(User boardCreator, string boardName)
        {
            //checkIfLogedIn();
            Board board = boardCreator.getBoardByName(boardName);
            if (board == null)
                throw new ArgumentException("No such board was found");
            board.boardUsers.Add(email);
            boards.addboard(board);
            (new DBoardsController()).Update(boardCreator.email, board.id, BoardsDTO.UsersEmailColumnName, string.Join(",", board.boardUsers));


        }
        /// <summary>
        /// change the column limit (board parameter is added for dal uses later)
        /// </summary>
        /// <param name="column"> the column we want to change its limit .</param>
        /// <param name="board"> the board where the column is in .</param>
        /// <param name="newLimit"> the new limit for the column .</param>
        /// <returns> returns nothing, it checks if the column is in the board and calls columns function.</returns>
        public void ChangeColumnLimit(Column column, Board board, int newLimit,string creatorEmail)
        {
            checkIfLogedIn();
            if (board.columns.Exists(x => x == column))
            {
                column.changeLimit(newLimit, creatorEmail, board.id, board.columns.IndexOf(column));
            }
        }
///--------------------------------------------password and email methods -----------------------------------------------------------------------------------------------------------

        /// <summary>
        /// change the user's password.
        /// </summary>
        /// <param name="newPassword">The new password for the user. The user must be logged in.</param>
        /// <returns>it returns nothing, it checks the validity of the password and change it if it valide</returns>
        private void changePassword(string newPassword)
        {
            //checkIfLogedIn();
            DUserController dUser = new DUserController();
            dUser.InsertOldPassword(password,email);
            newPassword = validatePasswod(newPassword);
            oldPassword.Add(password);
            password = newPassword;
            dtoUser.Password = newPassword;
            
        }
        /// <summary>
        /// checking password validity to the rules
        /// </summary>
        /// <param name="pw">the password which being checked for validity of the rules</param>
        /// <returns>A boolean variable.true if the password stand in the rules and false if it doesn't</returns>
        private Boolean validatePasswordRules(string pw) {
            Boolean checkNum = false;
            Boolean checkCapital = false;
            Boolean checkSmall = false;
            if (pw == null)
                return false;
            if (pw.Length < passMinLen | pw.Length > passMaxLen)
                return checkNum;

            foreach (char i in pw)
            {
                //capitall letter check
                if (i > 64 & i < 91)
                {
                    checkCapital = true;
                }
                //number check
                if (i > 47 & i < 58)
                {
                    checkNum = true;
                }
                //small character check
                if (i > 96 & i < 123)
                {
                    checkSmall = true;
                }
            }
            if (!(checkCapital & checkNum & checkSmall))
                return false;
            else
                return true;

        }
        /// <summary>
        /// checking if the password has been used before by the user
        /// </summary>
        /// <param name="pw">the new password of the user</param>
        /// <returns>A boolean variable, return true if it hasn't been used before and false otherwise</returns>
        private Boolean validatePasswordMatch(string pw)
        {
            foreach (string i in oldPassword)
            {
                if (pw.Equals(i))
                    return false;
            }
            return true;
        }
        /// <summary>
        /// check password validity to the rules and that it hasn't been used before
        /// </summary>
        /// <param name="newPassword">Email of user. Must be logged in</param>
        /// <returns>A string variable with the new password, if it isn't valid it returns an error message</returns>
        public string validatePasswod(string newPassword)
        {
            if (!(validatePasswordMatch(newPassword) & validatePasswordRules(newPassword)))
                throw new ArgumentException("the password doesn't stand in the password rules or it was already used" );
            return newPassword;
        }


        

        /// <summary>
        /// check if passwords are the same (to keep password private)
        /// </summary>
        /// <param name="password"> the password we compare to the user's password</param>
        /// <returns>a boolean variable - true if the passwords are equal, and false otherwise </returns>
        public bool equalPasswords(string password)
        {
            if (password.Equals(this.password))
                return true;
            else
                return false;
        }

///--------------------------------------------Task methods ---------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// get all the tasks in "inProgress" column from all the boards of the user
        /// </summary>
        /// <returns>A list of al the tasks in "inProgress", it can return null there aren't any.</returns>
        public List<Task> getAllInProgressTasks()
        {
            //checkIfLogedIn();
            List<Task> list = new List<Task>();
            foreach (Task task in myAssignments)
            {
                if (task.columnOrdinal == 1)
                    list.Add(task);
            }
            return list;
        }


        /// <summary>
        /// get a board by using it's name
        /// </summary>
        /// <param name="name">the name of the board the user search for </param>
        /// <returns>A board if there is a board with this name or null if there is no board with this name</returns> 
        public void changeAssignee(User newAssignee, Task task)
        {
            //checkIfLogedIn();
            myAssignments.Remove(task);
            task.assigneeEmail = newAssignee.email;
            newAssignee.myAssignments.Add(task);
            (new DTask()).Update(task.email, task.boardId, task.taskId, TaskDTO.AssigneeColumnName, newAssignee.email);

        }
        


    }
}

