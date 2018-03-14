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
        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        public PresetTxt()
        {

        }


        /// <summary>
        /// Загружает пресет из Txt-файла.
        /// </summary>
        /// <param name = "fileName">
        /// Путь к файлу.
        /// </param>
        /// <param name = "oscillators">
        /// Неинициализированный массив осцилляторов для загрузки в него пресета.
        /// </param>
        /// <exception cref = "PresetException"></exception>
        public void LoadPreset(String fileName, out Oscillator[] oscillators)
        {
            try
            {
                oscillators = null;
                using (StreamReader reader = new StreamReader(fileName))
                {
                    String first = reader.ReadToEnd().Trim('\n', '\r', ' ');
                    String[] second = first.Split(new Char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    oscillators = new Oscillator[second.Length];
                    for (Int32 i = 0; i < second.Length; i++)
                    {
                        second[i].Trim('\n', '\r', ' ');
                        String[] third = second[i].Split(new Char[] { ' ' }, StringSplitOptions.None);
                        oscillators[i] = new Oscillator
                        (
                            Convert.ToBoolean(third[0]),
                            Convert.ToInt32(third[1]),
                            Convert.ToInt32(third[2]),
                            Convert.ToInt32(third[3]),
                            (Waveshape)Convert.ToInt32(third[4])
                        );
                    }
                }
            }
            catch
            {
                throw new PresetException("Ошибка при загрузке пресета из Txt-файла.");
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
        public void SavePreset(String fileName, Oscillator[] oscillators)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    for (Int32 i = 0; i < oscillators.Length; i++)
                    {
                        writer.WriteLine
                        (
                            oscillators[i].Enable
                            + " " +
                            oscillators[i].Volume
                            + " " +
                            oscillators[i].Frequency
                            + " " +
                            oscillators[i].Amplitude
                            + " " +
                            (Int32)oscillators[i].Shape
                        );
                    }
                }
            }
            catch
            {
                throw new PresetException("Ошибка при записи пресета в Txt-файл.");
            }
        }
    }
}