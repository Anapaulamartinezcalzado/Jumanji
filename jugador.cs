public class Jugador
{
   // public string Nombre { get; }  Nombre del jugador
    private int jugadorX; // Posición X del jugador
    private int jugadorY; // Posición Y del jugador
    public int Vida {get; set;}
    public Posicion posicion {get; set;}
    

    public Jugador(string nombre, int x, int y, int vida)
    {
       // Nombre = nombre;
        jugadorX = x;
        jugadorY = y;
        Vida = vida;
    }

    // Propiedades para acceder a las coordenadas del jugador
    public int PosicionX
    {
        get { return jugadorX; }
        set { jugadorX = value; }
    }

    public int PosicionY
    {
        get { return jugadorY; }
        set { jugadorY = value; }
    }

    // Método para mover al jugador
    public void Mover(int deltaX, int deltaY)
    {
        jugadorX += deltaX;
        jugadorY += deltaY;
    }
}
