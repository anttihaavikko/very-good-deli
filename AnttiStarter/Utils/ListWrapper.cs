using System.Collections.Generic;
using System.Linq;
using Godot;

namespace AnttiStarter.Utils;

public partial class ListWrapper : Node
{
    public List<T> Get<T> () where T : Node
    {
        return GetChildren().Where(c => c is T).Cast<T>().ToList();
    }
}