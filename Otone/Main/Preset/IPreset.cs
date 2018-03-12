using Otone.Main.Synthesis;
using System;

namespace Otone.Main.Preset
{
    /// <summary>
    /// Определяет интерфейс поведения класса, работающего с пресетом осциллятора.
    /// </summary>
    public interface IPreset
    {
        /// <summary>
        /// Сохраняет пресет.
        /// </summary>
        /// <param name = "fileName">
        /// </param>
        /// Путь к файлу или базе данных.
        /// <param name = "oscs">
        /// Массив из осцилляторов для записи его в пресет.
        /// </param>
        void SavePreset(String path, Oscillator[] oscs);


        /// <summary>
        /// Загружает пресет.
        /// </summary>
        /// <param name = "path">
        /// Путь к файлу или базу данных.
        /// </param>
        /// <param name = "oscs">
        /// Неинициализированный массив осцилляторов для загрузки в него пресета.
        /// </param>
        void LoadPreset(String path, out Oscillator[] oscs);
    }
}