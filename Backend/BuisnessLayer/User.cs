﻿using System;
using introSE.KanbanBoard.Backend.BuisnessLayer;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace introSE.KanbanBoard.Backend.BuisnessLayer
{

    public class User
    {
        // feilds
        private string password;
        private List<string> oldPassword;
        public string email;
        private Boards boards;
        public Boolean login;
        public readonly int passMinLen = 4;
        public readonly int passMaxLen = 20;
        //constructor
        public User(string em, string pw)
        {
            email = em;
            password = validatePasswod(pw);
            password = pw;
            oldPassword = new List<string>();
            boards = new Boards();
        }
        //methods
        public Board newBoard(string name)
        {
            name = boards.getValidatename(name);
            Board board = new Board(name, this, new Column("backlog"), new Column("in progress"), new Column("done"));
            boards.addboard(board, name);
            return board;
        }
        public void removeBoard(Board board)
        {
            boards.removeBoard(board);
        }
        public void logout()
        {
            login = false;
        }
        private void changePassword(string newPassword)
        {
            newPassword = validatePasswod(newPassword);
            oldPassword.Add(password);
            password = newPassword;
        }
        private Boolean validatePasswordRules(string pw) {
            Boolean checkNum = false;
            Boolean checkCapital = false;
            Boolean checkSmall = false;
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
        private Boolean validatePasswordMatch(string pw)
        {
            foreach (string i in oldPassword)
            {
                if (pw.Equals(i))
                    return false;
            }
            return true;
        }
        public string validatePasswod(string newPassword)
        {
            while (!(validatePasswordMatch(newPassword) & validatePasswordRules(newPassword)))
            {
                Console.WriteLine("choose a password that hasn't been used or a password that complies with the rules");
                newPassword = Console.ReadLine();
            }
            return newPassword;
        }
        public string getpassword()
        {
            return password;
        }
        public List<Task> getAllInProgressTasks()
        {
            List<Task> list = new List<Task>();
            foreach (Board i in boards.boards)
            {
                List<Task> listToAdd = i.getInProgressTasks();
                list.AddRange(listToAdd);
            }
            return list;
        }

        public Board getBoardByName(string name)
        {
            return boards.getBoardByName(name);
        }

    }
}

