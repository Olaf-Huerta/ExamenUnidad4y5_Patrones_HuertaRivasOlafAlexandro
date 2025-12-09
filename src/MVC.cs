using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenU4y5
{
    public class Vista
    {
        public void MostrarMenuPrincipal()
        {
            Console.Clear();
            Console.WriteLine("=== SISTEMA DE PLANTILLAS ===");
            Console.WriteLine("1. Crear CV");
            Console.WriteLine("2. Crear Folleto");
            Console.WriteLine("3. Crear Certificado");
            Console.WriteLine("4. Crear Factura");
            Console.WriteLine("5. Salir");
            Console.Write("Seleccione una opción: ");
        }

        public void MostrarMenuPersonalizacion(string titulo, List<string> opciones, List<string> elegidas)
        {
            Console.Clear();
            Console.WriteLine($"--- Personalizando {titulo} ---");
            for (int i = 0; i < opciones.Count; i++)
            {
                string estado = elegidas.Contains((i + 1).ToString()) ? "(Elegido)" : "";
                Console.WriteLine($"{i + 1}. {opciones[i]} {estado}");
            }
            Console.WriteLine($"{opciones.Count + 1}. Finalizar Documento");
            Console.Write("Seleccione opción: ");
        }

        public void MostrarResultado(string nombre, string contenido)
        {
            Console.Clear();
            Console.WriteLine("=== DOCUMENTO GENERADO ===");
            Console.WriteLine($"Tipo: {nombre}");
            Console.WriteLine("--------------------------");
            if (string.IsNullOrEmpty(contenido)) Console.WriteLine("(Sin componentes extra)");
            else Console.Write(contenido);
            Console.WriteLine("--------------------------");
            Console.WriteLine("Presione Enter para volver al menú...");
            Console.ReadLine();
        }

        public void MensajeError(string msg)
        {
            Console.WriteLine(msg);
            Console.ReadKey();
        }
    }
    public class Controlador
    {
        private Vista _vista;

        public Controlador()
        {
            _vista = new Vista();
        }

        public void Iniciar()
        {
            bool salir = false;
            while (!salir)
            {
                _vista.MostrarMenuPrincipal();
                string op = Console.ReadLine();

                switch (op)
                {
                    case "1":
                        GestionarFlujo("CV", GeneradorPlantillas.Instancia.CrearBaseCV(),
                            new List<string> { "Añadir Título", "Añadir Foto", "Añadir Resumen", "Añadir Pie de Página" });
                        break;
                    case "2":
                        GestionarFlujo("Folleto", GeneradorPlantillas.Instancia.CrearBaseFolleto(),
                            new List<string> { "Añadir Título", "Añadir Gráficos Fondo", "Añadir Doble Cara", "Añadir Pie de Página" });
                        break;
                    case "3":
                        GestionarFlujo("Certificado", GeneradorPlantillas.Instancia.CrearBaseCertificado(),
                            new List<string> { "Añadir Título", "Añadir Borde Elegante", "Añadir Espacio Firma", "Añadir Pie de Página" });
                        break;
                    case "4":
                        GestionarFlujo("Factura", GeneradorPlantillas.Instancia.CrearBaseFactura(),
                            new List<string> { "Añadir Datos Fiscales", "Añadir Desglose Impuestos", "Añadir Sello Digital", "Añadir Pie de Página" });
                        break;
                    case "5":
                        salir = true;
                        break;
                    default:
                        _vista.MensajeError("Opción no válida.");
                        break;
                }
            }
        }

        private void GestionarFlujo(string tipo, IComponenteDocumento docBase, List<string> textosOpciones)
        {
            ContextoDocumento contexto = new ContextoDocumento(docBase);
            List<string> elegidas = new List<string>();

            bool salirDelMenu = false;

            while (!salirDelMenu)
            {

                _vista.MostrarMenuPersonalizacion(tipo, textosOpciones, elegidas);

              
                if (!contexto.EnEdicion())
                {
                    Console.WriteLine();
                    Console.WriteLine("\n[AVISO]: El documento está FINALIZADO.");
                    Console.WriteLine("Seleccione la opción 5 nuevamente para imprimir.");
                }

                string entrada = Console.ReadLine();
                int opcionNum;

                if (int.TryParse(entrada, out opcionNum) && opcionNum == textosOpciones.Count + 1)
                {
                    if (contexto.EnEdicion())
                    {
                        contexto.Finalizar();
                        Console.WriteLine("\nEstado cambiado a FINALIZADO.");
                        Console.ReadKey();
                    }
                    else
                    {
                        salirDelMenu = true;
                    }
                }
                else if (elegidas.Contains(entrada))
                {
                    _vista.MensajeError("Opción ya seleccionada. Presiona Enter.");
                }
                else
                {
                    bool aplicado = AplicarDecoradorEspecifico(contexto, tipo, entrada);

                    if (aplicado)
                    {
                        if (contexto.EnEdicion())
                        {
                            elegidas.Add(entrada);
                        }
                        else
                        {
                            Console.WriteLine("(Presione Enter para continuar)");
                            Console.ReadLine();
                        }
                    }
                    else
                    {
                        _vista.MensajeError("Opción no válida.");
                    }
                }
            }

            _vista.MostrarResultado(tipo, contexto.Documento.GenerarDescripcion());
        }

        private bool AplicarDecoradorEspecifico(ContextoDocumento ctx, string tipoDoc, string opcion)
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
