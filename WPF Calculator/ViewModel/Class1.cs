using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm;
using System.Globalization;

namespace WPF_Calculator.ViewModel
{
    public partial class CalculatorClass : ObservableObject
    {
        public CalculatorClass()
        {
            currentNumber = 0;
            displayedItem = 0;
            currentOperator = null;
        }

        [ObservableProperty]
        private decimal displayedItem;

        private decimal? currentNumber = 0;

        bool isDecimal = false;
        
        private enum Operators
        {
            Addition = '+',
            Subtraction = '-',
            Multiplication = '*',
            Division = '/',
        }

        private Operators? currentOperator;


        [RelayCommand]
        public void AddNumber(string number)
        {
            if(isDecimal)
            {
                string str = DisplayedItem.ToString();
                if (!str.Contains(NumberFormatInfo.CurrentInfo.NumberDecimalSeparator))
                    str += NumberFormatInfo.CurrentInfo.NumberDecimalSeparator;
                DisplayedItem = decimal.Parse(str + number);
            }

            DisplayedItem *= 10;
            DisplayedItem += decimal.Parse(number);
        }

        [RelayCommand]
        public void AddOperator(string operator1)
        {
            switch (operator1)
            {
                case "+":
                    currentOperator = Operators.Addition;
                    break;
                case "-":
                    currentOperator = Operators.Subtraction;
                    break;
                case "/":
                    currentOperator = Operators.Division;
                    break;
                case "*":
                    currentOperator = Operators.Multiplication;
                    break;
                default:
                    return;
            }

            currentNumber = DisplayedItem;
            DisplayedItem = 0;
            isDecimal = false;
        }
        
        [RelayCommand]
        public void ShowSum()
        {
            if (currentNumber == null) return;

            switch(currentOperator)
            {
                case Operators.Addition:
                    DisplayedItem = (decimal)(currentNumber + DisplayedItem);
                    currentNumber = null;
                    break;
                case Operators.Subtraction:
                    DisplayedItem = (decimal)(currentNumber - DisplayedItem);
                    currentNumber = null;
                    break;
                case Operators.Division:
                    DisplayedItem = (decimal)(currentNumber / DisplayedItem);
                    currentNumber = null;
                    break;
                case Operators.Multiplication:
                    DisplayedItem = (decimal)(currentNumber * DisplayedItem);
                    currentNumber = null;
                    break;
            }
        }

        [RelayCommand]
        public void ClearResult()
        {
            currentNumber = null;
            DisplayedItem = 0;
            currentOperator = null;
        }

        [RelayCommand]
        public void Inverse()
        {
            DisplayedItem = -DisplayedItem;
        }
        
        [RelayCommand]
        public void Decimal()
        {
            isDecimal = true;
        }

        [RelayCommand]
        public void ClickOnPercentage()
        {
            if (currentNumber is null || currentOperator is null) return;

            switch (currentOperator)
            {
                case Operators.Addition:
                    DisplayedItem = (decimal)(currentNumber + DisplayedItem / 100 * currentNumber);
                    break;
                case Operators.Subtraction:
                    DisplayedItem = (decimal)(currentNumber - DisplayedItem / 100 * currentNumber);
                    break;
                case Operators.Multiplication:
                    DisplayedItem = (decimal)(currentNumber * DisplayedItem / 100);
                    break;
                case Operators.Division:
                    DisplayedItem = (decimal)(currentNumber / DisplayedItem * 100);
                    break;
            }
        }

    }
}
