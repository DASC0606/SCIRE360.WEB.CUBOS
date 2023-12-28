using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad.EntityApi
{
    public class TblDocsDigitalRutaEA
    {
        public long Id { get; set; }
        public string Ruc { get; set; }
        public string CompaniaId { get; set; }
        public string PlanillaId { get; set; }
        public string PeriodoId { get; set; }
        public string EjercicioId { get; set; }
        public string PersonalId { get; set; }
        public string Directory { get; set; }
        public string Filename { get; set; }
        public string Tipo { get; set; }
        public string ProcesoId { get; set; }
        public DateTime Fechainsert { get; set; }
        public string UsuScire3 { get; set; }
        public DateTime? Fechamodif { get; set; }
        public string UsuScire3Mod { get; set; }
    }
}
