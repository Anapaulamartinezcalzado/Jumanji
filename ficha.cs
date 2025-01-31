using System;
using System.Collections.Generic;

public class Token
{   
    public string Symbol { get; set; } // Símbolo del token
    public int PositionX { get; set; }  // Posición X del token en el laberinto
    public int PositionY { get; set; }  // Posición Y del token en el laberinto
    public int Turnos {get; set;}
    
    public bool EsTurnoActual {get; set;}
    public List<string> Abilities { get; set; } // Lista de habilidades del token
    

    public Token(string symbol, int x, int y)
    {
        Turnos = 0;
        EsTurnoActual = false;
        Symbol = symbol;
        PositionX = x;
        PositionY = y;
        
        Abilities = new List<string>();
    }



    public void Move(int deltaX, int deltaY)
    {
        PositionX += deltaX; // Actualiza la posición X
        PositionY += deltaY; // Actualiza la posición Y
    }

    public void AddAbility(string ability)
    {
        Abilities.Add(ability); // Agrega una habilidad a la lista
    }
}
