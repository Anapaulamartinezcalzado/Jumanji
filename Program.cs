public class Program
{
    public static void Main()
    {
        Laberinto laberinto = new Laberinto(21, 25);
       
        // Crear el token inicial
        Token token = new Token("A", 5, 5);
        token.AddAbility("Destruir paredes");

        // Colocar el token en el laberinto
        laberinto.PlaceToken(token);
        string[] ficha = { "Sol", "Carita Feliz", "Rombo", "Música", "Infinity" };
        string[] simbolo = { "@", "#", "$", "&", "*" };

        Console.WriteLine("Elija un símbolo para su jugador");

        // Mostrar opciones de símbolos
        for (int i = 0; i < ficha.Length; i++)
        {
            Console.WriteLine($"{ficha[i]}: {simbolo[i]}");
        }

        string eleccion;
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

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string validación = laberinto.PosicionValida(nuevaX, nuevaY); //validar la posición
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            if (validación != null)
            {
                Console.WriteLine(validación); // Mostrar el mensaje de validación
                laberinto.RemoveToken(token);
                laberinto.MarcarMovimientoInvalido(nuevaX, nuevaY, validación[0]);
                laberinto.MostrarLaberinto();
                System.Threading.Thread.Sleep(200);

                laberinto.MarcarMovimientoInvalido(nuevaX, nuevaY, ' ');
                laberinto.PlaceToken(token);
            }
            else
            {
                // Actualizar la posición del token
                laberinto.RemoveToken(token); // Remover el token de la posición anterior
                token.Move(deltaX, deltaY);   // Mover el token
                laberinto.PlaceToken(token);
                laberinto.ActualizarPosicion(nuevaX, nuevaY, new char[] { token.Symbol[0] }); // Utilizar el método para actualizar la posición
                laberinto.MostrarLaberinto();  // Mostrar el laberinto actualizado
                System.Threading.Thread.Sleep(200);
            }
        }
    }
}
