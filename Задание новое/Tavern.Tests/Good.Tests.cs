using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tavern.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CheckPriceOrdinaryItem() //проверяем уменьшение цены товара при наступлении полночи
        {
            float ExceptedPrice = 0.5F, ResultPrice;
            Good tovar = new Good("Чипсы", 30, 1.5F);
            tovar.UpdateGood();
            ResultPrice = tovar.Price;
            Assert.AreEqual(ExceptedPrice, ResultPrice);
        }

        [TestMethod]
        public void CheckDaysOrdinaryItem() //проверяем уменьшение срока годности товара при наступлении полночи
        {
            int ExceptedDays = 29, ResultDays;
            Good tovar = new Good("Чипсы", 30, 1.5F);
            tovar.UpdateGood();
            ResultDays = tovar.Days;
            Assert.AreEqual(ExceptedDays, ResultDays);
        }

        [TestMethod]
        public void CheckPriceWhenDays0() //проверяем изменение цены на просроченный товар
        {
            float ExceptedPrice = 2F, ResultPrice;
            Good tovar = new Good("Чипсы", 0, 4F);
            tovar.UpdateGood();
            ResultPrice = tovar.Price;
            Assert.AreEqual(ExceptedPrice, ResultPrice);
        }

        [TestMethod]
        public void CheckPriceKonjak() //проверяем изменения цены коньяка
        {
            float ExceptedPrice = 31F, ResultPrice;
            Good tovar = new Good("Коньяк", 30, 30F);
            tovar.UpdateGood();
            ResultPrice = tovar.Price;
            Assert.AreEqual(ExceptedPrice, ResultPrice);
        }

        [TestMethod]
        public void CheckPriceKingArthur() //проверяем изменения цены меча короля Артура
        {
            float ExceptedPrice = 50F, ResultPrice;
            Good tovar = new Good("Меч короля Артура", 30, 50F);
            tovar.UpdateGood();
            tovar.UpdateGood();
            ResultPrice = tovar.Price;
            Assert.AreEqual(ExceptedPrice, ResultPrice);
        }
        [TestMethod]
        public void CheckDaysKingArthur() //проверяем изменения срока годности меча короля Артура
        {
            int ExceptedDays = 30, ResultDays;
            Good tovar = new Good("Меч короля Артура", 30, 50F);
            tovar.UpdateGood();
            tovar.UpdateGood();
            ResultDays = tovar.Days;
            Assert.AreEqual(ExceptedDays, ResultDays);
        }

        [TestMethod]
        public void CheckPriceConcertWhenMore10Days() //Проверяем цену на билет когда до концерта больше 10 дней
        {
            float ExceptedPrice = 41F, ResultPrice;
            Good tovar = new Good("Билет на концерт", 30, 40F);
            tovar.UpdateGood();
            ResultPrice = tovar.Price;
            Assert.AreEqual(ExceptedPrice, ResultPrice);
        }

        [TestMethod]
        public void CheckPriceConcertWhen10Days() //Проверяем цену на билет когда до концерта 10 дней
        {
            float ExceptedPrice = 42F, ResultPrice;
            Good tovar = new Good("Билет на концерт", 10, 40F);
            tovar.UpdateGood();
            ResultPrice = tovar.Price;
            Assert.AreEqual(ExceptedPrice, ResultPrice);
        }

        [TestMethod]
        public void CheckPriceConcertWhen5Days() //Проверяем цену на билет когда до концерта 5 дней
        {
            float ExceptedPrice = 43F, ResultPrice;
            Good tovar = new Good("Билет на концерт", 5, 40F);
            tovar.UpdateGood();
            ResultPrice = tovar.Price;
            Assert.AreEqual(ExceptedPrice, ResultPrice);
        }

        [TestMethod]
        public void CheckPriceConcertWhen0Days() //Проверяем цену на билет когда актуальность билет истекла
        {
            float ExceptedPrice = 0F, ResultPrice;
            Good tovar = new Good("Билет на концерт", 0, 40F);
            tovar.UpdateGood();
            ResultPrice = tovar.Price;
            Assert.AreEqual(ExceptedPrice, ResultPrice);
        }

        [TestMethod]
        public void NumberOFSimbolsAfterComma() //проверяем округление цены при количестве знаков больше двух после запятой
        {
            float ExceptedPrice = 0F, ResultPrice;
            Good tovar = new Good("Кола", 0, 0.01F);
            tovar.UpdateGood();
            ResultPrice = tovar.Price;
            Assert.AreEqual(ExceptedPrice, ResultPrice);
        }

        [TestMethod]
        public void SetCurrentPrice() //устанавливаем число больше 50 и ожидаем исключение
        {
            Exception Result = null;
            try
            {
                Good tovar = new Good("Хлеб", 0, 100F);
            }
            catch (Exception ex)
            {
                Result = ex;
            }
            Assert.IsNotNull(Result);
        }
    }
}
