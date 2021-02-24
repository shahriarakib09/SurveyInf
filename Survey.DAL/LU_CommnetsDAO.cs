using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using System.Data.Common;
using DbExecutor;
using SurveyEntity;

namespace SurveyDAL
{
	public class LU_CommnetsDAO : IDisposable
	{
		private static volatile LU_CommnetsDAO instance;
		private static readonly object lockObj = new object();
		public static LU_CommnetsDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new LU_CommnetsDAO();
			}
			return instance;
		}
		public static LU_CommnetsDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new LU_CommnetsDAO();
						}
					}
				}
				return instance;
			}
		}

		public void Dispose()
		{
			((IDisposable)GetInstanceThreadSafe).Dispose();
		}

		DBExecutor dbExecutor;

		public LU_CommnetsDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<LU_Commnets> Get(Int32? id = null)
		{
			try
			{
				List<LU_Commnets> LU_CommnetsLst = new List<LU_Commnets>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramId", id, DbType.Int32, ParameterDirection.Input)
				};
				LU_CommnetsLst = dbExecutor.FetchData<LU_Commnets>(CommandType.StoredProcedure, "wsp_LU_Commnets_Get", colparameters);
				return LU_CommnetsLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<LU_Commnets> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<LU_Commnets> LU_CommnetsLst = new List<LU_Commnets>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				LU_CommnetsLst = dbExecutor.FetchData<LU_Commnets>(CommandType.StoredProcedure, "wsp_LU_Commnets_GetDynamic", colparameters);
				return LU_CommnetsLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public List<LU_Commnets> GetPaged(int startRecordNo, int rowPerPage, string whereClause, string sortColumn, string sortOrder, ref int rows)
		{
			try
			{
				List<LU_Commnets> LU_CommnetsLst = new List<LU_Commnets>();
				Parameters[] colparameters = new Parameters[5]{
				new Parameters("@StartRecordNo", startRecordNo, DbType.Int32, ParameterDirection.Input),
				new Parameters("@RowPerPage", rowPerPage, DbType.Int32, ParameterDirection.Input),
				new Parameters("@WhereClause", whereClause, DbType.String, ParameterDirection.Input),
				new Parameters("@SortColumn", sortColumn, DbType.String, ParameterDirection.Input),
				new Parameters("@SortOrder", sortOrder, DbType.String, ParameterDirection.Input),
				};
				LU_CommnetsLst = dbExecutor.FetchDataRef<LU_Commnets>(CommandType.StoredProcedure, "LU_Commnets_GetPaged", colparameters, ref rows);
				return LU_CommnetsLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(LU_Commnets _LU_Commnets, string transactionType)
		{
			string ret = string.Empty;
			try
			{
				Parameters[] colparameters = new Parameters[9]{
				new Parameters("@paramId", _LU_Commnets.Id, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramCommentsTypeId", _LU_Commnets.CommentsTypeId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramComments", _LU_Commnets.Comments, DbType.String, ParameterDirection.Input),
				new Parameters("@paramCreatorId", _LU_Commnets.CreatorId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramCreationDate", _LU_Commnets.CreationDate, DbType.DateTime, ParameterDirection.Input),
				new Parameters("@paramModifierId", _LU_Commnets.ModifierId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramModificationDate", _LU_Commnets.ModificationDate, DbType.DateTime, ParameterDirection.Input),
				new Parameters("@paramIsActive", _LU_Commnets.IsActive, DbType.Boolean, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_LU_Commnets_Post", colparameters, true);
				dbExecutor.ManageTransaction(TransactionType.Commit);
			}
			catch (DBConcurrencyException except)
			{
				dbExecutor.ManageTransaction(TransactionType.Rollback);
				throw except;
			}
			catch (Exception ex)
			{
				dbExecutor.ManageTransaction(TransactionType.Rollback);
				throw ex;
			}
			return ret;
		}
	}
}
