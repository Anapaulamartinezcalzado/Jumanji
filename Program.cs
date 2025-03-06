using System;
using System.Threading;

public class Program
{
    private static int tiempoPorTurno = 10000; // el tiempo está en milisegundos
    private static bool turnoJugador1 = true; // Indica si es el turno del jugador 1
    private static DateTime inicioTurno; // Tiempo de inicio del turno actual

    public static void Main()
    {
        Laberinto laberinto = new Laberinto(21, 25);

        // Crear el token inicial
        Token token = new Token("A", 5, 5);
        Ficha ficha1 = new Ficha("A", 8, 8);

        // Colocar el token en el laberinto
        laberinto.PlaceToken(token);
        laberinto.Lugar(ficha1);

        string[] ficha = { "Martha", "Spencer", "Bethany", "Jeffrey", "Dr Smolder" };
        string[] simbolo = { "@", "#", "$", "&", "*" };
         Console.WriteLine("BIENVENIDO A JUMANJI.");


        Console.WriteLine("Elija un símbolo para su jugador");

        // Mostrar opciones de símbolos
        for (int i = 0; i < ficha.Length; i++)
        {
            Console.WriteLine($"{ficha[i]}: {simbolo[i]}");
        }

        string eleccion;
        Console.WriteLine("Elija una ficha para un segundo jugador");

        for (int j = 0; j < ficha.Length; j++)
        {
            Console.WriteLine($"{ficha[j]}: {simbolo[j]}");
        }

        string eleccion2;
        bool seleccionValida = false;

        // Bucle para seleccionar un símbolo válido
        while (!seleccionValida)
        {
            eleccion = Console.ReadLine()!;
            int indiceFicha = Array.IndexOf(simbolo, eleccion);

            if (indiceFicha >= 0 && indiceFicha < ficha.Length)
            {
                // Asigna el símbolo elegido al token
                token.Symbol = simbolo[indiceFicha];
                laberinto.PlaceToken(token);
                seleccionValida = true; // Salir del bucle si la selección es válida
            }
            else
            {
                Console.WriteLine("Símbolo no válido. Por favor, elija un símbolo correcto.");
            }

            eleccion2 = Console.ReadLine()!;
            int indiceFicha1 = Array.IndexOf(simbolo, eleccion2);

            if (indiceFicha1 >= 0 && indiceFicha1 < ficha.Length)
            {
                ficha1.Simbolo = simbolo[indiceFicha1];
                laberinto.Lugar(ficha1);
                seleccionValida = true;
            }
            else
            {
                Console.WriteLine("Símbolo no válido. Por favor, elija un símbolo correcto");
            }
        }

        Console.WriteLine("Habilidades disponibles:");
        Console.WriteLine("1. AtraviesaParedes");
        Console.WriteLine("2. InmuneATrampas");
        Console.WriteLine("3. Teletransportación");

        Console.Write("Elija una habilidad para su primer jugador (1-3): ");
        int habilidadElegida = int.Parse(Console.ReadLine()!);

        switch (habilidadElegida)
        {
            case 1:
                token.AddAbility("AtraviesaParedes");
                break;
            case 2:
                token.AddAbility("InmuneATrampas");
                break;
            case 3:
                token.AddAbility("Teletransportación");
                break;
            default:
                Console.WriteLine("Habilidad no válida.");
                break;
        }

        Console.WriteLine("Habilidades disponibles:");
        Console.WriteLine("1. AtraviesaParedes");
        Console.WriteLine("2. InmuneATrampas");
        Console.WriteLine("3. Teletransportación");

        Console.Write("Elija una habilidad para su segundo jugador (1-3): ");
        int habilidadSeleccionada = int.Parse(Console.ReadLine()!);

        switch (habilidadSeleccionada)
        {
            case 1:
                ficha1.AddAbility("AtraviesaParedes");
                break;
            case 2:
                ficha1.AddAbility("InmuneATrampas");
                break;
            case 3:
                ficha1.AddAbility("Teletransportación");
                break;
            default:
                Console.WriteLine("Habilidad no válida.");
                break;
        }

        Console.WriteLine("TENDRÁS ALREDEDOR DE 10 SEGUNDOS PARA PODER MOVER CADA FICHA. PARA INICIAR LA PARTIDA SOLO BASTA CON PRESIONAR UNA LETRA.  ENJOY YOUR PLAY!");

        inicioTurno = DateTime.Now; // Iniciar el temporizador del primer turno

        while (true)
        {
            if (turnoJugador1)
            {
                Console.WriteLine("Turno del jugador 1");
                ProcesarMovimientoJugador1(laberinto, token);

                // Verificar si el tiempo del turno ha expirado
                if ((DateTime.Now - inicioTurno).TotalMilliseconds >= tiempoPorTurno)
                {
                    Console.WriteLine("Tiempo del turno del jugador 1 ha expirado.");
                    turnoJugador1 = false; // Cambiar al turno del jugador 2
                    inicioTurno = DateTime.Now; // Reiniciar el temporizador
                }
            }
            else
            {
                Console.WriteLine("Turno del jugador 2");
                ProcesarMovimientoJugador2(laberinto, ficha1);

                // Verificar si el tiempo del turno ha expirado
                if ((DateTime.Now - inicioTurno).TotalMilliseconds >= tiempoPorTurno)
                {
                    Console.WriteLine("Tiempo del turno del jugador 2 ha expirado.");
                    turnoJugador1 = true; // Cambiar al turno del jugador 1
                    inicioTurno = DateTime.Now; // Reiniciar el temporizador
                }
            }

            laberinto.MostrarLaberinto();
        }
    }

