using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames
{
    public enum PlayMode
    {
        Normal, War
    }

    public enum WinType
    {
        EmptyHand, WarWin, TimeBased
    }

    public class CardGameWar
    {
        public Players Player1 { get; set; }
        public Players Player2 { get; set; }
        public Decks Deck { get; set; }
        public Tables Table { get; set; }
        public PlayMode Mode { get; set; }
        public PlayMode ModePrevious { get; set; }
        public string Winner { get; set; }
        public int WarCount { get; set; }
        public int RoundCount { get; set; }
        public WinType WinType { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan GameDuration { get; set; }

        public Random RndNumber = new Random();

        public CardGameWar(string player1Name, string player2Name, WinType winType = WinType.EmptyHand)
        {
            this.Player1 = new Players(player1Name);
            this.Player2 = new Players(player2Name);
            this.Deck = new Decks();
            this.Table = new Tables();
            this.Winner = string.Empty;
            this.Mode = PlayMode.Normal;
            this.ModePrevious = PlayMode.Normal;
            this.WarCount = 0;
            this.RoundCount = 0;
            this.WinType = winType;
        }

        /// <summary>
        /// Shuffles the deck.
        /// </summary>
        public void ShuffleDeck()
        {
            this.Deck.Shuffle(RndNumber);
        }

        /// <summary>
        /// Deals all cards in the deck between players based on the deal method.
        /// </summary>
        /// <param name="dealMethod"></param>
        /// <param name="playersCount"></param>
        public void DealAllCards(DealMethod dealMethod, int playersCount)
        {
            // Deal cards 
            // Hard-coded for 2 players at this time
            var cardDictionary = this.Deck.DealAll(dealMethod, playersCount);

            this.Player1.UpdatePlayerHand(cardDictionary[1]);
            this.Player2.UpdatePlayerHand(cardDictionary[2]);
        }

        /// <summary>
        /// Shuffles the deck first; then deals all cards in the deck between players based on the deal method.
        /// </summary>
        /// <param name="dealMethod"></param>
        /// <param name="playersCount"></param>
        public void ShuffleAndDeal(DealMethod dealMethod, int playersCount)
        {
            this.ShuffleDeck();
            this.DealAllCards(dealMethod, playersCount);
        }

        /// <summary>
        /// Based on the play mode, each player selects his cards and adds them to the table for competition. It adds "PlayCount" counter by one.
        /// </summary>
        public void PlayRound()
        {
            this.RoundCount ++;
            this.ClearPreviousRoundCards();
            int noCardsToPlay = (this.Mode == PlayMode.War) ? 4 : 1; 

            this.Player1.PlayCards(noCardsToPlay, RndNumber);
            this.Player2.PlayCards(noCardsToPlay, RndNumber);

            // there might be situations that played cards counts are not equal at war.
            // I allow to continue the game. 
            // but it may not be fair. e.g. Player 1 places 2 cards but Player 2 places 4 cards
            this.Table.Add(Player1.PlayedCards, Player2.PlayedCards);
        }

        /// <summary>
        /// Clears the table and played cards if was is not declared. 
        /// It should be used before playing a new round.
        /// </summary>
        private void ClearPreviousRoundCards()
        {
            if (this.Mode == PlayMode.Normal)
            {
                this.Player1.PlayedCards.Clear();
                this.Player1.PlayedCards.Clear();
                this.Table.ClearPile();
            }            
        }

        /// <summary>
        /// Compares the competitors on the table, and determins the Play Mode.
        /// It updates the winner.
        /// </summary>
        public void DecideRoundWinner()
        {
            // if mode becomes war from Normal, add counter
            // if mode becomes Normal from War, add Wins in Player

            this.ModePrevious = this.Mode;

            if (this.Table.Competitor1.Value > this.Table.Competitor2.Value)
            {
                this.Mode = PlayMode.Normal;
                this.UpdateWinner(1);
            }
            else if (this.Table.Competitor1.Value < this.Table.Competitor2.Value)
            {
                this.Mode = PlayMode.Normal;
                this.UpdateWinner(2);
            }
            else
            {
                this.WarCount++;
                this.Mode = PlayMode.War;
            }
        }

        /// <summary>
        /// Updates the winner stock and counts; clears the table afterwards.
        /// </summary>
        /// <param name="noWinner"></param>
        private void UpdateWinner(int noWinner)
        {
            if (noWinner == 1)
            {
                this.Player1.UpdatePlayerStock(this.Table.Pile);        // update the winner stock + update the winner counts
                this.Player1.RoundWins++;                               // update the total wins count for the winner
                this.Player1.WarWins += (this.WinWar()) ? 1 : 0;        // increase wins count of the winner if it wins war
                this.Winner = this.Player1.Name;
            }
            else
            {
                this.Player2.UpdatePlayerStock(this.Table.Pile);
                this.Player2.RoundWins++;
                this.Player2.WarWins += (this.WinWar()) ? 1 : 0;
                this.Winner = this.Player2.Name;
            }                                         
        }

        /// <summary>
        /// Returns True if the player wins war.
        /// </summary>
        /// <returns></returns>
        private bool WinWar()
        {
            if (this.Mode == PlayMode.Normal && this.ModePrevious == PlayMode.War) return true;
            else return false;
        }

        /// <summary>
        /// Returns false if the game end condition satisfies.
        /// The end condition is based on the property "WinKind".
        /// </summary>
        /// <returns></returns>
        public bool ContinueGame()
        {
            bool continueFlag = true;

            switch (this.WinType)
            {
                case WinType.EmptyHand:
                    continueFlag = (this.Player1.NumberOfCards == 0 || this.Player2.NumberOfCards == 0) ? false : true;
                    break;
                case WinType.WarWin:
                    continueFlag = (this.Player1.WarWins >= 5 || this.Player2.WarWins >= 5) ? false : true;
                    break;
                case WinType.TimeBased:
                    // not implemented yet
                    break;
                default:
                    break;
            }
            return continueFlag;
        }

        /// <summary>
        /// Updates the final winner of the game.
        /// </summary>
        public void FinalWinner()
        {
            switch (this.WinType)
            {
                case WinType.EmptyHand:
                    if (this.Player1.NumberOfCards == 0)
                        this.Winner = this.Player2.Name;
                    else
                        this.Winner = this.Player1.Name;
                    break;
                case WinType.WarWin:
                    if (this.Player1.WarWins >= 5)
                        this.Winner = this.Player1.Name;
                    else
                        this.Winner = this.Player2.Name;
                    break;
                case WinType.TimeBased:
                    // not implemented yet
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Sets the start time of the game.
        /// </summary>
        public void SetStartTime()
        {
            this.StartTime = DateTime.Now;
        }

        /// <summary>
        /// Sets the end time of the game and finds the game duration.
        /// </summary>
        public void SetEndTime()
        {
            this.EndTime = DateTime.Now;
            this.GameDuration = this.EndTime.Subtract(this.StartTime);
        }
    }
}
