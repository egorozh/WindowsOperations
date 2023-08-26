using System.IO;
using FileOperations.Abstractions;
using FileOperations.Abstractions.Enums;


namespace FileOperations.MacOs;


public class MacOsFileOperations : IFileOperations
{
    public void DeleteDirectory(string directory, DeleteDirectoryOption onDirectoryNotEmpty, RecycleOption recycle = RecycleOption.DeletePermanently)
    {
        if (recycle == RecycleOption.SendToRecycleBin)
        {
            MoveToRecycleBin(directory);
            return;
        }
        
        Directory.Delete(directory, recursive: onDirectoryNotEmpty == DeleteDirectoryOption.DeleteAllContents);
    }

    
    public void DeleteFile(string file, RecycleOption recycle)
    {
        if (recycle == RecycleOption.SendToRecycleBin)
        {
            MoveToRecycleBin(file);
            return;
        }
        
        File.Delete(file);
    }
    

    private void MoveToRecycleBin(string path)
    {
        
    }
}