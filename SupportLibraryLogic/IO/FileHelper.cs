using System;
using System.IO;
using System.Security.Principal;
using System.Runtime.InteropServices;

namespace SupportLibrary.IO
{
    /// <summary>
    /// Helper class for File access related tasks.<para/>
    /// For example: Read and Save both Text and Binary files, or clean-up old files from a directory.
    /// </summary>
    public sealed class FileHelper : IFileHelper
    {
        [DllImport("advapi32.dll")]
        private static extern int RevertToSelf();

        /// <summary>
        /// Reads a binary file in the specified path.<para/>
        /// Uses the current WindowsIdentity.ImpersonationLevel settings.
        /// </summary>
        /// <param name="filePath">Directory path to the file. If not exits a DirectoryNotFoundException will be thrown.</param>
        /// <param name="fileName">Name of the file. If not exits a FileNotFoundException will be thrown.</param>
        /// <returns>A byte array containing the contents of the file.</returns>
        public byte[] ReadBinary(string filePath, string fileName)
        {
            try
            {
                return ReadBinary(filePath, fileName, false);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Reads a binary file in the specified path.
        /// </summary>
        /// <param name="filePath">Directory path to the file. If not exits a DirectoryNotFoundException will be thrown.</param>
        /// <param name="fileName">Name of the file. If not exits a FileNotFoundException will be thrown.</param>
        /// <param name="revertImpersonation">Flag to revert impersonation while reading the file.</param>
        /// <returns>A byte array containing the contents of the file.</returns>
        public byte[] ReadBinary(string filePath, string fileName, bool revertImpersonation)
        {
            WindowsIdentity currentUser = null;

            try
            {
                string fullName = Path.Combine(filePath, fileName);

                // obtener el usuario con el que esta corriendo el proceso (en ASP.net: Impersonate = true)
                currentUser = WindowsIdentity.GetCurrent();

                if (revertImpersonation && currentUser.ImpersonationLevel == TokenImpersonationLevel.Impersonation)
                {
                    // P/Invoke: revierte la impersonalización desde este punto del código
                    // desde aquí se ejecuta con la identidad base (en ASP.net: Impersonate = false)
                    RevertToSelf();
                }

                // si no existen, generar excepción
                if (!Directory.Exists(filePath))    { throw new DirectoryNotFoundException(); }
                if (!File.Exists(fullName))         { throw new FileNotFoundException(); }

                // leer el archivo desde disco & retornar
                Byte[] fileContent = File.ReadAllBytes(fullName);

                return fileContent;
            }
            catch (Exception) { throw; }
            finally
            {
                // volver a ejecutar bajo la identidad original del usuario (en ASP.net: Impersonate = true)
                if (revertImpersonation && currentUser.ImpersonationLevel == TokenImpersonationLevel.None) { currentUser.Impersonate(); }
            }
        }

        /// <summary>
        /// Reads a text file in the specified path.<para/>
        /// Uses the current WindowsIdentity.ImpersonationLevel settings.
        /// </summary>
        /// <param name="filePath">Directory path to the file. If not exits a DirectoryNotFoundException will be thrown.</param>
        /// <param name="fileName">Name of the file. If not exits a FileNotFoundException will be thrown.</param>
        /// <returns>A string containing the contents of the file.</returns>
        public string ReadText(string filePath, string fileName)
        {
            try
            {
                return ReadText(filePath, fileName, false);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Reads a text file in the specified path.
        /// </summary>
        /// <param name="filePath">Directory path to the file. If not exits a DirectoryNotFoundException will be thrown.</param>
        /// <param name="fileName">Name of the file. If not exits a FileNotFoundException will be thrown.</param>
        /// <param name="revertImpersonation">Flag to revert impersonation while reading the file.</param>
        /// <returns>A string containing the contents of the file.</returns>
        public string ReadText(string filePath, string fileName, bool revertImpersonation)
        {
            WindowsIdentity currentUser = null;

            try
            {
                string fullName = Path.Combine(filePath, fileName);

                // obtener el usuario con el que esta corriendo el proceso (en ASP.net: Impersonate = true)
                currentUser = WindowsIdentity.GetCurrent();

                if (revertImpersonation && currentUser.ImpersonationLevel == TokenImpersonationLevel.Impersonation)
                {
                    // P/Invoke: revierte la impersonalización desde este punto del código
                    // desde aquí se ejecuta con la identidad base (en ASP.net: Impersonate = false)
                    RevertToSelf();
                }

                // si no existen, generar excepción
                if (!Directory.Exists(filePath))    { throw new DirectoryNotFoundException(); }
                if (!File.Exists(fullName))         { throw new FileNotFoundException(); }

                // leer el archivo desde disco & retornar
                string fileContent = File.ReadAllText(fullName);

                return fileContent;
            }
            catch (Exception) { throw; }
            finally
            {
                // volver a ejecutar bajo la identidad original del usuario (en ASP.net: Impersonate = true)
                if (revertImpersonation && currentUser.ImpersonationLevel == TokenImpersonationLevel.None) { currentUser.Impersonate(); }
            }
        }

        /// <summary>
        /// Creates or overwrites a binary file in the specified path with the given content.<para/>
        /// Uses the current WindowsIdentity.ImpersonationLevel settings.
        /// </summary>
        /// <param name="filePath">Directory path to the file. If not exits it will be created.</param>
        /// <param name="fileName">Name of the file. If exits it will be overwritten.</param>
        /// <param name="fileContent">The buffer containing data to write on the file.</param>
        public void SaveBinary(string filePath, string fileName, byte[] fileContent)
        {
            try
            {
                SaveBinary(filePath, fileName, fileContent, false);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Creates or overwrites a file in the specified path with the given content.
        /// </summary>
        /// <param name="filePath">Directory path to the file. If not exits it will be created.</param>
        /// <param name="fileName">Name of the file. If exits it will be overwritten.</param>
        /// <param name="fileContent">The buffer containing data to write on the file.</param>
        /// <param name="revertImpersonation">Flag to revert impersonation while saving the file.</param>
        public void SaveBinary(string filePath, string fileName, byte[] fileContent, bool revertImpersonation)
        {
            WindowsIdentity currentUser = null;

            try
            {
                string fullName = Path.Combine(filePath, fileName);

                // obtener el usuario con el que esta corriendo el proceso (en ASP.net: Impersonate = true)
                currentUser = WindowsIdentity.GetCurrent();

                if (revertImpersonation && currentUser.ImpersonationLevel == TokenImpersonationLevel.Impersonation)
                {
                    // P/Invoke: Revierte la impersonalización desde este punto del codigo
                    // desde aquí se ejecuta con la identidad base (en ASP.net: Impersonate = false)
                    RevertToSelf();
                }

                // si no existe, crear el directorio
                if (!Directory.Exists(filePath)) { Directory.CreateDirectory(filePath); }

                // guardar el archivo a disco
                using (FileStream fileStream = File.Create(fullName))
                {
                    fileStream.Write(fileContent, 0, fileContent.Length);
                }
            }
            catch (Exception) { throw; }
            finally
            {
                // volver a ejecutar bajo la identidad original del usuario (en ASP.net: Impersonate = true)
                if (revertImpersonation && currentUser.ImpersonationLevel == TokenImpersonationLevel.None) { currentUser.Impersonate(); }
            }
        }

        /// <summary>
        /// Creates or overwrites a text file in the specified path with the given content.<para/>
        /// Uses the current WindowsIdentity.ImpersonationLevel settings.
        /// </summary>
        /// <param name="filePath">Directory path to the file. If not exits it will be created.</param>
        /// <param name="fileName">Name of the file. If exits it will be overwritten.</param>
        /// <param name="fileContent">The buffer containing data to write on the file.</param>
        public void SaveText(string filePath, string fileName, string fileContent)
        {
            try
            {
                SaveText(filePath, fileName, TextFileMode.Create, fileContent, false);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Creates or overwrites a text file in the specified path with the given content.<para/>
        /// </summary>
        /// <param name="filePath">Directory path to the file. If not exits it will be created.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="fileMode">File operation mode.</param>
        /// <param name="fileContent">The buffer containing text to write on the file.</param>
        /// <param name="revertImpersonation">Flag to revert impersonation while saving the file.</param>
        public void SaveText(string filePath, string fileName, TextFileMode fileMode, string fileContent, bool revertImpersonation)
        {
            TextWriter textWriter = null;
            WindowsIdentity currentUser = null;

            try
            {
                string fullName = Path.Combine(filePath, fileName);

                // obtener el usuario con el que esta corriendo el proceso (en ASP.net: Impersonate = true)
                currentUser = WindowsIdentity.GetCurrent();

                if (revertImpersonation && currentUser.ImpersonationLevel == TokenImpersonationLevel.Impersonation)
                {
                    // P/Invoke: revierte la impersonalización desde este punto del codigo
                    // desde aquí se ejecuta con la identidad base (en ASP.net: Impersonate = false)
                    RevertToSelf();
                }

                // si no existe, crear el directorio
                if (!Directory.Exists(filePath)) { Directory.CreateDirectory(filePath); }

                // guardar el archivo a disco
                switch (fileMode)
	            {
                    case TextFileMode.Create:   textWriter = File.CreateText(fullName); break;
                    case TextFileMode.Append:   textWriter = File.AppendText(fullName); break;
                    default: throw new ArgumentException(String.Format("FileMode.{0} is invalid.", fileMode), "fileMode");
	            }

                textWriter.Write(fileContent);
            }
            catch (Exception) { throw; }
            finally
            {
                if (textWriter != null) { textWriter.Dispose(); }
                // volver a ejecutar bajo la identidad original del usuario (en ASP.net: Impersonate = true)
                if (revertImpersonation && currentUser.ImpersonationLevel == TokenImpersonationLevel.None) { currentUser.Impersonate(); }
            }
        }

        /// <summary>
        /// Deletes old files from the given path.<para/>
        /// Uses the SearchOption.TopDirectoryOnly and the current WindowsIdentity.ImpersonationLevel settings.
        /// </summary>
        /// <param name="filePath">Directory path to the files. If not exits a DirectoryNotFoundException will be thrown.</param>
        /// <param name="filterPattern">The search string to match against the names of files in path, or 'null' to apply no filter. For example: '*.*', '*.txt' or '*.docx'.</param>
        /// <param name="daysBack">Amount of days to consider a file as old.</param>
        public void DeleteOldFiles(string filePath, string filterPattern, uint daysBack)
        {
            try
            {
                DeleteOldFiles(filePath, filterPattern, daysBack, SearchOption.TopDirectoryOnly, false);
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Deletes old files from the given path.
        /// </summary>
        /// <param name="filePath">Directory path to the files. If not exits a DirectoryNotFoundException will be thrown.</param>
        /// <param name="filterPattern">The search string to match against the names of files in path, or 'null' to apply no filter. For example: '*.*', '*.txt' or '*.docx'.</param>
        /// <param name="daysBack">Amount of days to consider a file as old.</param>
        /// <param name="searchOption">One of the enumeration values that specifies whether the search operation should inlude all subdirectories or only the current directory.</param>
        /// <param name="revertImpersonation">Flag to revert impersonation while the saving of the file.</param>
        public void DeleteOldFiles(string filePath, string filterPattern, uint daysBack, SearchOption searchOption, bool revertImpersonation)
        {
            filterPattern = filterPattern ?? "*.*";
            WindowsIdentity currentUser = null;

            try
            {
                // obtener el Token de la identidad con la que esta corriendo el proceso de ASP (Impersonate = true)
                currentUser = WindowsIdentity.GetCurrent();

                if (revertImpersonation && currentUser.ImpersonationLevel == TokenImpersonationLevel.Impersonation)
                {
                    // P/Invoke: revierte la impersonalización desde este punto del codigo
                    // desde aquí se ejecuta con la identidad base (en ASP.net: Impersonate = false)
                    RevertToSelf();
                }

                // si no existen, generar excepción
                if (!Directory.Exists(filePath))    { throw new DirectoryNotFoundException(); }

                // buscar todos los archivos
                string[] files = Directory.GetFiles(filePath, filterPattern, searchOption);

                // borrar archivos 'antiguos'
                for (int i = 0; i < files.Length; i++)
                {
                    DateTime creationTime = File.GetCreationTime(files[i]);
                    if (creationTime < DateTime.Now.AddDays(-daysBack))  { File.Delete(files[i]); }
                }
            }
            catch (Exception) { throw; }
            finally
            {
                // volver a ejecutar bajo la identidad original del usuario (en ASP.net: Impersonate = true)
                if (revertImpersonation && currentUser.ImpersonationLevel == TokenImpersonationLevel.None) { currentUser.Impersonate(); }
            }
        }
    }
}
