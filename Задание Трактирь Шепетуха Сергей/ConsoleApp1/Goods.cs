using System;

namespace ConsoleApp1
{
    public class Goods
    {
        public string name;
        public int days;
        float price;

        public Goods(string name,int days,float price)
        {
            this.days = days;
            this.name = name;
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

        public void UpdateGoods() // если время пришло то просто будет запускаться этот метод, проверка на время будет делать извне
        {
            if(name != "Меч короля Артура" && name != "Билет на концерт")
            { 
            if (days > 0 && name != "Коньяк")
            {
                price--;
                days--;
            }
            else
            {
                    if (name != "Коньяк")
                    {
                        Price = Price/2;
                        days--;
                    }
                    else
                        Price++;
            }
            }
            else if(name == "Билет на концерт")
            {
                if (days > 10)
                {
                    Price++;
                    days--;
                }
                else
                {
                    if (days == 0)
                    {
                        Price = 0;
                        days--;
                    }
                    else
                    {
                        Price = (days <= 10 && days > 5) ? (Price + 2) : (Price + 3);
                        days--;
                    }
                }
            }
        }

    }
}
