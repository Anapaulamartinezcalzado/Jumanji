using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

public class Laberinto
{
    public char[,] laberinto; 
    public int token;
    private int filas;
    private int columnas;
    public int PosicionW;
    public int Posicionz;
    public List<(int x, int y)> trampas { get; private set; }
    public List<(int w, int z)> trampitas { get; private set;}
    public int Filas => filas; // Propiedad para acceder a filas
    public int Columnas => columnas; // Propiedad para acceder a columnas
     public (int x, int y) PosicionVictoria {get; private set;}


    
    public Laberinto(int filas, int columnas)
    {
        this.filas = filas;
        this.columnas = columnas;
        trampas = new List<(int x, int z)>();
        trampitas = new List<(int w, int z)>();
        laberinto = new char[filas, columnas]; 
        GenerarLaberinto(0, 0);

        PosicionVictoria = (filas -1, columnas -1);
    }

    private void GenerarLaberinto(int x, int y)
    {
        laberinto[x, y] = ' '; // Marca el camino como espacio vacío
        // Direcciones: Arriba, Abajo, Izquierda, Derecha
        int[] dx = { -1, 1, 0, 0 };
        int[] dy = { 0, 0, -1, 1 };
        Random rand = new Random();

        // Mezclar direcciones
        for (int i = 0; i < 4; i++)
        {
            int dir = rand.Next(4);
            int tempX = dx[i];
            int tempY = dy[i];
            dx[i] = dx[dir];
            dy[i] = dy[dir];
            dx[dir] = tempX;
            dy[dir] = tempY;
        }

        // Recursión para crear caminos
        for (int i = 0; i < 4; i++)
        {
            int nuevoX = x + dx[i] * 2;
            int nuevoY = y + dy[i] * 2;
            if (nuevoX >= 0 && nuevoX < filas && nuevoY >= 0 && nuevoY < columnas && laberinto[nuevoX, nuevoY] == 0)
            {
                laberinto[x + dx[i], y + dy[i]] = ' '; // Crea un camino
                GenerarLaberinto(nuevoX, nuevoY);
            }
        }

        laberinto[4,5] = '▓';
        laberinto[9,18] ='▓';
        laberinto[10,5] = '░';
        laberinto[7,7]  = '░';
    }

    public void Mover(int deltaW, int deltaZ)
      {
         PosicionW += deltaW;
         Posicionz += deltaZ;
      }

    public void AgregarTrampa(int x, int y)
    {
        if (x >= 0 && x < filas && y >= 0 && y < columnas)
        {
            trampas.Add((x,y));
            laberinto[x,y] = '▲';
        }
    }

    public void AgregarTrampita(int w, int z)
    {
        if (w >= 0 && w < filas && z >= 0 && z < columnas)
        {
            trampitas.Add((w,z));
        }
    }

    public bool  PosicionValida(int x, int y )
    {
       if (x < 0 || x >= Filas || y < 0 || y >= Columnas) 
       return false;
        
        
       if (laberinto[x,y] == '░' || laberinto[x,y] == '▓')
       {
        return false;
       }
      return laberinto[x,y] != '█' && laberinto[x,y] != '▲';
    }

    public bool HayTrampa(int x, int y)
    {
        return trampas.Contains((x,y));
    }


     public bool Posicion(int w, int z)
     {  

        //verifica límites
       if (w < 0 || w >= Filas || z < 0 ||z >= Columnas)
        return false;
        
        if (laberinto[w,z] == '░' || laberinto[w,z] == '▓')
       {
        return false;
       }
      return laberinto[w,z] != '█' && laberinto[w,z] != '▲';
     }
     

    public void PlaceToken(Token token)
    {
        if (PosicionValida(token.PositionX, token.PositionY))
        {
            laberinto[token.PositionX, token.PositionY] = token.Symbol[0]; // Almacena el símbolo directamente
        }
    }

      public void Lugar(Ficha ficha)
    {
        if (Posicion(ficha.PosicionW, ficha.Posicionz))
       {
        laberinto[ficha.PosicionW, ficha.Posicionz] = ficha.Simbolo[0];
       }
    }


    public void RemoveToken(Token token)
    {
        if (PosicionValida(token.PositionX, token.PositionY))
        {
            laberinto[token.PositionX, token.PositionY] = ' '; // Marca como espacio vacío
        }
    }

     public void Remover(Ficha ficha)
    {
        if (Posicion(ficha.PosicionW, ficha.Posicionz))
        {
            laberinto[ficha.PosicionW, ficha.Posicionz] = ' ';
        }
    }

    public void ActualizarPosicion(int nuevaX, int nuevaY, char[] validación)
    {
        laberinto[nuevaX, nuevaY] = validación[0];
    }
    

    public void MostrarLaberinto()
    {
        for (int i = 0; i < filas; i++)
        {
            for (int j = 0; j < columnas; j++)
            {

                if (i == PosicionVictoria.x && j == PosicionVictoria.y)
                {
                    Console.Write('☺'); //mostrar la victoria
                }
                else
                {
                      Console.Write(laberinto[i,j] == 0 ? '█' : laberinto[i,j]);
                }
                
            }
            Console.WriteLine();
        }
    }

    
}
