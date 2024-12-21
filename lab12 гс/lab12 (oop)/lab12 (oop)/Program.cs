//using System;
//using System.IO;
//using System.Text;
//using System.IO.Compression;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace lab_12
//{
//    class LogHelper
//    {
//        public static string LogFilePath = "D:\\уник\\3 сем\\ООП\\lab12\\lab12 (oop)\\lab12 (oop)\\bin\\Debug\\net8.0\\logfile.txt";

//        public static async Task WriteToLog(string data)
//        {
//            await File.AppendAllTextAsync(LogFilePath, '\n' + data);
//        }

//        public static async Task ReadLog()
//        {
//            Console.WriteLine(await File.ReadAllTextAsync(LogFilePath));
//        }

//        public static async Task CreateLogFile()
//        {
//            if (!File.Exists(LogFilePath))
//            {
//                using (File.Create(LogFilePath)) { }
//            }

//            await File.AppendAllTextAsync(LogFilePath, "Log Start\n\n");
//            await File.AppendAllTextAsync(LogFilePath, "Date: ");
//            await File.AppendAllTextAsync(LogFilePath, DateTime.Now.ToShortDateString());
//            await File.AppendAllTextAsync(LogFilePath, "\nTime: ");
//            await File.AppendAllTextAsync(LogFilePath, DateTime.Now.ToShortTimeString());
//            await File.AppendAllTextAsync(LogFilePath, "\n\nINFO:\n");
//        }

//        public static async Task FilterLogByKeyword(string keyword = null)
//        {
//            try
//            {
//                var lines = await File.ReadAllLinesAsync(LogFilePath);
//                var filteredLines = new List<string>();
//                int count = 0;

//                int currentHour = DateTime.Now.Hour;

//                foreach (var line in lines)
//                {
//                    bool matches = true;

//                    if (!string.IsNullOrEmpty(keyword) && !line.Contains(keyword, StringComparison.OrdinalIgnoreCase))
//                    {
//                        matches = false;
//                    }

//                    if (line.Contains("Time:"))
//                    {
//                        var timePart = line.Split(':')[1].Trim();
//                        if (TimeSpan.TryParse(timePart, out var logTime))
//                        {
//                            if (logTime.Hours != currentHour)
//                            {
//                                matches = false;
//                            }
//                        }
//                        else
//                        {
//                            matches = false; // Time parsing failed
//                        }
//                    }

//                    if (matches)
//                    {
//                        filteredLines.Add(line);
//                        count++;
//                    }
//                }

//                Console.WriteLine($"Found {count} records.");
//                Console.WriteLine(string.Join("\n", filteredLines));

//                Console.WriteLine("Log filtered, only current hour records left.");
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Error filtering logs: {ex.Message}");
//            }
//        }
//    }

//    class DiskHelper
//    {
//        public static async Task LogFreeSpace(string drive)
//        {
//            long space;

//            DriveInfo driveInfo = new DriveInfo(drive);
//            space = driveInfo.AvailableFreeSpace / 1024 / 1024 / 1024;

//            await LogHelper.WriteToLog($"Free space on {drive}: {space} GB");
//        }

//        public static async Task LogFileSystem(string drive)
//        {
//            if (!Directory.Exists(drive)) throw new Exception("Directory does not exist");
//            await LogHelper.WriteToLog($"File system on {drive}: {new DriveInfo(drive).DriveFormat}");
//        }

//        public static async Task LogAllDriveInfo()
//        {
//            foreach (var drive in DriveInfo.GetDrives())
//            {
//                await LogHelper.WriteToLog("\n" + drive.Name);
//                await LogHelper.WriteToLog($"Total space: {new DriveInfo(drive.Name).TotalSize / 1024 / 1024 / 1024} GB");
//                await LogFreeSpace(drive.Name);
//                await LogHelper.WriteToLog($"Volume label: {new DriveInfo(drive.Name).VolumeLabel}");
//                await LogFileSystem(drive.Name);
//                await LogHelper.WriteToLog("\n");
//            }
//        }
//    }

