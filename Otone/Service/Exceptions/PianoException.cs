using System;

namespace Otone.Service.Exceptions
{
    /// <summary>
    /// Представляет исключение семейства пространства имён Otone.Piano.
    /// </summary>
    public class PianoException : Exception
    {
        /// <param name = "message">
        /// Сообщение об ошибке.
        /// </param>
        public PianoException(String message) : base(message)
        {

        }


        /// <param name = "type">
        /// Тип исключения.
        /// </param>
        public PianoException(PianoExceptionType type)
        {
            switch (type)
            {
                case PianoExceptionType.OutOfPermissibleValue:
                    new Exception("Нарушены границы допустимого значения.");
                    break;
                case PianoExceptionType.IndexesMismatch:
                    new Exception("Правая октава должна быть больш левой.");
                    break;
                case PianoExceptionType.Any:
                    new Exception();
                    break;
            }
        }
    }
}