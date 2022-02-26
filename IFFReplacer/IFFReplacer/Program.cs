using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace IFFReplacer
{
    class Program
    {
        static void Main(string[] args)
        {
            string pathVO = @"F:\Téléchargements\setups\EnCours\data-files\TRE-MERGE\SPECIAL\DVD\special6";
            string pathVF = @"F:\Téléchargements\setups\EnCours\data-files\TRE-MERGE\SPECIAL\CD\special6";
            string merge = @"F:\Téléchargements\setups\EnCours\data-files\TRE-MERGE\SPECIAL\merge\special6";


            Console.WriteLine("traitement...");

            foreach (string path in Directory.GetFiles(pathVO, "*.iff", SearchOption.TopDirectoryOnly))
            {
                List<string> currentFileWavFiles = GetWAVFileNames(path);
                if(currentFileWavFiles.Any())
                {
                    string matchingFile = GetMatchingFile(currentFileWavFiles, pathVF, path);
                    string destFileName = String.Format("{0}\\{1}", merge, Path.GetFileName(path));

                    string message = String.Format("copie de {0} vers {1}", matchingFile, destFileName);

                    Console.WriteLine(message);
                    Debug.WriteLine(message);

                    File.Copy(matchingFile, destFileName, true);
                }
            }
            
            Console.WriteLine("traitement global terminé");
            Console.WriteLine("Appuyez sur une touche pour continuer...");
            Console.ReadKey();
        }

        static string GetMatchingFile(List<string> voFileWavFiles, string vfDirPath, string voFilePath)
        {
            string match = "";

            List<string> dirFiles = Directory.GetFiles(vfDirPath, "*.iff", SearchOption.TopDirectoryOnly).ToList();

            foreach(string path in dirFiles)
            {
                List<string> wavFiles = GetWAVFileNames(path);

                if(IsTheSameList(voFileWavFiles, wavFiles))
                {
                    match = path;
                }
            }

            //jamais arrivé
            if(String.IsNullOrEmpty(match))
            {
                match = voFilePath;
            }

            return match;
        }

        static bool IsTheSameList(List<string> voFileWavFiles, List<string> vfFileWavFiles)
        {
            bool isTheSame = false;

            foreach (string wavFile in vfFileWavFiles)
            {
                foreach (string innerWavFile in voFileWavFiles)
                {
                    if (wavFile == innerWavFile)
                    {
                        isTheSame = true;
                    }
                    else
                    {
                        isTheSame = false;
                    }
                }
            }

            return isTheSame;
        }

        static List<string> GetWAVFileNames(string voFilePath)
        {
            List<string> names = new List<string>();

            string content = File.ReadAllText(voFilePath, Encoding.ASCII);

            string[] linedContent = content.Split();

            names = linedContent.Where(x => x.Contains(".wav")).ToList();
            
            for(int i = 0; i < names.Count; i++)
            {
                string strToClean = names[i];

                string upUntilDotWav = strToClean.Substring(0, strToClean.IndexOf(".wav"));

                string filenameWithoutExtension = "";

                foreach(char c in upUntilDotWav.Reverse())
                {
                    if(Char.IsControl(c) == false)
                    {
                        filenameWithoutExtension = filenameWithoutExtension + c;
                    }
                    else
                    {
                        break;
                    }
                }

                string realName = "";

                foreach (char c in filenameWithoutExtension.Reverse())
                {
                    realName = realName + c;
                }

                names[i] = String.Format("{0}.wav",realName);
            }

            return names;
        }
    }
}
