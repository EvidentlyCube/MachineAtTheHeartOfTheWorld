using System;
using System.Collections.Generic;
using System.IO;
using IrregularMachine.BitmapTexts;
using IrregularMachine.IrregularEngine.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended.TextureAtlases;

namespace IrregularMachine.Core {
    public static class Sfx {
        private static Random _random = new Random();
        public static List<SoundEffect> sfxStep { get; private set; }
        public static List<SoundEffect> sfxGlass { get; private set; }
        public static List<SoundEffect> sfxGlassReverse { get; private set; }
        public static List<SoundEffect> sfxLevelCompleted { get; private set; }
        public static SoundEffect Music { get; private set; }
        public static SoundEffectInstance MusicInstance { get; private set; }
        
        public static SoundEffect MusicOutro { get; private set; }
        public static SoundEffect Punch;

        public static Dictionary<string, SoundEffect> VoiceOvers;

        public static void PlayStepSound() {
            sfxStep[_random.Next(0, sfxStep.Count)].Play(Save.SoundVolume, 0.8f * (float)_random.NextDouble() * 0.4f, 0);
        }
        public static void PlayLevelCompleted() {
            sfxLevelCompleted[_random.Next(0, sfxLevelCompleted.Count)].Play(Save.SoundVolume, 1, 0);
        }
        
        public static void PlayGlassSound(EngineActionType type) {
            float typeValue = (int) type;
            typeValue /= 8f; 
            sfxGlass[_random.Next(0, sfxGlass.Count)].Play(Save.SoundVolume, -1 + typeValue * 1.0f, 0);
        }
        
        public static void PlayGlassReverseSound(EngineActionType type) {
            float typeInt = (int) type;
            typeInt /= 8f; 
            sfxGlassReverse[_random.Next(0, sfxGlassReverse.Count)].Play(Save.SoundVolume, -1 + typeInt * 1.0f, 0);
        }
        
        public static void Load(ContentManager content) {
            Logger.Init("Loading SFX assets");
            
            Punch = LoadSfx(content, "sfx_punch");
            
            Music = LoadSfx(content, "Music-TheEldestTribe");
            MusicOutro = LoadSfx(content, "Music-Outro");
            MusicInstance = Music.CreateInstance();
            MusicInstance.IsLooped = true;

            sfxStep = new List<SoundEffect>();
            sfxStep.Add(LoadSfx(content, "sfx_Step1"));
            sfxStep.Add(LoadSfx(content, "sfx_Step2"));
            sfxStep.Add(LoadSfx(content, "sfx_Step3"));
            sfxStep.Add(LoadSfx(content, "sfx_Step4"));
            sfxStep.Add(LoadSfx(content, "sfx_Step5"));
            
            sfxGlass = new List<SoundEffect>();
            sfxGlass.Add(LoadSfx(content, "sfx_Glass"));
            
            sfxGlassReverse = new List<SoundEffect>();
            sfxGlassReverse.Add(LoadSfx(content, "sfx_GlassRev"));
            
            sfxLevelCompleted = new List<SoundEffect>();
            sfxLevelCompleted.Add(LoadSfx(content, "sfx_levelCompleted"));
            
            VoiceOvers = new Dictionary<string, SoundEffect>();
            LoadVoiceOver(content, "voice_ingame10a");
            LoadVoiceOver(content, "voice_ingame10b");
            LoadVoiceOver(content, "voice_ingame10c");
            LoadVoiceOver(content, "voice_ingame10d");
            LoadVoiceOver(content, "voice_ingame1a");
            LoadVoiceOver(content, "voice_ingame1b");
            LoadVoiceOver(content, "voice_ingame2a");
            LoadVoiceOver(content, "voice_ingame2b");
            LoadVoiceOver(content, "voice_ingame3a");
            LoadVoiceOver(content, "voice_ingame3b");
            LoadVoiceOver(content, "voice_ingame3c");
            LoadVoiceOver(content, "voice_ingame4a");
            LoadVoiceOver(content, "voice_ingame4b");
            LoadVoiceOver(content, "voice_ingame5a");
            LoadVoiceOver(content, "voice_ingame5b");
            LoadVoiceOver(content, "voice_ingame5c");
            LoadVoiceOver(content, "voice_ingame6a");
            LoadVoiceOver(content, "voice_ingame6b");
            LoadVoiceOver(content, "voice_ingame7a");
            LoadVoiceOver(content, "voice_ingame7b");
            LoadVoiceOver(content, "voice_ingame7c");
            LoadVoiceOver(content, "voice_ingame7d");
            LoadVoiceOver(content, "voice_ingame8a");
            LoadVoiceOver(content, "voice_ingame9a");
            LoadVoiceOver(content, "voice_ingame9b");
            LoadVoiceOver(content, "voice_ingame9c");
            LoadVoiceOver(content, "voice_intro1a");
            LoadVoiceOver(content, "voice_intro1b");
            LoadVoiceOver(content, "voice_intro1c");
            LoadVoiceOver(content, "voice_intro2a");
            LoadVoiceOver(content, "voice_intro2b");
            LoadVoiceOver(content, "voice_intro2c");
            LoadVoiceOver(content, "voice_intro2d");
            LoadVoiceOver(content, "voice_intro3a");
            LoadVoiceOver(content, "voice_intro3b");
            LoadVoiceOver(content, "voice_intro3c");
            LoadVoiceOver(content, "voice_intro3d");
            LoadVoiceOver(content, "voice_intro3e");

        }

        private static SoundEffect LoadSfx(ContentManager content, string assetName) {
            Logger.Init($"Loading sound effect: {assetName}");
            
            return content.Load<SoundEffect>(assetName);
        }

        private static void LoadVoiceOver(ContentManager content, string assetName) {
            VoiceOvers[assetName] = LoadSfx(content, assetName);

        }
    }
}