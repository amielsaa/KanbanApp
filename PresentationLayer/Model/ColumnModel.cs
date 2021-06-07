using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace IntroSE.Kanban.PresentationLayer.Model
{
    public class ColumnModel : NotifiableModelObject
    {

        private readonly UserModel user;
        public ObservableCollection<TaskModel> Tasks { get; set; }

        private ColumnModel(BackendController controller, ObservableCollection<TaskModel> tasks) : base(controller)
        {
            this.Tasks = tasks;
            Tasks.CollectionChanged += HandleChange;
        }

        public ColumnModel(BackendController controller, UserModel user) : base(controller)
        {
            this.user = user;
            //Tasks = new ObservableCollection<ColumnModel>(controller.GetAllMessagesIds(user.Email).
                //Select((c, i) => new ColumnModel(controller, controller.GetMessage(user.Email, i), user)).ToList());
            Tasks.CollectionChanged += HandleChange;
        }


        public void RemoveMessage(TaskModel t)
        {

            Tasks.Remove(t);

        }

        private void HandleChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            //read more here: https://stackoverflow.com/questions/4279185/what-is-the-use-of-observablecollection-in-net/4279274#4279274
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (TaskModel y in e.OldItems)
                {

                    //Controller.RemoveMessage(user.Email, y.Id);
                }

            }
        }

    }
}