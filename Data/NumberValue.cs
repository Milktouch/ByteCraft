using System;
using ByteCraft.Exceptions;
using ByteCraft.Data.Arithmetic;
using ByteCraft.Data.Equality;

namespace ByteCraft.Data
{
    public class NumberValue : Value , IAddition<NumberValue>, Subtraction<NumberValue>, Multiplication<NumberValue>, Division<NumberValue>, InEquality<NumberValue>
    {
        public NumberValue(decimal value): base(value, ValueTypes.NUMBER)
        {
        }

        public NumberValue Add(NumberValue val)
        {
            return new NumberValue(this.GetNumber() + val.GetNumber());
        }

        public override Value Copy()
        {
            return new NumberValue(this.value);
        }

        public NumberValue Divide(NumberValue val)
        {
            if (val.GetNumber() == 0)
            {
                throw new RuntimeError("Cannot divide by Zero");
            }
            return new NumberValue(this.GetNumber() / val.GetNumber());
        }

        public decimal GetNumber()
        {
            return this.value;
        }

        public BooleanValue GreaterThan(NumberValue value)
        {
            return new BooleanValue(this.GetNumber() > value.GetNumber());
        }

        public BooleanValue GreaterThanOrEqual(NumberValue value)
        {
           return new BooleanValue(this.GetNumber() >= value.GetNumber());
        }

        public BooleanValue LessThan(NumberValue value)
        {
            return new BooleanValue(this.GetNumber() < value.GetNumber());
        }

        public BooleanValue LessThanOrEqual(NumberValue value)
        {
            return new BooleanValue(this.GetNumber() <= value.GetNumber());
        }

        public NumberValue Multiply(NumberValue val)
        {
            return new NumberValue(this.GetNumber() * val.GetNumber());
        }

        public NumberValue Subtract(NumberValue val)
        {
            return new NumberValue(this.GetNumber() - val.GetNumber());
        }
    }
}