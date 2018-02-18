using System;
using System.IO;

namespace IrregularMachine.Core {
    public static class Save {
        private const string SaveName = "Save.txt";
        public static float MusicVolume = 1;
        public static float SoundVolume = 1;
        public static float VoiceOverVolume = 1;
        public static int LastLevel;

        public static void WriteSave() {
            var path = GetProgramDataPath() + Path.DirectorySeparatorChar + SaveName;
            Directory.CreateDirectory(GetProgramDataPath());
            using (var file = new FileStream(path, FileMode.Create, FileAccess.Write))
            using (var writer = new BinaryWriter(file)) {
                writer.Write((Int32)LastLevel);
                writer.Write(MusicVolume);
                writer.Write(SoundVolume);
                writer.Write(VoiceOverVolume);
            }
        }

        public static void LoadSave() {
            var path = GetProgramDataPath() + Path.DirectorySeparatorChar + SaveName;
            if (!File.Exists(path)) {
                return;
            }

            try {
                using (var file = new FileStream(path, FileMode.Open, FileAccess.Read))
                using (var reader = new BinaryReader(file)) {
                    LastLevel = reader.ReadInt32();
                    MusicVolume = reader.ReadSingle();
                    SoundVolume = reader.ReadSingle();
                    VoiceOverVolume = reader.ReadSingle();
                }

                LastLevel--;
            }
            catch (Exception) {
                // Ignore
            }
        }

        private static string GetProgramDataPath() {
            return Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)
                   + Path.DirectorySeparatorChar + "EvidentlyCube"
                   + Path.DirectorySeparatorChar + "MachineAtTheHeartOfTheWorld";
        }
    }
}