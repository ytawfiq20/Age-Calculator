
using System;
using System.Runtime.InteropServices.Marshalling;

namespace Birth
{
    public class NumberOfDays
    {
        public static void Main(string[] args)
        {
            NumberOfDays n = new NumberOfDays();
            n.CalculateAge();
        }

        private string[] ReverseArray(string[] arr)
        {
            for (int i = 0, j = arr.Length - 1; i <= j; i++, j--)
            {
                string temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
            }
            return arr;
        }

        private string[] CurrentDate()
        {
            DateTime dateTime = DateTime.Now;
            string[] d1 = dateTime.ToString().Split(" ");
            return d1[0].Split("-");
        }

        private string? TakeUserDateOfBirth()
        {
            Console.WriteLine("Enter your date of birth with one of these formats");
            Console.WriteLine("dd/mm/yyyy");
            Console.WriteLine("yyyy/mm/dd");
            Console.WriteLine("dd-mm-yyyy");
            Console.WriteLine("yyyy-mm-dd");
            Console.WriteLine("---------------");
            string DateOfBirth = Console.ReadLine()??"3/3/2005";
            return DateOfBirth;
        }

        private string[] UserDateOfBirth(string DateOfBirth)
        {
            if (DateOfBirth[1] == '/' || DateOfBirth[2] == '/')
            {
                string[] d = DateOfBirth.Split("/");
                if (DateOfBirth[2] == '/' || DateOfBirth[1] == '/')
                {
                    return ReverseArray(d);
                }
                return d;
            }
            else if (DateOfBirth[1] == '-' || DateOfBirth[2] == '-')
            {
                string[] d = DateOfBirth.Split("-");
                if (DateOfBirth[2] == '-' || DateOfBirth[1] == '-')
                {
                    return ReverseArray(d);
                }
                return d;
            }
            throw new Exception("Unknown format");
        }

        private bool IsLeapYear(int year)
        {
            return (year % 4 == 0 && year % 100 != 0) || year % 400 == 0;
        }

        private int MonthNumberOfDays(int year, int Month)
        {
            if (Month == 1 || Month == 3 || Month == 5 || Month == 7 || Month == 8 || Month == 10 || Month == 12)
            {
                return 31;
            }
            else if (Month == 4 || Month == 6 || Month == 9 || Month == 11)
            {
                return 30;
            }
            return IsLeapYear(year) ? 29 : 28;
        }

        private int DaysBetweenBirthYearAndCurrentYear(int year)
        {
            int currYear = Int32.Parse(CurrentDate()[0]);
            int ans = 0;
            for(int i= year + 1; i<currYear; i++)
            {
                if (IsLeapYear(i))
                {
                    ans += 366;
                }
                else
                {
                    ans += 365;
                }
            }
            return ans;
        }

        private int DaysInCurrentYear()
        {
            int currYear = Int32.Parse(CurrentDate()[0]);
            int currMonth = Int32.Parse(CurrentDate()[1]);
            int currDay = Int32.Parse(CurrentDate()[2]);
            int ans = 0;
            for(int i = currMonth-1; i>0; i--)
            {
                ans += MonthNumberOfDays(currYear, i);
            }
            ans += currDay;
            return ans;
        }

        private int DaysInBirthYear(string DateOfBirth)
        {
            string[] d = UserDateOfBirth(DateOfBirth);
            int birthYear = Int32.Parse(d[0]);
            int birthMonth = Int32.Parse(d[1]);
            int birthDay = Int32.Parse(d[2]);
            int ans = 0;
            for(int i=12; i>birthMonth; i--)
            {
                ans += MonthNumberOfDays(birthYear, i);
            }
            ans += MonthNumberOfDays(birthYear, birthMonth) -  birthDay;
            return ans;
        }

        private void PrintAge(string DateOfBirth)
        {
            string[] d = UserDateOfBirth(DateOfBirth);
            int birthYear = Int32.Parse(d[0]);
            int currYear = Int32.Parse(CurrentDate()[0]);
            int years = (currYear - 1) - (birthYear);
            int birthMonth = Int32.Parse(d[1]);
            int currMonth = Int32.Parse(CurrentDate()[1]);
            int months = (12 - birthMonth + currMonth - 1);
            //Console.WriteLine(months);
            if(months>=12)
            {
                years++;
                months -= 12;
            }
            int birthDay = Int32.Parse(d[2]);
            int currDay = Int32.Parse(CurrentDate()[2]);
            int days = (MonthNumberOfDays(birthYear, birthMonth) - birthDay) + currDay;
            
            //Console.WriteLine(days);
            if(days>= MonthNumberOfDays(birthYear, birthMonth))
            {
                months += 1;
                if (months >= 12)
                {
                    years++;
                    months -= 12;
                }
                days -= MonthNumberOfDays(birthYear, birthMonth);
            }
            if (birthYear < 0)
            {
                years--;
            }
            Console.WriteLine($"Age: {years}(years), {months}(months), {days}(days)");

        }

        private string GetDay(string DateOfBirth)
        {
            string[] Days = { "Saturday", "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday"};
            string[] d1 = UserDateOfBirth(DateOfBirth);
            string[] d2 = DateTime.Today.ToString("D").Split(",");
            string currDay = d2[0];
            int d = 0;
            for(int i=0; i<Days.Length; i++)
            {
                if (Days[i] == currDay)
                {
                    d = i;
                }
            }
            int inputYear = Int32.Parse(d1[0]);
            int allDays = DaysBetweenBirthYearAndCurrentYear(inputYear) + DaysInCurrentYear() + DaysInBirthYear(DateOfBirth);
            int day = (7-(allDays%7)+d)%7;
            return Days[day];
        }

        private void CalculateAge()
        {
            Console.WriteLine(" ------------------------");
            Console.WriteLine("|                        |");
            Console.WriteLine("| Age Calculator For Fun |");
            Console.WriteLine("|                        |");
            Console.WriteLine(" ------------------------");
            
            string userInput = TakeUserDateOfBirth();
            string[] currDate = CurrentDate();
            string[] DateofBirth = UserDateOfBirth(userInput);
            int birthYear = Int32.Parse(DateofBirth[0]);
            int days = DaysBetweenBirthYearAndCurrentYear(birthYear) + DaysInCurrentYear() + DaysInBirthYear(userInput);
            var years = Math.Floor(days / 365.25);
            int inputYear = Int32.Parse(UserDateOfBirth(userInput)[0]);
            if (inputYear < 0)
            {
                years--;
            }
            Console.WriteLine(userInput+" was ("+GetDay(userInput)+")");
            PrintAge(userInput);
            Console.WriteLine("Number of Years = " + years);
            Console.WriteLine("Number of Months = " + (int)(days / 30.44f));
            Console.WriteLine("Number of Days = " + days);
            Console.WriteLine("Number of Hours = " + days * 24);
            Console.WriteLine("Number of Minutes = " + days * 24 * 60.0);
            Console.WriteLine("Number of Seconds = " + (days * 24 * 60.0 * 60));
        }



    }
}
