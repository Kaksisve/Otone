namespace Otone.Main.Synthesis
{
    /// <summary>
    /// Типы звуковой волны.
    /// </summary>
    public enum Waveshape
    {
        /// <summary>
        /// Синусоида.
        /// </summary>
        Sine,
        /// <summary>
        /// Тангенсоида (со срезом по амплитуде).
        /// </summary>
        Tangent,
        /// <summary>
        /// Секансоида.
        /// </summary>
        Secant,
        /// <summary>
        /// Белый (случайные значения).
        /// </summary>
        Noise,
        /// <summary>
        /// Белый шум (амплитудные значения).
        /// </summary>
        WhiteNoise,
        /// <summary>
        /// Меандр.
        /// </summary>
        Square,
        /// <summary>
        /// Пилообразная волна.
        /// </summary>
        Saw,
        /// <summary>
        /// Труегольная волна.
        /// </summary>
        Triangle,
        /// <summary>
        /// Пустое значение.
        /// </summary>
        Void
    }
}