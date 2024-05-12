public class Artifact{
    public readonly string name;
    public readonly Dictionary<string,Variable> variables = new();
    public readonly ArtifactDefinition definition;
    public Artifact(string name, ArtifactDefinition definition){
        this.name = name;
        this.definition = definition;
    }


}