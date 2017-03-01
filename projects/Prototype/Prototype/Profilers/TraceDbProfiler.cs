﻿using log4net;
using Oracle.ManagedDataAccess.Client;
using StackExchange.Profiling.Data;
using System;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace Prototype.Profilers
{
    /// <summary>
    /// トレースDBプロファイラ。
    /// </summary>
    public class TraceDbProfiler : IDbProfiler
    {
        private Stopwatch stopwatch;
        private string commandText;
        private string parameters;

        /// <summary>
        /// ログインターフェース。
        /// </summary>
        private readonly ILog logger = LogManager.GetLogger(@"SqlLogger");

        /// <summary>
        /// SQL文のカテゴリ。
        /// </summary>
        public object ExecuteType { get; private set; }

        #region IDbProfiler members

        /// <summary>
        /// アクティブ状態を表す値を取得します。
        /// </summary>
        public bool IsActive => true;

        /// <summary>
        /// コマンドが完了された時に呼ばれます。
        /// </summary>
        /// <param name="profiledDbCommand">SQLステートメントを表すインターフェイス</param>
        /// <param name="executeType">SQL文のカテゴリ</param>
        /// <param name="reader">取得結果セットを読み込むインターフェイス</param>
        public void ExecuteFinish(IDbCommand profiledDbCommand, SqlExecuteType executeType, System.Data.Common.DbDataReader reader)
        {
            this.commandText = profiledDbCommand.CommandText;
            var sb = new StringBuilder();
            foreach (OracleParameter param in profiledDbCommand.Parameters)
            {
                sb.Append($"{param.ParameterName}:{param.Value},");
            }
            this.parameters = "{" + sb.ToString().Trim(',') + "}";

            if (executeType != SqlExecuteType.Reader)
            {
                stopwatch.Stop();
                logger.Debug($"SqlExecute:{commandText}");
                logger.Debug($"SqlParameters:{parameters}");
            }
        }

        /// <summary>
        /// コマンドが開始された時に呼ばれます。
        /// </summary>
        /// <param name="profiledDbCommand">SQLステートメントを表すインターフェイス</param>
        /// <param name="executeType">SQL文のカテゴリ</param>
        public void ExecuteStart(IDbCommand profiledDbCommand, SqlExecuteType executeType)
        {
            stopwatch = Stopwatch.StartNew();
        }

        /// <summary>
        /// エラー発生時に呼ばれます。
        /// </summary>
        /// <param name="profiledDbCommand">SQLステートメントを表すインターフェイス</param>
        /// <param name="sqlExecuteType">SQL文のカテゴリ</param>
        /// <param name="exception">発生した例外</param>
        public void OnError(IDbCommand profiledDbCommand, SqlExecuteType sqlExecuteType, Exception exception)
        {
            logger.Debug($"SqlError:{profiledDbCommand.CommandText}");
            logger.Debug(exception);
        }

        /// <summary>
        /// Readerが完了した時に呼ばれます。
        /// </summary>
        /// <param name="reader">取得結果セットを読み込むインターフェイス</param>
        public void ReaderFinish(IDataReader reader)
        {
            stopwatch.Stop();
            logger.Debug($"SqlExecute:{commandText}");
            logger.Debug($"SqlParameters:{parameters}");
        }

        #endregion IDbProfiler members
    }
}