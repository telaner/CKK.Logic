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
        
        
        
        private readonly string _tableName = "Products";

        public ProductRepository() 
        {
           
        }

        public void Add(Product item)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DatabaseConnectionFactory.CnnVal("StructuredProjectDB"))) 
            {
                List<Product> products = new List<Product>();
                products.Add(item);
                connection.Execute("dbo.Product_Insert @Price, @Quantity, @Name", products);
            }
        }

        public Product Find(int id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DatabaseConnectionFactory.CnnVal("StructuredProjectDB")))
            {
                var query = SqlMapper.QuerySingleOrDefault<Product>(connection, $"SELECT * FROM {_tableName} WHERE Id = @ID", new {Id = id});
                if (query == null)
                    throw new KeyNotFoundException($"{_tableName} with id [{id}] could not be found.");
                return query;
            }
        }

        public IEnumerable<Product> GetItemsByName(string name)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DatabaseConnectionFactory.CnnVal("StructuredProjectDB")))
            {
                var query = SqlMapper.Query<Product>(connection, $"SELECT * FROM {_tableName} WHERE Name like '%' + @Name + '%'", new {Name = name});
                if (query == null)
                    throw new KeyNotFoundException($"{_tableName} with name [{name}] could not be found.");
                return query;
            }
        }

        public IEnumerable<Product> GetAll()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DatabaseConnectionFactory.CnnVal("StructuredProjectDB")))
            {
                var output = connection.Query<Product>( $"SELECT * FROM {_tableName}");
                return output;
            }
        }

        public IEnumerable<Product> GetItemsByPrice(decimal price)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DatabaseConnectionFactory.CnnVal("StructuredProjectDB")))
            {
                return SqlMapper.Query<Product>(connection, $"SELECT * FROM {_tableName} WHERE Price =@Price", new { Price = price });
            }
        }

        public IEnumerable<Product> GetItemsByQuantity(int quantity)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DatabaseConnectionFactory.CnnVal("StructuredProjectDB")))
            {
                return SqlMapper.Query<Product>(connection, $"SELECT * FROM {_tableName} WHERE Quantity =@Quantity", new { Qauntity = quantity });
            }
        }

        public void Remove(Product item)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DatabaseConnectionFactory.CnnVal("StructuredProjectDB")))
            {
                SqlMapper.Execute(connection, $"DELETE FROM {_tableName} WHERE Id = @Id", new {Id = item.Id});
            }
        }

        public void Update(Product item)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DatabaseConnectionFactory.CnnVal("StructuredProjectDB")))
            {
                var query = "UPDATE Products SET Name = @Name, Price = @Price, Quantity = @ Quantity WHERE Id= @Id";
                var parameters = new DynamicParameters();
                parameters.Add("Id", item.Id, DbType.Int32);
                parameters.Add("Name", item.Name, DbType.String);
                parameters.Add("Price", item.Price, DbType.Currency);
                parameters.Add("Quantity", item.Quantity, DbType.Int32);
                {
                    SqlMapper.Execute(connection, query, parameters);
                }

            }
            
        }
    }
}
