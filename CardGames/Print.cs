using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames
{
    public static class Print
    {
        /// <summary>
        /// Returns a header in header 2 format.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string PrintHeader(string value)
        {
            return string.Format("<h2>{0}</h2>", value);
        }

        /// <summary>
        /// Returns all players' hands.
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        public static string PrintAllPlayersHand(CardGameWar game)
        {
            return string.Format("{0} <br/><br/> {1}",
                PrintPlayerHand(game.Player1), PrintPlayerHand(game.Player2));
        }

        private static string PrintPlayerHand(Players player)
        {
            var p1 = string.Format("<h4>{0}'s hand:</h4><br/>{1}",
                player.Name, FormatGroupOfCards(player.PlayerHand.Hand));

            var p2 = PrintHansStatistics(player);

            return p1 + "<br/>" + p2; 
        }

        /// <summary>
        /// Returns the entire deck.
        /// </summary>
        /// <param name="deck"></param>
        /// <returns></returns>
        public static string PrintDeck(Decks deck)
        {
            return FormatGroupOfCards(deck.DeckOfCards);
        }

        private static string FormatGroupOfCards(List<Card> cardList)
        {
            var result = "";

            foreach (var card in cardList)
            {
                result += string.Format("{0}<br/>", FormatCard(card));
            }
            return result;
        }

        private static string FormatCard(Card card)
        {
            return string.Format("{0} of {1}", card.Letter, card.Suit);
        }

        /// <summary>
        /// Returns battle cards, bounty, and the round winner. It returns the status of players.
        /// </summary>
        /// <param name="warGame"></param>
        /// <returns></returns>
        public static string PrintRoundResult(CardGameWar warGame)
        {
            string result = string.Empty;

            result += PrintHeader(string.Format("Round {0}:", warGame.RoundCount));
            result += PrintBattle(warGame.Table) + "<br/>";
            result += PrintBounty(warGame.Table) + "<br/>";
            result += PrintRoundWinner(warGame) + "<br/>";
            result += PrintAllPlayersStatus(warGame);

            return result;
        }

        private static string PrintBattle(Tables table)
        {
            return string.Format("Battle Cards: {0} <---> {1}", 
                FormatCard(table.Competitor1), FormatCard(table.Competitor2));
        }

        private static string PrintBounty(Tables table)
        {
            return string.Format("Bounty...<br/>{0}", FormatGroupOfCards(table.Pile));
        }

        private static string PrintRoundWinner(CardGameWar game)
        {
            if (game.Mode == PlayMode.Normal)
                return string.Format("<b>{0} wins!</b>", game.Winner);
            else
                return string.Format("==========> <b>WAR</b> <==========");
        }

        private static string PrintAllPlayersStatus(CardGameWar game)
        {
            return string.Format("{0}: {1}<br/>{2}: {3}",
                game.Player1.Name, PrintPlayerStatus(game.Player1), 
                game.Player2.Name, PrintPlayerStatus(game.Player2));
        }

        private static object PrintPlayerStatus(Players player)
        {
            return string.Format("{0} card(s) in hand - {1} card(s) in total - {2} war win(s) so far.", 
                player.NumberOfCardsHand, player.NumberOfCards, player.WarWins);
        }

        /// <summary>
        /// Returns the final winner along with some game statistics.
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        public static string PrintFinalWinner(CardGameWar game)
        {
            string result = string.Format("<h2> Final winner is {0}.</h2>" + 
                "<br/>Game duration: {1:N2} milliseconds." +
                "<br/>Number of rounds: {2}" +
                "<br/>Number of wars: {3}", game.Winner, game.GameDuration.TotalMilliseconds ,game.RoundCount, game.WarCount);

            return result;
        }    
        
        public static string PrintHansStatistics(Players player)
        {
            var stat = player.HandStat();
            return $"{Suits.Clubs}: {stat[Suits.Clubs]}, " +
                   $"{Suits.Diamonds}: {stat[Suits.Diamonds]}, " +
                   $"{Suits.Hearts}: {stat[Suits.Hearts]}, " +
                   $"{Suits.Spades}: {stat[Suits.Spades]}, ";
        }    
    }
}
