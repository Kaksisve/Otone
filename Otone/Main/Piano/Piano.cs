using System;
using Otone.Service.Exceptions;

namespace Otone.Main.Keys
{
    /// <summary>
    /// Представляет поля и методы для работы с массивом клавиш. Не наследуется.
    /// </summary>
    public sealed class Piano
    {
        private Note[,] notes;
        /// <summary>
        /// Отдельная клавиша.
        /// </summary>
        /// <param name = "key">
        /// Клавиша.
        /// </param>
        /// <param name = "octave">
        /// Октава.
        /// </param>
        public Note this[Int32 key, Int32 octave]
        {
            get
            {
                return notes[key, octave];
            }
        }



        private Int32 octaves;
        /// <summary>
        /// Количество октав.
        /// </summary>
        public Int32 Octaves
        {
            get
            {
                return octaves;
            }
        }


        private const Int32 keys = 12;
        /// <summary>
        /// Количество клавиш в октаве.
        /// </summary>
        public Int32 Keys
        {
            get
            {
                return keys;
            }
        }


        private static Int32 keysCount = 12;
        public static Int32 KeysCount
        {
            get
            {
                return keysCount;
            }
        }


        private Int32 startOctave;


        private Int32 endOctave;


        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name = "left">
        /// Левая граница (включённый предел).
        /// </param>
        /// <param name = "right">
        /// Правая граница (включённый предел).
        /// </param>
        /// <exception cref = "PianoException"></exception>
        public Piano(Octave left, Octave right)
        {
            if ((Int32)right - (Int32)left < 1) throw new PianoException("Правая октава должна быть больше левой.");
            startOctave = (Int32)left;
            endOctave = (Int32)right;
            octaves = (right - left) + 1;
            InitNotes(out notes);
        }


        private void InitNotes(out Note[,] notes)
        {
            notes = new Note[keys, octaves];
            for (Int32 i = 0; i < keys; i++)
            {
                for (Int32 j = 0; j < octaves; j++)
                {
                    notes[i, j] = new Note((Key)i, (Octave)j + startOctave);
                }
            }
        }
    }
}