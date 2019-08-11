using FluentNHibernate.Mapping;

namespace ConsoleApp2
{
    public class AlumnoMap : ClassMap<Alumno>
    {
        public AlumnoMap()
        {
            Table("Alumno");
            Id(a => a.Id).GeneratedBy.SequenceIdentity();
            Map(a => a.Nombre).CustomSqlType("VARCHAR(400)");
            Map(a => a.Apellido);
        }
    }
}