//    class FileHelper
//    {
//        public static async Task LogFileInfo(string fileName)
//        {
//            await LogHelper.WriteToLog($"File info for: {new FileInfo(fileName).Name}");
//            FileInfo fileInfo = new FileInfo(fileName);
//            await LogHelper.WriteToLog($"Full path: {fileInfo.FullName}");
//            await LogHelper.WriteToLog($"Size: {fileInfo.Length / 1024} KB");
//            await LogHelper.WriteToLog($"Extension: {fileInfo.Extension}");
//            await LogHelper.WriteToLog($"Name: {fileInfo.Name}");
//            await LogHelper.WriteToLog($"Creation date: {fileInfo.CreationTime}");
//            await LogHelper.WriteToLog($"Last modified: {fileInfo.LastWriteTime}");
//            await LogHelper.WriteToLog("\n");
//        }
//    }

//    class DirectoryHelper
//    {
//        public static async Task LogDirectoryInfo(string dir)
//        {
//            await LogHelper.WriteToLog($"Directory info for: {new DirectoryInfo(dir).Name}");
//            await LogHelper.WriteToLog($"File count: {new DirectoryInfo(dir).GetFiles().Length}");
//            await LogHelper.WriteToLog($"Creation date: {new DirectoryInfo(dir).CreationTime}");
//            await LogHelper.WriteToLog($"Subdirectory count: {new DirectoryInfo(dir).GetDirectories().Length}");
//            await LogHelper.WriteToLog($"Parent directory: {new DirectoryInfo(dir).Parent.FullName}");
//            await LogHelper.WriteToLog("\n");
//        }
//    }

//    class FileManager
//    {
//        static async Task CopyFileAsync(string sourcePath, string destinationPath)
//        {
//            using (var sourceStream = new FileStream(sourcePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
//            using (var destinationStream = new FileStream(destinationPath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
//            {
//                await sourceStream.CopyToAsync(destinationStream);
//            }
//        }

//        static async Task DeleteFileAsync(string filePath)
//        {
//            await Task.Run(() => File.Delete(filePath));
//        }

//        public static async Task MoveFileAsync(string sourcePath, string destinationPath)
//        {
//            await CopyFileAsync(sourcePath, destinationPath);
//            await DeleteFileAsync(sourcePath);
//        }

//        static string path = "D:\\уник\\3 сем\\ООП\\lab12\\lab12 (oop)\\lab12 (oop)\\bin\\Debug\\net8.0\\Inspect";
//        static string filePath = "D:\\уник\\3 сем\\ООП\\lab12\\lab12 (oop)\\lab12 (oop)\\bin\\Debug\\net8.0\\DirectoryInfo.txt";

//        public static async Task InspectDirectoryAsync(string inspectDir)
//        {
//            var dirs = Directory.GetDirectories(inspectDir);
//            var files = Directory.GetFiles(inspectDir);

//            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

//            if (!File.Exists(filePath)) using (File.Create(filePath)) ;
//            else await File.WriteAllTextAsync(filePath, string.Empty);

//            await File.AppendAllTextAsync(filePath, "Directory List:\n");
//            foreach (var dir in dirs)
//            {
//                await File.AppendAllTextAsync(filePath, dir + "\n");
//            }

//            await File.AppendAllTextAsync(filePath, "\nFiles List:\n");
//            foreach (var file in files)
//            {
//                await File.AppendAllTextAsync(filePath, file + "\n");
//            }

//            await CopyFileAsync(filePath, "Z:\\Inspect\\COPY.txt");
//            await DeleteFileAsync(filePath);
//        }

