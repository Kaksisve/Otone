using System;

namespace Otone.Service.Exceptions
{
    /// <summary>
    /// Представляет исключение семейства пространства имён Otone.Piano.
    /// </summary>
    public class PianoException : Exception
    {
        /// <summary>
        /// Выбрасывает исключение.
        /// </summary>
        /// <param name = "message">
        /// Сообщение об ошибке.
        /// </param>
        public PianoException(String message) : base(message)
        {

        }
    }
}