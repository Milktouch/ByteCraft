using System;
using ByteCraft.Exceptions;
using ByteCraft.Data.Arithmetic;
using ByteCraft.Data.Equality;
using ByteCraft.Data.OtherQualities;

namespace ByteCraft.Data
{
    public class NumberValue : Value , Addition, Subtraction, Multiplication, Division, InEquality , Modulo
    {
        public readonly decimal value;
        public NumberValue(decimal value): base(ValueTypes.NUMBER)
        {
            this.value = value;
        }

        public Value Add(Value val)
        {
            if(!val.CanBeCastTo<NumberValue>())
            {
                throw new RuntimeError("Cannot add a non-number to a number");
            }
            NumberValue numVal = val.As<NumberValue>();
            return new NumberValue(this.value + numVal.value);
        }

        public override Value Copy()
        {
            return new NumberValue((decimal)this.value);
        }

        public Value Divide(Value val)
        {
            if(!val.CanBeCastTo<NumberValue>())
            {
                throw new RuntimeError("Cannot divide a number by a non-number");
            }
            NumberValue numVal = val.As<NumberValue>();
            if (numVal.value == 0)
            {
                throw new RuntimeError("Cannot divide by Zero");
            }
            return new NumberValue(this.value / numVal.value);
        }

        public BooleanValue GreaterThan(Value val)
        {
            if(!val.CanBeCastTo<NumberValue>())
            {
                throw new RuntimeError("Cannot compare a number to a non-number");
            }
            NumberValue numVal = val.As<NumberValue>();
            return new BooleanValue(this.value > numVal.value);
        }

        public BooleanValue GreaterThanOrEqual(Value val)
        {
            if(!val.CanBeCastTo<NumberValue>())
            {
                throw new RuntimeError("Cannot compare a number to a non-number");
            }
            NumberValue numVal = val.As<NumberValue>();
           return new BooleanValue(this.value >= numVal.value);
        }

        public BooleanValue LessThan(Value val)
        {
            if(!val.CanBeCastTo<NumberValue>())
            {
                throw new RuntimeError("Cannot compare a number to a non-number");
            }
            NumberValue numVal = val.As<NumberValue>();
            return new BooleanValue(this.value < numVal.value);
        }

        public BooleanValue LessThanOrEqual(Value val)
        {
            if(!val.CanBeCastTo<NumberValue>())
            {
                throw new RuntimeError("Cannot compare a number to a non-number");
            }
            NumberValue numVal = val.As<NumberValue>();
            return new BooleanValue(this.value <= numVal.value);
        }

        public Value Multiply(Value val)
        {
            if(!val.CanBeCastTo<NumberValue>())
            {
                throw new RuntimeError("Cannot multiply a number by a non-number");
            }
            NumberValue numVal = val.As<NumberValue>();
            return new NumberValue(this.value * numVal.value);
        }

        public Value Subtract(Value val)
        {
            if(!val.CanBeCastTo<NumberValue>())
            {
                throw new RuntimeError("Cannot subtract a number by a non-number");
            }
            NumberValue numVal = val.As<NumberValue>();
            return new NumberValue(this.value - numVal.value);
        }
        
        public Value Modulo(Value val)
        {
            if(!val.CanBeCastTo<NumberValue>())
            {
                throw new RuntimeError("Cannot modulo a number by a non-number");
            }
            NumberValue numVal = val.As<NumberValue>();
            return new NumberValue(this.value % numVal.value);
        }

        public override string ToString()
        {
            return this.value.ToString();
        }

        protected override object? GetValue()
        {
            return value;
        }
    }
}