//        public static async Task MoveMP3FilesAsync(string dir)
//        {
//            if (!Directory.Exists("Z:\\MP3Files")) Directory.CreateDirectory("Z:\\MP3Files");
//            var filter = Directory.GetFiles(dir, "*.mp3");
//            foreach (var file in filter)
//            {
//                await MoveFileAsync(file, "Z:\\MP3Files\\" + file.Substring(file.LastIndexOf("\\") + 1));
//            }

//            Directory.CreateDirectory("Z:\\lab12_OOP\\Inspect\\MP3Files");
//            filter = Directory.GetFiles("Z:\\MP3Files");
//            foreach (var file in filter)
//            {
//                await MoveFileAsync(file, "Z:\\lab12_OOP\\Inspect\\MP3Files\\" + file.Substring(file.LastIndexOf("\\") + 1));
//            }

//            Directory.Delete("Z:\\MP3Files");
//        }

//        public static async Task CreateZipArchive()
//        {
//            File.Delete("Z:\\Archive.zip");
//            ZipFile.CreateFromDirectory("Z:\\lab12_OOP\\Inspect\\MP3Files", "Z:\\Archive.zip");
//            ZipFile.ExtractToDirectory("Z:\\Archive.zip", "Z:\\lab12_OOP", true);
//        }
//    }

//    class Program
//    {
//        static async Task Main(string[] args)
//        {
//            try
//            {
//                if (!Directory.Exists("Z:\\lab12_OOP")) Directory.CreateDirectory("Z:\\lab12_OOP");
//                await LogHelper.CreateLogFile();

//                await DiskHelper.LogAllDriveInfo();
//                await FileHelper.LogFileInfo(@"Z:\MOON - Dust (slow_reverb).mp3");
//                await DirectoryHelper.LogDirectoryInfo("C:\\Users\\stude\\");
//                await FileManager.InspectDirectoryAsync("C:\\users\\stude");
//                await FileManager.MoveMP3FilesAsync("Z:\\lab12_OOP\\Test");
//                await FileManager.CreateZipArchive();
//                await LogHelper.FilterLogByKeyword(keyword: "Free space");

//                await LogHelper.ReadLog();
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.Message);
//                Environment.Exit(69);
//            }
//        }
//    }
//}



