using System;
using System.Collections.Generic;

public class Token
{   
    public  int Vida  { get; set;} = 3;
    public string Symbol { get; set; } // Símbolo del token
    public int PositionX { get; set; }  // Posición X del token en el laberinto
    public int PositionY { get; set; }  // Posición Y del token en el laberinto
    public List<string> Habilidades { get; set; }
    public bool AtraviesaParedes { get; private  set; } = false;


    public Token(string symbol, int x, int y)
    {
        Symbol = symbol;
        PositionX = x;
        PositionY = y;
        Habilidades = new List<string>();
       
        
    }



    public void Move(int deltaX, int deltaY)
    {
        PositionX += deltaX; // Actualiza la posición X
        PositionY += deltaY; // Actualiza la posición Y
    }
   public void ActivarAtraviesaParedes(bool activar)
   {
       AtraviesaParedes = activar;
   }
   
}

public class Ficha
  {
    public int PosicionW { get; set; }
    public int Posicionz { get; set; }
    public string Simbolo { get; set; }
    public List<string> Habilidades { get; set;}
    public int Vida { get; set; }

    public Ficha( string símbolo,int w, int z)
    {
        PosicionW = w;
        Posicionz = z;
        Vida = 5;
        Habilidades = new List<string>();
        Simbolo = símbolo;
    }

    public void AddAbility(string habilidad)
    {
        Habilidades.Add(habilidad);
    }

    public void Mover(int deltaW, int deltaZ)
      {
         PosicionW += deltaW;
         Posicionz += deltaZ;
      }
  }
