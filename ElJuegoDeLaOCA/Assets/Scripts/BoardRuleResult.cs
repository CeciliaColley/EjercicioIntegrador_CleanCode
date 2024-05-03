public class BoardRuleResult
{
    public int newPosition;
    public bool jugador1PierdeTurno;
    public bool jugador2PierdeTurno;
    public string textWhatHappened;

    public BoardRuleResult(int newPosition, bool jugador1PierdeTurno, bool jugador2PierdeTurno, string textWhatHappened)
    {
        this.newPosition = newPosition;
        this.jugador1PierdeTurno = jugador1PierdeTurno;
        this.jugador2PierdeTurno = jugador2PierdeTurno;
        this.textWhatHappened = textWhatHappened;
    }

    public BoardRuleResult(int currentPosition)
    {
        newPosition = currentPosition;
        jugador1PierdeTurno = false;
        jugador2PierdeTurno = false;
        textWhatHappened = ".";
    }
}
