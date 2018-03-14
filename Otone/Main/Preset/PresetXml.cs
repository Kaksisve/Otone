using Otone.Main.Synthesis;
using Otone.Service.Exceptions;
using System;
using System.IO;
using System.Runtime.Serialization;

namespace Otone.Main.Preset
{
    /// <summary>
    /// Представляет методы для работы с пресетом формата Xml. Не наследуется.
    /// </summary>
    public sealed class PresetXml : IPreset
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        public PresetXml()
        {

        }


        /// <summary>
        /// Сохраняет пресет в Xml-файл.
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
                DataContractSerializer serializer = new DataContractSerializer(typeof(Oscillator[]));
                using (Stream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    serializer.WriteObject(stream, oscillators);
                }
            }
            catch
            {
                throw new PresetException("Ошибка при записи пресета в Xml-файл.");
            }
        }


        /// <summary>
        /// Загружает пресет из Xml-файла.
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
                DataContractSerializer serializer = new DataContractSerializer(typeof(Oscillator[]));
                using (Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    oscillators = (Oscillator[])serializer.ReadObject(stream);
                }
            }
            catch
            {
                throw new PresetException("Ошибка при загрузке пресета из Xml-файла.");
            }
        }
    }
}