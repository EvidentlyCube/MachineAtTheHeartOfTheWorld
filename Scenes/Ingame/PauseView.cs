using System;
using System.Collections.Generic;
using IrregularMachine.BitmapTexts;
using IrregularMachine.Core;
using IrregularMachine.IrregularEngine.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace IrregularMachine.Scenes.Ingame {
    public class PauseView {
        private readonly BitmapText _header;
        private readonly BitmapText _resume;
        private readonly BitmapText _musicVolume;
        private readonly BitmapText _sfxVolume;
        private readonly BitmapText _voiceOverVolume;
        private readonly BitmapText _quit;

        private readonly List<BitmapText> _options;
        private BitmapText _selectedOption;

        public bool IsActive = false;

        public PauseView() {
            _header = new BitmapText(Gfx.BitmapFont, "Machine at the Heart of the World");
            _resume = new BitmapText(Gfx.BitmapFont, "Resume");
            _musicVolume = new BitmapText(Gfx.BitmapFont, "Music volume: " + Math.Round(Save.MusicVolume * 10));
            _sfxVolume = new BitmapText(Gfx.BitmapFont, "Sound effects volume: " + Math.Round(Save.SoundVolume * 10));
            _voiceOverVolume = new BitmapText(Gfx.BitmapFont,
                "Voice over volume: " + Math.Round(Save.VoiceOverVolume * 10));
            _quit = new BitmapText(Gfx.BitmapFont, "Quit");

            _resume.FontScale = 0.5f;
            _musicVolume.FontScale = 0.5f;
            _sfxVolume.FontScale = 0.5f;
            _voiceOverVolume.FontScale = 0.5f;
            _quit.FontScale = 0.5f;

            _header.Width = S.ViewportWidth;
            _resume.Width = S.ViewportWidth;
            _musicVolume.Width = S.ViewportWidth;
            _sfxVolume.Width = S.ViewportWidth;
            _voiceOverVolume.Width = S.ViewportWidth;
            _quit.Width = S.ViewportWidth;

            _header.HorizontalAlign = BitmapTextHorizontalAlign.Center;
            _resume.HorizontalAlign = BitmapTextHorizontalAlign.Center;
            _musicVolume.HorizontalAlign = BitmapTextHorizontalAlign.Center;
            _sfxVolume.HorizontalAlign = BitmapTextHorizontalAlign.Center;
            _voiceOverVolume.HorizontalAlign = BitmapTextHorizontalAlign.Center;
            _quit.HorizontalAlign = BitmapTextHorizontalAlign.Center;

            _header.Y = 200;
            _resume.Y = _header.Y + _header.Height + 100;
            _musicVolume.Y = _resume.Y + _resume.Height;
            _sfxVolume.Y = _musicVolume.Y + _musicVolume.Height;
            _voiceOverVolume.Y = _sfxVolume.Y + _sfxVolume.Height;
            _quit.Y = _voiceOverVolume.Y + _voiceOverVolume.Height;

            _options = new List<BitmapText> {
                _resume,
                _musicVolume,
                _sfxVolume,
                _voiceOverVolume,
                _quit
            };
            _selectedOption = _resume;

            _options.ForEach(x => x.Alpha = 0.5f);
            _selectedOption.Alpha = 1;
        }

        public void Update() {
            var index = _options.IndexOf(_selectedOption);
            if (KeyboardManager.Instance.IsKeyJustPressed(Keys.Up)) {
                index--;
                if (index < 0) {
                    index = _options.Count - 1;
                }
            }

            if (KeyboardManager.Instance.IsKeyJustPressed(Keys.Down)) {
                index++;
                if (index >= _options.Count) {
                    index = 0;
                }
            }

            if (KeyboardManager.Instance.IsKeyJustPressed(Keys.Left)) {
                if (_selectedOption == _musicVolume) {
                    Save.MusicVolume = Math.Max(0, Save.MusicVolume - 0.1f);
                    _musicVolume.Text = "Music volume: " + Math.Round(Save.MusicVolume * 10);
                    Sfx.MusicInstance.Volume = Save.MusicVolume;
                    Save.WriteSave();
                    Sfx.PlayGlassReverseSound(EngineActionType.Special);
                }

                if (_selectedOption == _sfxVolume) {
                    Save.SoundVolume = Math.Max(0, Save.SoundVolume - 0.1f);
                    _sfxVolume.Text = "Sound effects volume: " + Math.Round(Save.SoundVolume * 10);
                    Save.WriteSave();
                    Sfx.PlayGlassReverseSound(EngineActionType.Special);
                }

                if (_selectedOption == _voiceOverVolume) {
                    Save.VoiceOverVolume = Math.Max(0, Save.VoiceOverVolume - 0.1f);
                    _voiceOverVolume.Text = "Voice over volume: " + Math.Round(Save.VoiceOverVolume * 10);
                    Save.WriteSave();
                    Sfx.PlayGlassReverseSound(EngineActionType.Special);
                }
            }
            
            if (KeyboardManager.Instance.IsKeyJustPressed(Keys.Right)) {
                if (_selectedOption == _musicVolume) {
                    Save.MusicVolume = Math.Min(1, Save.MusicVolume + 0.1f);
                    _musicVolume.Text = "Music volume: " + Math.Round(Save.MusicVolume * 10);
                    Sfx.MusicInstance.Volume = Save.MusicVolume;
                    Save.WriteSave();
                    Sfx.PlayGlassSound(EngineActionType.Special);
                }

                if (_selectedOption == _sfxVolume) {
                    Save.SoundVolume = Math.Min(1, Save.SoundVolume + 0.1f);
                    _sfxVolume.Text = "Sound effects volume: " + Math.Round(Save.SoundVolume * 10);
                    Save.WriteSave();
                    Sfx.PlayGlassSound(EngineActionType.Special);
                }

                if (_selectedOption == _voiceOverVolume) {
                    Save.VoiceOverVolume = Math.Min(1, Save.VoiceOverVolume + 0.1f);
                    _voiceOverVolume.Text = "Voice over volume: " + Math.Round(Save.VoiceOverVolume * 10);
                    Save.WriteSave();
                    Sfx.PlayGlassSound(EngineActionType.Special);
                }
            }

            if (KeyboardManager.Instance.IsKeyJustPressed(Keys.Enter)) {
                if (_selectedOption == _resume) {
                    IsActive = false;
                }

                if (_selectedOption == _quit) {
                    GameCore.Instance.Exit();
                }
            }

            _selectedOption = _options[index];
            _options.ForEach(x => x.Alpha = 0.5f);
            _selectedOption.Alpha = 1;
        }

        public void Draw(SpriteBatch batch) {
            if (!IsActive) {
                return;
            }

            batch.Draw(Gfx.Black, new Rectangle(0, 0, S.ViewportWidth, S.ViewportHeight), new Color(1f, 1f, 1f, 0.8f));

            _header.Draw(batch);
            _resume.Draw(batch);
            _musicVolume.Draw(batch);
            _sfxVolume.Draw(batch);
            _voiceOverVolume.Draw(batch);
            _quit.Draw(batch);
        }
    }
}