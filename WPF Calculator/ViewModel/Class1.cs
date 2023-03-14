using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

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
    

        [ObservableProperty]
        private int inputValue;
        
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
            }

            currentNumber = DisplayedItem;
            DisplayedItem = 0;
        }
        
        [RelayCommand]
        public void ShowSum()
        {
            switch(currentOperator)
            {
                case Operators.Addition:
                    DisplayedItem = (decimal)(currentNumber + DisplayedItem);
                    currentNumber = null;
                    break;
                case Operators.Subtraction:
                    DisplayedItem = (decimal)(currentNumber - DisplayedItem);
                    break;
                case Operators.Division:
                    DisplayedItem = (decimal)(currentNumber / DisplayedItem);
                    break;
                case Operators.Multiplication:
                    DisplayedItem = (decimal)(currentNumber * DisplayedItem);
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
    

    }
}
