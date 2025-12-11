using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenU4y5.Controladores
{
    public static class Decoradores
    {
        public static bool AplicarDecoradorEspecifico(ContextoDocumento ctx, string tipoDoc, string opcion)
        {
            switch (tipoDoc)
            {
                case "CV":
                    switch (opcion)
                    {
                        case "1": ctx.IntentarDecorar(d => new TituloDecorator(d)); return true;
                        case "2": ctx.IntentarDecorar(d => new FotoDecorator(d)); return true;
                        case "3": ctx.IntentarDecorar(d => new ResumenDecorator(d)); return true;
                        case "4": ctx.IntentarDecorar(d => new PieDePaginaDecorator(d)); return true;
                    }
                    break;
                case "Folleto":
                    switch (opcion)
                    {
                        case "1": ctx.IntentarDecorar(d => new TituloDecorator(d)); return true;
                        case "2": ctx.IntentarDecorar(d => new GraficosDecorator(d)); return true;
                        case "3": ctx.IntentarDecorar(d => new DobleCaraDecorator(d)); return true;
                        case "4": ctx.IntentarDecorar(d => new PieDePaginaDecorator(d)); return true;
                    }
                    break;
                case "Certificado":
                    switch (opcion)
                    {
                        case "1": ctx.IntentarDecorar(d => new TituloDecorator(d)); return true;
                        case "2": ctx.IntentarDecorar(d => new BordeEleganteDecorator(d)); return true;
                        case "3": ctx.IntentarDecorar(d => new FirmaAutorizadaDecorator(d)); return true;
                        case "4": ctx.IntentarDecorar(d => new PieDePaginaDecorator(d)); return true;
                    }
                    break;
                case "Factura":
                    switch (opcion)
                    {
                        case "1": ctx.IntentarDecorar(d => new DatosFiscalesDecorator(d)); return true;
                        case "2": ctx.IntentarDecorar(d => new DesgloseImpuestosDecorator(d)); return true;
                        case "3": ctx.IntentarDecorar(d => new SelloDigitalDecorator(d)); return true;
                        case "4": ctx.IntentarDecorar(d => new PieDePaginaDecorator(d)); return true;
                    }
                    break;
            }
            return false;
        }
    }
}