using CKK.Logic.Models;
using CKK.Logic.Repository.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Repository.Implementation
{
    public class ProductRepository : IProductRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly string _tableName = "Products";
        public ProductRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public void Add(Product entity)
        {
            using (var conn = _connectionFactory.GetConnection)
            {
                List<Product> products = new List<Product>();
                products.Add(entity);
                SqlMapper.Execute(conn, "dbo.Product_Insert @Price, @Quantity, @Name", products);
            }
        }
        public Product Find(int id)
        {
            using (var conn = _connectionFactory.GetConnection)
            {

                var result = SqlMapper.QuerySingleOrDefault<Product>(conn, $"SELECT * FROM {_tableName} WHERE Id=@Id", new { Id = id });
                if (result == null)
                    throw new KeyNotFoundException($"{_tableName} with id [{id}] could not be found.");
                return result;
            }
        }
        public IEnumerable<Product> GetAll()
        {
            using (var conn = _connectionFactory.GetConnection)
            {
                var result = SqlMapper.Query<Product>(conn, $"SELECT * FROM {_tableName}");
                return result;

            }
        }
        public IEnumerable<Product> GetItemsByName(string name)
        {
            using (var conn = _connectionFactory.GetConnection)
            {
 
                var result = SqlMapper.Query<Product>(conn, $"SELECT * FROM {_tableName} WHERE Name like '%' +  @Name + '%'", new { Name = name });
                if (result == null)
                    throw new KeyNotFoundException($"{_tableName} with name [{name}] could not be found.");
                return result;
            }
        }
        public IEnumerable<Product> GetItemsByPrice(decimal price)
        {
            using (var conn = _connectionFactory.GetConnection)
            {
                return SqlMapper.Query<Product>(conn, $"SELECT * FROM {_tableName} WHERE Price = @price", new { price = price });
            }
        }
        public IEnumerable<Product> GetItemsByQuantity(int quantity)
        {
            using (var conn = _connectionFactory.GetConnection)
            {
                return SqlMapper.Query<Product>(conn, $"SELECT * FROM {_tableName} WHERE quantity = @quantity", new { quantity = quantity });
            }
        }
        public void Remove(Product entity)
        {
            using (var conn = _connectionFactory.GetConnection)
            {
                SqlMapper.Execute(conn, $"DELETE FROM {_tableName} WHERE Id=@Id", new { Id = entity.Id });
            }
        }
        public void Update(Product entity)
        {
            var query = "UPDATE Products SET Name = @Name, Price = @Price, Quantity = @Quantity WHERE Id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", entity.Id, DbType.Int32);
            parameters.Add("Name", entity.Name, DbType.String);
            parameters.Add("Price", entity.Price, DbType.String);
            parameters.Add("Quantity", entity.Quantity, DbType.String);
            using (var conn = _connectionFactory.GetConnection)
            {
                SqlMapper.Execute(conn, query, parameters);
            }
        }
    }
 
}
