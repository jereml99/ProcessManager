using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;

namespace ProcessManager;

public class MainModelView : INotifyPropertyChanged
{
    private ObservableCollection<Process> _processes;
    public ObservableCollection<Process> processes
    {
        get { return _processes; }
        set
        {
            _processes = value;
            OnPropertyChanged("Processes");
        }
    }

    public MainModelView()
    {
        processes = new ObservableCollection<Process>(Process.GetProcesses());
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string name)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}