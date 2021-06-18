using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public struct Board
    {
        public readonly string BoardName;
        public readonly string CreatorEmail;
        public readonly int TaskId;

        internal Board(string boardName,string creatorEmail,int taskId)
        {
            this.BoardName = boardName;
            this.CreatorEmail = creatorEmail;
            this.TaskId = taskId;
        }
    }
}
