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
        public string Password { get { return password; } set {  } }
        private List<string> oldPassword;
        public List<Task> myAssignments;
        public string email;
        private Boards boards;
        public Boolean login;
        public UserDTO dtoUser;
        public readonly int passMinLen = 4;
        public readonly int passMaxLen = 20;
        public BoardController boardController;
        //constructor
        public User(string em, string pw, BoardController bController)
        {
            email =validateEmail(em);
            oldPassword = new List<string>();
            if (!validatePasswordRules(pw))
                throw new ArgumentException("this password does'nt stand in the password rules");   
            password = pw;
            List<Board> boardList  = boardController.getAllUserBoards(email);
            boards = new Boards(boardList ,em,0);
            this.boardController.AddBoardsToBC(email, boards);
            login = false;
            boardController = bController;
        }
        public User(string em, string pw, List<string> oldPw, List<Task> myAssignments, int boardsId, BoardController boardController)
        {
            email = em;
            password = pw;
            oldPassword = oldPw;
            this.myAssignments = myAssignments;
            List<Board> boardList = boardController.getAllUserBoards(email);
            this.boards = new Boards(boardList, em, boardsId);
            this.boardController.AddBoardsToBC(email, boards);
            login = false;
        }
        //methods

        /// <summary>
        /// Add a new board.
        /// </summary>
        /// <param name="name"> the name of the new Board, it must not be used by this user before. The user must be logged in.</param>
        /// <returns>A board with the name it was given and 3 empty columns, in a case where the name was already used it shoulf return an error message </returns>
        public Board newBoard(string name)
        {
            name = boards.getValidatename(name);
            Board board = new Board(name, email,boards.id, new Column("backlog"), new Column("in progress"), new Column("done"));
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
            boards.removeBoard(board);
            boardController.deleteBoard(board);
        }
        public void logout()
        {
            login = false;
        }
        /// <summary>
        /// change the user's password.
        /// </summary>
        /// <param name="newPassword">The new password for the user. The user must be logged in.</param>
        /// <returns>it returns nothing, it checks the validity of the password and change it if it valide</returns>
        private void changePassword(string newPassword)
        {
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
        /// checking validity of an email
        /// </summary>
        /// <param name="email">Email of user. Must be logged in</param>
        /// <returns>a string variable with the email, if it isn't valid it returns an error insted. </returns>
        public string validateEmail(string email)
        {
            string expression = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
            + "@"
            + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$";
            if (email!=null&&email[email.Length-1]<65)
                throw new ArgumentException("email isn't valid");
            if (Regex.IsMatch(email, expression))
                return email;
            else
                throw new ArgumentException("email isn't valid");
        }
        /// <summary>
        /// get all the tasks in "inProgress" column from all the boards of the user
        /// </summary>
        /// <returns>A list of al the tasks in "inProgress", it can return null there aren't any.</returns>
        public List<Task> getAllInProgressTasks()
        {
            List<Task> list = boards.getAllInProgressTasks();
            return list;
        }

        /// <summary>
        /// get a board by using it's name
        /// </summary>
        /// <param name="name">the name of the board the user search for </param>
        /// <returns>A board if there is a board with this name or null if there is no board with this name</returns>
        public Board getBoardByName(string name)
        {
            return boards.getBoardByName(email, name);
        }

        public void joinBoard(User otherUser , string boardName)
        {
            Board board = otherUser.getBoardByName(boardName);
            board.boardUsers.Add(email);
            boards.addboard( board);
            boardController.addBoard(board);

        }
        public void changeAssignee(User assignee, Task task)
        {
            myAssignments.Remove(task);
            task.assigneeEmail = assignee.email;
            assignee.myAssignments.Add(task);
            new DTask().Update(email, task.boardId, task.taskId, TaskDTO.AssigneeColumnName, assignee.email);

        }

    }
}

