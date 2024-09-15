using Microsoft.Win32;


class Programm
{
    static void Main()
    {
        string exePath = @"C:\Users\filin\source\repos\demon\demon\Program.cs";

        SetStartupRegistry(exePath);

        FileSystemWatcher watcher = new FileSystemWatcher();
        watcher.Path = @"C:\PathToMonitor";
        watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite;
        watcher.Filter = "*.txt";
        watcher.Created += OnChanged;
        watcher.Deleted += OnChanged;
        watcher.Changed += OnChanged;
        watcher.EnableRaisingEvents = true;

        Console.WriteLine("Нажмите q чтобы завершить программу");
        while (Console.Read() != 'q') ;
    }

    private static void OnChanged(object source, FileSystemEventArgs e)
    {
        string logMessage = $"{e.ChangeType}: {e.FullPath} at {DateTime.Now}";
        File.AppendAllText(@"C:\PathToLog\log.txt", logMessage + Environment.NewLine);
    }
    private static void SetStartupRegistry(string exePath)
    {
        RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
        key.SetValue("demon", exePath);
    }
}
