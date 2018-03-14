using System;

namespace Otone.Service.Exceptions
{
    /// <summary>
    /// Представляет исключение семейства пространства имён Otone.Preset.
    /// </summary>
    public class PresetException : Exception
    {
        /// <summary>
        /// Выбрасывает исключение.
        /// </summary>
        /// <param name = "message">
        /// Сообщение об ошибке.
        /// </param>
        public PresetException(String message) : base(message)
        {

        }
    }
}