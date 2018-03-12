using System;
using System.Xml.Serialization;
using Otone.Service.Exceptions;

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
        [XmlAttribute]
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
        [XmlAttribute]
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
        [XmlAttribute]
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
        [XmlAttribute]
        public Int32 Amplitude
        {
            get
            {
                return amplitude;
            }
        }


        private WaveShape shape;
        /// <summary>
        /// Волноформа.
        /// </summary>
        [XmlAttribute]
        public WaveShape Shape
        {
            get
            {
                return shape;
            }
        }


        public Oscillator()
        {

        }


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
        public Oscillator(Boolean enable, Int32 volume, Int32 frequency, Int32 amplitude, WaveShape shape)
        {
            if (volume < 0 || volume > 150 || frequency < 1 || frequency > 100 || amplitude < 1 || amplitude > 100) throw new SynthesisException(SynthesisExceptionType.OutOfPermissibleValue);
            this.enable = enable;
            this.volume = volume;
            this.frequency = frequency;
            this.amplitude = amplitude;
            this.shape = shape;
        }
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
        public Oscillator(Boolean enable, Double volume, Int32 frequency, Int32 amplitude, WaveShape shape)
        {
            if (volume < 0 || volume > 1.5 || frequency < 1 || frequency > 100 || amplitude < 1 || amplitude > 100) throw new SynthesisException(SynthesisExceptionType.OutOfPermissibleValue);
            this.enable = enable;
            this.volume = (Int32)(volume * 100);
            this.frequency = frequency;
            this.amplitude = amplitude;
            this.shape = shape;
        }
    }
}