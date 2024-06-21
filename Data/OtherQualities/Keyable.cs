namespace ByteCraft.Data.OtherQualities;

public interface Keyable
{
    public Value GetValue(Value key);
    public void SetValue(Value key, Value value);
}