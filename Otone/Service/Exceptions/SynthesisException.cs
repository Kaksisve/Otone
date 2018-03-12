using System;

namespace Otone.Service.Exceptions
{
    /// <summary>
    /// Представляет исключение семейства пространства имён Otone.Synthesis.
    /// </summary>
    public class SynthesisException : Exception
    {
        /// <param name = "message">
        /// Сообщение об ошибке.
        /// </param>
        public SynthesisException(String message) : base(message)
        {

        }


        /// <param name = "type">
        /// Тип исключения.
        /// </param>
        public SynthesisException(SynthesisExceptionType type)
        {
            switch (type)
            {
                case SynthesisExceptionType.OutOfPermissibleValue:
                    new Exception("Нарушены границы допустимого значения.");
                    break;
                case SynthesisExceptionType.Any:
                    new Exception();
                    break;
            }
        }
    }
}