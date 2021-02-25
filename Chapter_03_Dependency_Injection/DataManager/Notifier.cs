namespace Chapter_03_Dependency_Injection.DataManager
{
    public interface INotifier
    {
        bool SendMessage(string message);
    }
    ///
    /// <summary> Class <c>Notifier<c> contains a SendMessage method
    /// that returns true
    public class Notifier : INotifier
    {
        public bool SendMessage(string message)
        {
            return true;
        }
    }
}