    // Declaración de ProcesarMovimientoJugador1
    private static void ProcesarMovimientoJugador1(Laberinto laberinto, Token token)
    {
        Console.WriteLine($"Posición actual: ({token.PositionX}, {token.PositionY})");
        Console.Write("Ingrese movimiento de su primer jugador (W: arriba, S: abajo, A: izquierda, D: derecha, T: Teletransportación): ");

        string movimiento = Console.ReadLine()!.ToUpper();

        int deltaX = 0, deltaY = 0;

        switch (movimiento)
        {
            case "W":
                deltaX--; // arriba
                break;
            case "S":
                deltaX++; // abajo
                break;
            case "A":
                deltaY--; // izquierda
                break;
            case "D":
                deltaY++; // derecha
                break;
            case "T":
                if (token.HasAbility("Teletransportación"))
                {
                    laberinto.RemoveToken(token);
                    token.Teletransportar(laberinto);
                    laberinto.PlaceToken(token);
                    Console.WriteLine("Teletransportación realizada.");
                    return; // Saltar al siguiente ciclo del bucle
                }
                else
                {
                    Console.WriteLine("No tienes la habilidad de teletransportación. Por favor vuelve a moverte ");
                    return;
                }
            default:
                Console.WriteLine("Movimiento inválido");
                return; // Volver a pedir movimiento
        }

        // Mover el token y actualizar la posición en el laberinto
        int nuevaX = token.PositionX + deltaX;
        int nuevaY = token.PositionY + deltaY;

        bool esVálida = laberinto.PosicionValida(nuevaX, nuevaY); // validar la posición

        if (esVálida)
        {
            laberinto.RemoveToken(token);
            token.Move(deltaX, deltaY);
            laberinto.PlaceToken(token);

            // verificar si alcanzó la victoria
            if (token.PositionX == laberinto.PosicionVictoria.x && token.PositionY == laberinto.PosicionVictoria.y)
            {
                Console.WriteLine("¡Felicidades! El primer jugador ha ganado.");
                Environment.Exit(0); // Terminar el programa
            }

            // verificar si hay una trampa en la nueva posición
            if (laberinto.HayTrampa(nuevaX, nuevaY) && !token.HasAbility("Inmune a Trampas"))
            {
                token.Vida--; // reducir la vida
                Console.WriteLine($"Has caído en una trampa! Vida restante {token.Vida}");

                if (token.Vida <= 0)
                {
                    Console.WriteLine("El primer jugador ha perdido todas sus vidas!");
                    Environment.Exit(0); //terminar el programa
                }
            }
        }
        else
        {
            Console.WriteLine("Movimiento inválido para el primer jugador");
        }
    }

    // Declaración de ProcesarMovimientoJugador2
    private static void ProcesarMovimientoJugador2(Laberinto laberinto, Ficha ficha1)
    {
        Console.WriteLine($"Posición actual del segundo jugador: ({ficha1.PosicionW}, {ficha1.Posicionz})");
        Console.Write("Ingrese movimiento del segundo jugador (W: arriba, S: abajo, A: izquierda, D: derecha , T: Teletransportación): ");
        string movimientos = Console.ReadLine()!.ToUpper();

        int deltaW = 0, deltaZ = 0;

        switch (movimientos)
        {
            case "W":
                deltaW = -1;
                break;
            case "S":
                deltaW = 1;
                break;
            case "A":
                deltaZ = -1;
                break;
            case "D":
                deltaZ = 1;
                break;
            case "T":
                if (ficha1.TieneAbility("Teletransportación"))
                {
                    laberinto.Remover(ficha1);
                    ficha1.Teletransportar(laberinto);
                    laberinto.Lugar(ficha1);
                    Console.WriteLine("Teletransportación realizada.");
                    return;
                }
                else
                {
                    Console.WriteLine("No tienes la habilidad de transportación.Vuelve a moverte");
                    return;
                }
            default:
                Console.WriteLine("Has seleccionado un movimiento incorrecto");
                return;
        }

        // mover esta ficha y actualizar su posición
        int nuevoW = ficha1.PosicionW + deltaW;
        int nuevoZ = ficha1.Posicionz + deltaZ;

        bool esVálido = laberinto.Posicion(nuevoW, nuevoZ);

        if (ficha1.TieneAbility("AtraviesaParedes"))
        {
            esVálido = true; // Ignorar las paredes
        }

        if (esVálido)
        {
            laberinto.Remover(ficha1);
            ficha1.Mover(deltaW, deltaZ);
            laberinto.Lugar(ficha1);

            // Verificar si ficha1 ha alcanzado la posición de victoria
            if (ficha1.PosicionW == laberinto.PosicionVictoria.x && ficha1.Posicionz == laberinto.PosicionVictoria.y)
            {
                Console.WriteLine("¡Felicidades! El segundo jugador ha ganado.");
                Environment.Exit(0); // Terminar el programa
            }

            // verificar si hay una trampa en la nueva posición
            if (laberinto.HayTrampa(nuevoW, nuevoZ) && !ficha1.TieneAbility("Inmune a trampas"))
            {
                ficha1.Vida--;
                Console.WriteLine($"El segundo jugador ha caído en una trampa! Vida restante: {ficha1.Vida}");

                if (ficha1.Vida <= 0)
                {
                    Console.WriteLine("El segundo jugador ha perdido todas sus vidas!");
                    Environment.Exit(0);
                }
            }
        }
        else
        {
            Console.WriteLine("Movimiento inválido para el segundo jugador");
        }
    }
}