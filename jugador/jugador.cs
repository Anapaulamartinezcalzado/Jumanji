

public class Jugador
{
   private int jugadorW;
   private int jugadorZ;
    private int jugadorX; // Posición X del jugador
    private int jugadorY; // Posición Y del jugador
    
    
    

    public Jugador( int x, int y, int w, int z)
    {
        jugadorX = x;
        jugadorY = y;
         jugadorW = w;
        jugadorZ = z;
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

     public int PosicionW
    {
        get { return jugadorW;}
        set { jugadorW = value;}
    }

    public int Posicionz
    {
        get { return jugadorZ;}
        set { jugadorZ = value;} 
    }

    // Método para mover al jugador
    public void Mover(int deltaX, int deltaY, int deltaW, int deltaZ)
    {
        jugadorX += deltaX;
        jugadorY += deltaY;
        jugadorW += deltaW;
        jugadorZ += deltaZ;
    }
}