using Otone.Main.Synthesis;
using Otone.Service.Exceptions;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Soap;

namespace Otone.Main.Preset
{
    /// <summary>
    /// Представляет методы для работы с пресетом формата Soap. Не наследуется.
    /// </summary>
    public sealed class PresetSoap : IPreset
    {
        public PresetSoap()
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
        public void SavePreset(String fileName, Oscillator[] oscs)
        {
            try
            {
                SoapFormatter formatter = new SoapFormatter();
                using (FileStream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    formatter.Serialize(stream, oscs);
                }

            }
            catch
            {
                throw new PresetException(PresetExceptionType.SoapSaveError);
            }
        }


        /// <summary>
        /// Загружает пресет из файла.
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
                SoapFormatter formatter = new SoapFormatter();
                using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    oscs = (Oscillator[])formatter.Deserialize(stream);
                }
            }
            catch
            {
                throw new PresetException(PresetExceptionType.SoapLoadError);
            }
        }
    }
}