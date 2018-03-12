using Otone.Main.Synthesis;
using Otone.Service.Exceptions;
using System;
using System.IO;
using System.Xml.Serialization;

namespace Otone.Main.Preset
{
    /// <summary>
    /// Представляет методы для работы с пресетом формата Xml. Не наследуется.
    /// </summary>
    public sealed class PresetXml : IPreset
    {
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
        public void SavePreset(String fileName, Oscillator[] oscs)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Oscillator[]));
                using (Stream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    serializer.Serialize(stream, oscs);
                }
            }
            catch
            {
                throw new PresetException(PresetExceptionType.XmlSaveError);
            }
        }


        /// <summary>
        /// Загружает пресет из Xml-файла.
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
                XmlSerializer serializer = new XmlSerializer(typeof(Oscillator));
                using (Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    oscs = (Oscillator[])serializer.Deserialize(stream);
                }
            }
            catch
            {
                throw new PresetException(PresetExceptionType.XmlLoadError);
            }
        }
    }
}