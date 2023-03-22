using System;

namespace HW8
{
    class Program
    {
        static void Main(string[] args)
        {
            int c = printInputInt("----------------------------------------\nВведите количество столбцов матрицы"),
                r = printInputInt("\nВведите количество строк матрицы"),
                b = printInputInt("\nВведите количество ячеек матрицы");

            #region упорядочить строки/столбцы по возрастанию/убыванию
            int[,] arrInt = CreateRandomIntMatrix(r, c, -99, 99);
            print($"----------------------------------------\nСозданная матрица целых чисел:\n{IntMatrixToString(arrInt)}");
            
            bool checkRow = EnterYesOrNot("\nЕсли вы хотите отстортировать по строкам, напишите - да, если по столбцам, напишите - нет\n", "да", "нет"), 
            orderByAscending = EnterYesOrNot("\nЕсли вы хотите отстортировать по возрастанию, напишите - да, если по убыванию, напишите - нет\n", "да", "нет");

            print(checkRow ?
                $"----------------------------------------\nОтсортированная по строкам матрица целых чисел:\n{IntMatrixToString(OrderMatrix(arrInt, orderByAscending, checkRow))}"
                :
                $"----------------------------------------\nОтсортированная по столбцам матрица целых чисел:\n{IntMatrixToString(OrderMatrix(arrInt, orderByAscending, checkRow))}");
            #endregion
            #region наименьшая сумма строк в матрице
            print($"Минимальная сумма строки матрицы: {LowSumRowIn2dMatrix(arrInt)}");
            #endregion
            #region произведение двух матриц
            int[,] arrInt2 = CreateRandomIntMatrix(c, r, -99, 99); // количество столбцов первой матрицы, должно быть равно количеству строк второй матрицы
            print($"----------------------------------------\nВторая созданная матрица целых чисел:\n{IntMatrixToString(arrInt)}");
            print($"----------------------------------------\nРезультат умножения матриц:\n{IntMatrixToString(Multiply2dMatrix(arrInt, arrInt2))}");
            #endregion
            #region трехмерный массив неповторяющихся двузначных чисел с выводом по строкам и указанием индексов элементов (v)
            int[, ,] arr3Int = CreateNotRepeatedRandomInt3Matrix(r, c, b);
            print(Matrix3ToStringWithIndex(arr3Int));
            #endregion
            #region спиральное заполнение массива
            print($"----------------------------------------\nСпиральное заполнение матрицы:\n{IntMatrixToString(SpiralFillMatrix(printInputInt("\nВведите любую размерность матрицы, которую необходимо заполнить спирально")))}");
            #endregion
        }

        /// <summary>
        /// Создание матрицы заданного размера и её спиральное заполнение
        /// </summary>
        /// <param name="size">размерность матрицы</param>
        /// <returns>спирально заполненная матрица</returns>
        static int [,] SpiralFillMatrix (int size)
        {
            int [,] arr = new int[size, size];

            int count = 1;
            int distanceFromBound = 0;
            bool column = true,
                 back = false;
            int i = 1;

            while (count < (arr.GetLength(0) * arr.GetLength(1))+1)
            {
                if (!back)
                {
                    if (column)
                    {
                        for (int c = 0 + distanceFromBound; c < arr.GetLength(1) - distanceFromBound; c++)
                        {
                            arr[distanceFromBound, c] = count;
                            count++;
                        }
                        column = !column;
                    }
                    else
                    {
                        for (int r = 0 + i; r < arr.GetLength(1) - distanceFromBound; r++)
                        {
                            arr[r, arr.GetLength(1) - 1 - distanceFromBound] = count;
                            count++;
                        }
                        column = !column;
                        back = !back;
                    }
                }
                else
                {
                    if (column)
                    {
                        for (int c = arr.GetLength(1) - 1 - i; c >= 0 + distanceFromBound; c--)
                        {
                            arr[arr.GetLength(0) - 1 - distanceFromBound, c] = count;
                            count++;
                        }
                        column = !column;
                    }
                    else
                    {
                        for (int r = arr.GetLength(0) - 1 - i; r > 0 + distanceFromBound; r--)
                        {
                            arr[r, 0 + distanceFromBound] = count;
                            count++;
                        }
                        distanceFromBound++;
                        i++;
                        back = !back;
                        column = !column;
                    }
                }
            }

            return arr;
        }

