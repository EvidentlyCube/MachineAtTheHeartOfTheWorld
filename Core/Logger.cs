using System;
using System.IO;

namespace IrregularMachine.Core {
    public static class Logger {
        private static FileStream _fileStream;
        private static StreamWriter _fileStreamWriter;

        static Logger() {
            if (!File.Exists("logs")) {
                Directory.CreateDirectory("logs");
            }

//            var fileName = "logs/" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss-ffff") + ".txt";
//            _fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
//            _fileStreamWriter = new StreamWriter(_fileStream);
        }
        
        public static void Init(string text) {
            Log($"<INIT> {text}");
        }
        
        public static void Debug(string text) {
            Log($"<INPUT> {text}");
        }
        
        public static void Input(string text) {
            Log($"<INPUT> {text}");
        }
        
        public static void Error(string text) {
            Log($"<ERROR> {text}");            
        }

        private static void Log(string text) {
            text = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.ffff}] {text}";
            Console.WriteLine(text);
//            _fileStreamWriter.Write(text);
//            _fileStreamWriter.Write('\n');
//            _fileStreamWriter.Flush();
        }
    }
}