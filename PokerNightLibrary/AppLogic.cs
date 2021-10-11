using PokerNightLibrary.Enums;
using PokerNightLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerNightLibrary
{
    public static class AppLogic
    {
        public static List<Card> InitializeDeck()
        {
            var CardDeck = new List<Card>();

            foreach (var color in Enum.GetValues(typeof(ColorsEnum)).Cast<ColorsEnum>())
            {
                foreach (var value in Enum.GetValues(typeof(ValuesEnum)).Cast<ValuesEnum>())
                {
                    CardDeck.Add(new Card
                    {
                        Color = color,
                        Value = value
                    });
                }
            }

            return CardDeck;
        }

        private static Dictionary<Enum, int> CountColors(Hand hand)
        {
            var ColorsDic = new Dictionary<Enum, int>();

            foreach(var color in Enum.GetValues(typeof(ColorsEnum)).Cast<ColorsEnum>())
            {
                ColorsDic.Add(color, hand.Cards.Count(f => f.Color == color));
            }

            return ColorsDic;
        }

        public static Dictionary<Enum, int> CountValues(Hand hand)
        {
            var ValuesDic = new Dictionary<Enum, int>();

            foreach (var value in Enum.GetValues(typeof(ValuesEnum)).Cast<ValuesEnum>())
            {
                ValuesDic.Add(value, hand.Cards.Count(f => f.Value == value));
            }

            return ValuesDic;
        }

        private static int PairsCount(Dictionary<Enum, int> hand)
        {
            return hand.Count(f => f.Value == 2);
        }

        private static bool ThreeCheck(Dictionary<Enum, int> hand)
        {
            return hand.Any(f => f.Value == 3);
        }

        private static bool FourCheck(Dictionary<Enum, int> hand)
        {
            return hand.Any(f => f.Value == 4);
        }

        private static bool SameColorCheck(Dictionary<Enum, int> hand)
        {
            return hand.Any(f => f.Value == 5);
        }

        private static bool RoyalCheck(Dictionary<Enum, int> hand)
        {
            if(hand.Last().Value == 1)
            {
                return true;
            }
            return false;
        }

        private static bool OrderExistance(Dictionary<Enum, int> hand)
        {
            var orderCounter = 0;

            foreach (var item in hand)
            {
                if (item.Value == 1)
                {
                    orderCounter++;
                }
                else
                {
                    orderCounter = 0;
                }

                if(orderCounter == 5)
                {
                    return true;
                }
            }
            return false;
        }

        public static string GetHandName(Hand hand)
        {
            var handValuesDic = CountValues(hand);
            var handColorsDic = CountColors(hand);

            if (OrderExistance(handValuesDic))
            {
                if(SameColorCheck(handColorsDic))
                {
                    if (RoyalCheck(handValuesDic))
                    {
                        hand.HandName = HandNamesEnum.RoyalFlush;
                        return "Royal Flush";
                    }
                    else
                    {
                        hand.HandName = HandNamesEnum.StraightFlush;
                        return "Straight Flush";
                    }
                }
                else
                {
                    hand.HandName = HandNamesEnum.Straight;
                    return "Straight";
                }
            }
            else
            {
                if (FourCheck(handValuesDic))
                {
                    hand.HandName = HandNamesEnum.FourOfAKind;
                    return "Four of a Kind";
                }
                if (ThreeCheck(handValuesDic))
                {
                    if (PairsCount(handValuesDic) == 1)
                    {
                        hand.HandName = HandNamesEnum.FullHouse;
                        return "Full House";
                    }
                    else
                    {
                        hand.HandName = HandNamesEnum.ThreeOfAKind;
                        return "Three of a Kind";
                    }
                }
                if (SameColorCheck(handColorsDic))
                {
                    hand.HandName = HandNamesEnum.Flush;
                    return "Flush";
                }
                if (PairsCount(handValuesDic) == 2)
                {
                    hand.HandName = HandNamesEnum.TwoPair;
                    return "Two Pair";
                }
                if (PairsCount(handValuesDic) == 1)
                {
                    hand.HandName = HandNamesEnum.OnePair;
                    return "One Pair";
                }
                hand.HandName = HandNamesEnum.HighCard;
                return "High Card";

            }

        }

    }
}
