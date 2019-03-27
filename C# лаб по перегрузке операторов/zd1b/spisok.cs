using System;
using System.Collections.Generic;
using System.Linq;

namespace zd1b
{
    /**
    * производный класс spisok<T> насследующий класс коллекций List
    * реализует перегрузку для списков типа spisok<T>
    */
    public class spisok<T>: List<T>
    {
        /**
        * перегрузка оператора + 
        * @param { spisok<T> } numbers1 - коллекция 1 значений int
        * @param { spisok<T> } numbers2 - коллекция 2 значений int
        * @return {result} возврашает результат
        */
        public static spisok<T> operator+(spisok<T> numbers1,spisok<T> numbers2)
        {
            spisok<T> result = new spisok<T>();
            result.AddRange(numbers1);
            result.AddRange(numbers2);
            return result;
        }
        /**
        * перегрузка оператора - 
        * @param { spisok<T> } numbers - коллекция  значений int
        * @param { T } del - удаляемое значение
        * @return {numbers1} возврашает результат
        */
        public static spisok<T> operator -(spisok<T> numbers1,T del)
        {
            if(!numbers1.Remove(del))
            {
                Console.WriteLine("такого элемента не существует в списке");
            }
            return numbers1;
        }
        /**
        * перегрузка оператора == 
        * @param { spisok<T> } numbers1 - коллекция 1 значений int
        * @param { spisok<T> } numbers2 - коллекция 2 значений int
        * @return {true} возврашает списки равны
        * @return {false} возврашает списки не равны
        */
        public static bool operator ==(spisok<T> numbers1, spisok<T> numbers2)
        {
            if (numbers1.SequenceEqual(numbers2))
            {
                Console.WriteLine("Результат перегрузки (==) Списки равны\n");
                return true;
            }
            else
            {
                Console.WriteLine("Результат перегрузки (==) Cписки не равны\n");
                return false;
            }
        }
        /**
        * перегрузка оператора != 
        * @param { spisok<T> } numbers1 - коллекция 1 значений int
        * @param { spisok<T> } numbers2 - коллекция 2 значений int
        * @return {true} возврашает списки не равны
        * @return {false} возврашает списки равны
        */
        public static bool operator !=(spisok<T> numbers1, spisok<T> numbers2)
        {
            if (numbers1.SequenceEqual(numbers2))
            {
                Console.WriteLine("Результат перегрузки (!=) Cписки равны\n");
                return false;
            }
            else
            {
                Console.WriteLine("Результат перегрузки (!=) Cписки не равны\n");
                return true;
            }
        }
        /**
        * перегрузка оператора ~ 
        * @param { spisok<T> } numbers - коллекция значений int
        * @return {true} возврашает если список пуст
        * @return {false} возврашает если список не пуст
        */
        public static bool operator ~(spisok<T> numbers)
        {
            int i = 0;
            foreach (var k in numbers)
            {
                i++;
            }
            if (i == 0)
            {
                Console.WriteLine("Результат перегрузки (~): список пуст\n");
                return true;
            }
            else
            {
                Console.WriteLine("Результат перегрузки (~): Список не пуст\n");
                return false;
            }
        }
    }
    /**
    * класс initialize содержащий методы проверки перегрузки
    */
    public class initialize
    {
        /**
        * деструктор выводящий сообщение при вызове сборщика муссора
        */
        ~initialize()
        {
            Console.WriteLine("программа была завершена!был вызван сборщик муссора");
            //Console.ReadKey();
        }
        /**
        * метод проверки перегрузки оператора + 
        * @param { spisok<T> } numbers1 - коллекция 1 значений int
        * @param { spisok<T> } numbers2 - коллекция 2 значений int
        * @param { spisok<T> } numbers3 - коллекция 3 значений int
        * @return { ArrayElem } возврашает элементы списка в ввиде массива
        */
        public int[] plus(spisok<int> numbers1,spisok<int> numbers2,spisok<int> numbers3)
        {
            int[] ArrayElem = new int[100];
            int i = 0;
            try
            { 
                numbers3 = numbers1 + numbers2;
                Console.WriteLine("Результат перегрузки (+):");
                foreach (var k in numbers3)
                {   
                    Console.WriteLine(k);
                    ArrayElem[i] = k;
                    i++;
                }
            }    
            catch
            {
                Console.WriteLine("было вызвано исключение!Ошибка!");
            }
            return ArrayElem;
        }
        /**
        * Метод проверки перегрузки оператора - 
        * @param { spisok<T> } numbers - коллекция  значений int
        * @param { T } del - удаляемое значение
        * @return { ArrayElem } возврашает элементы списка в ввиде массива
        */
        public int[] minus(spisok<int> numbers,int del)
        { 
            int[] ArrayElem = new int[100];
            int i = 0;
            try
            {
                numbers = numbers - del;
                Console.WriteLine("Результат перегрузки (-):");
                foreach (var k in numbers)
                {
                    Console.WriteLine(k);
                    ArrayElem[i] = k;
                    i++;
                }
            }
            catch
            {
                Console.WriteLine("было вызвано исключение!Ошибка!");
            }
            return ArrayElem;
        }
        /**
        * Метод проверки перегрузки оператора == 
        * @param { spisok<T> } numbers1 - коллекция 1 значений int
        * @param { spisok<T> } numbers2 - коллекция 2 значений int
        * @return {otvet2} возврашает результат проверки на равенство
        */
        public int bools1(spisok<int> numbers1, spisok<int> numbers2)
        {
            int otvet2 = 2;
            try
            {
                bool otvet = numbers1 == numbers2;
                if (otvet == false)
                    otvet2 = 0;
                else
                    otvet2 = 1;
                return otvet2;
            }
            catch
            {
                Console.WriteLine("было вызвано исключение!Ошибка!");
                return otvet2;
            }
        }
        /**
        * Метод проверки перегрузки оператора != 
        * @param { spisok<T> } numbers1 - коллекция 1 значений int
        * @param { spisok<T> } numbers2 - коллекция 2 значений int
        * @return {otvet2} возврашает результат проверки на не равенство
        */
        public int bools2(spisok<int> numbers1,spisok<int> numbers2)
        {
            int otvet2 = 2;
            bool otvet = false;
            try
            {
                otvet = numbers1 != numbers2;
                if (otvet == false)
                    otvet2 = 0;
                else
                    otvet2 = 1;
                return otvet2;
            }
            catch
            {
                Console.WriteLine("было вызвано исключение!Ошибка!");
                return otvet2;
            }
        }
        /**
        * Метод проверки перегрузки оператора ~ 
        * @param { spisok<T> } numbers - коллекция значений int
        * @return {otvet2} возврашает результат проверки на пустоту
        */
        public int clrlist(spisok<int> numbers)
        {
            int otvet2 = 2;
            try
            { 
            bool otvet = ~numbers;
                if (otvet == false)
                    otvet2 = 0;
                else
                    otvet2 = 1;
                return otvet2;
            }
            catch
            {
                Console.WriteLine("было вызвано исключение!Ошибка!");
                return otvet2;
            }   
        }
    }

}