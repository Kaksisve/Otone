namespace Otone.Service.Exceptions
{
    public enum PresetExceptionType
    {
        /// <summary>
        /// Ошибка при записи в Txt-файл.
        /// </summary>
        TxtSaveError,
        /// <summary>
        /// Ошибка при загрузке из Txt-файла.
        /// </summary>
        TxtLoadError,
        /// <summary>
        /// Ошибка при записи в Xml-файл.
        /// </summary>
        XmlSaveError,
        /// <summary>
        /// Ошибка при загрузке из Xml-файла.
        /// </summary>
        XmlLoadError,
        /// <summary>
        /// Ошибка при записи в базу данных Sql.
        /// </summary>
        SqlSaveError,
        /// <summary>
        /// Ошибка при загрузке из базы данных Sql.
        /// </summary>
        SqlLoadError,
        /// <summary>
        /// Ошибка при записи в Soap-файл.
        /// </summary>
        SoapSaveError,
        /// <summary>
        /// Ошибка при загрузке из Soap-файла.
        /// </summary>
        SoapLoadError,
        /// <summary>
        /// Ошибка при записи в Binary-файл.
        /// </summary>
        BinarySaveError,
        /// <summary>
        /// Ошибка при загрузке из Binary-файла.
        /// </summary>
        BinaryLoadError,
        /// <summary>
        /// Другое.
        /// </summary>
        Any
    }
}