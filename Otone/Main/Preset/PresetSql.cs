using Otone.Main.Synthesis;
using Otone.Service.Exceptions;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Otone.Main.Preset
{
    /// <summary>
    /// Представляет методы для работы с пресетом формата Sql. Не наследуется.
    /// </summary>
    public sealed class PresetSql : IPreset
    {
        private String name = "Preset";


        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name = "name">
        /// Имя таблицы в базе данных.
        /// </param>
        public PresetSql(String name)
        {
            this.name = name;
        }


        /// <summary>
        /// Сохраняет пресет в базу данных Sql.
        /// </summary>
        /// <param name = "connectionString">
        /// Строка подключения к БД.
        /// </param>
        /// <param name = "oscillators">
        /// Массив осцилляторов для записи его в пресет.
        /// </param>
        /// <exception cref = "PresetException"></exception>
        public void SavePreset(String connectionString, Oscillator[] oscillators)
        {
            try
            {
                SqlConnection db = new SqlConnection(connectionString);
                db.Open();
                String markup = "CREATE TABLE [dbo].[" + name + "]" +
                                "(" +
                                    "ID INT IDENTITY(1, 1) NOT NULL, " +
                                    "Enable BIT NOT NULL, " +
                                    "Volume INT NOT NULL, " +
                                    "Frequency INT NOT NULL, " +
                                    "Amplitude INT NOT NULL, " +
                                    "Shape INT NOT NULL" +
                                ");";
                SqlCommand build = new SqlCommand(markup, db);
                build.ExecuteNonQuery();
                String query = "INSERT INTO " +
                                name +
                                "(" +
                                    "Enable, " +
                                    "Volume, " +
                                    "Frequency, " +
                                    "Amplitude, " +
                                    "Shape" +
                                ") " +
                                "VALUES(@param0, @param1, @param2, @param3, @param4)";
                SqlCommand insert = new SqlCommand(query, db);
                insert.Parameters.Add("@param0", SqlDbType.Bit);
                insert.Parameters.Add("@param1", SqlDbType.Int);
                insert.Parameters.Add("@param2", SqlDbType.Int);
                insert.Parameters.Add("@param3", SqlDbType.Int);
                insert.Parameters.Add("@param4", SqlDbType.Int);
                for (Int32 i = 0; i < oscillators.Length; i++)
                {
                    insert.Parameters[0].Value = oscillators[i].Enable;
                    insert.Parameters[1].Value = oscillators[i].Volume;
                    insert.Parameters[2].Value = oscillators[i].Frequency;
                    insert.Parameters[3].Value = oscillators[i].Amplitude;
                    insert.Parameters[4].Value = (Int32)oscillators[i].Shape;
                    insert.ExecuteNonQuery();
                }
                db.Close();
            }
            catch
            {
                throw new PresetException("Ошибка при записи пресета в базу данных Sql.");
            }
        }


        /// <summary>
        /// Загружает пресет из базы данных Sql.
        /// </summary>
        /// <param name = "connectionString">
        /// Строка подключения к БД.
        /// </param>
        /// <param name = "oscillators">
        /// Неинициализированный массив осцилляторов для загрузки в него пресета.
        /// </param>
        /// <exception cref = "PresetException"></exception>
        public void LoadPreset(String connectionString, out Oscillator[] oscillators)
        {
            try
            {
                oscillators = null;
                SqlConnection db = new SqlConnection(connectionString);
                db.Open();
                SqlCommand select = new SqlCommand("SELECT COUNT(*) AS OSCS FROM " + name, db);
                using (SqlDataReader reader = select.ExecuteReader())
                {
                    reader.Read();
                    oscillators = new Oscillator[Convert.ToInt32(reader["OSCS"].ToString())];
                }
                select.CommandText = "SELECT * FROM " + name;
                using (SqlDataReader reader = select.ExecuteReader())
                {
                    Int32 i = 0;
                    while (reader.Read())
                    {
                        oscillators[i] = new Oscillator
                        (
                            Convert.ToBoolean(reader["Enable"]),
                            Convert.ToInt32(reader["Volume"]),
                            Convert.ToInt32(reader["Frequency"]),
                            Convert.ToInt32(reader["Amplitude"]),
                            (Waveshape)Convert.ToInt32(reader["Shape"])
                        );
                        i++;
                    }
                }
                db.Close();
            }
            catch
            {
                throw new PresetException("Ошибка при загрузке пресета из базы данных Sql.");
            }
        }
    }
}