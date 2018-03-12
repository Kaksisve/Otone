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
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }
                String markup = "CREATE TABLE [dbo].[" + name + "] \n" +
                                "( \n" +
                                    "ID INT IDENTITY(1, 1) NOT NULL \n" +
                                    "Enable BINARY NOT NULL \n" +
                                    "Volume INT NOT NULL \n" +
                                    "Frequency INT NOT NULL \n" +
                                    "Amplitude INT NOT NULL \n" +
                                    "Shape INT NOT NULL \n" +
                                "); \n";
                SqlCommand build = new SqlCommand(markup, db);
                build.ExecuteNonQuery();
                SqlCommand insert = new SqlCommand
                {
                    CommandText = "INSERT INTO " +
                                        name +
                                        "(" +
                                            "Enable, " +
                                            "Volume, " +
                                            "Frequency, " +
                                            "Amplitude, " +
                                            "Shape, " +
                                        ")" +
                                        "values(@param0, @param1, @param2, @param3, @param4, @param5, @param6)"
                };
                insert.Parameters.Add("@param0", SqlDbType.Binary);
                insert.Parameters.Add("@param1", SqlDbType.Int);
                insert.Parameters.Add("@param2", SqlDbType.Int);
                insert.Parameters.Add("@param3", SqlDbType.Int);
                insert.Parameters.Add("@param4", SqlDbType.Int);
                for (Int32 i = 0; i < oscillators.Length; i++)
                {
                    insert.Parameters[0].Value = Convert.ToByte(oscillators[i].Enable);
                    insert.Parameters[1].Value = oscillators[i].Volume;
                    insert.Parameters[2].Value = oscillators[i].Frequency;
                    insert.Parameters[3].Value = oscillators[i].Amplitude;
                    insert.Parameters[4].Value = (Int32)oscillators[i].Shape;
                    insert.ExecuteNonQuery();
                }
            }
            catch
            {
                throw new PresetException(PresetExceptionType.SqlSaveError);
            }
        }


        /// <summary>
        /// Загружает пресет из базы данных Sql.
        /// </summary>
        /// <param name = "connectionString">
        /// Строка подключения к БД.
        /// </param>
        /// <param name = "oscs">
        /// Неинициализированный массив осцилляторов для загрузки в него пресета.
        /// </param>
        /// <exception cref = "PresetException"></exception>
        public void LoadPreset(String connectionString, out Oscillator[] oscs)
        {
            try
            {
                oscs = null;
                SqlConnection db = new SqlConnection(connectionString);
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }
                SqlCommand select = new SqlCommand("SELECT * FROM " + name);
                using (SqlDataReader reader = select.ExecuteReader())
                {
                    oscs = new Oscillator[select.ExecuteNonQuery()];
                    for (Int32 i = 0; i < oscs.Length; i++)
                    {
                        oscs[i] = new Oscillator
                        (
                            Convert.ToBoolean(Convert.ToInt32(reader.GetSqlValue(0))),
                            Convert.ToInt32(reader.GetSqlValue(1)),
                            Convert.ToInt32(reader.GetSqlValue(2)),
                            Convert.ToInt32(reader.GetSqlValue(3)),
                            (WaveShape)Convert.ToInt32(reader.GetSqlValue(4))
                        );
                    }
                }
            }
            catch
            {
                throw new PresetException(PresetExceptionType.SqlLoadError);
            }
        }
    }
}