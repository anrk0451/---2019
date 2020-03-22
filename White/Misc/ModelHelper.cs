using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace White.Misc
{
	/// <summary>
	/// 对象-实体 转换
	/// </summary>
	class ModelHelper
	{
		public static List<T> TableToEntity<T>(DataTable dt) where T : new()
		{
			List<T> lists = new List<T>();
			if (dt.Rows.Count > 0)
			{
				foreach (DataRow row in dt.Rows)
				{
					lists.Add(SetVal(new T(), row));
				}
			}
			return lists;
		}

		public static T SetVal<T>(T entity, DataRow row) where T : new()
		{
			Type type = typeof(T);
			PropertyInfo[] pi = type.GetProperties();
			foreach (PropertyInfo item in pi)
			{
				if (row[item.Name] != null && row[item.Name] != DBNull.Value)
				{
					if (item.PropertyType.IsGenericType && item.PropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
					{
						Type conversionType = item.PropertyType;
						NullableConverter nullableConverter = new NullableConverter(conversionType);
						conversionType = nullableConverter.UnderlyingType;
						item.SetValue(entity, Convert.ChangeType(row[item.Name], conversionType), null);
					}
					else
					{
						item.SetValue(entity, Convert.ChangeType(row[item.Name], item.PropertyType), null);
					}
				}
			}
			return entity;
		}

		public static DataTable EntityToDataTable<T>(List<T> list) where T : new()
		{
			if (list == null || list.Count == 0)
			{
				return null;
			}

			DataTable dataTable = new DataTable(typeof(T).Name);
			foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
			{
				if (propertyInfo.PropertyType.IsGenericType && propertyInfo.PropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
				{
					Type conversionType = propertyInfo.PropertyType;
					NullableConverter nullableConverter = new NullableConverter(conversionType);
					conversionType = nullableConverter.UnderlyingType;
					dataTable.Columns.Add(new DataColumn(propertyInfo.Name, conversionType));
				}
				else
				{
					dataTable.Columns.Add(new DataColumn(propertyInfo.Name, propertyInfo.PropertyType));
				}
			}

			foreach (T model in list)
			{
				DataRow dataRow = dataTable.NewRow();
				foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
				{
					object value = propertyInfo.GetValue(model, null);
					if (value != null)
					{
						dataRow[propertyInfo.Name] = propertyInfo.GetValue(model, null);
					}
					else
					{
						dataRow[propertyInfo.Name] = DBNull.Value;
					}
				}
				dataTable.Rows.Add(dataRow);
			}
			return dataTable;
		}
	}
}
