using System;
using ByteCraft.Exceptions;
using ByteCraft.Data.Arithmetic;
using System.Runtime.CompilerServices;
using ByteCraft.Data.Equality;

namespace ByteCraft.Data
{
    public class NumberValue : Value<decimal> , Addition<decimal>, Subtraction<decimal>, Multiplication<decimal>, Division<decimal>, InEquality<decimal>
    {
        public NumberValue(decimal value): base(value, ValueTypes.NUMBER)
        {
        }

        public Value<decimal> Add(Value<decimal> val)
        {
            return new NumberValue(this.value + val.value);
        }

        public Value<decimal> Divide(Value<decimal> val)
        {
            if (val.value == 0)
            {
                throw new RuntimeError("Division by zero");
            }
            return new NumberValue(this.value / val.value);
        }

        public Value<bool> GreaterThan(Value<decimal> value)
        {
            return new BooleanValue(this.value > value.value);
        }

        public Value<bool> GreaterThanOrEqual(Value<decimal> value)
        {
            return new BooleanValue(this.value >= value.value);
        }

        public Value<bool> LessThan(Value<decimal> value)
        {
            return new BooleanValue(this.value < value.value);
        }

        public Value<bool> LessThanOrEqual(Value<decimal> value)
        {
            return new BooleanValue(this.value <= value.value);
        }

        public Value<decimal> Multiply(Value<decimal> val)
        {
            return new NumberValue(this.value * val.value);
        }

        public Value<decimal> Subtract(Value<decimal> val)
        {
            return new NumberValue(this.value - val.value);
        }
    }
}