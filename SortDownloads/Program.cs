namespace SortDownloads
{

    /*
     * TODO
     * 
     * - move files with same name
     * 
     */

    class DownloadPath
    {
        // ATTRIBUTES
        private string Username { get; set; }
        private string Path { get; set; }


        // CONSTRUCTOR
        public DownloadPath()
        {
            Username = Environment.UserName;
            Path = $"c:\\users\\{Username}\\downloads";
        }


        // METHODS
        private string[] GetFiles(string filter="")
        {
            if (filter == "")
            {
                string[] downloadFiles = System.IO.Directory.GetFiles(Path);
                return downloadFiles;
            }
            
            else
            {
                string[] downloadFiles = System.IO.Directory.GetFiles(Path, filter);
                return downloadFiles;
            }
        }
        private string[] GetFileNames(string filter)
        {
            string[] fileNames = new string[GetFiles(filter).Length];
            int i = 0;

            foreach (string filepath in GetFiles(filter))
            {
                fileNames[i] = System.IO.Path.GetFileName(filepath);
                i++;
            }

            return fileNames;
        }
        private string[] GetExtensions()
        {
            string[] extensionsArray = new string[GetFiles().Length];
            int i = 0;

            foreach (string file in GetFiles())
            {
                extensionsArray[i] = System.IO.Path.GetExtension(file);
                i++;
            }

            var distinctExtensionsArray = extensionsArray.ToHashSet().ToArray(); // removes duplicats

            return distinctExtensionsArray;
        }
        private void CreateExtensionDirectorys()
        {
            foreach (string extension in GetExtensions())
            {
                string dirPath = $"{Path}\\{extension}";
                Directory.CreateDirectory(dirPath);
            }
        }
        private void MoveFilesToExtensionDirectorys()
        {
            foreach (string extension in GetExtensions())
            {      
                
                try
                {
                    foreach (string file in GetFileNames("*" + extension))
                    {
                        //Console.WriteLine($"{Path}\\{file}");
                        //Console.WriteLine($"{Path}\\{extension}\\{file}");
                        //Console.WriteLine("");                
                        File.Move($"{Path}\\{file}", $"{Path}\\{extension}\\{file}");
                    }
                }
                
                catch (IOException) 
                {
                    foreach (string file in GetFileNames("*" + extension))
                    {
                        //Console.WriteLine($"{Path}\\{file}");
                        //Console.WriteLine($"{Path}\\{extension}\\{file}");
                        //Console.WriteLine("");                
                        File.Move($"{Path}\\{file}", $"{Path}\\{extension}\\({DateTime.Now.ToString().Replace(' ', '-').Replace('.', '-').Replace(':', '-')})_{file}");
                    }
                }
            }  
        }
        
        public void Sort()
        {
            CreateExtensionDirectorys();
            MoveFilesToExtensionDirectorys();
        }



        // UNUSED METHODS
        private void PrintFiles(string filter="")
        {
            
            if (filter == "")
            {
                foreach (string file in GetFiles())
                {
                    Console.WriteLine(file);
                }
            }
            
            else
            {
                foreach (string file in GetFiles(filter))
                {
                    Console.WriteLine(file);
                }
            }
        }
        private void PrintExtensions()
        {
            foreach (string file in GetExtensions())
            {
                Console.WriteLine(file);
            }
        }
        private string[] GetExtensionDirs()
        {
            string[] extensionDirsArray = new string[GetExtensions().Length];
            int i = 0;

            foreach (string extension in GetExtensions())
            {
                string extensionDirPath = $"{Path}\\{extension}";
                extensionDirsArray[i] = extensionDirPath;
                i++;
            }

            return extensionDirsArray;

        }


    }


    class Program
    {

        static void Main()
        {

            DownloadPath test = new();

            test.Sort();

            
        }

    }

}


