using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames
{
    public static class Utility
    {
        /// <summary>
        /// Shuffle the list of cards.
        /// </summary>
        /// <param name="cardList"></param>
        /// <param name="rnd"></param>
        /// <returns></returns>
        public static List<Card> Shuffle(List<Card> cardList, Random rnd)
        {
            int count = cardList.Count;

            while (count > 1)
            {
                int rndIndex = rnd.Next(count--);
                var temp = cardList[rndIndex];
                cardList[rndIndex] = cardList[count];
                cardList[count] = temp;
            }
            return cardList;
        }

        /// <summary>
        /// Adds all element in the second list to the end of the fist list.
        /// </summary>
        /// <param name="cardList1"></param>
        /// <param name="cardList2"></param>
        /// <returns></returns>
        public static List<Card> AddWholeList(List<Card> cardList1, List<Card> cardList2)
        {
            foreach (var card in cardList2)
            {
                cardList1.Add(card);
            }

            return cardList1;
        }
    }
}
