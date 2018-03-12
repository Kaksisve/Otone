namespace Otone.Service.Exceptions
{
    public enum PianoExceptionType
    {
        /// <summary>
        /// Нарушены границы допустимого значения.
        /// </summary>
        OutOfPermissibleValue,
        /// <summary>
        /// Правая октава меньше или равна левой.
        /// </summary>
        IndexesMismatch,
        /// <summary>
        /// Такой клавиши в массиве нет.
        /// </summary>
        ThereIsNoThisKey,
        /// <summary>
        /// Другое.
        /// </summary>
        Any
    }
}