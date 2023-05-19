using System;
using Activity.DAL;
using Activity.DAL.Entity;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Proxy;
using Minxtu.DAL;

namespace Activity.Biz
{

    public static class Provider
    {
        static readonly WindsorContainer container = new WindsorContainer(new DefaultProxyFactory());
        private static readonly bool isInit;
        static Provider()
        {
            try
            {
                lock (container)
                {
                    if (!isInit)
                    {
                        //container.Register(Component.For<IDataAccessService>().ImplementedBy<DataAccessService>());// Postgres
                       // container.Register(Component.For<IWebuxAffService>().ImplementedBy<WebuxAffService>());//mySQL 
                        container.Register(Component.For<IDataAccessSQLServerService>().ImplementedBy<DataAccessSQLServerService>());//Sql server


                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

       

        public static IDataAccessService  DataAccessService 
        {
           
            get
            {
                return container.Resolve<IDataAccessService>();
            }
        }
        public static IWebuxAffService WebuxAffService
        {

            get
            {
                return container.Resolve<IWebuxAffService>();
            }
        }
        public static IDataAccessSQLServerService DataAccessSQLServerService
        {

            get
            {
                return container.Resolve<IDataAccessSQLServerService>();
            }
        }
    }
}
