using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlConnection
{
    public sealed class ConnectionScope : IDisposable
    {
        private static string s_connectionString;
        private static string s_providerName;

        [ThreadStatic]
        private static ConnectionManager s_connection;

        public ConnectionScope()
        {
            this.Init(null, null);
        }

        private void Init(string connectionString, string providerName)
        {
            string localConnectionString = connectionString;
            string localProviderName = providerName;

            //如果当前连接不存在则创建一个
            if (s_connection == null)
            {
                s_connection = new ConnectionManager();
            }
            else
            {
                //获取栈顶连接
                ConnectionInfo info = s_connection.GetTopStackInfo();

                if (info != null)
                {

                    //如果当前层级没有指定连接字符串或提供程序,则默认从栈顶取
                    if (string.IsNullOrEmpty(connectionString))
                    {
                        localConnectionString = info.ConnectionString;
                    }
                    if (string.IsNullOrEmpty(providerName))
                    {
                        localProviderName = info.ProviderName;
                    }

                }
            }

            //即没有指定连接字符串,栈顶也没有获取到.则使用默认链接字符串和提供程序
            if (string.IsNullOrEmpty(localConnectionString))
            {
                localConnectionString = s_connectionString;
            }
            if (string.IsNullOrEmpty(localProviderName))
            {
                localProviderName = s_providerName;
            }
            //将当前连接字符串 压入堆栈中
            s_connection.PushTransactionMode(localConnectionString, localProviderName);
        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
