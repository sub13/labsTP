using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace zd1btests
{
    [TestClass]
    public class zd1tests
    {
        [TestMethod]
        public void Check_peregruzka_Plus()
        {
            int[] excepted = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            zd1b.initialize obj = new zd1b.initialize();
            zd1b.spisok<int> numbers1 = new zd1b.spisok<int>() { 1, 2, 3, 4, 5};
            zd1b.spisok<int> numbers2 = new zd1b.spisok<int>() { 6, 7, 8, 9, 10};
            zd1b.spisok<int> numbers3 = new zd1b.spisok<int>();
            int[] ArrayElem = obj.plus(numbers1, numbers2, numbers3);
            for (int i = 0; i <= 9; i++)
            {
                Assert.AreEqual(excepted[i], ArrayElem[i]);
            }
        }
        [TestMethod]
        public void Check_peregruzka_Minus()
        {
            int[] excepted = { 1, 1 };
            zd1b.initialize obj = new zd1b.initialize();
            int deletingElem = 9;
            zd1b.spisok<int> Sendnumbers = new zd1b.spisok<int>() { 9, 1, 1 };
            int[] ArrayElem = obj.minus(Sendnumbers,deletingElem);
            for (int i = 0; i <= 1; i++)
            {
                Assert.AreEqual(excepted[i], ArrayElem[i]);
            }
        }
        [TestMethod]
        public void Check_peregruzka_bools1_WhenTrue()
        {
            int excepted = 1;
            zd1b.initialize obj = new zd1b.initialize();
            zd1b.spisok<int> Sendnumbers1 = new zd1b.spisok<int>() { 1, 2, 3 };
            zd1b.spisok<int> Sendnumbers2 = new zd1b.spisok<int>() { 1, 2, 3 };
            int result = obj.bools1(Sendnumbers1, Sendnumbers2);
            Assert.AreEqual(excepted, result);
        }
        [TestMethod]
        public void Check_peregruzka_bools1_WhenFalse()
        {
            int excepted = 0;
            zd1b.initialize obj = new zd1b.initialize();
            zd1b.spisok<int> Sendnumbers1 = new zd1b.spisok<int>() { 1, 2, 3 };
            zd1b.spisok<int> Sendnumbers2 = new zd1b.spisok<int>() { 1, 3 };
            int result = obj.bools1(Sendnumbers1, Sendnumbers2);
            Assert.AreEqual(excepted, result);
        }
        [TestMethod]
        public void Check_peregruzka_bools2_WhenTrue()
        {
            int excepted = 1;
            zd1b.initialize obj = new zd1b.initialize();
            zd1b.spisok<int> Sendnumbers1 = new zd1b.spisok<int>() { 1, 2, 3 };
            zd1b.spisok<int> Sendnumbers2 = new zd1b.spisok<int>() { 1, 3 };
            int result = obj.bools2(Sendnumbers1, Sendnumbers2);
            Assert.AreEqual(excepted, result);
        }
        [TestMethod]
        public void Check_peregruzka_bools2_WhenFalse()
        {
            int excepted = 0;
            zd1b.initialize obj = new zd1b.initialize();
            zd1b.spisok<int> Sendnumbers1 = new zd1b.spisok<int>() { 1, 2, 3 };
            zd1b.spisok<int> Sendnumbers2 = new zd1b.spisok<int>() { 1, 2, 3 };
            int result = obj.bools2(Sendnumbers1, Sendnumbers2);
            Assert.AreEqual(excepted, result);
        }
        [TestMethod]
        public void Check_peregruzka_tilda_WhenFalse()
        {
            int excepted = 0;
            zd1b.initialize obj = new zd1b.initialize();
            zd1b.spisok<int> Sendnumbers = new zd1b.spisok<int>() { 1, 2, 3 };
            int result = obj.clrlist(Sendnumbers);
            Assert.AreEqual(excepted, result);
        }
        [TestMethod]
        public void Check_peregruzka_tilda_WhenTrue()
        {
            int excepted = 1;
            zd1b.initialize obj = new zd1b.initialize();
            zd1b.spisok<int> Sendnumbers = new zd1b.spisok<int>();
            int result = obj.clrlist(Sendnumbers);
            Assert.AreEqual(excepted, result);
        }
        [TestMethod]
        public void Check_all() //proverka vsego odnovremenno
        {
            bool excepted = true;
            bool result;
            int[] sendArrayInt1 = { 1, 2, 3 };
            int[] sendArrayInt2 = { 1, 4, 5 };
            zd1b.Program obj = new zd1b.Program(ref sendArrayInt2, ref sendArrayInt1);
            obj.workoperators();
            result = obj.done;
            Assert.AreEqual(excepted, result);
        }
    }
}
