using FrameWorkRHP_Mono.Core.Models.Custom;
using FrameWorkRHP_Mono.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace FrameWorkRHP_Mono.Infrastructure.Repository
{
    public class GenericDataTables : IGenericDataTables
    {
        protected readonly AppsDbContext _context; 
        public GenericDataTables(AppsDbContext context)
        {
            _context = context; 
        }
        public async Task<cstmResultModelDataTable> getWithDataTable<ModelParam>(string ParamQuery, int ParamDraw)
        { 
            var result = new cstmResultModelDataTable();
            var listData = new List<ModelParam>();
            double doubleTotalData = 0;

            try
            {
                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = ParamQuery;
                    command.CommandType = CommandType.Text;

                    _context.Database.OpenConnection();

                    using (var reader = command.ExecuteReader())
                    {
                       
                        while (reader.Read())
                        {
                            ModelParam objModel = default(ModelParam);
                            objModel = Activator.CreateInstance<ModelParam>();
                            foreach (PropertyInfo prop in objModel.GetType().GetProperties())
                            {
                                if (!Attribute.IsDefined(prop, typeof(NotMappedAttribute)))
                                { 
                                    if (!object.Equals(reader[prop.Name], DBNull.Value))
                                    { 
                                        doubleTotalData = prop.Name.ToUpper() == "TOTALDATA" ? Convert.ToDouble(reader[prop.Name]) : doubleTotalData;
                                        prop.SetValue(objModel, reader[prop.Name], null);
                                    }
                                }
                            }
                            listData.Add(objModel);
                        } 
                    }
                }

                if (listData.Count > 0)
                {
                    result.data = listData;
                    result.draw = ParamDraw;
                    result.recordsTotal = doubleTotalData;
                    result.recordsFiltered = doubleTotalData;
                     
                }
                else
                {
                    result.data = listData;
                    result.draw = ParamDraw;
                    result.recordsTotal = 0;
                    result.recordsFiltered = 0;
                }
                return result;   
            }
            catch (Exception ex)
            {
                result.errorMessage = ex.Message;
                result.recordsTotal = 0;
                result.recordsFiltered = 0;
                return result;
            }


            //try
            //{
            //    var queryable = _context.Set<T>().FromSqlRaw(ParamQuery); 
            //    var resultExecQuery = await queryable.AsNoTracking().ToListAsync();

            //    if (resultExecQuery.Count > 0)
            //    {
            //        result.data = resultExecQuery;
            //        result.draw = ParamDraw;

            //        Type type = resultExecQuery.GetType();
            //        var xxx = type.GetProperties();
            //        PropertyInfo propertyInfo = type.GetProperty("TOTALDATA");
            //        if (propertyInfo != null)
            //        {
            //            result.recordsTotal = propertyInfo.GetValue(resultExecQuery);
            //            result.recordsFiltered = propertyInfo.GetValue(resultExecQuery);
            //        }

            //    }
            //    else
            //    {
            //        result.data = resultExecQuery;
            //        result.draw = ParamDraw;
            //        result.recordsTotal = 0;
            //        result.recordsFiltered = 0;
            //    }

            //    return result;
            //}
            //catch (Exception ex)
            //{
            //    result.errorMessage = ex.Message;
            //    result.recordsTotal = 0;
            //    result.recordsFiltered = 0;
            //    return result;
            //}


        }
         
    }
}
