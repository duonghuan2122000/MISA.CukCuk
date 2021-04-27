using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Core.Interfaces.Repository;
using MySqlConnector;
using System;
using System.Data;

namespace MISA.Infrastructure.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected IConfiguration _configuration;
        protected string _connectionString;
        private string _tableName = typeof(T).Name;

        public BaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("database");
        }

        public int Delete(Guid id)
        {
            // Thiết lập kết nối cơ sở dữ liệu.
            var connection = new MySqlConnection(_connectionString);

            // stored procedures.
            var proc = $"Proc_Delete{_tableName}";

            var p = new DynamicParameters();
            p.Add($"{_tableName}Id", id);

            var rowsAffect = connection.Execute(proc, p, commandType: CommandType.StoredProcedure);

            return rowsAffect;
        }

        public T Get(Guid id)
        {
            // Thiết lập kết nối cơ sở dữ liệu.
            var connection = new MySqlConnection(_connectionString);

            var proc = $"Proc_H_Get{_tableName}ById";

            var p = new DynamicParameters();
            p.Add($"{_tableName}Id", id);

            // Lấy thông tin khách hàng.
            var entity = connection.QueryFirstOrDefault<T>(proc, p, commandType: CommandType.StoredProcedure);
            return entity;
        }

        public int Insert(T t)
        {
            // Thiết lập kết nối cơ sở dữ liệu.
            var connection = new MySqlConnection(_connectionString);

            var proc = $"Proc_Insert{_tableName}";

            var rowsAffect = connection.Execute(proc, t, commandType: CommandType.StoredProcedure);

            return rowsAffect;
        }

        public int Update(T t)
        {
            // Thiết lập kết nối cơ sở dữ liệu.
            var connection = new MySqlConnection(_connectionString);

            var proc = $"Proc_Update{_tableName}";

            var rowsAffect = connection.Execute(proc, t, commandType: CommandType.StoredProcedure);

            return rowsAffect;
        }
    }
}
