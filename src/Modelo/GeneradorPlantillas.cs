using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenU4y5
{
    public class GeneradorPlantillas
    {
        private static GeneradorPlantillas _instancia;

        private GeneradorPlantillas() { }

        public static GeneradorPlantillas Instancia
        {
            get
            {
                if (_instancia == null)
                    _instancia = new GeneradorPlantillas();
                return _instancia;
            }
        }

        public IComponenteDocumento CrearBaseCV() => new DocumentoBase("CV");
        public IComponenteDocumento CrearBaseFactura() => new DocumentoBase("Factura Fiscal");
        public IComponenteDocumento CrearBaseFolleto() => new DocumentoBase("Folleto Informativo");
        public IComponenteDocumento CrearBaseCertificado() => new DocumentoBase("Certificado de Reconocimiento");
    }
}