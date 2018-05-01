using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames
{
    public class Tables
    {
        public List<Card> Pile { get; set; }
        public Card Competitor1 { get; set; }
        public Card Competitor2 { get; set; }

        public Tables()
        {
            this.Pile = new List<Card>();
            this.Competitor1 = new Card();
            this.Competitor2 = new Card();
        }
        
        /// <summary>
        /// Add two card lists to the table and flip the last of each group for competition
        /// </summary>
        /// <param name="cardGroup1"></param>
        /// <param name="cardGroup2"></param>
        public void Add(List<Card> cardGroup1, List<Card> cardGroup2)
        {
            // update competitors
            this.Competitor1 = cardGroup1.ElementAt(cardGroup1.Count - 1); // flip the last element - place it for competition
            this.Competitor2 = cardGroup2.ElementAt(cardGroup2.Count - 1); // flip the last element - place it for competition

            // add 2 groups to Pile
            this.Pile = Utility.AddWholeList(this.Pile, cardGroup1);
            this.Pile = Utility.AddWholeList(this.Pile, cardGroup2);
        }

        /// <summary>
        /// Removes all cards from the table.
        /// </summary>
        public void ClearPile()
        {
            this.Pile.Clear();
        }
    }
}
