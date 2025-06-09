using System.ComponentModel;
using System.IO.Compression;
using System.Security.AccessControl;
using System.Windows.Input;
using AbroadConcepts.CommandLine;
using AbroadConcepts.IO;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ZippyLibrary.Interfaces;
using ZippyLibrary.Models;

namespace ZippyLibrary.ViewModels;


public partial class ZipViewModel: ObservableObject, IDisposable
{
    private readonly IViewDialog _viewDialog;
    private readonly CloseMethod<int> _closeMethod;
    private bool _isDisposed = false;
    private ZipArchiver _zipArchiver;
    private Stream _zipStream;

    public string PartialFileName => FileName.Substring(FileName.LastIndexOf('\\') + 1);

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(PartialFileName))]
    private string _fileName = string.Empty;

    public string ActionDisplay => string.IsNullOrEmpty(ActionName) ? "Ready" : ActionName;
   
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ActionDisplay))]
    private string _actionName = string.Empty;

    [ObservableProperty]
    private int _actionValue = 0;

    [ObservableProperty]
    private int _maxActionValue = 0;

    [ObservableProperty]
    private bool _isDirty = false;

    public ICommand NewCommand => new RelayCommand(ExecuteNew);
    public ICommand OpenCommand => new RelayCommand(ExecuteOpen);
    public ICommand SaveCommand => new RelayCommand(ExecuteSaveButton);
    public ICommand SaveAsCommand => new RelayCommand(ExecuteSaveAs);
    public ICommand ExitCommand => new RelayCommand(ExecuteExit);
    public ICommand AboutCommand => new RelayCommand(ExecuteAbout);
    public ICommand AddCommand => new RelayCommand(ExecuteAdd);
    public ICommand RemoveCommand => new RelayCommand(ExecuteRemove);

    public ZipViewModel(IViewDialog viewDialog, CloseMethod<int> closeMethod)
    {
        _viewDialog = viewDialog;
        _closeMethod = closeMethod;
        _zipStream = null!;
        _zipArchiver = null!;
        Load(Environment.GetCommandLineArgs().Skip(1).ToArray());
    }

    public void Add(AddZipContent addZipContent)
    {
        _zipArchiver.Add(addZipContent.Filename, addZipContent.EntryLevel, addZipContent.Override, addZipContent.Compression, addZipContent.Directory, (file, message) =>
        {
            IsDirty = true;
            ActionName = $"Added {file}";

            if (message != "OK")
            {
                _viewDialog.ShowError(message);
            }
        });
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void Load(string[] strings)
    {
        var mainArgument = new MainArgument();
        var commandArguments = new CommandArguments(strings);
        if (commandArguments.Parse(mainArgument))
        {
            if (!string.IsNullOrEmpty(mainArgument.ZipFile))
            {
                var fileNames = mainArgument.ZipFile.EnsureExtension(".zip").GetFiles(false, true).ToList();
                if (fileNames.Count == 1)
                {
                    FileName = fileNames[0].Replace(".\\", "");
                    ExecuteOpen(FileName);
                }
                else if (fileNames.Count > 1)
                {
                    _viewDialog.ShowError("Argument represent multiple files");
                }
            }
        }
        else
        {
            ExecuteNew();
        }
    }

    public void Remove(RemoveZipContent removeZipContent)
    {
        foreach (var pattern in removeZipContent.Entries)
        {
            _zipArchiver.Remove(pattern, (file, message) =>
            {
                IsDirty = true;
                ActionName = $"Removed {file}";

                if (message != "OK")
                {
                    _viewDialog.ShowError(message);
                }
            });
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_isDisposed)
        {
            _isDisposed = true;
            DisposeZip();
        }
    }

    private void DisposeZip()
    {
        if (_zipArchiver != null)
        {
            _zipArchiver.Dispose();
            _zipArchiver = null!;
            _zipStream.Dispose();
            _zipStream = null!;
        }
    }

    private void ExecuteNew()
    {
        try
        {
            ExecuteSave();

            DisposeZip();
            FileName = string.Empty;
            _zipStream = new MemoryStream();
            _zipArchiver = new ZipArchiver(_zipStream);
        }
        catch (Exception ex)
        {
            _viewDialog.ShowError(ex.Message);
        }
    }

    private void ExecuteOpen(string fileName)
    {
        ActionName = "Loading";
        var isNew = !File.Exists(fileName);
        _zipStream = File.Open(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
        _zipArchiver = new ZipArchiver(_zipStream);
        if (isNew)
        {
            ExecuteSave(true);
        }
        ActionName = string.Empty;
        ActionValue = 0;
        MaxActionValue = 1;
    }

    private void ExecuteOpen()
    {
        try
        {
            ExecuteSave();
            DisposeZip();
            FileName = _viewDialog.GetOpenFile();
            ExecuteOpen(FileName);
        }
        catch (Exception ex)
        {
            _viewDialog.ShowError(ex.Message);
        }
    }

    private void ExecuteSave(bool overrideCheck = false)
    {
        var shouldSave = IsDirty;
        if (IsDirty && !overrideCheck)
        {
            shouldSave = _viewDialog.ShouldSave();
        }

        if (IsDirty && shouldSave)
        {
            try
            {
                ActionName = "Saving";
                if (string.IsNullOrEmpty(FileName))
                {
                    ExecuteSaveAs();
                }
                else
                {
                    _zipArchiver.Save(_zipStream);
                    IsDirty = false;
                }
            }
            catch (Exception ex)
            {
                _viewDialog.ShowError(ex.Message);
            }
            finally
            {
                ActionValue = 0;
                MaxActionValue = 10;
                ActionName = string.Empty;
            }

        }
    }

    private void ExecuteSaveButton()
    {
        ExecuteSave(true);
    }

    private void ExecuteSaveAs()
    {
        try
        {
            var fileName = _viewDialog.GetSaveFile();
            if (!string.IsNullOrEmpty(fileName))
            {
                using var stream = File.Open(fileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
                _zipArchiver.Save(stream);
                FileName = fileName;
                IsDirty = false;
            }
        }
        catch (Exception ex)
        {
            _viewDialog.ShowError(ex.Message);
        }
    }

    private void ExecuteExit()
    {
        ExecuteSave();
        _closeMethod.Invoke(0);
    }

    private void ExecuteAdd()
    {
        var addZipContent = _viewDialog.GetAddInformation();
        Add(addZipContent);
    }

    private void ExecuteRemove()
    {
        var removeZipContent = _viewDialog.GetRemoveInformation();
        Remove(removeZipContent);
    }

    private void ExecuteAbout()
    {
        _viewDialog.ShowAbout();
    }
}
