// 1. Originator - объект, состояние которого сохраняется
public class TextEditor
{
    private string _content;

    public void Write(string text)
    {
        _content += text;
        Console.WriteLine($"Текст: {_content}");
    }

    // Сохраняет текущее состояние в Memento
    public Memento Save()
    {
        return new Memento(_content);
    }

    // Восстанавливает состояние из Memento
    public void Restore(Memento memento)
    {
        _content = memento.GetState();
        Console.WriteLine($"Восстановлено: {_content}");
    }
}

// 2. Memento - хранит состояние TextEditor
public class Memento
{
    private readonly string _state;

    internal Memento(string state)
    {
        _state = state;
    }

    public string GetState()
    {
        return _state;
    }
}

// 3. Caretaker - управляет историей сохранений
public class History
{
    private Stack<Memento> _mementos = new Stack<Memento>();

    public void Push(Memento memento)
    {
        _mementos.Push(memento);
    }

    public Memento Pop()
    {
        if (_mementos.Count == 0)
            throw new InvalidOperationException("История пуста.");
        return _mementos.Pop();
    }
}

// Пример использования
class Program
{
    static void Main()
    {
        var editor = new TextEditor();
        var history = new History();

        editor.Write("Привет, ");
        history.Push(editor.Save()); // Сохраняем состояние

        editor.Write("мир!");
        history.Push(editor.Save());

        editor.Write(" Это новый текст.");

        // Откатываемся к предыдущему состоянию
        editor.Restore(history.Pop());
        // Откатываемся ещё раз
        editor.Restore(history.Pop());
    }
}
