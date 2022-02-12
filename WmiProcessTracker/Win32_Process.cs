using System.Management;
using System.Runtime.CompilerServices;

namespace WmiProcessTracker
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/windows/win32/cimwin32prov/win32-process
    /// </summary>
    public class Win32_Process
    {
        readonly ManagementBaseObject _e;
        internal Win32_Process(ManagementBaseObject e)
        {
            _e = e;// (ManagementBaseObject)e.Clone();
        }
        private T Get<T>([CallerMemberName] string name = "") => _e.Get<T>("TargetInstance", name);


        public string CreationClassName => Get<string>();
        public string Caption => Get<string>();
        public string CommandLine => Get<string>();
        //datetime CreationDate;
        public string CreationDate => Get<string>();
        public string CSCreationClassName => Get<string>();
        public string CSName => Get<string>();
        public string Description => Get<string>();
        public string ExecutablePath => Get<string>();
        public UInt16 ExecutionState => Get<UInt16>();
        public string Handle => Get<string>();
        public UInt32 HandleCount => Get<UInt32>();
        //datetime InstallDate;
        public string InstallDate => Get<string>();
        public UInt64 KernelModeTime => Get<UInt64>();
        public UInt32 MaximumWorkingSetSize => Get<UInt32>();
        public UInt32 MinimumWorkingSetSize => Get<UInt32>();
        public string Name => Get<string>();
        public string OSCreationClassName => Get<string>();
        public string OSName => Get<string>();
        public UInt64 OtherOperationCount => Get<UInt64>();
        public UInt64 OtherTransferCount => Get<UInt64>();
        public UInt32 PageFaults => Get<UInt32>();
        public UInt32 PageFileUsage => Get<UInt32>();
        public UInt32 ParentProcessId => Get<UInt32>();
        public UInt32 PeakPageFileUsage => Get<UInt32>();
        public UInt64 PeakVirtualSize => Get<UInt64>();
        public UInt32 PeakWorkingSetSize => Get<UInt32>();
        public UInt32 Priority => Get<UInt32>();
        public UInt64 PrivatePageCount => Get<UInt64>();
        public UInt32 ProcessId => Get<UInt32>();
        public UInt32 QuotaNonPagedPoolUsage => Get<UInt32>();
        public UInt32 QuotaPagedPoolUsage => Get<UInt32>();
        public UInt32 QuotaPeakNonPagedPoolUsage => Get<UInt32>();
        public UInt32 QuotaPeakPagedPoolUsage => Get<UInt32>();
        public UInt64 ReadOperationCount => Get<UInt64>();
        public UInt64 ReadTransferCount => Get<UInt64>();
        public UInt32 SessionId => Get<UInt32>();
        public string Status => Get<string>();
        //public datetime TerminationDate;
        public string TerminationDate => Get<string>();
        public UInt32 ThreadCount => Get<UInt32>();
        public UInt64 UserModeTime => Get<UInt64>();
        public UInt64 VirtualSize => Get<UInt64>();
        public string WindowsVersion => Get<string>();
        public UInt64 WorkingSetSize => Get<UInt64>();
        public UInt64 WriteOperationCount => Get<UInt64>();
        public UInt64 WriteTransferCount => Get<UInt64>();
    };
}