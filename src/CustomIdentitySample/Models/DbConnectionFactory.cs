using System.Data.Common;
using System.Web.Configuration;

namespace CustomIdentitySample.Models
{
    /// <summary>
    /// DB接続のFactory
    /// </summary>
    /// <remarks>
    /// DapperとGlimpse.Adoを想定
    /// </remarks>
    public static class DbConnectionFactory
    {
        public static DbConnection Create(string connectionName)
        {
            var connectionString = WebConfigurationManager.ConnectionStrings[connectionName].ConnectionString;

            // Glimpse.Adoに対応する為に、ファクトリ経由で作る
            //   (直接newするとGlimpse.Adoがフックしてくれないので)
            var factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            var con = factory.CreateConnection();
            con.ConnectionString = connectionString;

            return con;
        }
    }
}