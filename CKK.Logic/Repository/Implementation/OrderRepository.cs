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
    public class OrderRepository : IOrderRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly string _tableName = "Orders";

        public OrderRepository(IConnectionFactory connectionFactory) 
        {
            _connectionFactory = connectionFactory;
        }
        public void Add(Order item)
        {
            using (var conn = _connectionFactory.GetConnection)
            {
                string insertQuery = @"INSERT INT [dbo].[Orders]([OrderNumber], [ShoppingCartId]) VALUES (@OrderNumber, @ShoppingCartId)";
                SqlMapper.Execute(conn, insertQuery, item);
            }
        }

        public IEnumerable<Order> GetAll()
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                return SqlMapper.Query<Order>(connection, $"SELECT * FROM {_tableName}");
            }
        }

        public Order GetOrderByCustomer(int Id)
        {
            using (var conn = _connectionFactory.GetConnection)
            {
                var query = SqlMapper.QuerySingleOrDefault<Order>(conn, $"SELECT * FROM {_tableName} WHERE Id = @OrderId", new { ID = Id });
                if (query == null)
                    throw new KeyNotFoundException($"{_tableName} with id [{Id}] could not be found.");
                return query;
            };
        }

        public void Remove(Order item)
        {
            using (var conn = _connectionFactory.GetConnection)
            {
                SqlMapper.Execute(conn, $"DELETE FROM {_tableName} WHERE Id = @Id", new { Id = item });
            }
        }

        public void Update(Order item)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = "UPDATE Orders SET OrderNumber = @OrderNumber, ShoppingCartId = @ShoppingCartId, WHERE Id= @OrderId";
                var parameters = new DynamicParameters();
                parameters.Add("ShoppingCartId", item, DbType.Int32);
                
                {
                    SqlMapper.Execute(connection, query, parameters);
                }

            }
        }
    }
}
