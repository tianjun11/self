using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SqlConnection
{
    internal static class Initializer
    {

        private static bool s_inited = false;

        /// <summary>
        /// 初始化连接字符串,预编译实体
        /// </summary>
        /// <exception cref="InvalidOperationException">多次调用本函数</exception>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="providerName">提供程序名称</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void UnSafeInit(string connectionString, string providerName)
        {
            if (s_inited)
                throw new InvalidOperationException("请不要多次调用UnSafeInit方法!");


            //// 设置默认的连接字符串。
            //ConnectionScope.SetDefaultConnection(connectionString, providerName);

            s_inited = true;

        }
    }
}
