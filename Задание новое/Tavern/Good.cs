using System;

namespace Tavern
{
    public class Good
    {
        string Name { get; set; }
        int days, daysChange = 1, multiplier = 2;
        float price, priceChange;

        public Good(string name, int days, float price)
        {
            Days = days;
            Name = name;
            Price = price;
        }

        public float Price
        {
            get
            {
                return price;
            }
            set
            {
                if (value >= 0F && value <= 50F)
                {
                    price = Convert.ToSingle(Math.Round(value, 2));
                }
                else
                    throw new ArgumentException("Такой цены товара быть не может!");
            }
        }

        public int Days
        {
            get
            {
                return days;
            }
            set
            {
                if (value >= 0)
                    days = value;
                else
                    throw new ArgumentException("новый товар не может быть просроченным");
            }
        }

        public void UpdateGood() // если время пришло то просто будет запускаться этот метод, проверка на время будет делать извне
        {
            switch (Name)
            {
                case "Коньяк": priceChange = -1; break;
                case "Меч короля Артура": break;
                case "Билет на концерт":  PriceChangeTicket(); days = days - daysChange; break;
                default:
                    if (days == 0) Price = Price / multiplier;
                    else priceChange = 1;
                    days = days - daysChange; break;
            }
            Price = Price - priceChange;
        }

        void PriceChangeTicket()
        {
            if (days > 10) priceChange = -1;
            else if (days <= 10 && days > 5) priceChange = -2;
            else if (days <= 5 && days > 0) priceChange = -3;
            else if (days == 0) priceChange = Price;
        }
    }
}