using System;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace lab12
{
    public class KAALog
    {
        private readonly string logFilePath;

        public KAALog(string fileName = "logfile.txt")
        {
            logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            if (!File.Exists(logFilePath))
            {
                try
                {
                    File.Create(logFilePath).Dispose();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при создании лог-файла: {ex.Message}");
                }
            }
        }

        public void WriteLog(string action, string details)
        {
            try
            {
                string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}  {action}  {details}";
                File.AppendAllText(logFilePath, logEntry + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка записи в лог: {ex.Message}");
            }
        }

        public string ReadLog()
        {
            try
            {
                return File.ReadAllText(logFilePath);
            }
            catch (Exception ex)
            {
                WriteLog("Ошибка чтения лога", ex.Message);
                return $"Ошибка: {ex.Message}";
            }
        }

        public string SearchLog(string searchTerm)
        {
            try
            {
                return string.Join(Environment.NewLine,
                    File.ReadAllLines(logFilePath).Where(line => line.Contains(searchTerm)));
            }
            catch (Exception ex)
            {
                WriteLog("Ошибка поиска в логе", ex.Message);
                return $"Ошибка: {ex.Message}";
            }
        }

        public string FilterByDate(DateTime startDate, DateTime? endDate = null)
        {
            try
            {
                return string.Join(Environment.NewLine,
                    File.ReadAllLines(logFilePath).Where(line =>
                    {
                        string datePart = line.Split('|')[0].Trim();
                        if (DateTime.TryParse(datePart, out DateTime logDate))
                        {
                            return logDate >= startDate && (endDate == null || logDate <= endDate);
                        }
                        return false;
                    }));
            }
            catch (Exception ex)
            {
                WriteLog("Ошибка фильтрации лога", ex.Message);
                return $"Ошибка: {ex.Message}";
            }
        }

        public void CurrentHourEntries()
        {
            try
            {
                var currentHour = DateTime.Now.Hour;
                File.WriteAllLines(logFilePath, File.ReadAllLines(logFilePath).Where(line =>
                {
                    string datePart = line.Split('|')[0].Trim();
                    return DateTime.TryParse(datePart, out DateTime logDate) &&
                           logDate.Hour == currentHour && logDate.Date == DateTime.Now.Date;
                }));
            }
            catch (Exception ex)
            {
                WriteLog("Ошибка обработки логов", ex.Message);
            }
        }
    }

    public class KAADiskInfo
    {
        private readonly KAALog logger;

        public KAADiskInfo(KAALog log)
        {
            logger = log;
        }

        public void DiskInfo()
        {
            try
            {
                foreach (var drive in DriveInfo.GetDrives())
                {
                    if (drive.IsReady)
                    {
                        Console.WriteLine($"Диск: {drive.Name}");
                        Console.WriteLine($"Файловая система: {drive.DriveFormat}");
                        Console.WriteLine($"Объем: {drive.TotalSize / 1e+9} ГБ");
                        Console.WriteLine($"Доступно: {drive.AvailableFreeSpace / 1e+9} ГБ");
                        Console.WriteLine($"Метка тома: {drive.VolumeLabel}");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.WriteLog("Ошибка получения информации о дисках", ex.Message);
            }
        }
    }

    public class KAAFileInfo
    {
        private readonly KAALog logger;

        public KAAFileInfo(KAALog log)
        {
            logger = log;
        }

        public void FileInfo(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    var fileInfo = new FileInfo(filePath);
                    Console.WriteLine($"Полный путь: {fileInfo.FullName}");
                    Console.WriteLine($"Размер: {fileInfo.Length} байт");
                    Console.WriteLine($"Расширение: {fileInfo.Extension}");
                    Console.WriteLine($"Имя файла: {fileInfo.Name}");
                    Console.WriteLine($"Дата создания: {fileInfo.CreationTime}");
                    Console.WriteLine($"Дата изменения: {fileInfo.LastWriteTime}");
                }
                else
                {
                    Console.WriteLine("Файл не найден.");
                }
            }
            catch (Exception ex)
            {
                logger.WriteLog("Ошибка получения информации о файле", ex.Message);
            }
        }
    }

    public class KAADirInfo
    {
        private readonly KAALog logger;

        public KAADirInfo(KAALog log)
        {
            logger = log;
        }

        public void DirInfo(string dirPath)
        {
            try
            {
                if (Directory.Exists(dirPath))
                {
                    var dirInfo = new DirectoryInfo(dirPath);
                    Console.WriteLine($"Количество файлов: {dirInfo.GetFiles().Length}");
                    Console.WriteLine($"Дата создания: {dirInfo.CreationTime}");
                    Console.WriteLine($"Количество поддиректорий: {dirInfo.GetDirectories().Length}");
                    Console.WriteLine($"Родительская директория: {dirInfo.Parent?.FullName}");
                }
                else
                {
                    Console.WriteLine("Директория не найдена.");
                }
            }
            catch (Exception ex)
            {
                logger.WriteLog("Ошибка получения информации о директории", ex.Message);
            }
        }
    }

    public class KAAFileManager
    {
        private readonly KAALog logger;

        public KAAFileManager(KAALog log)
        {
            logger = log;
        }

        public void InspectDirectory(string dirPath)
        {
            try
            {
                if (Directory.Exists(dirPath))
                {
                    string inspectDir = Path.Combine(dirPath, "KAAInspect");
                    Directory.CreateDirectory(inspectDir);

                    string dirInfoPath = Path.Combine(inspectDir, "kaadirinfo.txt");
                    using (var writer = new StreamWriter(dirInfoPath))
                    {
                        foreach (var entry in Directory.EnumerateFileSystemEntries(dirPath))
                        {
                            writer.WriteLine(entry);
                        }
                    }

                    string copyPath = Path.Combine(inspectDir, "kaadirinfo_copy.txt");
                    File.Copy(dirInfoPath, copyPath);
                    File.Delete(dirInfoPath);
                }
            }
            catch (Exception ex)
            {
                logger.WriteLog("Ошибка директории", ex.Message);
            }
        }

        public void CopyFilesWithExtension(string sourceDir, string extension)
        {
            try
            {
                if (Directory.Exists(sourceDir))
                {
                    string filesDir = Path.Combine(sourceDir, "KAAFiles");
                    Directory.CreateDirectory(filesDir);

                    foreach (var file in Directory.GetFiles(sourceDir, $"*.{extension}"))
                    {
                        File.Copy(file, Path.Combine(filesDir, Path.GetFileName(file)));
                    }

                    Directory.Move(filesDir, Path.Combine(sourceDir, "KAAInspect", "KAAFiles"));
                }
            }
            catch (Exception ex)
            {
                logger.WriteLog("Ошибка копирования файлов", ex.Message);
            }
        }

        public void ArchiveFiles(string dirPath, string archivePath)
        {
            try
            {
                Console.WriteLine($"Проверка директории: {dirPath}");
                if (!Directory.Exists(dirPath))
                {
                    Console.WriteLine("Указанная директория не существует.");
                    return;
                }

                Console.WriteLine($"Создаём архив по пути: {archivePath}");
                ZipFile.CreateFromDirectory(dirPath, archivePath);
                Console.WriteLine($"Архив успешно создан: {archivePath}");
            }
            catch (Exception ex)
            {
                logger.WriteLog("Ошибка архивирования", ex.Message);
                Console.WriteLine($"Ошибка архивирования: {ex.Message}");
            }
        }

        public void ExtractArchive(string zipFilePath, string extractPath)
        {
            try
            {
                if (!Directory.Exists(extractPath))
                {
                    Directory.CreateDirectory(extractPath);
                }

                ZipFile.ExtractToDirectory(zipFilePath, extractPath);
                Console.WriteLine($"Файлы успешно разархивированы в: {extractPath}");
            }
            catch (Exception ex)
            {
                logger.WriteLog("Ошибка разархивирования", ex.Message);
                Console.WriteLine($"Ошибка разархивирования: {ex.Message}");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var logger = new KAALog();

            try
            {
                logger.WriteLog("Программа стартовала", "Начало работы");

                var diskInfo = new KAADiskInfo(logger);
                diskInfo.DiskInfo();

                string currentDirectory = Directory.GetCurrentDirectory();
                Console.WriteLine($"Текущая директория: {currentDirectory}");

                var fileInfo = new KAAFileInfo(logger);
                string filePath = Path.Combine(currentDirectory, "logfile.txt");
                fileInfo.FileInfo(filePath);

                var dirInfo = new KAADirInfo(logger);
                Console.Write("Введите путь к директории: ");
                string dirPath = Console.ReadLine();
                dirInfo.DirInfo(dirPath);

                var fileManager = new KAAFileManager(logger);

                Console.WriteLine("\nАрхивирование");
                Console.Write("Введите путь для сохранения архива: ");
                string archivePath = Console.ReadLine();
                fileManager.ArchiveFiles(dirPath, archivePath);

                Console.WriteLine("\nРазархивирование");
                Console.Write("Введите путь к архиву: ");
                string zipFilePath = Console.ReadLine();
                Console.Write("Введите путь для разархивирования: ");
                string extractPath = Console.ReadLine();
                fileManager.ExtractArchive(zipFilePath, extractPath);

                Console.WriteLine("\nРабота завершена.");
                logger.WriteLog("Программа завершена", "Работа успешно выполнена");
            }
            catch (Exception ex)
            {
                logger.WriteLog("Ошибка в программе", ex.Message);
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }
}
