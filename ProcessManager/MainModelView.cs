using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;

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
        RefreshCommand = new RelayCommand(Refresh);
    }

    private void Refresh(object obj)
    {
        processes.Clear();
        foreach (var process in Process.GetProcesses())
        {
            processes.Add(process);
        }
    }

    public ICommand RefreshCommand { get; set; }

}