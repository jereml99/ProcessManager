using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;

namespace ProcessManager;

public class MainModelView : INotifyPropertyChanged
{
    private ObservableCollection<Process> _processes;
    private DispatcherTimer _timer;
    public ObservableCollection<Process> processes
    {
        get { return _processes; }
        set
        {
            _processes = value;
            OnPropertyChanged("processes");
        }
    }
    
    public Process? SelectedProcess { get; set; }

    public MainModelView()
    {
        processes = new ObservableCollection<Process>(Process.GetProcesses());
        RefreshCommand = new RelayCommand(Refresh);
        StartAutomaticRefreshCommand = new RelayCommand(StartAutomaticRefresh);
        PauseAutomaticRefreshCommand = new RelayCommand(PauseAutomaticRefresh, CanPauseAutomaticRefresh);
        FilterCommand = new RelayCommand(Filter);
        KillCommand = new RelayCommand(Kill, CanKill);
        
        _timer = new DispatcherTimer();
        _timer.Tick += ((sender, args) => Refresh(null));
    }

    private bool CanKill(object obj) => SelectedProcess != null;

    private void Kill(object obj)
    {
        SelectedProcess.Kill();
        SelectedProcess = null;
        Refresh(null);
    }


    private void Refresh(object obj)
    {
        processes.Clear();
        foreach (var process in Process.GetProcesses())
        {
            processes.Add(process);
        }
    }

    private bool CanPauseAutomaticRefresh(object obj) => _timer.IsEnabled;
    
    private void PauseAutomaticRefresh(object obj)
    {
        _timer.Stop();
    }
    
    
    private void StartAutomaticRefresh(object obj)
    {
        _timer.Stop();
        _timer.Interval = TimeSpan.FromSeconds(Int32.Parse(obj.ToString()));
        _timer.Start();
    }
    
    private void Filter(object obj)
    {
        processes.Clear();
        foreach (var process in Process.GetProcesses()
                     .Where(p => p.ProcessName.IndexOf(obj.ToString(), StringComparison.OrdinalIgnoreCase) != -1))
        {
            processes.Add(process);
        }
    }
    

    public ICommand RefreshCommand { get; set; }
    public ICommand StartAutomaticRefreshCommand { get; set; }
    
    public ICommand PauseAutomaticRefreshCommand { get; set; }
    
    public ICommand FilterCommand { get; set; }
    public ICommand KillCommand { get; set; }
    

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string name)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
    

}
