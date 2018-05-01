using System;
using System.Collections.Generic;

namespace CardGames
{
    public class Players
    {
        public string Name { get; set; }
        public Hands PlayerHand { get; set; }
        public List<Card> PlayedCards { get; set; }
        public int NumberOfCardsHand { get; set; }
        public int NumberOfCardsStock { get; set; }
        public int NumberOfCards { get; set; }
        public int WarWins { get; set; }           // number of wins in a specific game, e.g. in War, number of winning war
        public int RoundWins { get; set; }         // number of wins of a game, e.g. total number of wins in Hokm

        public Players(string name)
        {
            this.Name = name;
            this.WarWins = 0;
            this.RoundWins = 0;
            this.PlayerHand = new Hands();
            this.PlayedCards = new List<Card>();
            this.NumberOfCardsHand = 0;
            this.NumberOfCardsStock = 0;
            this.NumberOfCards = this.NumberOfCardsHand + this.NumberOfCardsStock;
        }

        /// <summary>
        ///  Updates the card counts in the hand, stock and in total.
        /// </summary>
        public void UpdateCardNumbers()
        {
            this.NumberOfCardsHand = this.PlayerHand.Hand.Count;
            this.NumberOfCardsStock = this.PlayerHand.Stock.Count;
            this.NumberOfCards = this.NumberOfCardsHand + this.NumberOfCardsStock;
        }

        /// <summary>
        /// Selects the cards the player plays, and update the property "PlayedCards". 
        /// It updates the player's card counts.
        /// Before selection, it transfers the stock to hand - after shuffling the stock - if the cards in hand are less than the cards to play.
        /// </summary>
        /// <param name="noCards"></param>
        public void PlayCards(int noCardsToPlay, Random rndNumber)
        {
            this.CheckAndFillPlayerHand(noCardsToPlay, rndNumber);
            this.PlayedCards = this.PlayerHand.Select(noCardsToPlay);
            this.UpdateCardNumbers();
        }

        /// <summary>
        /// If the cards in hand are less than the number of cards to play,
        /// it transfers the stock to hand, after shuffling the stock - 
        /// </summary>
        /// <param name="noCardsToPlay"></param>
        /// <param name="rndNumber"></param>
        private void CheckAndFillPlayerHand(int noCardsToPlay, Random rndNumber)
        {
            if (this.PlayerHand.Hand.Count < noCardsToPlay)
                this.GetStockToHand(rndNumber);
        }

        /// <summary>
        /// Shuffles the stock of the player, 
        /// and adds all its cards to the hand. It updates the player's card counts.
        /// </summary>
        /// <param name="rand"></param>
        public void GetStockToHand(Random rand)
        {
            this.PlayerHand.ShuffleStock(rand);
            this.PlayerHand.TransferStockToHand();
            this.UpdateCardNumbers();
        }

        /// <summary>
        /// Adds the cards in the list to the player's hand. 
        /// It updates the player's card counts.
        /// </summary>
        /// <param name="cardList"></param>
        public void UpdatePlayerHand(List<Card> cardList)
        {
            this.PlayerHand.AddToHand(cardList);
            this.UpdateCardNumbers();
        }

        /// <summary>
        /// Adds the cards in the list to the player's stock. 
        /// It updates the player's card counts.
        /// </summary>
        /// <param name="cardList"></param>
        public void UpdatePlayerStock(List<Card> cardList)
        {
            this.PlayerHand.AddToStock(cardList);
            this.UpdateCardNumbers();
        }

        public Dictionary<Suits, int> HandStat()
        {
            var result = new Dictionary<Suits, int>();

            for (int i = 0; i < 4; i++)
            {
                result[(Suits)i] = this.PlayerHand.Hand.FindAll(p => p.Suit == (Suits)i).Count;
            }

            return result;
        }

    }
}
