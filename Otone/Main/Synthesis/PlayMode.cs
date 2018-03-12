namespace Otone.Main.Synthesis
{
    /// <summary>
    /// Типы проигрывания звука.
    /// </summary>
    public enum PlayMode
    {
        /// <summary>
        /// Стандартный (без расстройки)
        /// </summary>
        Standart,
        /// <summary>
        /// Усреднение данных.
        /// </summary>
        Average,
        /// <summary>
        /// С расстройкой.
        /// </summary>
        WithDetune,
    }
}