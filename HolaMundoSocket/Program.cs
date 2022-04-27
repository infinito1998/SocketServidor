using HolaMundoSocket.Comunicaciones;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HolaMundoSocket
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["puerto"]);
            Console.WriteLine("Iniciando Servidor en Puerto: " + puerto);
            ServerSocket servidor = new ServerSocket(puerto);
            if (servidor.Iniciar())
            {
                Console.WriteLine("Servidor Conectado Correctamente");
                while (true)
                {
                    Console.WriteLine("Esperando Cliente");
                    Socket socketCliente = servidor.ObtenerCLiente();
                    ClienteCon cliente = new ClienteCon(socketCliente);

                    cliente.Escribir("Ingresa Palabra(chao para salir): ");
                    bool terminar = false;
                    while (!terminar)
                    {
                        string respuesta = cliente.Leer();
                        Console.WriteLine("Cliente Dice: {0}", respuesta);

                        if (respuesta == "chao")
                        {
                            cliente.Desconectar();
                            terminar = true;
                        }
                        else
                        {

                            Console.WriteLine("Ingrese mensaje al cliente: ");
                            string escrito = Console.ReadLine().Trim();
                            cliente.Escribir(escrito);
                            if (escrito == "chao")
                            {
                                cliente.Desconectar();
                                terminar = true;
                            }
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Error, el puerto {0} esta en uso ", puerto);
            }
        }
    }
}
