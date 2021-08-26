using System;
using System.IO;
using System.Linq;


namespace DirectoryAlgorithm
{
    class Program
    {
        private static double KB_TO_MB = 1024.0;

        // change the value of these constants according to your needs
        private static string QUEUE_DIR = "C:\\directory-chooser-algorithm";
        private static int FIRST_DIRECTORIES = 8;
        private static double ALGORITHM_TIME_COEFFICIENT = 1.0; 
        private static double ALGORITHM_SIZE_COEFFICIENT = 1.0; 

        public static double calculateDirectoryPreference(DirectoryInfo directory, int dirPlace, DateTime timeNow)
        {
            string dirPath = directory.FullName;
           
            DateTime dirCreationTime = Directory.GetCreationTime(dirPath);
            double timeElapsed = (timeNow - dirCreationTime).TotalMinutes;

            double dirSize = directory.EnumerateFiles().Sum(file => (Double)file.Length / 1024.0);
            double size_calculation = dirSize >= 1 ? Math.Sqrt(dirSize) : Math.Pow(dirSize, 2);

            double preference = (double)(( ALGORITHM_TIME_COEFFICIENT * timeElapsed - ALGORITHM_SIZE_COEFFICIENT * size_calculation ));
            preference = preference >= 0 ? ( preference / Math.Sqrt(dirPlace) ) : ( preference * Math.Sqrt(dirPlace) );

            // Console.WriteLine is for seeing results of preference algorithm 
            Console.WriteLine("dirPath: " + dirPath + "\ndir size: " + dirSize + "\ndir preference: " + preference + "\n"); 

            return preference;
        }

        /**returns empty string if no directory is available*/
        public static string getNextDirectory(string[] DirectoriesToIgnore = null)
        {
            string preferencedDirectory = "";
            DirectoryInfo queueDirInfo = new DirectoryInfo(QUEUE_DIR);
            DirectoryInfo[] directories = queueDirInfo.GetDirectories().OrderBy(dir => dir.CreationTime).ToArray();
            double maxPreference = -10000, currPreference;
            DateTime nowTimestamp = DateTime.Now;

            for (int i = 0; i < Math.Min(FIRST_DIRECTORIES, directories.Length); i++) {
                if (DirectoriesToIgnore != null && DirectoriesToIgnore.Contains(directories[i].Name.ToString())) {
                    continue;
                }
                currPreference = calculateDirectoryPreference(directories[i], i+1, nowTimestamp);
                if(maxPreference < currPreference) {
                    preferencedDirectory = directories[i].FullName;
                    maxPreference = currPreference;
                }
            }
            return preferencedDirectory;
        }


        static void Main(string[] args)
        {
            string[] DirectoriesToIgnore = { "directory1", "directory3" };
            string preferenceDirName = getNextDirectory(DirectoriesToIgnore);

            Console.WriteLine("\n\n" + preferenceDirName + "\n"); 
        }
    }
}
