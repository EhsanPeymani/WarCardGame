using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames
{
    public class Hands
    {
        public List<Card> Hand { get; set; }
        public List<Card> Stock { get; set; }

        public Hands()
        {
            Hand = new List<Card>();
            Stock = new List<Card>();
        }

        /// <summary>
        /// Returns a list of cards from the hand by taking them from the beginning of the hand.
        /// If the number of requested cards is larger than the number of cards in hand, it returns all the available cards in hand.
        /// </summary>
        /// <param name="noCards"></param>
        /// <returns></returns>
        public List<Card> Select(int noCardsToSelect)
        {
            var cardList = new List<Card>();
            int maxNumberOfIteration = (this.Hand.Count >= noCardsToSelect) ? noCardsToSelect : this.Hand.Count;

            for (int i = 0; i < maxNumberOfIteration ; i++)
                cardList.Add(this.Hand.ElementAt(i));

            this.RemoveFromHand(cardList);
            return cardList;
        }

        /// <summary>
        /// Adds a list of cards to the player's hand.
        /// </summary>
        /// <param name="cardList"></param>
        public void AddToHand(List<Card> cardList)
        {
            foreach (var card in cardList)
                this.Hand.Add(card);
        }
        
        /// <summary>
        /// Removes a card from the hand.
        /// </summary>
        /// <param name="card"></param>
        public void RemoveFromHand(Card card)
        {
            if (this.Hand.Exists(p => p == card))
            {
                this.Hand.Remove(card);
            }
        }

        /// <summary>
        /// Removes a list of cards from the hand.
        /// </summary>
        /// <param name="cardList"></param>
        public void RemoveFromHand(List<Card> cardList)
        {
            foreach (var card in cardList)
                this.RemoveFromHand(card);
        }

        /// <summary>
        /// Adds one card to the stock
        /// </summary>
        /// <param name="card"></param>
        public void AddToStock(Card card)
        {
            this.Stock.Add(card);
        }

        /// <summary>
        /// Add a list of cards to the stock
        /// </summary>
        /// <param name="cardList"></param>
        public void AddToStock(List<Card> cardList)
        {
            foreach (var card in cardList)
                this.AddToStock(card);
        }

        /// <summary>
        /// Shuffle the stock.
        /// </summary>
        /// <param name="random"></param>
        public void ShuffleStock(Random random)
        {
            Utility.Shuffle(this.Stock, random);
        }

        /// <summary>
        /// Removes all elements form the stock.
        /// </summary>
        public void ClearStock()
        {
            this.Stock.Clear();
        }

        /// <summary>
        /// Adds all cards of the stock to the hand, and clear the stock.
        /// </summary>
        public void TransferStockToHand()
        {
            this.AddToHand(this.Stock);
            this.ClearStock();
        }
    }
}
