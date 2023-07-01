using System;
using System.IO;

namespace SupportLibrary.IO
{
    /// <summary>
    /// Interface for File access related tasks.<para/>
    /// For example: Read and Save both Text and Binary files, or clean-up old files from a directory.
    /// </summary>
    public interface IFileHelper
    {
        /// <summary>
        /// Reads a binary file in the specified path.<para/>
        /// Uses the current WindowsIdentity.ImpersonationLevel settings.
        /// </summary>
        /// <param name="filePath">Directory path to the file. If not exits a DirectoryNotFoundException will be thrown.</param>
        /// <param name="fileName">Name of the file. If not exits a FileNotFoundException will be thrown.</param>
        /// <returns>A byte array containing the contents of the file.</returns>
        byte[] ReadBinary(string filePath, string fileName);

        /// <summary>
        /// Reads a binary file in the specified path.
        /// </summary>
        /// <param name="filePath">Directory path to the file. If not exits a DirectoryNotFoundException will be thrown.</param>
        /// <param name="fileName">Name of the file. If not exits a FileNotFoundException will be thrown.</param>
        /// <param name="revertImpersonation">Flag to revert impersonation while reading the file.</param>
        /// <returns>A byte array containing the contents of the file.</returns>
        byte[] ReadBinary(string filePath, string fileName, bool revertImpersonation);

        /// <summary>
        /// Reads a text file in the specified path.<para/>
        /// Uses the current WindowsIdentity.ImpersonationLevel settings.
        /// </summary>
        /// <param name="filePath">Directory path to the file. If not exits a DirectoryNotFoundException will be thrown.</param>
        /// <param name="fileName">Name of the file. If not exits a FileNotFoundException will be thrown.</param>
        /// <returns>A string containing the contents of the file.</returns>        
        string ReadText(string filePath, string fileName);

        /// <summary>
        /// Reads a text file in the specified path.
        /// </summary>
        /// <param name="filePath">Directory path to the file. If not exits a DirectoryNotFoundException will be thrown.</param>
        /// <param name="fileName">Name of the file. If not exits a FileNotFoundException will be thrown.</param>
        /// <param name="revertImpersonation">Flag to revert impersonation while reading the file.</param>
        /// <returns>A string containing the contents of the file.</returns>
        string ReadText(string filePath, string fileName, bool revertImpersonation);

        /// <summary>
        /// Creates or overwrites a binary file in the specified path with the given content.<para/>
        /// Uses the current WindowsIdentity.ImpersonationLevel settings.
        /// </summary>
        /// <param name="filePath">Directory path to the file. If not exits it will be created.</param>
        /// <param name="fileName">Name of the file. If exits it will be overwritten.</param>
        /// <param name="fileContent">The buffer containing data to write on the file.</param>
        void SaveBinary(string filePath, string fileName, byte[] fileContent);

        /// <summary>
        /// Creates or overwrites a file in the specified path with the given content.
        /// </summary>
        /// <param name="filePath">Directory path to the file. If not exits it will be created.</param>
        /// <param name="fileName">Name of the file. If exits it will be overwritten.</param>
        /// <param name="fileContent">The buffer containing data to write on the file.</param>
        /// <param name="revertImpersonation">Flag to revert impersonation while saving the file.</param>
        void SaveBinary(string filePath, string fileName, byte[] fileContent, bool revertImpersonation);

        /// <summary>
        /// Creates or overwrites a text file in the specified path with the given content.<para/>
        /// Uses the current WindowsIdentity.ImpersonationLevel settings.
        /// </summary>
        /// <param name="filePath">Directory path to the file. If not exits it will be created.</param>
        /// <param name="fileName">Name of the file. If exits it will be overwritten.</param>
        /// <param name="fileContent">The buffer containing data to write on the file.</param>
        void SaveText(string filePath, string fileName, string fileContent);

        /// <summary>
        /// Creates or overwrites a text file in the specified path with the given content.<para/>
        /// </summary>
        /// <param name="filePath">Directory path to the file. If not exits it will be created.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="fileMode">File operation mode.</param>
        /// <param name="fileContent">The buffer containing text to write on the file.</param>
        /// <param name="revertImpersonation">Flag to revert impersonation while saving the file.</param>
        void SaveText(string filePath, string fileName, TextFileMode fileMode, string fileContent, bool revertImpersonation);

        /// <summary>
        /// Deletes old files from the given path.<para/>
        /// Uses the SearchOption.TopDirectoryOnly and the current WindowsIdentity.ImpersonationLevel settings.
        /// </summary>
        /// <param name="filePath">Directory path to the files. If not exits a DirectoryNotFoundException will be thrown.</param>
        /// <param name="filterPattern">The search string to match against the names of files in path, or 'null' to apply no filter. For example: '*.*', '*.txt' or '*.docx'.</param>
        /// <param name="daysBack">Amount of days to consider a file as old.</param>
        void DeleteOldFiles(string filePath, string filterPattern, uint daysBack);

        /// <summary>
        /// Deletes old files from the given path.
        /// </summary>
        /// <param name="filePath">Directory path to the files. If not exits a DirectoryNotFoundException will be thrown.</param>
        /// <param name="filterPattern">The search string to match against the names of files in path, or 'null' to apply no filter. For example: '*.*', '*.txt' or '*.docx'.</param>
        /// <param name="daysBack">Amount of days to consider a file as old.</param>
        /// <param name="searchOption">One of the enumeration values that specifies whether the search operation should inlude all subdirectories or only the current directory.</param>
        /// <param name="revertImpersonation">Flag to revert impersonation while the saving of the file.</param>
        void DeleteOldFiles(string filePath, string filterPattern, uint daysBack, SearchOption searchOption, bool revertImpersonation);
    }
}
