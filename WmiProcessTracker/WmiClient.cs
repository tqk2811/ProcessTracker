using System.Management;

namespace WmiProcessTracker
{
    /// <summary>
    /// https://stackoverflow.com/questions/43269057/using-wmi-to-monitor-process-creation-event/43284669 <br>
    /// </br>https://docs.microsoft.com/en-us/windows/win32/wmisdk/--instancedeletionevent <br>
    /// </br>https://docs.microsoft.com/en-us/dotnet/api/system.management.eventwatcheroptions?view=dotnet-plat-ext-6.0
    /// </summary>
    public class WmiClient : IDisposable
    {
        readonly ManagementEventWatcher watcherCreation;
        readonly ManagementEventWatcher watcherDeletion;
        public event Action<Win32_Process> ProcessCreation;
        public event Action<Win32_Process> ProcessDeletion;
        /// <summary>
        /// 
        /// </summary>
        public WmiClient(int blockSize = 1024)
        {
            {
                ManagementScope managementScope = new ManagementScope();
                EventQuery eventQuery = new EventQuery("SELECT * FROM __InstanceCreationEvent WITHIN 1 WHERE TargetInstance ISA 'Win32_Process'");//__InstanceDeletionEvent 
                EventWatcherOptions eventOptions = new EventWatcherOptions();
                eventOptions.BlockSize = blockSize;
                eventOptions.Timeout = TimeSpan.MaxValue;

                watcherCreation = new ManagementEventWatcher(managementScope, eventQuery, eventOptions);
                watcherCreation.EventArrived += this.WatcherCreation_EventArrived; ;
                watcherCreation.Stopped += this.Watcher_Stopped;
            }
            {
                ManagementScope managementScope = new ManagementScope();
                EventQuery eventQuery = new EventQuery("SELECT * FROM __InstanceDeletionEvent WITHIN 1 WHERE TargetInstance ISA 'Win32_Process'");//__InstanceDeletionEvent 
                EventWatcherOptions eventOptions = new EventWatcherOptions();
                eventOptions.BlockSize = blockSize;
                eventOptions.Timeout = TimeSpan.MaxValue;

                watcherDeletion = new ManagementEventWatcher(managementScope, eventQuery, eventOptions);
                watcherDeletion.EventArrived += this.WatcherDeletion_EventArrived;
                watcherDeletion.Stopped += this.Watcher_Stopped;
            }
        }

        private void WatcherDeletion_EventArrived(object sender, EventArrivedEventArgs e)
        {
            //Console.WriteLine(
            //"Process {0} has been created, path is: {1}",
            //((ManagementBaseObject)e.NewEvent
            //["TargetInstance"])["Name"],
            //((ManagementBaseObject)e.NewEvent
            //["TargetInstance"])["ExecutablePath"]);
            Win32_Process win32_Process = new Win32_Process(e.NewEvent);
            ThreadPool.QueueUserWorkItem((obj) => ProcessDeletion?.Invoke(win32_Process), null);
        }

        private void WatcherCreation_EventArrived(object sender, EventArrivedEventArgs e)
        {
            Win32_Process win32_Process = new Win32_Process(e.NewEvent);
            ThreadPool.QueueUserWorkItem((obj) => ProcessCreation?.Invoke(win32_Process), null);
        }

        private void Watcher_Stopped(object sender, StoppedEventArgs e)
        {
            ManagementEventWatcher watcher = sender as ManagementEventWatcher ?? throw new NullReferenceException(nameof(sender));
            switch (e.Status)
            {
                default:
                    watcher.Start();
                    break;
            }
        }

        ~WmiClient()
        {
            watcherCreation.Dispose();
        }

        public void Dispose()
        {
            watcherCreation.Dispose();
            GC.SuppressFinalize(this);
        }

        public void Start()
        {
            watcherCreation.Start();
            watcherDeletion.Start();
        }

        public void Test()
        {
            while (true)
            {

                ManagementBaseObject e = watcherCreation.WaitForNextEvent();
                string text = $"Process {((ManagementBaseObject)e["TargetInstance"])["Name"]} has been created, path is: {((ManagementBaseObject)e["TargetInstance"])["ExecutablePath"]}";
            }
        }
    }
}