using Otone.Main.Synthesis;
using Otone.Service.Exceptions;
using System;
using System.IO;

namespace Otone.Main.Preset
{
    /// <summary>
    /// Представляет методы для работы с пресетом формата Txt. Не наследуется.
    /// </summary>
    public sealed class PresetTxt : IPreset
    {
        public PresetTxt()
        {

        }


        /// <summary>
        /// Загружает пресет из Txt-файла.
        /// </summary>
        /// <param name = "fileName">
        /// Путь к файлу.
        /// </param>
        /// <param name = "oscs">
        /// Неинициализированный массив осцилляторов для загрузки в него пресета.
        /// </param>
        /// <exception cref = "PresetException"></exception>
        public void LoadPreset(String fileName, out Oscillator[] oscs)
        {
            try
            {
                oscs = null;
                using (StreamReader reader = new StreamReader(fileName))
                {
                    String pre = reader.ReadToEnd().Trim('\n', '\r', ' ');
                    String[] av = pre.Split(new Char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    oscs = new Oscillator[av.Length];
                    for (Int32 i = 0; i < av.Length; i++)
                    {
                        av[i].Trim('\n', '\r', ' ');
                        String[] post = av[i].Split(new Char[] { ' ' }, StringSplitOptions.None);
                        oscs[i] = new Oscillator
                        (
                            Convert.ToBoolean(post[0]),
                            Convert.ToInt32(post[1]),
                            Convert.ToInt32(post[2]),
                            Convert.ToInt32(post[3]),
                            (WaveShape)Convert.ToInt32(post[4])
                        );
                    }
                }
            }
            catch
            {
                throw new PresetException(PresetExceptionType.TxtLoadError);
            }
        }


        /// <summary>
        /// Сохраняет пресет в Txt-файл.
        /// </summary>
        /// <param name = "fileName">
        /// Путь к файлу.
        /// </param>
        /// <param name = "oscillators">
        /// Массив осцилляторов для записи его в пресет.
        /// </param>
        /// <exception cref = "PresetException"></exception>
        public void SavePreset(String fileName, Oscillator[] oscs)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    for (Int32 i = 0; i < oscs.Length; i++)
                    {
                        writer.WriteLine
                        (
                            oscs[i].Enable
                            + " " +
                            oscs[i].Volume
                            + " " +
                            oscs[i].Frequency
                            + " " +
                            oscs[i].Amplitude
                            + " " +
                            (Int32)oscs[i].Shape
                        );
                    }
                }
            }
            catch
            {
                throw new PresetException(PresetExceptionType.TxtSaveError);
            }
        }
    }
}