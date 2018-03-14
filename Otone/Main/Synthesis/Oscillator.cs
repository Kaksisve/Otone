using Otone.Service.Exceptions;
using System;

namespace Otone.Main.Synthesis
{
    /// <summary>
    /// Представляет поля и методы для работы с абстракцией осциллятора.
    /// </summary>
    [Serializable]
    public class Oscillator : IMixer
    {
        private Boolean enable;
        /// <summary>
        /// Состояние вкл / выкл.
        /// </summary>
        public Boolean Enable
        {
            get
            {
                return enable;
            }
        }


        private Int32 volume;
        /// <summary>
        /// Процентная громкость.
        /// </summary>
        public Int32 Volume
        {
            get
            {
                return volume;
            }
        }


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


        private Int32 amplitude;
        /// <summary>
        /// Амплитуда.
        /// </summary>
        public Int32 Amplitude
        {
            get
            {
                return amplitude;
            }
        }


        private Waveshape shape;
        /// <summary>
        /// Волноформа.
        /// </summary>
        public Waveshape Shape
        {
            get
            {
                return shape;
            }
        }


        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name = "enable">
        /// Состояние вкл / выкл.
        /// </param>
        /// <param name = "volume">
        /// Процентная громкость [0; 150] %.
        /// </param>
        /// <param name = "frequency">
        /// Частота [1; 100] Гц.
        /// </param>
        /// <param name = "amplitude">
        /// Амплитуда [1; 100] м.
        /// </param>
        /// <param name = "shape">
        /// Волноформа.
        /// </param>
        /// <exception cref = "SynthesisException"></exception>
        public Oscillator(Boolean enable, Int32 volume, Int32 frequency, Int32 amplitude, Waveshape shape)
        {
            if (volume < 0 || volume > 150 || frequency < 1 || frequency > 100 || amplitude < 1 || amplitude > 100) throw new SynthesisException("Нарушены границы допустимого значения.");
            this.enable = enable;
            this.volume = volume;
            this.frequency = frequency;
            this.amplitude = amplitude;
            this.shape = shape;
        }


        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name = "enable">
        /// Состояние вкл / выкл.
        /// </param>
        /// <param name = "volume">
        /// Процентная громкость ((0; 1.5] * 100) %.
        /// </param>
        /// <param name = "frequency">
        /// Частота [1; 100] Гц.
        /// </param>
        /// <param name = "amplitude">
        /// Амплитуда [1; 100] м.
        /// </param>
        /// <param name = "shape">
        /// Волноформа.
        /// </param>
        /// <exception cref = "SynthesisException"></exception>
        public Oscillator(Boolean enable, Double volume, Int32 frequency, Int32 amplitude, Waveshape shape)
        {
            if (volume < 0 || volume > 1.5 || frequency < 1 || frequency > 100 || amplitude < 1 || amplitude > 100) throw new SynthesisException("Нарушены границы допустимого значения.");
            this.enable = enable;
            this.volume = (Int32)(volume * 100);
            this.frequency = frequency;
            this.amplitude = amplitude;
            this.shape = shape;
        }
    }
}