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
            Console.WriteLine("--- SISTEMA DE PLANTILLAS ---");
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
            Console.WriteLine("--- DOCUMENTO GENERADO ---");
            Console.WriteLine($"Tipo: {nombre}");
            Console.WriteLine("--------------------------");

            if (string.IsNullOrEmpty(contenido)) Console.WriteLine("(Sin componentes extra)");
            else Console.Write(contenido);

            Console.WriteLine("--------------------------");
            Console.WriteLine("Presione Enter para volver al menú.");
            Console.ReadLine();
        }

        public void MensajeError(string msg)
        {
            Console.WriteLine(msg);
            Console.ReadKey();
        }
    }
}
