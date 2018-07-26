using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlConnection
{
    class ConnectionManager : IDisposable
    {
        private readonly Stack<TransactionStackItem> _transactionModes = new Stack<TransactionStackItem>();

        public ConnectionInfo GetTopStackInfo()
        {
            if (this._transactionModes.Count > 0)
            {
                return this._transactionModes.Peek().Info;
            }

            return null;
        }
        internal void PushTransactionMode(string connectionString, string providerName)
        {
            //参数校验
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException("connectionString");

            if (string.IsNullOrEmpty(providerName))
            {
                throw new ArgumentNullException("providerName");
            }

            //准备新的事务栈成员
            TransactionStackItem stackItem = new TransactionStackItem();

            foreach (TransactionStackItem item in this._transactionModes)
            {
                //查找出栈中相符的数据库请求。
                if (item.Info.ConnectionString == connectionString && item.Info.ProviderName == providerName)
                {
                    stackItem.Info = item.Info;
                    stackItem.EnableTranscation = item.EnableTranscation;
                    stackItem.CanClose = false;
                    break;
                }
            }
            //事务模式-新作用域要求启用事务
            //info==null说明父级不存在开启事务场景,本层级需要打开事务,并可以在本层级关闭.
            if (stackItem.Info == null)
            {
                stackItem.Info = new ConnectionInfo(connectionString, providerName);
                stackItem.CanClose = true;
            }
            stackItem.EnableTranscation = true;

            //压栈操作
            this._transactionModes.Push(stackItem);
        }



        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
