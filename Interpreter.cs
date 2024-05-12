public class Interpreter{
    public readonly int currentLine;
    public readonly List<string> code;
    public Interpreter(List<string> code){
        
    }
    public Interpreter(){
        
    }
    public void GenerateRuntime(){
        List<string> dataSection = new List<string>();
        int dataStart = 0;
        List<string> codeSection = new List<string>();
        int codeStart = 0;
        for(int i = 0; i < code.Count; i++){
            if(code[i].Trim().Equals("#data:")){
                dataStart = i;
                break;
            }
        }
        for(int i = 0; i < code.Count; i++){
            if(code[i].Trim().Equals("#code:")){
                codeStart = i;
                break;
            }
        }
        if(dataStart<codeStart){
            for(int i = dataStart+1; i < codeStart; i++){
                dataSection.Add(code[i]);
            }
            for(int i = codeStart+1; i < code.Count; i++){
                codeSection.Add(code[i]);
            }
        }
        else{
            for(int i = codeStart+1; i < dataStart; i++){
                codeSection.Add(code[i]);
            }
            for(int i = dataStart+1; i < code.Count; i++){
                dataSection.Add(code[i]);
            }
        }
    }
}