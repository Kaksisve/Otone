using System;

namespace Otone.Service.Exceptions
{
    /// <summary>
    /// Представляет исключение семейства пространства имён Otone.Preset.
    /// </summary>
    public class PresetException : Exception
    {
        /// <param name = "message">
        /// Сообщение об ошибке.
        /// </param>
        public PresetException(String message) : base(message)
        {

        }


        /// <param name = "type">
        /// Тип исключения.
        /// </param>
        public PresetException(PresetExceptionType type)
        {
            switch (type)
            {
                case PresetExceptionType.TxtSaveError:
                    new Exception("Ошибка при записи пресета в Txt-файл.");
                    break;
                case PresetExceptionType.TxtLoadError:
                    new Exception("Ошибка при загрузке пресета из Txt-файла.");
                    break;
                case PresetExceptionType.XmlSaveError:
                    new Exception("Ошибка при записи пресета в Xml-файл.");
                    break;
                case PresetExceptionType.XmlLoadError:
                    new Exception("Ошибка при загрузке пресета из Xml-файла.");
                    break;
                case PresetExceptionType.SqlSaveError:
                    new Exception("Ошибка при записи пресета в базу данных Sql.");
                    break;
                case PresetExceptionType.SqlLoadError:
                    new Exception("Ошибка при загрузке пресета из базы данных Sql.");
                    break;
                case PresetExceptionType.SoapSaveError:
                    new Exception("Ошибка при записи пресета в Soap-файл.");
                    break;
                case PresetExceptionType.SoapLoadError:
                    new Exception("Ошибка при загрузке пресета из Soap-файла.");
                    break;
                case PresetExceptionType.BinarySaveError:
                    new Exception("Ошибка при записи пресета в Binary-файл.");
                    break;
                case PresetExceptionType.BinaryLoadError:
                    new Exception("Ошибка при загрузке пресета из Binary-файла.");
                    break;
                case PresetExceptionType.Any:
                    new Exception();
                    break;
            }
        }
    }
}