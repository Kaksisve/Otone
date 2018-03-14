using System;
using Otone.Service.Exceptions;

namespace Otone.Main.Keys
{
    /// <summary>
    /// Представляет поле для работы с камертоном.
    /// </summary>
    public class PitchFork
    {
        private Int32 frequency;
        /// <summary>
        /// Частота.
        /// </summary>
        public Int32 Frequency
        {
            get
            {
                return frequency;
            }
        }


        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name = "frequency">
        /// Частота [1; 100] Гц.
        /// </param>
        public PitchFork(Int32 frequency)
        {
            if (frequency < 1 || frequency > 100) throw new PianoException("Нарушены границы допустимого значения.");
            this.frequency = frequency;
        }
    }
}