        /// <summary>
        /// Перемножение двух целочисленных матриц
        /// </summary>
        /// <param name="A">матрица 1</param>
        /// <param name="B">матрица 2</param>
        /// <returns>перемноженная матрица</returns>
        static int[,] Multiply2dMatrix(int[,] A, int[,] B)
        {
            int[,] C = new int[A.GetLength(0), B.GetLength(1)];
            
            if (A.GetLength(0) != B.GetLength(1))
            {
                print("Ошибка, для перемножения матриц, количество столбцов первой матрицы, должно быть равно количеству строк второй матрицы");
                return C;
            }
            else
            {
                for (int i = 0; i < C.GetLength(0); i++)
                {
                    for (int j = 0; j < C.GetLength(1); j++)
                    {
                        for (int k = 0; k < B.GetLength(0); k++)
                        {
                            C[i, j] += A[i, k] * B[k, j];
                        }
                    }
                }

                return C;
            }
        }

        /// <summary>
        /// Минимальная сумма в строке матрицы целых чисел
        /// </summary>
        /// <param name="arr">матрица целых чисел</param>
        /// <returns>минимальная сумма в строке</returns>
        static int LowSumRowIn2dMatrix(int[,] arr)
        {
            int[] temparr = new int[2];
            int minSum = 0, tmpSum = 0;
            bool first = true;
            for (int r = 0; r < arr.GetLength(0); r++)
            {
                for (int c = 0; c < arr.GetLength(1); c++)
                {
                    tmpSum += arr[r, c];
                }
                if (first)
                {
                    minSum = tmpSum;
                    first = false;
                }
                if (tmpSum < minSum)
                {
                    minSum = tmpSum;
                    tmpSum = 0;
                }
            }
            return minSum;
        }

        /// <summary>
        /// Перевод полученных строк в булево значение
        /// </summary>
        /// <param name="check">сопровождающий вопрос, на который подразумевается ответ</param>
        /// <param name="strYes">строка, считающаяся true</param>
        /// <param name="strNot">строка, считающаяся false</param>
        /// <returns>булево значение после полученной строки</returns>
        static bool EnterYesOrNot(string check, string strYes, string strNot)
        {
            Console.Write($"{check} \n{strYes} или {strNot}\n");

            if (Console.ReadLine() == strYes)
            {
                return true;
            }
            else if (Console.ReadLine() == strNot)
            {
                return false;
            }
            return false;
        }

        /// <summary>
        /// Сортировка значений матрицы целых чисел по строкам, или столбцам, по возрастанию, или убыванию
        /// </summary>
        /// <param name="arr">матрица</param>
        /// <param name="checkRow">если true = проверка по строкам, если false - проверка по столбцам</param>
        /// <param name="orderByAscending">если true = сортировка по возрастанию, если false - сортировка по убыванию</param>
        /// <returns></returns>
        static int[,] OrderMatrix(int[,] arr, bool checkRow, bool orderByAscending)
        {
            for (int c = 0; c < arr.GetLength(0); c++)
            {
                for (int r = 0; r < arr.GetLength(1); r++)
                {
                    for (int n = 0; n < arr.GetLength(checkRow? 0 : 1); n++)
                    {
                        if (checkRow)
                        {
                            if (orderByAscending ? arr[c, r] > arr[n, r] : arr[c, r] < arr[n, r])
                            {
                                ReplaceMemoryElements(ref arr[c, r], ref arr[n, r]);
                            }
                        }
                        if (!checkRow)
                        {
                            if (orderByAscending ? arr[c, r] > arr[c, n] : arr[c, r] < arr[c, n])
                            {
                                ReplaceMemoryElements(ref arr[c, r], ref arr[c, n]);
                            }
                        }
                    }
                }
            }
            return arr;
        }

