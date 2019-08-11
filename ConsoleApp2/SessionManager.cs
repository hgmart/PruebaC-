using System;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate5;

namespace ConsoleApp2
{
    public class SessionManager : IDisposable
    {
        private ISession _session;
        private ISessionFactory _sessionFactory;

        public bool Available => _sessionFactory != null;
        public ISession Session => (null == this._session || !this._session.IsOpen) ? Iniciar() : _session;

        private static volatile SessionManager SingletonSessionManager;
        private static object mutex = new Object();
        private string ConnectionString => new DatabaseConnectionStringBuilder().Build();
        private SessionManager() { }

        public static SessionManager InstanciaUnica => SingletonSessionManager ?? InstanciarSingleton();

        private static SessionManager InstanciarSingleton()
        {
            lock (mutex)
            {
                return SingletonSessionManager ?? (SingletonSessionManager = new SessionManager());
            }
        }

        private ISession Iniciar()
        {
            return _session = (_sessionFactory ?? Conectar()).OpenSession();
        }

        private ISessionFactory Conectar()
        {
            _sessionFactory = Fluently
                        .Configure()
                        .Database(MsSqlConfiguration.MsSql2012.ConnectionString(ConnectionString))
                        .Mappings(m => m.FluentMappings.AddFromAssemblyOf<AlumnoMap>()) //Mapeo de clases, con solo hacer una referencia a una clase nos mapeara todas las clases
                        .BuildSessionFactory();

            return _sessionFactory;

            //Con esta configuración cualquier modificación en nuestro modelo se aplicará automaticamente en 
            //f.ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(true, true));

            //Esta linea de código la descomentaremos y comentaremos la anterios si queremos resetear toda nuestra base de datos
            //f.ExposeConfiguration(cfg => new SchemaExport(cfg).Create(true,true));
        }

        public void Dispose()
        {
            if (_sessionFactory != null)

                _sessionFactory.Dispose();
        }

        public static void CerrarConexion()
        {
            if ( SingletonSessionManager != null)
                SingletonSessionManager.Dispose();
        }
    }
}
