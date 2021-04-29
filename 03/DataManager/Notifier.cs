namespace Chapter_03_Dependency_Injection.DataManager
{
    public interface INotifier
    {
        bool SendMessage(string message);
    }

    public class Notifier : INotifier
    {
        public bool SendMessage(string message)
        {
            // some logic here to publish the message

            return true;
        }
    }
}