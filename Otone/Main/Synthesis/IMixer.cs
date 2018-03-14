using System;

namespace Otone.Main.Synthesis
{
    /// <summary>
    /// Определяет интерфейс поведения класса, работающего со звуком. Представляет микшер.
    /// </summary>
    internal interface IMixer
    {
        /// <summary>
        /// Процентная громкость.
        /// </summary>
        Int32 Volume
        {
            get;
        }
    }
}