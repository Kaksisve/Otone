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
        /// Клавиша.
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


        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name = "key">
        /// Клавиша.
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
        /// Получает частоту клавиши.
        /// </summary>
        /// <param name = "pitchFork">
        /// Камертон.
        /// </param>
        /// <param name = "interval">
        /// Интервал между клавишей и камертоном.
        /// </param>
        public Int32 GetFrequency(PitchFork pitchFork)
        {
            return ((Int32)(pitchFork.Frequency * Math.Pow(2, GetInterval() / Piano.KeysCount)));
        }


        private Int32 GetInterval()
        {
            return (((Int32)Octave * Piano.KeysCount) + (Int32)Key);
        }
    }
}