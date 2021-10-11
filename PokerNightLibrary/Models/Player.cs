namespace PokerNightLibrary.Models
{
    public class Player
    {
        public string PlayerName { get; private set; }
        public Hand PlayerHand { get; set; }

        public Player(string playerName)
        {
            PlayerName = playerName;
            PlayerHand = new Hand();
        }

    }
}
