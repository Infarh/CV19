using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using CV19.ViewModels.Base;

namespace CV19.ViewModels
{
    /// <summary>
    /// Информация о каталогах
    /// </summary>
    class DirectoryViewModel : ViewModel
    {
        private readonly DirectoryInfo _DirectoryInfo;
        /// <summary>
        /// Информация по субкаталогам
        /// </summary>
        public IEnumerable<DirectoryViewModel> SubDirectories
        {
            get
            {
                try
                {
                    return _DirectoryInfo
                       .EnumerateDirectories()
                       .Select(dir_info => new DirectoryViewModel(dir_info.FullName));
                }
                catch (UnauthorizedAccessException e)
                {
                    Debug.WriteLine(e.ToString());
                }

                return Enumerable.Empty<DirectoryViewModel>();
            }
        }
        /// <summary>
        /// Информация по файлам
        /// </summary>
        public IEnumerable<FileViewModel> Files
        {
            get
            {
                try
                {
                    var files = _DirectoryInfo
                       .EnumerateFiles()
                       .Select(file => new FileViewModel(file.FullName));
                    return files;
                }
                catch (UnauthorizedAccessException e)
                {
                    Debug.WriteLine(e.ToString());
                }

                return Enumerable.Empty<FileViewModel>();
            }
        }
        /// <summary>
        /// Элемент каталога
        /// </summary>
        public IEnumerable<object> DirectoryItems
        {
            get
            {
                try
                {
                    return SubDirectories.Cast<object>().Concat(Files);
                }
                catch (UnauthorizedAccessException e)
                {
                    Debug.WriteLine(e.ToString());
                }
                return Enumerable.Empty<object>();
            }
        }
        /// <summary>
        /// Название каталога
        /// </summary>
        public string Name => _DirectoryInfo.Name;
        /// <summary>
        /// Полный путь к каталогу
        /// </summary>
        public string Path => _DirectoryInfo.FullName;
        /// <summary>
        /// Время создания каталога
        /// </summary>
        public DateTime CreationTime => _DirectoryInfo.CreationTime;
        /// <summary>
        /// Получение вьюмодели каталога
        /// </summary>
        /// <param name="Path">Путь к каталогу</param>
        public DirectoryViewModel(string Path) => _DirectoryInfo = new DirectoryInfo(Path);
    }
    /// <summary>
    /// Информация по файлам
    /// </summary>
    class FileViewModel : ViewModel
    {
        private FileInfo _FileInfo;
        /// <summary>
        /// Название файла
        /// </summary>
        public string Name => _FileInfo.Name;
        /// <summary>
        /// Полный путь к файлу
        /// </summary>
        public string Path => _FileInfo.FullName;
        /// <summary>
        /// Время создания
        /// </summary>
        public DateTime CreationTime => _FileInfo.CreationTime;
        /// <summary>
        /// Получение вьюмодели файла
        /// </summary>
        /// <param name="Path">Полный путь к файлу</param>
        public FileViewModel(string Path) => _FileInfo = new FileInfo(Path);
    }
}
