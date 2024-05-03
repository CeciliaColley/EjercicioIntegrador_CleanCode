using System;

public class BoardRule
{
    public virtual bool IsCompatible(int posicionJugador)
    {
        throw new NotImplementedException();
    }

    public virtual BoardRuleResult Act(int idJugador, int posicionJugador)
    {
        throw new NotImplementedException();
    }
}
