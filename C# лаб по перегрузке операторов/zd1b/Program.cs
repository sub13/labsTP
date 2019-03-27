using System;
using System.Collections.Generic;

namespace zd1b  
{
    /**
    * класс Program который создает определенное количество списков
    * вызывает методы из класса initialize для проверки перегрузки операторов
    * содержит точку входа в программу
    */
    public class Program
    {
        spisok<int>  numbers1 = new spisok<int>();
        spisok<int>  numbers2 = new spisok<int>();
        spisok<int>  numbers3 = new spisok<int>();
        spisok<int>  numbers = new spisok<int>(){1,2,3,4};
        public bool done;
        static int[] numbelem1 = { 1, 2, 3 }, numbelem2 = { 5, 6, 7 };
        /**
        * создает экземпляр Program
        * @constructor 
        * @param { ref int[] } numbelem1 - массив1 значений int
        * @param { ref int[] } numbelem2 - массив2 значений int
        */
        public Program(ref int[] numbelem1, ref int[] numbelem2)
        {
            foreach (var k in numbelem1)
                numbers1.Add(k);
            foreach (var k in numbelem2)
                numbers2.Add(k);
        }
        /**
        * Метод вызова всех методов проверки перегрузки операторов 
        */
        public void workoperators()
        {
            initialize obj = new initialize();
            try
            {
                obj.plus(numbers1, numbers2, numbers3);
                obj.minus(numbers, 4);
                obj.bools1(numbers1, numbers2);
                obj.bools2(numbers1, numbers2);
                obj.clrlist(numbers);
                done = true;
            }
            catch
            {
                Console.WriteLine("неправильно переданы значения");
                done = false;
            }

        }
        /**
        * Точка входа в программу
        * Cоздает обьект класса zd1
        */
        static void Main()
        {
            Program obj2 = new Program(ref numbelem1, ref numbelem2);
            obj2.workoperators();
            Console.ReadKey();
        }
    }
}
