



public class ContadorTurno
{
    private List<Token> tokens;
    private int turnoActual;

    public ContadorTurno()
    {
        tokens = new List<Token>();
        turnoActual = 0;
    }

    public void AgregarFicha(Token token)
    {
        tokens.Add(token);
    }

    public void CambiarTurno()
    {
        if (tokens.Count == 0)
        return;
        turnoActual = (turnoActual + 1) % tokens.Count;
        ActualizarEstado();
    }

    private void ActualizarEstado()
    {
        if (tokens.Count == 0)
        return;
       for (int i = 0; i < tokens.Count; i++)
       {
        tokens[i].EsTurnoActual = i == turnoActual;
       }
    }

    public Token ObtenerTokenActual()
    {
        if (tokens.Count == 0)
        return null;
        return tokens[turnoActual];
    }

    public int ObtenerTurnoActual()
    {
        return turnoActual;
    }

    
}


   
