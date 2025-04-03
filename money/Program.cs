using System;

public class MoneyInfo
{
    public static void Main(string[] args)
    {
        Money m1 = null;
        Money m2 = null;

        while (true)
        {
            Console.WriteLine("\nВыберите действие:");
            Console.WriteLine("1. Создать первую сумму");
            Console.WriteLine("2. Создать вторую сумму");
            Console.WriteLine("3. Сложить суммы");
            Console.WriteLine("4. Вычесть суммы");
            Console.WriteLine("5. Разделить сумму на число");
            Console.WriteLine("6. Умножить сумму на число");
            Console.WriteLine("7. Увеличить сумму на 1 копейку");
            Console.WriteLine("8. Уменьшить сумму на 1 копейку");
            Console.WriteLine("9. Сравнить суммы (<, >, ==, !=)");
            Console.WriteLine("10. Вывести суммы");
            Console.WriteLine("0. Выход");

            Console.Write("Ваш выбор: ");
            string choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        m1 = CreateMoney("первую");
                        break;
                    case "2":
                        m2 = CreateMoney("вторую");
                        break;
                    case "3":
                        if (m1 == null || m2 == null) { Console.WriteLine("Создайте обе суммы."); break; }
                        Console.WriteLine($"Результат: {m1 + m2}");
                        break;
                    case "4":
                        if (m1 == null || m2 == null) { Console.WriteLine("Создайте обе суммы."); break; }
                        Console.WriteLine($"Результат: {m1 - m2}");
                        break;
                    case "5":
                        if (m1 == null) { Console.WriteLine("Создайте первую сумму."); break; }
                        Console.Write("Введите делитель: ");
                        int divisor = int.Parse(Console.ReadLine());
                        Console.WriteLine($"Результат: {m1 / divisor}");
                        break;
                    case "6":
                        if (m1 == null) { Console.WriteLine("Создайте первую сумму."); break; }
                        Console.Write("Введите множитель: ");
                        int multiplier = int.Parse(Console.ReadLine());
                        Console.WriteLine($"Результат: {m1 * multiplier}");
                        break;
                    case "7":
                        if (m1 == null) { Console.WriteLine("Создайте первую сумму."); break; }
                        Console.WriteLine($"Результат: {++m1}");
                        break;
                    case "8":
                        if (m1 == null) { Console.WriteLine("Создайте первую сумму."); break; }
                        Console.WriteLine($"Результат: {--m1}");
                        break;
                    case "9":
                        if (m1 == null || m2 == null) { Console.WriteLine("Создайте обе суммы."); break; }
                        Console.WriteLine($"{m1} < {m2}: {m1 < m2}");
                        Console.WriteLine($"{m1} > {m2}: {m1 > m2}");
                        Console.WriteLine($"{m1} == {m2}: {m1 == m2}");
                        Console.WriteLine($"{m1} != {m2}: {m1 != m2}");
                        break;
                    case "10":
                        Console.WriteLine($"Первая сумма: {(m1 != null ? m1.ToString() : "Не создана")}");
                        Console.WriteLine($"Вторая сумма: {(m2 != null ? m2.ToString() : "Не создана")}");
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор.");
                        break;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Ошибка: Неверный формат числа.");
            }
            catch (DivideByZeroException e)
            {
                Console.WriteLine($"Ошибка: {e.Message}");
            }
            catch (BankruptException e)
            {
                Console.WriteLine($"Ошибка: {e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Произошла непредвиденная ошибка: {e.Message}");
            }
        }
    }

    static Money CreateMoney(string order)
    {
        Console.Write($"Введите гривны для {order} суммы: ");
        int hryvnas = int.Parse(Console.ReadLine());
        Console.Write($"Введите копейки для {order} суммы: ");
        int kopecks = int.Parse(Console.ReadLine());

        try
        {
            return new Money(hryvnas, kopecks);
        }
        catch (ArgumentException e)
        {
            Console.WriteLine($"Ошибка: {e.Message}");
            return null;
        }
    }
}