using System.IO;
using AppKit;
using FileOperations.Abstractions;
using FileOperations.Abstractions.Enums;
using Foundation;


namespace FileOperations.MacOs;


/// <summary>
/// Use <see cref="Initialize"/> for working api
/// </summary>
public class MacOsFileOperations : IFileOperations
{
    public static void Initialize() => NSApplication.Init();
 
    
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


    private void MoveToRecycleBin(string path, bool isDir)
    {
        NSFileManager fileManager = NSFileManager.DefaultManager;

        var url = new NSUrl(path, isDir: isDir);

        if (url is not null)
            fileManager.TrashItem(url, out var res, out var error);
    }
}