using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenU4y5
{
    public interface IComponenteDocumento
    {
        string GenerarDescripcion();
    }

    public class DocumentoBase : IComponenteDocumento
    {
        private string _nombre;
        public DocumentoBase(string nombre) { _nombre = nombre; }

        public string GenerarDescripcion() => $"--- FORMATO BASE: {_nombre} ---\n";
    }

    public abstract class DocumentoDecorador : IComponenteDocumento
    {
        protected IComponenteDocumento _comp;
        public DocumentoDecorador(IComponenteDocumento c) { _comp = c; }
        public virtual string GenerarDescripcion() => _comp.GenerarDescripcion();
    }

    public class TituloDecorator : DocumentoDecorador
    {
        public TituloDecorator(IComponenteDocumento c) : base(c) { }
        public override string GenerarDescripcion() => _comp.GenerarDescripcion() + "- Título Principal\n";
    }
    public class PieDePaginaDecorator : DocumentoDecorador
    {
        public PieDePaginaDecorator(IComponenteDocumento c) : base(c) { }
        public override string GenerarDescripcion() => _comp.GenerarDescripcion() + "- Pie de Página\n";
    }

    public class FotoDecorator : DocumentoDecorador
    {
        public FotoDecorator(IComponenteDocumento c) : base(c) { }
        public override string GenerarDescripcion() => _comp.GenerarDescripcion() + "- Espacio para Foto de Perfil\n";
    }
    public class ResumenDecorator : DocumentoDecorador
    {
        public ResumenDecorator(IComponenteDocumento c) : base(c) { }
        public override string GenerarDescripcion() => _comp.GenerarDescripcion() + "- Sección de Resumen Profesional\n";
    }

    public class DatosFiscalesDecorator : DocumentoDecorador
    {
        public DatosFiscalesDecorator(IComponenteDocumento c) : base(c) { }
        public override string GenerarDescripcion() => _comp.GenerarDescripcion() + "- Encabezado\n";
    }
    public class DesgloseImpuestosDecorator : DocumentoDecorador
    {
        public DesgloseImpuestosDecorator(IComponenteDocumento c) : base(c) { }
        public override string GenerarDescripcion() => _comp.GenerarDescripcion() + "- Desglose de Impuestos\n";
    }
    public class SelloDigitalDecorator : DocumentoDecorador
    {
        public SelloDigitalDecorator(IComponenteDocumento c) : base(c) { }
        public override string GenerarDescripcion() => _comp.GenerarDescripcion() + "- Sello Digital\n";
    }

    public class GraficosDecorator : DocumentoDecorador
    {
        public GraficosDecorator(IComponenteDocumento c) : base(c) { }
        public override string GenerarDescripcion() => _comp.GenerarDescripcion() + "- Gráficos e Imágenes de fondo\n";
    }
    public class DobleCaraDecorator : DocumentoDecorador
    {
        public DobleCaraDecorator(IComponenteDocumento c) : base(c) { }
        public override string GenerarDescripcion() => _comp.GenerarDescripcion() + "- Diseño a Doble Cara\n";
    }

    public class BordeEleganteDecorator : DocumentoDecorador
    {
        public BordeEleganteDecorator(IComponenteDocumento c) : base(c) { }
        public override string GenerarDescripcion() => _comp.GenerarDescripcion() + "- Borde Elegante y Sello de Agua\n";
    }
    public class FirmaAutorizadaDecorator : DocumentoDecorador
    {
        public FirmaAutorizadaDecorator(IComponenteDocumento c) : base(c) { }
        public override string GenerarDescripcion() => _comp.GenerarDescripcion() + "- Espacio para Firma Autorizada\n";
    }
}
