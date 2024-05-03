
using System.Collections.Generic;
using System.Linq;

public class GoForward : BoardRule
{
    private Dictionary<int, int> goForwardTiles;

    public GoForward(Dictionary<int, int> goForwardTiles)
    {
        //TAREA: RECIBIR EL DICCIONARO POR PARAMETRO ACA
        this.goForwardTiles = goForwardTiles;
    }

    public override bool IsCompatible(int posicionJugador)
    {
        return goForwardTiles.ContainsKey(posicionJugador);
    }

    public override BoardRuleResult Act(int idJugador, int posicionJugador)
    {
        int nuevaPos = goForwardTiles[posicionJugador];
        return new BoardRuleResult(nuevaPos, false, false, ". You get to jump to tile " + nuevaPos);
    }
}
