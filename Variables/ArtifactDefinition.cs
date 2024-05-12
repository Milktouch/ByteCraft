public class ArtifactDefinition{
    public readonly string name;
    public Dictionary<string,string> variables = new();
    public ArtifactDefinition(string name){
        this.name = name;
    }
}
