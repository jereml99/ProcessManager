using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;

namespace ProcessManager;

public class MainModelView
{
    private ObservableCollection<Process> _processes;
    public ObservableCollection<Process> processes
    {
        get { return _processes; }
        set
        {
            _processes = value;
        }
    }

    public MainModelView()
    {
        processes = new ObservableCollection<Process>(Process.GetProcesses());
    }
    
}