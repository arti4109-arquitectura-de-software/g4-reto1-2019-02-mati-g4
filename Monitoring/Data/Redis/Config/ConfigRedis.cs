
using Microsoft.Extensions.Configuration;
using System;

namespace Monitoring.Data.Redis.Config
{
    public class ConfigRedis : IConfigRedis
    {
        #region Dependencies

        private readonly IConfiguration _configuration;

        #endregion

        #region Const

        private const string HOST_ATRIBUTE = "RedisConfig:Host";
        private const string PORT_ATRIBUTE = "RedisConfig:Port";
        private const string PASSWORD_ATRIBUTE = "RedisConfig:Password";
        private const string DATABASEID_ATRIBUTE = "RedisConfig:DatabaseId";

        #endregion

        #region Constructor

        public ConfigRedis(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #endregion

        #region IconfigRedis

        public string Host => _configuration.GetSection(HOST_ATRIBUTE)?.Value;

        public int Port => _configuration.GetSection(PORT_ATRIBUTE) == null ? default : Convert.ToInt32(_configuration.GetSection(PORT_ATRIBUTE).Value);

        public string Password => _configuration.GetSection(PASSWORD_ATRIBUTE)?.Value;

        public long DatabaseId => _configuration.GetSection(DATABASEID_ATRIBUTE) == null ? default : Convert.ToInt64(_configuration.GetSection(DATABASEID_ATRIBUTE).Value);
         
        #endregion

    }
}
