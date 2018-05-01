using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames
{
    public enum Suits
    {
        Diamonds, Hearts, Clubs, Spades
    }

    public enum Letters
    {
        Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace
    }

    public class Card
    {
        public Suits Suit { get; set; }
        public Letters Letter { get; set; }
        public int Value { get; set; }

        public Card() { }

        public Card(Suits suit, Letters letter)
        {
            this.Suit = suit;
            this.Letter = letter;
            this.Value = (int) letter;
        }
    }
}
