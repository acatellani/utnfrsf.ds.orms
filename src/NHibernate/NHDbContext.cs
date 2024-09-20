using Microsoft.Extensions.Logging;
using NHibernate;
using NHibernate.SqlCommand;

namespace utnfrsf.ds.orms.NHibernateHelper;

public class SqlLoggingInterceptor : EmptyInterceptor
{
    private readonly ILogger<SqlLoggingInterceptor> _logger;

    public SqlLoggingInterceptor(ILogger<SqlLoggingInterceptor> logger)
    {
        _logger = logger;
    }

    public override SqlString OnPrepareStatement(global::NHibernate.SqlCommand.SqlString sql)
    {
        if (_logger != null)
        {
            _logger.LogInformation(sql.ToString());
        }

        return base.OnPrepareStatement(sql);
    }
}