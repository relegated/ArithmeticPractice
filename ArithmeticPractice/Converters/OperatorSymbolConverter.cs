using ArithmeticPractice.Enums;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace ArithmeticPractice.Converters
{
    public class OperatorSymbolConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var operatorType = (EArithmeticOperator)value;
            switch (operatorType)
            {
                case EArithmeticOperator.Add:
                    return "+";
                case EArithmeticOperator.Subtract:
                    return "-";
                case EArithmeticOperator.Multiply:
                    return "×";
                case EArithmeticOperator.Divide:
                    return "÷";
                case EArithmeticOperator.Exponent:
                case EArithmeticOperator.Logarthim:
                case EArithmeticOperator.Undefined:
                default:
                    return string.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
