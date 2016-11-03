using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardBattle.Models
{
    public class Card : IEquatable<Card>, IComparable<Card>
    {
        public Card(Values value, Suit suit)
        {
            _suit = suit;
            Value = value;
        }

        private readonly Suit _suit;
        public Suit Suit
        {
            get
            {
                return _suit;
            }
        }

        public Values Value { get; private set; }

        public override string ToString()
        {
            return Value + " of " + Suit; ;
        }

        public bool Equals(Card other)
        {
            if(other == null)
            {
                return false;
            }
            return other.Suit == this.Suit && other.Value == this.Value;
        }

        public override bool Equals(Object other)
        {
            return Equals(other as Card);
        }

        public int CompareTo(Card other)
        {
            if (other == null) return 1;
            return (this.Value - other.Value) * 4 + this.Suit - other.Suit;
        }

        public override int GetHashCode()
        {
            //To allow a good repartition on the hash keys, we get the hash values of the card multiplied by two prime numbers. 
            return 3 * Value.GetHashCode() + Suit.GetHashCode();
        }

        public static int Compare(Card first, Card second)
        {
            if(first == second)
            {
                return 0;
            }
            return first.CompareTo(second);
        }

    }
}
