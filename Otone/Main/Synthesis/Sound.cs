using System;
using Microsoft.DirectX.DirectSound;
using System.Threading;
using Otone.Service.Exceptions;

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
        /// Процентная громкость.
        /// </summary>
        public Int32 Detune
        {
            get
            {
                return detune;
            }
        }


        private Oscillator[] oscillators;


        private const Int32 loopDuration = 44100; //Иные варианты: 31400, 58875, 43150
        /// <summary>
        /// Длина секундой звуковой петли.
        /// </summary>
        public Int32 LoopDuration
        {
            get
            {
                return loopDuration;
            }
        }


        Microsoft.DirectX.DirectSound.Buffer player;


        /// <param name = "volume">
        /// Процентная громкость [0; 150] %.
        /// </param>
        /// <param name = "oscs">
        /// Массив осцилляторов.
        /// </param>
        /// <param name = "detune">
        /// Расстройка [0; 10] мс.
        /// </param>
        public Sound(Int32 volume, Oscillator[] oscs)
        {
            if (volume < 0 || volume > 150 || oscs == null || oscs.Length == 0) throw new SynthesisException(SynthesisExceptionType.OutOfPermissibleValue);
            this.volume = volume;
            oscillators = oscs;
        }


        /// <param name = "volume">
        /// Процентная громкость [0; 150] %.
        /// </param>
        /// <param name = "oscs">
        /// Массив осцилляторов.
        /// </param>
        /// <param name = "detune">
        /// Расстройка [0; 10] мс.
        /// </param>
        public Sound(Int32 volume, Oscillator[] oscs, Int32 detune)
        {
            if (volume < 0 || volume > 150 || oscs == null || oscs.Length == 0 || detune < 0 || detune > 10) throw new SynthesisException(SynthesisExceptionType.OutOfPermissibleValue);
            this.volume = volume;
            oscillators = oscs;
            this.detune = detune;
        }


        /// <param name = "volume">
        /// Процентная громкость ([0; 1.5] * 100) %.
        /// </param>
        /// <param name = "oscillators">
        /// Массив осцилляторов.
        /// </param>
        /// <param name = "detune">
        /// Расстройка [0; 10] мс.
        /// </param>
        public Sound(Double volume, Oscillator[] oscillators)
        {
            if (volume < 0 || volume > 1.5 || oscillators == null || oscillators.Length == 0) throw new SynthesisException(SynthesisExceptionType.OutOfPermissibleValue);
            this.volume = (Int32)(volume * 100);
            this.oscillators = oscillators;
        }


        /// <param name = "volume">
        /// Процентная громкость ([0; 1.5] * 100) %.
        /// </param>
        /// <param name = "oscillators">
        /// Массив осцилляторов.
        /// </param>
        /// <param name = "detune">
        /// Расстройка [0; 10] мс.
        /// </param>
        public Sound(Double volume, Oscillator[] oscillators, Int32 detune)
        {
            if (volume < 0 || volume > 1.5 || oscillators == null || oscillators.Length == 0 || detune < 0 || detune > 10) throw new SynthesisException(SynthesisExceptionType.OutOfPermissibleValue);
            this.volume = (Int32)(volume * 100);
            this.oscillators = oscillators;
            this.detune = detune;
        }


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


        //https://habrahabr.ru/post/219337/
        private void Saw(Oscillator oscillator, out Double[] data)
        {
            data = new Double[loopDuration];
            SineAdditive(oscillator, out Double[] extra);
            for (Int32 i = 0; i < loopDuration; i++)
            {
                Double sum = 0;
                for (Int32 j = 0; j < loopDuration; j++)
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
                for (Int32 j = 0; j < loopDuration; j++)
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


        private void Average(Double[][] datas, out Double[] average)
        {
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


        public void PlayAverage()
        {
            Double[][] extra = new Double[oscillators.Length][];
            for (Int32 i = 0; i < oscillators.Length; i++)
            {
                switch (oscillators[i].Shape)
                {
                    case WaveShape.Sine:
                        Sine(oscillators[i], out extra[i]);
                        break;
                    case WaveShape.Tangent:
                        Tangent(oscillators[i], out extra[i]);
                        break;
                    case WaveShape.Secant:
                        Secant(oscillators[i], out extra[i]);
                        break;
                    case WaveShape.Noise:
                        Noise(oscillators[i], out extra[i]);
                        break;
                    case WaveShape.WhiteNoise:
                        WhiteNoise(oscillators[i], out extra[i]);
                        break;
                    case WaveShape.Square:
                        Square(oscillators[i], out extra[i]);
                        break;
                    case WaveShape.Saw:
                        Saw(oscillators[i], out extra[i]);
                        break;
                    case WaveShape.Triangle:
                        Triangle(oscillators[i], out extra[i]);
                        break;
                    case WaveShape.Void:
                        Void(oscillators[i], out extra[i]);
                        break;
                }
            }
            Average(extra, out Double[] data);
            player = new SecondaryBuffer(new BufferDescription(), new Device());
            player.Write(0, data, LockFlag.EntireBuffer);
            player.Play(0, BufferPlayFlags.Default);
        }


        public void Play(Oscillator[] oscillators)
        {

        }
    }
}