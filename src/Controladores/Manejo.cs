using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenU4y5.Controladores
{
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
                    bool aplicado = Decoradores.AplicarDecoradorEspecifico(contexto, tipo, entrada);

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
    }
}