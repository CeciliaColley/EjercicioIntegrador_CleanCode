
using System.Linq;

public class ThrowAgain : BoardRule
{
    private int[] throwAgainTiles;

    public ThrowAgain(int[] throwAgainTiles)
    {
        this.throwAgainTiles = throwAgainTiles;
        //TAREA: RECIBIR EL ARRAY POR PARAMETRO ACA
    }

    public override bool IsCompatible(int posicionJugador)
    {
        return throwAgainTiles.ToList().Contains(posicionJugador);
    }

    public override BoardRuleResult Act(int idJugador, int posicionJugador)
    {
        return new BoardRuleResult(posicionJugador, idJugador == 2, idJugador == 1, ". You get to role again.");
    }
}