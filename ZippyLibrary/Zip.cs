using AbroadConcepts.CommandLine;
using AbroadConcepts.IO;

using ZippyLibrary.Interfaces;
using ZippyLibrary.Models;

namespace ZippyLibrary;
public class Zip: IZip, IDisposable
{
    public string FileName { get; private set; } = string.Empty;
    private ZipArchiver _zipArchiver = null!;

    private Stream _zipStream = null!;
    public bool IsDirty { get; set; }
    private bool _isDisposed;
    private readonly IViewDialog _viewDialog;

    public string Action { get; set; } = string.Empty;

    public double ActionValue { get; set; }

    public double MaxValue { get; set; } 

    public Zip(IViewDialog viewDialog)
    {
        _viewDialog = viewDialog;
        ExecuteNew();
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
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

    public bool ExecuteNew()
    {
        try
        {
            ExecuteSave();

            DisposeZip();
            FileName = string.Empty;
            _zipStream = new MemoryStream();
            _zipArchiver = new ZipArchiver(_zipStream);
            return true;
        }
        catch (Exception ex)
        {
            _viewDialog.ShowError(ex.Message);
            return false;
        }
    }

    public bool ExecuteOpen()
    {
        try
        {
            ExecuteSave();
            DisposeZip();
            FileName = _viewDialog.GetOpenFile();
            ExecuteOpen(FileName);
            return true;
        }
        catch (Exception ex)
        {
            _viewDialog.ShowError(ex.Message);
            return false;
        }
    }

    public void ExecuteOpen (string fileName)
    {
        Action = "Loading";
        var isNew = !File.Exists(fileName);
        _zipStream = File.Open(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
        _zipArchiver = new ZipArchiver(_zipStream);
        if (isNew)
        {
            IsDirty = true;
            ExecuteSave(true);
        }
        Action = string.Empty;
        ActionValue = 0;
        MaxValue = 1;

    }

    public bool ExecuteSave(bool overrideCheck = false)
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
                Action = "Saving";
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
                return false;
            }
            finally
            {
                ActionValue = 0;
                MaxValue = 10;
                Action = string.Empty;
            }

            return true;
        }

        return false;
    }

    public bool ExecuteSaveAs()
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

            return true;
        }
        catch (Exception ex)
        {
            _viewDialog.ShowError(ex.Message);
            return false;
        }
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
                    FileName = fileNames[0].Replace(".\\","");
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
            _viewDialog.ShowError(commandArguments.Message);
        }
    }

    public void ShowAbout()
    {
        _viewDialog.ShowAbout();
    }

    public bool Add(AddZipContent addContent)
    {

        try
        {
            MaxValue = addContent.Filename.GetFiles().ToList().Count;
            ActionValue = 0;

            _zipArchiver.Add(addContent.Filename, addContent.EntryLevel, addContent.Override, addContent.Compression, addContent.Directory, (fileName, message) =>
            {
                Action = $"Added {fileName}";
                if (!string.IsNullOrEmpty(message))
                {
                    _viewDialog.ShowError(message);
                }
                ActionValue++;
            });
            IsDirty = true;
            return true;
        }
        catch (Exception ex)
        {
            _viewDialog.ShowError(ex.Message);
            return false;
        }
        finally
        {
            ActionValue = 0;
            Action = string.Empty;
        }
    }
    public bool Remove(RemoveZipContent removeContent)
    {

        try
        {
            MaxValue = removeContent.Entries.Count;
            ActionValue = 0;

            foreach(var entry in removeContent.Entries)
            {
                ActionValue++;
                Action = $"Remove {entry}";
                _zipArchiver.Remove(entry);
            }
            IsDirty = true;
            return true;
        }
        catch (Exception ex)
        {
            _viewDialog.ShowError(ex.Message);
            return false;
        }
        finally
        {
            ActionValue = 0;
            Action = string.Empty;
        }
    }
}
