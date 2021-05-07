using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DalObjects
{
    public abstract class DTO
    {
        public const string EmailColumnName = "email";
        protected DalController _controller;
        public string Email { get; set; } = "";
        protected DTO(DalController controller)
        {
            _controller = controller;
        }

    }
}
