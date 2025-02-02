public class Program
{
    public static void Main()
    {
        Laberinto laberinto = new Laberinto(21, 25);
       
        // Crear el token inicial
        Token token = new Token("A", 5, 5);
         Ficha ficha1 = new Ficha("A", 8,8);
          laberinto.AgregarTrampa(2, 3);
          laberinto.AgregarTrampa(5, 6);
          laberinto.AgregarTrampa(2,18);
          laberinto.AgregarTrampa(15,5);


        // Colocar el token en el laberinto
        laberinto.PlaceToken(token);
        laberinto.Lugar(ficha1);

        string[] ficha = { "Martha", "Spencer", "Bethany", "Jeffrey", "Dr Smolder" };
        string[] simbolo = { "@", "#", "$", "&", "*" };

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
            int indiceFicha1 = Array.IndexOf(simbolo,eleccion2);

            if (indiceFicha1 >=0 && indiceFicha1 < ficha.Length)
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

        while (true)
        {
            laberinto.MostrarLaberinto();
            Console.WriteLine($"Posición actual: ({token.PositionX}, {token.PositionY})");
            Console.Write("Ingrese movimiento (W: arriba, S: abajo, A: izquierda, D: derecha): ");
            string movimiento = Console.ReadLine()!.ToUpper();
            
            int deltaX = 0, deltaY = 0;

            switch (movimiento)
            {
                case "W":
                    deltaX--;//arriba
                    break;
                case "S":
                    deltaX++; //abajo
                    break;
                case "A":
                    deltaY--; //izquierda
                    break;
                case "D":
                    deltaY++; //derecha
                    break;
                default:
                    Console.WriteLine("Movimiento inválido");
                    continue; // Volver a pedir movimiento
            }

            // Mover el token y actualizar la posición en el laberinto
            int nuevaX = token.PositionX + deltaX;
            int nuevaY = token.PositionY + deltaY;


            bool esVálida = laberinto.PosicionValida(nuevaX, nuevaY); //validar la posición

            if (esVálida)
            {
                laberinto.RemoveToken(token); 
                token.Move(deltaX, deltaY); 
                laberinto.PlaceToken(token);

                //verificar si alcanzó la victoria
                if (token.PositionX == laberinto.PosicionVictoria.x && token.PositionY == laberinto.PosicionVictoria.y)
                {
                     Console.WriteLine("¡Felicidades! El primer jugador ha ganado.");
                        break;  
                }
                laberinto.MostrarLaberinto();

                //verificar si hay una trampa en la nueva posición
                if (laberinto.HayTrampa(nuevaX, nuevaY))
                {
                    token.Vida--; //reducir la vida
                    Console.WriteLine($"Has caído en una trampa! Vida restante {token.Vida}");
                }
            }
            else
            {
                Console.WriteLine("Movimiento inválido para el primer jugador");
            }


            Console.WriteLine($"Posición actual del segundo jugador: ({ficha1.PosicionW}, {ficha1.Posicionz})");
            Console.Write("Ingrese movimiento del segundo jugador (W: arriba, S: abajo, A: izquierda, D: derecha): ");
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
                default: 
                     Console.WriteLine("Has seleccionado un movimiento incorrecto");
                    continue;   
            }

                //mover esta ficha y actualizar su posición
                 int nuevoW = ficha1.PosicionW + deltaW;
                 int nuevoZ = ficha1.Posicionz + deltaZ;

                 bool esVálido = laberinto.Posicion(nuevoW,nuevoZ);
                 if (esVálido)
                 {
                    
                    laberinto.Remover(ficha1);
                    ficha1.Mover(deltaW, deltaZ);
                    laberinto.Lugar(ficha1);

                     // Verificar si ficha1 ha alcanzado la posición de victoria
                      if (ficha1.PosicionW == laberinto.PosicionVictoria.x && ficha1.Posicionz == laberinto.PosicionVictoria.y)
                         {
                               Console.WriteLine("¡Felicidades! El segundo jugador ha ganado.");
                                break; // Terminar el juego
                          }

        laberinto.MostrarLaberinto();
                    
                    //verificar si hay una trampa en la nueva posición
                    if (laberinto.HayTrampa(nuevoW, nuevoZ))
                    {
                        ficha1.Vida--;
                        Console.WriteLine($"El segundo jugador ha caído en una trampa! Vida restante: {ficha1.Vida}");
                    }
                    laberinto.MostrarLaberinto();
                 }
                 else
                 {
                    Console.WriteLine("Movimiento inválido para el segundo jugador");
                 }
        }
    }
}

