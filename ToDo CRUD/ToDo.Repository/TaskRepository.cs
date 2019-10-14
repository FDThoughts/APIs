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
    /// Task repository
    /// </summary>
    public class TaskRepository : BaseRepository, 
        IPaginationRepository<ToDoTask, TaskList>
    {
        /// <summary>
        /// Task columns map
        /// </summary>
        private ColumnMap _columnMapTask = new ColumnMap();
        /// <summary>
        /// Category columns map
        /// </summary>
        private ColumnMap _columnMapCategory = new ColumnMap();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="config"></param>
        public TaskRepository(IConfiguration config) :
            base(config)
        {
            _columnMapTask.Add("TaskId", "TaskId");
            _columnMapTask.Add("Title", "TaskTitle");
            _columnMapTask.Add("Description", "TaskDescription");
            _columnMapTask.Add("DueDate", "TaskDueDate");
            _columnMapTask.Add("Done", "TaskDone");

            _columnMapCategory.Add("CategoryId", "CategoryId");
            _columnMapCategory.Add("CategoryName", "Name");
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ToDoTask>> Get()
        {
            try
            {
                string getAllTasks = @"
                    SET NOCOUNT ON;
	                SELECT [ToDo].[TaskId] as TaskId, 
		                [Detail].[Title] as TaskTitle, 
		                [Detail].[Description] as TaskDescription, 
		                [Detail].[DueDate] as TaskDueDate, 
		                [Detail].[Done] as TaskDone,
		                [Category].[CatId] as CategoryId,
		                [Category].[Name] as CategoryName
	                FROM [dbo].[ToDo]
	                JOIN [dbo].[Detail] ON [ToDo].[TaskId] = [Detail].[TaskId]
	                JOIN [dbo].[Category] ON [ToDo].[CatId] = [Category].[CatId];
                ";
                SqlMapper.SetTypeMap(typeof(ToDoTask),
                    new CustomPropertyTypeMap(typeof(ToDoTask),
                    (type, columnName) =>
                        type.GetProperty(_columnMapTask[columnName])));
                SqlMapper.SetTypeMap(typeof(Category),
                    new CustomPropertyTypeMap(typeof(Category),
                    (type, columnName) =>
                        type.GetProperty(_columnMapCategory[columnName])));

                var result = await SqlMapper.QueryAsync<ToDoTask, Category, ToDoTask>
                    (conn, getAllTasks,
                        MapResults, splitOn: "CategoryId",
                        commandType: Text);

                result.AsList().ForEach(r => r.SetStatus());

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        

        /// <summary>
        /// Get with pagination
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public async Task<TaskList> Get(int pageSize, long pageNumber)
        {
            try
            {
                string getAllTasksWithPagination = @"
                    SET NOCOUNT ON;
	                DECLARE @TempResult TABLE ([TaskId] BIGINT, 
		                [TaskTitle] VARCHAR(200), 
		                [TaskDescription] VARCHAR(500), 
		                [TaskDueDate] DATE, 
		                [TaskDone] BIT, 
		                [CategoryId] BIGINT,
		                [CategoryName] VARCHAR(200));

	                INSERT INTO @TempResult ([TaskId], 
		                [TaskTitle], 
		                [TaskDescription], 
		                [TaskDueDate], 
		                [TaskDone], 
		                [CategoryId],
		                [CategoryName])
	                (
		                SELECT [ToDo].[TaskId] as [TaskId], 
			                [Detail].[Title] as [TaskTitle], 
			                [Detail].[Description] as [TaskDescription], 
			                [Detail].[DueDate] as [TaskDueDate], 
			                [Detail].[Done] as [TaskDone],
			                [Category].[CatId] as [CategoryId],
			                [Category].[Name] as [CategoryName]
		                FROM [dbo].[ToDo]
		                JOIN [dbo].[Detail] ON [ToDo].[TaskId] = [Detail].[TaskId]
		                JOIN [dbo].[Category] ON [ToDo].[CatId] = [Category].[CatId]
	                )

	                SELECT @TotalRowsCount = COUNT(*)
	                FROM @TempResult;

	                SELECT [TaskId], 
		                [TaskTitle], 
		                [TaskDescription], 
		                [TaskDueDate], 
		                [TaskDone], 
		                [CategoryId],
		                [CategoryName]
	                FROM @TempResult
	                ORDER BY [TaskId] 
	                OFFSET @PageSize * (@PageNumber - 1) ROWS
	                FETCH NEXT @PageSize ROWS ONLY;
                ";
                long totalCount;
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@PageSize", pageSize);
                parameters.Add("@PageNumber", pageNumber);
                parameters.Add("@TotalRowsCount",
                    System.Data.DbType.Int64, direction: Output);
                SqlMapper.SetTypeMap(typeof(ToDoTask),
                    new CustomPropertyTypeMap(typeof(ToDoTask),
                    (type, columnName) =>
                        type.GetProperty(_columnMapTask[columnName])));
                SqlMapper.SetTypeMap(typeof(Category),
                    new CustomPropertyTypeMap(typeof(Category),
                    (type, columnName) =>
                        type.GetProperty(_columnMapCategory[columnName])));

                var result = await SqlMapper.QueryAsync<ToDoTask, Category, ToDoTask>(conn,
                    getAllTasksWithPagination, MapResults, parameters,
                        splitOn: "CategoryId",
                    commandType: Text);

                result.AsList().ForEach(r => r.SetStatus());

                totalCount = parameters.Get<Int32>("@TotalRowsCount");
                TaskList tasks = new TaskList();
                tasks.Tasks = result.AsList();
                tasks.TotalCount = totalCount;
                return tasks;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public async Task<bool> Add(ToDoTask task)
        {
            try
            {
                string addTask = @"
                    SET NOCOUNT ON;
	                SET @AddedSuccessfully = 0;
	                BEGIN TRANSACTION; 	
		                DECLARE @TaskId BIGINT;
		                DECLARE @CategoryName VARCHAR(200);
		
		                BEGIN TRY
			                INSERT INTO [dbo].[Detail]([Title], [Description], 
				                [DueDate], [Done])
			                VALUES (@Title, @Description, @DueDate, @Done)
			                SELECT @TaskId = SCOPE_IDENTITY();

			                SELECT @CategoryName = [Name]
			                FROM [dbo].[Category]
			                WHERE [CatId] = @CatId;

			                IF (@CategoryName IS NULL) -- Save task under General (id = 1)
			                BEGIN			
				                INSERT INTO [dbo].[ToDo]([TaskId],[CatId])
				                VALUES (@TaskId, 1);
				                SET @AddedSuccessfully = 1;			
			                END
			                ELSE
			                BEGIN
				                INSERT INTO [dbo].[ToDo]([TaskId],[CatId])
				                VALUES (@TaskId, @CatId);
				                SET @AddedSuccessfully = 1;
			                END
		                END TRY
		                BEGIN CATCH
			                SET @AddedSuccessfully = 0;
			                ROLLBACK;
			                RETURN;
		                END CATCH
	                COMMIT TRANSACTION;
                ";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Title", task.Title);
                parameters.Add("@Description", task.Description);
                if (task.DueDate != null)
                    { parameters.Add("@DueDate", task.DueDate); }
                else
                {
                    parameters.Add("@DueDate", null);
                }
                if (task.Done)
                    parameters.Add("@Done", 1);
                else
                    parameters.Add("@Done", 0);
                parameters.Add("@CatId", task.Category.CategoryId);
                parameters.Add("@AddedSuccessfully", 
                    System.Data.DbType.Binary, direction: Output);
                await SqlMapper.ExecuteAsync(conn, addTask, parameters, 
                    null, null, commandType: Text);
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
        /// <param name="task"></param>
        /// <returns></returns>
        public async Task<bool> Update(long id, ToDoTask task)
        {
            try
            {
                string updateTask = @"
                    SET NOCOUNT ON;
	                SET @UpdatedSuccessfully = 0;
	                BEGIN TRANSACTION;
		                DECLARE @CategoryName VARCHAR(200);
		
		                BEGIN TRY
			                UPDATE [dbo].[Detail]
			                SET [Title] = @Title, 
				                [Description] = @Description, 
				                [DueDate] = @DueDate, 
				                [Done] = @Done
			                WHERE [TaskId] = @TaskId;

			                SELECT @CategoryName = [Name]
			                FROM [dbo].[Category]
			                WHERE [CatId] = @CatId;

			                IF (@CategoryName IS NULL) -- Save task under General (id = 1)
			                BEGIN			
				                UPDATE [dbo].[ToDo]
				                SET [CatId] = 1
				                WHERE [TaskId] = @TaskId;
				                SET @UpdatedSuccessfully = 1;			
			                END
			                ELSE
			                BEGIN
				                UPDATE [dbo].[ToDo]
				                SET [CatId] = @CatId
				                WHERE [TaskId] = @TaskId;
				                SET @UpdatedSuccessfully = 1;
			                END
		                END TRY
		                BEGIN CATCH
			                SET @UpdatedSuccessfully = 0;
			                ROLLBACK;
			                RETURN;
		                END CATCH
	                COMMIT TRANSACTION;
                ";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@TaskId", id);
                parameters.Add("@Title", task.Title);
                parameters.Add("@Description", task.Description);
                if (task.DueDate != null)
                { parameters.Add("@DueDate", task.DueDate); }
                else
                {
                    parameters.Add("@DueDate", null);
                }
                if (task.Done)
                    parameters.Add("@Done", 1);
                else
                    parameters.Add("@Done", 0);
                parameters.Add("@CatId", task.Category.CategoryId);
                parameters.Add("@UpdatedSuccessfully", 
                    System.Data.DbType.Binary, direction: Output);
                await SqlMapper.ExecuteAsync(conn, updateTask, parameters, 
                    null, null, commandType: Text);
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
                string deleteTask = @"
                    SET NOCOUNT ON;
	                SET @DeletedSuccessfully = 0;
	                BEGIN TRANSACTION; 
		                BEGIN TRY
			                DELETE FROM [dbo].[ToDo]
			                WHERE [TaskId] = @TaskId;
		
			                DELETE FROM [dbo].[Detail]
			                WHERE [TaskId] = @TaskId;

			                SET @DeletedSuccessfully = 1;
		                END TRY
		                BEGIN CATCH
			                SET @DeletedSuccessfully = 0;
			                ROLLBACK;
			                RETURN;
		                END CATCH  
	                COMMIT TRANSACTION;
                ";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@TaskId", id);
                parameters.Add("@DeletedSuccessfully",
                    System.Data.DbType.Binary, direction: Output);
                await SqlMapper.ExecuteAsync(conn, deleteTask, parameters,
                    null, null, commandType: Text);
                return parameters.Get<Int32>("@DeletedSuccessfully") == 1 ?
                    true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Map task and category columns
        /// </summary>
        /// <param name="task"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        private ToDoTask MapResults(ToDoTask task, Category category)
        {
            task.Category = category;
            return task;
        }
    }
}
