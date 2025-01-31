using System;


public class Posicion
{
    public int X {get; set;}
    public int Y {get; set;}

   public Posicion(int x, int y)
   {
    X = x;
    Y = y;
   }
}

public class Trampa 
{ 
     public Posicion posicion {get; set;}
    public bool EstaActiva {get; set;}
     public int Daño {get; set;}

     public Trampa(int x, int y, int daño)
     {    
        posicion = new Posicion(x,y);
        EstaActiva = true;
        Daño = daño;
     }

     public void Activar()
     {
        EstaActiva = true;
     }

     public void Desactivar()
     {
        EstaActiva = false;
     }
}