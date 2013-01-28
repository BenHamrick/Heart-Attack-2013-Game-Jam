using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;
using System.IO;

namespace HeartbeatGlobalGameJam2013
{
    class Sound
    {
        static List<SoundEffect> soundEffect = new List<SoundEffect>();
        static List<string> soundNames = new List<string>();

        static List<Song> songs = new List<Song>();
        static List<string> songNames = new List<string>();

        public static void LoadContent(ContentManager content)
        {
            soundNames.InsertRange(soundNames.Count, Directory.GetFiles("Content/Sound"));
            songNames.InsertRange(songNames.Count, Directory.GetFiles("Content/Music"));

            for (int i = soundNames.Count; i-- > 0; )
            {
                if (soundNames[i].Contains(".wma"))
                {
                    soundNames.RemoveAt(i);
                }
            }
            for (int i = 0; i < soundNames.Count; i++)
            {
                soundNames[i] = soundNames[i].Replace("Content/Sound\\", "");
                soundNames[i] = soundNames[i].Replace(".xnb", "");
                soundEffect.Add(content.Load<SoundEffect>("Sound/" + soundNames[i]));
            }
            for (int i = songNames.Count; i-- > 0; )
            {
                if (songNames[i].Contains(".wma"))
                {
                    songNames.RemoveAt(i);
                }
            }
            for (int i = 0; i < songNames.Count; i++)
            {
                songNames[i] = songNames[i].Replace("Content/Music\\", "");
                songNames[i] = songNames[i].Replace(".xnb", "");
                songs.Add(content.Load<Song>("Music/" + songNames[i]));

            }

        }

        public static void PlaySong(string name)
        {
            for (int i = 0; i < songNames.Count; i++)
            {
                if (songNames[i] == name)
                {
                    MediaPlayer.Play(songs[i]);
                    break;
                }
            }

            if (publicStatics.soundsEnabled == false)
            {
                MediaPlayer.Pause();
            }
        }

        public static void PauseSong(string name)
        {
            if (publicStatics.soundsEnabled)
            {
                for (int i = 0; i < songNames.Count; i++)
                {
                    if (songNames[i] == name)
                    {
                        MediaPlayer.Pause();
                        break;
                    }
                }
            }
        }

        public static void PlaySound(string name)
        {
            if (publicStatics.soundsEnabled)
            {
                for (int i = 0; i < soundNames.Count; i++)
                {
                    if (soundNames[i] == name)
                    {
                        soundEffect[i].Play();
                        break;
                    }
                }
            }
        }

        public static void unload()
        {
            if (publicStatics.soundsEnabled)
            {
                for (int i = 0; i < soundEffect.Count; i++)
                {
                    soundEffect[i].Dispose();
                }
            }
        }

        public static void StopAllSongs()
        {
            for (int i = 0; i < songNames.Count; i++)
            {
                MediaPlayer.Stop();
            }
        }
    }
}
