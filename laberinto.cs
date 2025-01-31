using System;
using System.Collections.Generic;

public class Laberinto
{
    public char[,] laberinto; 
    private int filas;
    private int columnas;
    public int Filas => filas; // Propiedad para acceder a filas
    public int Columnas => columnas; // Propiedad para acceder a columnas

    public Laberinto(int filas, int columnas)
    {
        this.filas = filas;
        this.columnas = columnas;
        laberinto = new char[filas, columnas]; // Inicializar como un arreglo de cadenas
        GenerarLaberinto(0, 0);
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
    }

    public string  PosicionValida(int x, int y)
    {
       if (x < 0 || x >= Filas || y < 0 || y >= Columnas)
        {
            return "X"; // Movimiento fuera del laberinto
        }
        if (laberinto[x, y] == '█')
        {
            return "X"; // Movimiento hacia una pared
        }
        return null; 
    }

    public void MarcarMovimientoInvalido(int x, int y, char valor)
    {
        if (x >= 0 && x < Filas && y >= 0 && y < Columnas)
        {
            laberinto[x, y] = valor;
        }
    }

    public void PlaceToken(Token token)
    {
        if (token.PositionX >= 0 && token.PositionX < filas && token.PositionY >= 0 && token.PositionY < columnas)
        {
            laberinto[token.PositionX, token.PositionY] = token.Symbol[0]; // Almacena el símbolo directamente
        }
    }

    public void RemoveToken(Token token)
    {
        if (token.PositionX >= 0 && token.PositionX < filas && token.PositionY >= 0 && token.PositionY < columnas)
        {
            laberinto[token.PositionX, token.PositionY] = ' '; // Marca como espacio vacío
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
                Console.Write(laberinto[i,j] == 0 ? '█' : laberinto[i,j]);
            }
            Console.WriteLine();
        }
    }

    
}