        /// <summary>
        /// Меняет значения целых чисел между переменными
        /// </summary>
        /// <param name="i">целочисленная переменная 1</param>
        /// <param name="j">целочисленная переменная 2</param>
        static void ReplaceMemoryElements(ref int i, ref int j)
        {
            j = i + j;
            i = j - i;
            j = j - i;
        }

        /// <summary>
        /// Заполнение трехмерной матрицы случайными, неповторяющимися двузначными целыми числами
        /// </summary>
        /// <param name="r">количество строк в матрице</param>
        /// <param name="c">количество столбцов в матрице</param>
        /// <param name="b">количество ячеек в матрице</param>
        /// <returns></returns>
        static int[,,] CreateNotRepeatedRandomInt3Matrix(int r, int c, int b)
        {
            int tempQuantity = r + c + b;
            int[] tempArr = new int[90];
            int[,,] arr3 = new int[r, c, b];
            if (tempQuantity < 90) // соответствует количеству неповторяющихся двузначных чисел
            {
                for (int i = 10; i < 100; i++) // заполняем массив неповторяющихся значений
                {
                    tempArr[i - 10] = i;
                }

                int t;
                Random rand = new Random();
                for (int i = 0; i < tempArr.Length; i++) // перемешиваем
                {
                    t = rand.Next(0, tempQuantity);
                    ReplaceMemoryElements(ref tempArr[i], ref tempArr[t]);
                }

                int count = 0;
                for (int i = 0; i < arr3.GetLength(0); i++)
                {
                    for (int j = 0; j < arr3.GetLength(1); j++)
                    {
                        for (int v = 0; v < arr3.GetLength(2); v++)
                        {
                            arr3[i, j, v] = tempArr[count]; // используем
                            count++;
                        }
                    }
                }

                return arr3;
            }
            else
            {
                return arr3;
            }
        }

        /// <summary>
        /// Перевод матрицы целых чисел в форматированную строку
        /// </summary>
        /// <param name="arr">матрица целых чисел</param>
        /// <returns>форматированная строка целых чисел</returns>
        static string Matrix3ToStringWithIndex(int[,,] arr)
        {
            string z = "";

            for (int n = 0; n < arr.GetLength(2); n++)
            {
                z += "\n-------------------";
                for (int i = 0; i < arr.GetLength(0); i++)
                {
                    z += "\n";
                    for (int j = 0; j < arr.GetLength(1); j++)
                    {
                        z += ($"{arr[i, j, n]}({i},{j},{n})\t");
                    }
                }
            }
            return z;
        }

        /// <summary>
        /// удобство вывода без лишнего текста
        /// </summary>
        /// <param name="mes">текст для вывода в консоль</param>
        static void print(string mes)
        {
            Console.WriteLine(mes);
        }

        /// </summary>
        /// отображение сообщения, прием из консоли значения, перевод полученного значения в число
        /// <param name="mes">сообщение перед запросом числа</param>
        /// <returns>число, введенное пользователем в консоли</returns>
        static int printInputInt(string mes)
        {
            Console.WriteLine(mes);
            int.TryParse(Console.ReadLine(), out int userNumber);
            return userNumber;
        }

        /// <summary>
        /// Создание матрицы случайных целых чисел
        /// </summary>
        /// <param name="sizeM">количество столбцов</param>
        /// <param name="sizeN">количество строк</param>
        /// <param name="min">минимально возможное значение</param>
        /// <param name="max">максимально возможное значение</param>
        /// <returns>матрица случайных целых чисел</returns>
        static int[,] CreateRandomIntMatrix(int r, int c, int min, int max)
        {
            int[,] arr = new int[r, c];

            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    arr[i, j] += new Random().Next(min, max + 1);
                }
            }
            return arr;
        }

        /// <summary>
        /// Перевод матрицы целых чисел в форматированную строку
        /// </summary>
        /// <param name="arr">матрица целых чисел</param>
        /// <returns>форматированная строка целых чисел</returns>
        static string IntMatrixToString(int[,] arr)
        {
            string z = "";
            for (int r = 0; r < arr.GetLength(0); r++)
            {
                z += "\n";
                for (int c = 0; c < arr.GetLength(1); c++)
                {
                    z += ($"{arr[r, c]}\t");
                }
            }
            return z;
        }
    }
}
