using PokerNightLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace PokerNightLibrary.Models
{
    public class Hand : IComparable<Hand>
    {
        public List<Card> Cards { get; set; } = new List<Card>();
        public HandNamesEnum HandName { get; set; }

        public int CompareTo([AllowNull] Hand other)
        {
            var compareResult = HandName.CompareTo(other.HandName);

            if ((compareResult == 0) && (HandName != HandNamesEnum.RoyalFlush))
            {
                var player1HighCard = (AppLogic.CountValues(this)).OrderBy(f => f.Value);
                var player2HighCard = (AppLogic.CountValues(other)).OrderBy(f => f.Value);

                return player1HighCard.Last().Key.CompareTo(player2HighCard.Last().Key);
            }
            else
                return compareResult;

        }

        public void DrawHand(List<Card> deck)
        {
            var random = new Random();

            for (int i = 0; i < 5; i++)
            {
                var howManyCardsInDeck = deck.Count;
                var tempIndex = random.Next(0, howManyCardsInDeck);
                Cards.Add(deck[tempIndex]);
                deck.Remove(deck[tempIndex]);
            }
            //for (int i = 8; i < 13; i++)
            //{
            //    Cards.Add(deck[i]);
            //}
        }
    }
}
