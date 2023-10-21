using System;

class Program
{
    static string[] units = { "рубль", "рубля", "рублей" };
    static string[] tens = { "", "", "двадцать", "тридцать", "сорок", "пятьдесят", "шестьдесят", "семьдесят", "восемьдесят", "девяносто" };
    static string[] ones = { "", "один", "два", "три", "четыре", "пять", "шесть", "семь", "восемь", "девять" };
    static string[] teens = { "десять", "одиннадцать", "двенадцать", "тринадцать", "четырнадцать", "пятнадцать", "шестнадцать", "семнадцать", "восемнадцать", "девятнадцать" };

    static string ConvertNumberToWords(int number)
    {
        if (number == 0)
        {
            return "ноль рублей";
        }

        if (number < 0 || number > 100000)
        {
            return "Введено некорректное число";
        }

        string result = "";

        if (number >= 1000)
        {
            int thousands = number / 1000;
            int last_thousands = thousands % 10;
            if (last_thousands == 1 && thousands != 11)
            {
                result += ConvertNumberToWordsLessThanThousand(thousands) + " тысяча ";
                result = result.Replace("один", "одна");
            }
            else if ((last_thousands == 2 || last_thousands == 3 || last_thousands == 4) && (thousands != 12 && thousands != 13 && thousands != 14))
            {
                result += ConvertNumberToWordsLessThanThousand(thousands) + " тысячи ";
                result = result.Replace("два", "две");
            }
            else
                result += ConvertNumberToWordsLessThanThousand(thousands) + " тысяч ";

            number %= 1000;
        }

        if (number > 0)
        {
            result += ConvertNumberToWordsLessThanThousand(number);
        }
        else
        {
            result = result.Trim();
        }

        return result + " " + GetRublesEnding(number);
    }

    static string ConvertNumberToWordsLessThanThousand(int number)
    {
        string result = "";

        int hundreds = number / 100;
        int lastTwoDigits = number % 100;

        if (hundreds > 0)
        {
            switch(hundreds)
            {
                case 1:
                    result += "сто" + " ";
                    break;
                case 2:
                    result += "двести" + " ";
                    break;
                case 3:
                case 4:
                    result += ones[hundreds] + "ста" + " ";
                    break;
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                    result += ones[hundreds] + "сот" + " ";
                    break;
            }
        }

        if (lastTwoDigits == 0)
        {
            result = result.Trim();
        }
        else if (lastTwoDigits < 10)
        {
            result += ones[lastTwoDigits];
        }
        else if (lastTwoDigits < 20)
        {
            result += teens[lastTwoDigits - 10];
        }
        else
        {
            int tensDigit = lastTwoDigits / 10;
            int onesDigit = lastTwoDigits % 10;
            result += tens[tensDigit];
            if (onesDigit > 0)
            {
                result += " " + ones[onesDigit];
            }
        }

        return result;
    }

    static string GetRublesEnding(int number)
    {
        int before_the_last_number = (number % 100) / 10; 
        int last_number = number % 10;
        if (last_number % 10 == 1 && before_the_last_number != 1)
        {
            return units[0];
        }
        else if (last_number % 10 >= 2 && last_number % 10 <= 4 && before_the_last_number != 1)
        {
            return units[1];
        }
        else
        {
            return units[2];
        }
    }

    static void Main()
    {
        Console.Write("Введите число (от 1 до 100000): ");
        string result = ConvertNumberToWords(inputNumber).Trim();
        result = char.ToUpper(result[0]) + result.Substring(1);
        Console.WriteLine(result);
    }
}
