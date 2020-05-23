using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using CV19.ViewModels.Base;

namespace CV19.ViewModels
{
    class DirectoryViewModel : ViewModel
    {
        private readonly DirectoryInfo _DirectoryInfo;

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

        public string Name => _DirectoryInfo.Name;

        public string Path => _DirectoryInfo.FullName;

        public DateTime CreationTime => _DirectoryInfo.CreationTime;

        public DirectoryViewModel(string Path) => _DirectoryInfo = new DirectoryInfo(Path);
    }

    class FileViewModel : ViewModel
    {
        private FileInfo _FileInfo;

        public string Name => _FileInfo.Name;

        public string Path => _FileInfo.FullName;

        public DateTime CreationTime => _FileInfo.CreationTime;

        public FileViewModel(string Path) => _FileInfo = new FileInfo(Path);
    }
}
