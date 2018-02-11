using System;
using System.Diagnostics.Contracts;

namespace IrregularMachine.Utils {
    public static class MathUtils {
        public static double ApproachFactor(double currentValue, double targetValue, double factor, double roundWhen, double maxSpeed) {
            Contract.Requires(factor > float.Epsilon);
            Contract.Requires(roundWhen > float.Epsilon);
            
            if (double.IsNaN(currentValue) || double.IsNaN(targetValue) || double.IsNaN(factor) || double.IsNaN(roundWhen))
                return currentValue;
            
            if (currentValue.Equals(targetValue) || Math.Abs(targetValue - currentValue) <= roundWhen) {
                return targetValue;
            } else {
                return currentValue + Clamp((targetValue - currentValue) * factor, -maxSpeed, maxSpeed);
            }
        }

        public static double Clamp(double value, double min, double max) {
            if (double.IsNaN(value) || double.IsNaN(min) || double.IsNaN(max))
                return value;
            else if (value > max)
                return max;
            else if (value < min)
                return min;
            else
                return value;
        }
    }
}