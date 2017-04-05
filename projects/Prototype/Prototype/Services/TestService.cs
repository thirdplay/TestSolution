﻿using log4net;
using Microsoft.Practices.ServiceLocation;
using OfficeOpenXml;
using Prototype.Constants;
using Prototype.DIAnnotations;
using Prototype.Entities;
using Prototype.Models;
using Prototype.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Prototype.Services
{
    /// <summary>
    /// テストサービスのインターフェース。
    /// </summary>
    [Service(typeof(TestService))]
    public interface ITestService
    {
    }

    /// <summary>
    /// テストサービス。
    /// </summary>
    public class TestService : ITestService, IDisposable
    {
        /// <summary>
        /// サービスロケーター。
        /// </summary>
        private readonly IServiceLocator serviceLocator;

        /// <summary>
        /// テストコンポーネント
        /// </summary>
        public ITestComponent TestComponent => this.serviceLocator.GetInstance<ITestComponent>();

        /// <summary>
        /// ログインターフェース。
        /// </summary>
        private readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        public TestService(IServiceLocator serviceLocator)
        {
            this.serviceLocator = serviceLocator;
            this.logger.Debug($"TestService:Constructor");
        }

        #region IDispose members

        /// <summary>
        /// このインスタンスによって使用されているリソースを全て破棄します。
        /// </summary>
        public void Dispose()
        {
            this.logger.Debug("TestService:Dispose");
        }

        #endregion
    }
}