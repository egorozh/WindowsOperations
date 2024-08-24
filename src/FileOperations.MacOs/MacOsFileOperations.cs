using System.IO;
using System.Linq;
using FileOperations.Abstractions;
using FileOperations.Abstractions.Enums;
using Foundation;

namespace FileOperations.MacOs;

public class MacOsFileOperations : IFileOperations
{
    public void DeleteDirectory(string directory, DeleteDirectoryOption onDirectoryNotEmpty,
        RecycleOption recycle = RecycleOption.DeletePermanently)
    {
        if (recycle == RecycleOption.SendToRecycleBin)
        {
            MoveToRecycleBin(directory, isDir: true);
            return;
        }

        Directory.Delete(directory, recursive: onDirectoryNotEmpty == DeleteDirectoryOption.DeleteAllContents);
    }


    public void DeleteFile(string file, RecycleOption recycle)
    {
        if (recycle == RecycleOption.SendToRecycleBin)
        {
            MoveToRecycleBin(file, isDir: false);
            return;
        }

        File.Delete(file);
    }

    public string[] GetLogicalDrives()
    {
        string driveName = Directory.GetLogicalDrives().First();

        return [driveName];
    }

    private void MoveToRecycleBin(string path, bool isDir)
    {
        NSFileManager fileManager = NSFileManager.DefaultManager;

        var url = new NSUrl(path, isDir: isDir);

        fileManager.TrashItem(url, out var res, out var error);
    }
}