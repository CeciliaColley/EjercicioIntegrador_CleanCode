
using System.Collections.Generic;

public class GoBackward : BoardRule
{
    private Dictionary<int, int> goBackwardTiles;

    public GoBackward(Dictionary<int, int> goBackwardTiles)
    {
        this.goBackwardTiles = goBackwardTiles;
        //TAREA: RECIBIR EL DICCIONARO POR PARAMETRO ACA
    }

    public override bool IsCompatible(int posicionJugador)
    {
        return goBackwardTiles.ContainsKey(posicionJugador);
    }

    public override BoardRuleResult Act(int idJugador, int posicionJugador)
    {
        int nuevaPos = goBackwardTiles[posicionJugador];
        return new BoardRuleResult(nuevaPos, false, false, ". You get bumped back to tile " + nuevaPos);
    }
}