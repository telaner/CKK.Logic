using CKK.Logic.Models;
using CKK.Logic.Repository.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Repository.Implementation
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly string _tableName = "Customers";

        public CustomerRepository(IConnectionFactory connectionFactory) 
        {
            _connectionFactory = connectionFactory;
        }

        public void Add(Customer item)
        {
            using (var connection = _connectionFactory.GetConnection) 
            {
                string insertQuery = @"INSERT INT [dbo].[Customers]([Address],[Name]) VALUES (@Address, @Name)";
                SqlMapper.Execute(connection, insertQuery, item);
            }
        }

        public Customer Find(int id)
        {
            using (var conn = _connectionFactory.GetConnection)
            {
                var query = SqlMapper.QuerySingleOrDefault<Customer>(conn, $"SELECT * FROM {_tableName} WHERE Id = @Id", new { Id = id });
                if (query == null)
                    throw new KeyNotFoundException($"{_tableName} with id [{id}] could not be found.");
                return query;
            }
        }

        public IEnumerable<Customer> GetAll()
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                return SqlMapper.Query<Customer>(connection, $"SELECT * FROM {_tableName}");
            }
        }

        public void Remove(Customer item)
        {
            using (var conn = _connectionFactory.GetConnection)
            {
                SqlMapper.Execute(conn, $"DELETE FROM {_tableName} WHERE Id = @Id", new { Id = item.Id });
            }
        }

        public void Update(Customer item)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = "UPDATE Customers SET Name = @Name, Address = @Address WHERE Id= @Id";
                var parameters = new DynamicParameters();
                parameters.Add("Id", item.Id, DbType.Int32);
                parameters.Add("Name", item.Name, DbType.String);
                parameters.Add("Address", item.Address, DbType.String);
                {
                    SqlMapper.Execute(connection, query, parameters);
                }

            }
        }
    }
}
