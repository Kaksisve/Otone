using System;

namespace Otone.Service.Exceptions
{
    /// <summary>
    /// Представляет исключение семейства пространства имён Otone.Synthesis.
    /// </summary>
    public class SynthesisException : Exception
    {
        /// <summary>
        /// Выбрасывает исключение.
        /// </summary>
        /// <param name = "message">
        /// Сообщение об ошибке.
        /// </param>
        public SynthesisException(String message) : base(message)
        {

        }
    }
}