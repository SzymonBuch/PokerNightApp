using PokerNightLibrary;
using PokerNightLibrary.Models;
using System;


namespace PokerNight
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = "";
            while (input != "x")
            {
                Console.Clear();
                var cardDeck = AppLogic.InitializeDeck();

                var playerOne = new Player("Player 1");
                var playerTwo = new Player("Player 2");

                playerOne.PlayerHand.DrawHand(cardDeck);
                playerTwo.PlayerHand.DrawHand(cardDeck);

                ShowHand(playerOne);
                ShowHand(playerTwo);

                ShowHandNames(playerOne, playerTwo);

                ShowWinner(playerOne, playerTwo); 
                input = Console.ReadLine();
            }
        }

        private static void ShowHand(Player player)
        {
            Console.WriteLine($"Player {player.PlayerName} hand is:");
            var counter = 1;
            foreach (var card in player.PlayerHand.Cards)
            {
                Console.WriteLine($"{counter++} card is {card.Value} of {card.Color}");
            }
            Console.WriteLine();
        }

        private static void ShowWinner(Player player1, Player player2)
        {
            Console.WriteLine();
            var winner = player1.PlayerHand.CompareTo(player2.PlayerHand);

            switch (winner)
            {
                case 1:
                    Console.WriteLine($"And the winner is: {player1.PlayerName}");
                    break;
                case -1:
                    Console.WriteLine($"And the winner i: {player2.PlayerName}");
                    break;
                case 0:
                    Console.WriteLine("It's a draw!");
                    break;
            }

        }

        private static void ShowHandNames(Player player1, Player player2)
        {
            Console.WriteLine($"{player1.PlayerName} hand name is: {AppLogic.GetHandName(player1.PlayerHand)}");
            Console.WriteLine($"{player2.PlayerName} hand name is: {AppLogic.GetHandName(player2.PlayerHand)}");
        }
    }
}
