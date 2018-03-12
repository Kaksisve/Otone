using System;

namespace Otone.Main.Keys
{
    /// <summary>
    /// Представляет поля и методы для работы с отдельной клавишей.
    /// </summary>
    public class Note
    {
        private Key key;
        /// <summary>
        /// Номер.
        /// </summary>
        public Key Key
        {
            get
            {
                return key;
            }
        }


        private Octave octave;
        /// <summary>
        /// Октава.
        /// </summary>
        public Octave Octave
        {
            get
            {
                return octave;
            }
        }


        /// <param name = "key">
        /// Номер.
        /// </param>
        /// <param name = "octave">
        /// Октава.
        /// </param>
        public Note(Key key, Octave octave)
        {
            this.key = key;
            this.octave = octave;
        }


        /// <summary>
        /// Получает частоту, зная камертон и интервал между клавишей и камертоном.
        /// </summary>
        /// <param name = "pitchFork">
        /// Камертон.
        /// </param>
        /// <param name = "interval">
        /// Интервал.
        /// </param>
        public Int32 GetFrequency(PitchFork pitchFork, Int32 interval)
        {
            return ((Int32)(pitchFork.Frequency * Math.Pow(2, interval / Piano.KeysCount)));
        }


        /// <summary>
        /// Получает интервал, зная клавишу.
        /// </summary>
        /// <param name = "note">
        /// Клавиша.
        /// </param>
        public Int32 GetInterval()
        {
            return (((Int32)Octave * Piano.KeysCount) + (Int32)Key);
        }
    }
}