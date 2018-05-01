using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames
{
    public enum DealMethod
    {
        Even, EveryFive
    }

    public class Decks
    {
        public List<Card> DeckOfCards { get; set; }

        public Decks()
        {
            // Create a list of cards 
            // 52 cards - 4 suits - each suit has 13 cards
            this.DeckOfCards = new List<Card>();

            for (int suit = 0; suit < 4; suit++)
            {
                for (int letter = 0; letter < 13; letter++)
                {
                    var card = new Card((Suits)suit, (Letters)letter);
                    this.DeckOfCards.Add(card);
                }
            }
        }

        /// <summary>
        /// Shuffles the deck.
        /// </summary>
        /// <param name="random"></param>
        public void Shuffle(Random random)
        {
            Utility.Shuffle(this.DeckOfCards, random);
        }

        /// <summary>
        /// Dealing all cards of the deck between players. 
        /// Remember to shuffle the deck first.
        /// </summary>
        /// <param name="method"></param>
        /// <param name="noPlayers"></param>
        /// <returns></returns>
        public Dictionary<int, List<Card>> DealAll(DealMethod method, int noPlayers)
        {
            // Dealing all cards between players based on the given method
  
            Dictionary<int, List<Card>> cardDictionary = new Dictionary<int, List<Card>>();

            switch (method)
            {
                case DealMethod.Even:
                    cardDictionary = DealCardsEven(noPlayers);
                    break;
                case DealMethod.EveryFive:
                    // not implemented yet
                    break;
                default:
                    // assert : not come here
                    break;
            }
            return cardDictionary;
        }

        private Dictionary<int, List<Card>> DealCardsEven(int noPlayers)
        {
            // Deal all cards evenly between players (that are either 2 or 4 playes)
            // You have shuffled cards first
             
            var cardDictionary = new Dictionary<int, List<Card>>();
            var tempList = new List<Card>();

            if (noPlayers == 2 || noPlayers == 4)
            {
                for (int player = 1; player <= noPlayers; player++)
                {
                    for (int i = 0 + player - 1; i < 52; i += noPlayers)
                        tempList.Add(this.DeckOfCards[i]);
                    cardDictionary.Add(player, tempList);
                    tempList = new List<Card>();
                }               
            }
            else
            { 
                // do nothing 
                // add code later - throw Exception
            }
            return cardDictionary;       
        }
    }
}
