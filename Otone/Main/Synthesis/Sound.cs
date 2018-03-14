using Microsoft.DirectX.DirectSound;
using Otone.Service.Exceptions;
using System;

namespace Otone.Main.Synthesis
{
    /// <summary>
    /// Представляет поля и методы для проигрывания звука. Не наследуется.
    /// </summary>
    public sealed class Sound : IMixer
    {
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


        private Int32 detune = 0;
        /// <summary>
        /// Расстройка.
        /// </summary>
        public Int32 Detune
        {
            get
            {
                return detune;
            }
        }


        private Oscillator[] oscillators;


        private const Int32 loopDuration = 44100;


        // [10; 44100]
        private const Int32 secondaryLoopDuration = loopDuration;


        private Microsoft.DirectX.DirectSound.Buffer player;


        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name = "volume">
        /// Процентная громкость [0; 150] %.
        /// </param>
        /// <param name = "oscillators">
        /// Массив осцилляторов.
        /// </param>
        /// <param name = "detune">
        /// Расстройка [0; 10000].
        /// </param>
        public Sound(Int32 volume, Oscillator[] oscillators, Int32 detune)
        {
            if (volume < 0 || volume > 150 || oscillators == null || oscillators.Length == 0 || detune < 0 || detune > 10000) throw new SynthesisException("Нарушены границы допустимого значения.");
            this.volume = volume;
            this.oscillators = oscillators;
            this.detune = detune;
        }


        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name = "volume">
        /// Процентная громкость ([0; 1.5] * 100) %.
        /// </param>
        /// <param name = "oscillators">
        /// Массив осцилляторов.
        /// </param>
        /// <param name = "detune">
        /// Расстройка [0; 10000].
        /// </param>
        public Sound(Double volume, Oscillator[] oscillators, Int32 detune)
        {
            if (volume < 0 || volume > 1.5 || oscillators == null || oscillators.Length == 0 || detune < 0 || detune > 10000) throw new SynthesisException("Нарушены границы допустимого значения.");
            this.volume = (Int32)(volume * 100);
            this.oscillators = oscillators;
            this.detune = detune;
        }


        #region Методы, отвечающие за генерацию звуковых данных.
        private void Sine(Oscillator oscillator, out Double[] data)
        {
            data = new Double[loopDuration];
            for (Int32 i = 0; i < loopDuration; i++)
            {
                data[i] = oscillator.Amplitude * Math.Sin(oscillator.Frequency * 2 * Math.PI * i);
                data[i] *= (oscillator.Volume / 100);
                data[i] *= (volume / 100);
            }
        }


        // Аддитивная функция, не использующая громкость мастер-канала.
        private void SineAdditive(Oscillator oscillator, out Double[] data)
        {
            data = new Double[loopDuration];
            for (Int32 i = 0; i < loopDuration; i++)
            {
                data[i] = oscillator.Amplitude * Math.Sin(oscillator.Frequency * 2 * Math.PI * i);
            }
        }


        // Аддитивная функция, не использующая громкость мастер-канала.
        private void CosineAdditive(Oscillator oscillator, out Double[] data)
        {
            data = new Double[loopDuration];
            for (Int32 i = 0; i < loopDuration; i++)
            {
                data[i] = oscillator.Amplitude * Math.Cos(oscillator.Frequency * 2 * Math.PI * i);
            }
        }


        private void Tangent(Oscillator oscillator, out Double[] data)
        {
            data = new Double[loopDuration];
            SineAdditive(oscillator, out Double[] extra1);
            CosineAdditive(oscillator, out Double[] extra2);
            for (Int32 i = 0; i < loopDuration; i++)
            {
                if (extra2[i] == 0) data[i] = oscillator.Amplitude;
                else data[i] = (extra1[i] / extra2[i]);
                if (data[i] > oscillator.Amplitude) data[i] = oscillator.Amplitude;
                data[i] *= (oscillator.Volume / 100);
                data[i] *= (volume / 100);
            }
        }


        private void Secant(Oscillator oscillator, out Double[] data)
        {
            data = new Double[loopDuration];
            CosineAdditive(oscillator, out Double[] extra);
            for (Int32 i = 0; i < loopDuration; i++)
            {
                if (extra[i] == 0) data[i] = oscillator.Amplitude;
                else data[i] = (1 / extra[i]);
                data[i] *= (oscillator.Volume / 100);
                data[i] *= (volume / 100);
            }
        }


        private void Noise(Oscillator oscillator, out Double[] data)
        {
            data = new Double[loopDuration];
            Random value = new Random();
            for (Int32 i = 0; i < loopDuration; i++)
            {
                data[i] = value.Next(-oscillator.Amplitude, oscillator.Amplitude + 1);
                data[i] *= (oscillator.Volume / 100);
                data[i] *= (volume / 100);
            }
        }


