using System;

public class DialogResult
{
    /// <summary>
    /// The actual value returned by the dialog.
    /// </summary>
    public object Value { get; }

    /// <summary>
    /// The type of the returned value.
    /// </summary>
    public Type ValueType { get; }

    public DialogResult(object value)
    {
        Value = value;
        ValueType = value?.GetType() ?? typeof(object);
    }

    /// <summary>
    /// Strongly-typed access to the result.
    /// Throws if the type does not match.
    /// </summary>
    public T GetValue<T>()
    {
        if (Value is T typedValue)
        {
            return typedValue;
        }
        throw new InvalidCastException($"Cannot cast result of type {ValueType} to {typeof(T)}.");
    }

    /// <summary>
    /// Tries to get the value without throwing if the type mismatches.
    /// </summary>
    public bool TryGetValue<T>(out T result)
    {
        if (Value is T typedValue)
        {
            result = typedValue;
            return true;
        }
        result = default!;
        return false;
    }

    public override string ToString() => Value?.ToString() ?? string.Empty;
}
