using Otone.Main.Synthesis;
using Otone.Service.Exceptions;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Otone.Main.Preset
{
    /// <summary>
    /// Представляет методы для работы с пресетом формата Binary. Не наследуется.
    /// </summary>
    public sealed class PresetBinary : IPreset
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        public PresetBinary()
        {

        }


        /// <summary>
        /// Сохраняет пресет в файл.
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
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    formatter.Serialize(stream, oscillators);
                }
            }
            catch
            {
                throw new PresetException("Ошибка при записи пресета в Binary-файл.");
            }
        }


        /// <summary>
        /// Загружает пресет из файла.
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
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    oscillators = (Oscillator[])formatter.Deserialize(stream);
                }
            }
            catch
            {
                throw new PresetException("Ошибка при загрузке пресета из Binary-файла.");
            }
        }
    }
}