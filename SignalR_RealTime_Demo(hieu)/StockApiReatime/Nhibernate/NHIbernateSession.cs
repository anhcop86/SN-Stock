using NHibernate;
using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StockApiReatime.Nhibernate
{
    public class NHIbernateSession
    {
        private static ISessionFactory _sessionFactory;
        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    var configuration = new Configuration();
                    var configurationPath = HttpContext.Current.Server.MapPath(@"~\Nhibernate\hibernate.cfg.xml");
                    configuration.Configure(configurationPath);
                    var employeeConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Nhibernate\Mappings\IPProxy.hbm.xml");
                    configuration.AddFile(employeeConfigurationFile);
                    ISessionFactory sessionFactory = configuration.BuildSessionFactory();
                    return sessionFactory;

                }
                return _sessionFactory;

            }
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();

        }
    }
}