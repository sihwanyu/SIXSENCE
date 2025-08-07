using System.Collections.Generic;

public static class SceneHistory
{
    private static Stack<string> history = new Stack<string>();

    public static void Push(string sceneName)
    {
        history.Push(sceneName);
    }

    public static string Pop()
    {
        return history.Count > 0 ? history.Pop() : null;
    }
}



