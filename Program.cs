using System;
using System.IO;

namespace FilesWork3
{

    class Program
    {
        public static long DirSize(DirectoryInfo d)
        {
            long size = 0;
            int count = 0;

            FileInfo[] fis = d.GetFiles();
            try
            {
                foreach (FileInfo fi in fis)
                {
                    size += fi.Length;
                    count += count;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка: " + ex.Message);
            }
            DirectoryInfo[] dis = d.GetDirectories();
            try
            {
                foreach (DirectoryInfo di in dis)
                {
                    size += DirSize(di);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка: " + ex.Message);
            }
            return size;
            
        }
        public static int FileNum(DirectoryInfo d)
        {
            int count = 0;
            FileInfo[] fis = d.GetFiles();
            try
            {
                foreach (FileInfo fi in fis)
                {
                    count = count+1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка: " + ex.Message);
            }
            DirectoryInfo[] dis = d.GetDirectories();
            try
            {
                foreach (DirectoryInfo di in dis)
                {
                    count = count + FileNum(di);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка: " + ex.Message);
            }
            return count;
        }
            static void Main(string[] args)
        {
            string dirName = @"D:\test";
            DirectoryInfo directory = new DirectoryInfo(dirName);
            DirectoryInfo[] directories = directory.GetDirectories();
            if (Directory.Exists(dirName))
            {
                
                FileInfo[] files = directory.GetFiles();
                long size = DirSize(directory);
                int count = FileNum(directory);
                Console.WriteLine("Исходный размер директории составляет {0} байт.", size);

                foreach (var dir in directories)
                {
                    try
                    {
                        DateTime begin = dir.LastAccessTime;
                        DateTime end = DateTime.Now;
                        TimeSpan rez = end - begin;
                        if (rez.TotalMinutes >= 1)
                        {
                            dir.Delete(true);
                        }
                    }
                    catch (UnauthorizedAccessException ex)
                    {
                        Console.WriteLine("Отсутствует доступ. Ошибка: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Произошла ошибка: " + ex.Message);
                    }
                }


                foreach (var file in files)
                {
                    try
                    {
                        DateTime begin = file.LastAccessTime;
                        DateTime end = DateTime.Now;
                        TimeSpan rez = end - begin;
                        if (rez.TotalMinutes >= 1)
                        {
                            file.Delete();
                        }
                    }
                    catch (UnauthorizedAccessException ex)
                    {
                        Console.WriteLine("Отсутствует доступ. Ошибка: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Произошла ошибка: " + ex.Message);
                    }
                }
                long size2 = DirSize(directory);
                int count2 = FileNum(directory);
                int delFiles = count - count2;
                Console.WriteLine("Общее количество удаленных файлов: {0}.", delFiles);
                Console.WriteLine("Текущий размер директории составляет {0} байт.", size2);

            }
            else { Console.WriteLine("По указанному адресу папка отсутствует"); }
            
        }
    }
}