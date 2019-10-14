using Dapper;
using System;
using System.Collections.Generic;
using static System.Data.CommandType;
using static System.Data.ParameterDirection;
using ToDo.Entities;
using ToDo.Repository.Interfaces;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace ToDo.Repository
{
    /// <summary>
    /// Category repository
    /// </summary>
    public class CategoryRepository : BaseRepository, 
        IRepository<Category>
    {
        /// <summary>
        /// Columns map
        /// </summary>
        private ColumnMap _columnMap = new ColumnMap();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="config"></param>
        public CategoryRepository(IConfiguration config):
            base(config)
        {
            _columnMap.Add("CategoryId", "CategoryId");
            _columnMap.Add("CategoryName", "Name");
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Category>> Get()
        {
            try
            {
                string getAllCategories = @"
                    SET NOCOUNT ON;
                    SELECT [Category].[CatId] as CategoryId, 
		                [Category].[Name] as CategoryName
	                FROM [dbo].[Category];
                ";
                SqlMapper.SetTypeMap(typeof(Category),
                    new CustomPropertyTypeMap(typeof(Category),
                    (type, columnName) =>
                        type.GetProperty(_columnMap[columnName])));
                return await SqlMapper.QueryAsync<Category>(conn,
                    getAllCategories, null, null, true, null,
                    commandType: Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        /// <summary>
        /// Add
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public async Task<bool> Add(Category category)
        {
            try
            {
                string addCategory = @"
                    SET NOCOUNT ON;
	                SET @AddedSuccessfully = 0;
	                BEGIN TRANSACTION; 
		                BEGIN TRY
			                INSERT INTO [dbo].[Category]([Name])
			                VALUES (@Name)
			                SET @AddedSuccessfully = 1;
		                END TRY
		                BEGIN CATCH
			                SET @AddedSuccessfully = 0;
			                ROLLBACK;
			                RETURN;
		                END CATCH
	                COMMIT TRANSACTION;
                ";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Name", category.Name);
                parameters.Add("@AddedSuccessfully", 
                    System.Data.DbType.Binary, direction: Output);
                await SqlMapper.ExecuteAsync(conn, addCategory, parameters, null, 
                    null, commandType: Text);
                return parameters.Get<Int32>("@AddedSuccessfully") == 1 ? 
                    true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="id"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        public async Task<bool> Update(long id, Category category)
        {
            try
            {
                string updateCategory = @"
                    SET NOCOUNT ON;
	                SET @UpdatedSuccessfully = 0;
	                BEGIN TRANSACTION; 
		                BEGIN TRY
			                UPDATE [dbo].[Category]
			                SET [Name] = @Name
			                WHERE [CatId] = @CatId
			                SET @UpdatedSuccessfully = 1;
		                END TRY
		                BEGIN CATCH
			                SET @UpdatedSuccessfully = 0;
			                ROLLBACK;
			                RETURN;
		                END CATCH
	                COMMIT TRANSACTION;
                ";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CatId", id);
                parameters.Add("@Name", category.Name);
                parameters.Add("@UpdatedSuccessfully", 
                    System.Data.DbType.Binary, direction: Output);
                await SqlMapper.ExecuteAsync(conn, updateCategory, 
                    parameters, null, 
                    null, commandType: Text);
                return parameters.Get<Int32>("@UpdatedSuccessfully") == 1 ?
                    true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> Delete(long id)
        {
            try
            {
                string deleteCategory = @"
                    SET NOCOUNT ON;
	                SET @DeletedSuccessfully = 0;
	                IF (@CatId = 1)
	                BEGIN
		                RETURN;
	                END
	                ELSE
	                BEGIN
		                BEGIN TRANSACTION; 
			                BEGIN TRY
				                UPDATE [dbo].[ToDo]
				                SET [CatId] = 1
				                WHERE [CatId] = @CatId;

				                DELETE FROM [dbo].[Category]
				                WHERE [CatId] = @CatId;

				                SET @DeletedSuccessfully = 1;
			                END TRY
			                BEGIN CATCH
				                SET @DeletedSuccessfully = 0;
				                ROLLBACK;
				                RETURN;
			                END CATCH  
		                COMMIT TRANSACTION;
                    END
                ";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CatId", id);
                parameters.Add("@DeletedSuccessfully",
                    System.Data.DbType.Binary, direction: Output);
                await SqlMapper.ExecuteAsync(conn, deleteCategory, parameters, null,
                    null, commandType: Text);
                return parameters.Get<Int32>("@DeletedSuccessfully") == 1 ?
                    true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