        private void WhiteNoise(Oscillator oscillator, out Double[] data)
        {
            data = new Double[loopDuration];
            for (Int32 i = 0; i < loopDuration; i++)
            {
                data[i] = oscillator.Amplitude;
                data[i] *= (oscillator.Volume / 100);
                data[i] *= (volume / 100);
            }
        }


        private void Square(Oscillator oscillator, out Double[] data)
        {
            data = new Double[loopDuration];
            SineAdditive(oscillator, out Double[] extra);
            for (Int32 i = 0; i < loopDuration; i++)
            {
                if (extra[i] > 0) data[i] = oscillator.Amplitude;
                else if (extra[i] < 0) data[i] = oscillator.Amplitude;
                else data[i] = 0;
                data[i] *= (oscillator.Volume / 100);
                data[i] *= (volume / 100);
            }
        }


        private void Saw(Oscillator oscillator, out Double[] data)
        {
            data = new Double[loopDuration];
            SineAdditive(oscillator, out Double[] extra);
            for (Int32 i = 0; i < loopDuration; i++)
            {
                Double sum = 0;
                for (Int32 j = 0; j < secondaryLoopDuration; j++)
                {
                    if (j != 0) sum += (Math.Pow(-1, j + 1) / j) * extra[i];
                    else sum += oscillator.Amplitude;
                }
                data[i] = (2 / Math.PI) * sum;
                if (data[i] > oscillator.Amplitude) data[i] = oscillator.Amplitude;
                data[i] *= (oscillator.Volume / 100);
                data[i] *= (volume / 100);
            }
        }


        private void Triangle(Oscillator oscillator, out Double[] data)
        {
            data = new Double[loopDuration];
            SineAdditive(oscillator, out Double[] extra);
            for (Int32 i = 0; i < loopDuration; i++)
            {
                Double sum = 0;
                for (Int32 j = 0; j < secondaryLoopDuration; j++)
                {
                    if (j != 0) sum += Math.Pow(-1, (j - 1) / 2) * (extra[i] / Math.Pow(j, 2));
                    else sum += oscillator.Amplitude;
                }
                data[i] = ((8 * oscillator.Amplitude) / Math.Pow(Math.PI, 2)) * sum;
                if (data[i] > oscillator.Amplitude) data[i] = oscillator.Amplitude;
                data[i] *= (oscillator.Volume / 100);
                data[i] *= (volume / 100);
            }
        }


        private void Void(Oscillator oscillator, out Double[] data)
        {
            data = new Double[loopDuration];
            // Не нужно.
            for (Int32 i = 0; i < loopDuration; i++)
            {
                data[i] = default(Int32);
            }
        }
        #endregion


        private void Average(Double[][] datas, out Double[] average)
        {
            if (detune != 0)
            {
                for (Int32 i = 0; i < datas.Length; i++)
                {
                    datas[i][0] = datas[i][1];
                    for (Int32 j = 1; j < detune - 1; j++)
                    {
                        datas[i][j] = datas[i][j + 1];
                    }
                }
            }
            average = new Double[loopDuration];
            for (Int32 i = 0; i < loopDuration; i++)
            {
                Double sum = 0;
                for (Int32 j = 0; j < loopDuration; j++)
                {
                    sum += datas[i][j];
                }
                average[i] = sum / datas.Length;
            }
        }


        /// <summary>
        /// Проигрывает звук.
        /// </summary>
        public void Play()
        {
            Double[][] extra = new Double[oscillators.Length][];
            for (Int32 i = 0; i < oscillators.Length; i++)
            {
                switch (oscillators[i].Shape)
                {
                    case Waveshape.Sine:
                        Sine(oscillators[i], out extra[i]);
                        break;
                    case Waveshape.Tangent:
                        Tangent(oscillators[i], out extra[i]);
                        break;
                    case Waveshape.Secant:
                        Secant(oscillators[i], out extra[i]);
                        break;
                    case Waveshape.Noise:
                        Noise(oscillators[i], out extra[i]);
                        break;
                    case Waveshape.WhiteNoise:
                        WhiteNoise(oscillators[i], out extra[i]);
                        break;
                    case Waveshape.Square:
                        Square(oscillators[i], out extra[i]);
                        break;
                    case Waveshape.Saw:
                        Saw(oscillators[i], out extra[i]);
                        break;
                    case Waveshape.Triangle:
                        Triangle(oscillators[i], out extra[i]);
                        break;
                    case Waveshape.Void:
                        Void(oscillators[i], out extra[i]);
                        break;
                }
            }
            Average(extra, out Double[] data);
            player = new SecondaryBuffer(new BufferDescription(), new Device());
            player.Write(0, data, LockFlag.EntireBuffer);
            player.Play(0, BufferPlayFlags.Default);
        }
    }
}