using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Money
{
    public int Hryvnas { get; private set; }
    public int Kopecks { get; private set; }

    public Money(int hryvnas, int kopecks)
    {
        if (hryvnas < 0 || kopecks < 0)
        {
            throw new ArgumentException("Сумма не может быть отрицательной.");
        }

        Hryvnas = hryvnas;
        Kopecks = kopecks;
        Normalize();
    }

    private void Normalize()
    {
        Hryvnas += Kopecks / 100;
        Kopecks %= 100;
    }

    public static Money operator +(Money m1, Money m2)
    {
        int newHryvnas = m1.Hryvnas + m2.Hryvnas;
        int newKopecks = m1.Kopecks + m2.Kopecks;

        try
        {
            return new Money(newHryvnas, newKopecks);
        }
        catch (ArgumentException)
        {
            throw new BankruptException("Результат сложения отрицательный.");
        }
    }

    public static Money operator -(Money m1, Money m2)
    {
        int newHryvnas = m1.Hryvnas - m2.Hryvnas;
        int newKopecks = m1.Kopecks - m2.Kopecks;

        if (newHryvnas < 0 || newKopecks < 0)
        {
            if (newHryvnas < 0 && newKopecks > 0)
            {
                if ((newHryvnas * (-1)) > (newKopecks / 100))
                {
                    throw new BankruptException("Результат вычитания отрицательный.");
                }
            }
            else
            {
                throw new BankruptException("Результат вычитания отрицательный.");
            }

        }

        try
        {
            return new Money(newHryvnas, newKopecks);
        }
        catch (ArgumentException)
        {
            throw new BankruptException("Результат вычитания отрицательный.");
        }
    }

    public static Money operator /(Money m, int divisor)
    {
        if (divisor == 0)
        {
            throw new DivideByZeroException("Деление на ноль.");
        }

        int newHryvnas = m.Hryvnas / divisor;
        int newKopecks = m.Kopecks / divisor;

        try
        {
            return new Money(newHryvnas, newKopecks);
        }
        catch (ArgumentException)
        {
            throw new BankruptException("Результат деления отрицательный.");
        }
    }

    public static Money operator *(Money m, int multiplier)
    {
        int newHryvnas = m.Hryvnas * multiplier;
        int newKopecks = m.Kopecks * multiplier;

        try
        {
            return new Money(newHryvnas, newKopecks);
        }
        catch (ArgumentException)
        {
            throw new BankruptException("Результат умножения отрицательный.");
        }
    }

    public static Money operator ++(Money m)
    {
        try
        {
            return new Money(m.Hryvnas, m.Kopecks + 1);
        }
        catch (ArgumentException)
        {
            throw new BankruptException("Результат инкремента отрицательный.");
        }
    }

    public static Money operator --(Money m)
    {

        if ((m.Hryvnas == 0 && m.Kopecks == 0))
        {
            throw new BankruptException("Результат декремента отрицательный.");
        }

        int newHryvnas = m.Hryvnas;
        int newKopecks = m.Kopecks - 1;

        if (newKopecks < 0)
        {
            newHryvnas--;
            newKopecks = 99;
        }

        if (newHryvnas < 0)
        {
            throw new BankruptException("Результат декремента отрицательный.");
        }

        try
        {
            return new Money(newHryvnas, newKopecks);
        }
        catch (ArgumentException)
        {
            throw new BankruptException("Результат декремента отрицательный.");
        }
    }
    public static bool operator <(Money m1, Money m2)
    {
        if (m1.Hryvnas < m2.Hryvnas)
        {
            return true;
        }
        if (m1.Hryvnas == m2.Hryvnas && m1.Kopecks < m2.Kopecks)
        {
            return true;
        }
        return false;
    }

    public static bool operator >(Money m1, Money m2)
    {
        return !(m1 < m2) && (m1 != m2);
    }

    public static bool operator ==(Money m1, Money m2)
    {
        if (ReferenceEquals(m1, m2)) return true;
        if (ReferenceEquals(m1, null) || ReferenceEquals(m2, null)) return false;
        return m1.Hryvnas == m2.Hryvnas && m1.Kopecks == m2.Kopecks;

    }

    public static bool operator !=(Money m1, Money m2)
    {
        return !(m1 == m2);
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        Money other = (Money)obj;
        return (Hryvnas == other.Hryvnas) && (Kopecks == other.Kopecks);
    }

    public override int GetHashCode()
    {
        return Hryvnas.GetHashCode() ^ Kopecks.GetHashCode();
    }

    public override string ToString()
    {
        return $"{Hryvnas}.{Kopecks:D2} грн.";
    }
}