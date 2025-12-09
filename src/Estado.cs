using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenU4y5
{
    public interface IEstadoDocumento
    {
        IComponenteDocumento AplicarDecoracion(IComponenteDocumento doc, Func<IComponenteDocumento, IComponenteDocumento> decorador);
        bool PuedeEditar();
    }

    public class EstadoEdicion : IEstadoDocumento
    {
        public bool PuedeEditar() => true;
        public IComponenteDocumento AplicarDecoracion(IComponenteDocumento doc, Func<IComponenteDocumento, IComponenteDocumento> decorador)
        {
            return decorador(doc);
        }
    }

    public class EstadoFinalizado : IEstadoDocumento
    {
        public bool PuedeEditar() => false;
        public IComponenteDocumento AplicarDecoracion(IComponenteDocumento doc, Func<IComponenteDocumento, IComponenteDocumento> decorador)
        {
            Console.WriteLine("BLOQUEADO: El documento está finalizado. No se pueden hacer cambios.");
            return doc;
        }
    }

    public class ContextoDocumento
    {
        public IComponenteDocumento Documento { get; private set; }
        private IEstadoDocumento _estado;

        public ContextoDocumento(IComponenteDocumento docInicial)
        {
            Documento = docInicial;
            _estado = new EstadoEdicion();
        }

        public void IntentarDecorar(Func<IComponenteDocumento, IComponenteDocumento> logicaDecorador)
        {
            Documento = _estado.AplicarDecoracion(Documento, logicaDecorador);
        }

        public void Finalizar()
        {
            _estado = new EstadoFinalizado();
        }

        public bool EnEdicion() => _estado.PuedeEditar();
    }
}
