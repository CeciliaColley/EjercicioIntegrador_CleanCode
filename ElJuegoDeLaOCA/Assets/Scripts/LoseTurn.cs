
using System.Linq;

public class LoseTurn : BoardRule
{
    private int[] loseTurnTiles;
    public LoseTurn(int[] loseTurnTiles)
    {
        this.loseTurnTiles = loseTurnTiles;
        //TAREA: RECIBIR EL ARRAY POR PARAMETRO ACA
    }

    public override bool IsCompatible(int posicionJugador)
    {
        return loseTurnTiles.ToList().Contains(posicionJugador);
    }

    public override BoardRuleResult Act(int idJugador, int posicionJugador)
    {
        return new BoardRuleResult(posicionJugador, idJugador == 1, idJugador == 2, ". You've lost your next turn.");
    }
}
