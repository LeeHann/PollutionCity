public enum ResourceType {
    None,
    Money,
    Paper,
    Can,
    Glass,
    Plastic
}

public static class ReosourcTypeFormatter {
    public static string Rsc2Str(this ResourceType val) {
        string ret = "";
        switch (val)
        {
            case ResourceType.None:
                break;
            
            case ResourceType.Money:
                ret = "돈";
                break;
            
            case ResourceType.Paper:
                ret = "종이";
                break;
            
            case ResourceType.Can:
                ret = "캔";
                break;
            
            case ResourceType.Glass:
                ret = "유리";
                break;
            
            case ResourceType.Plastic:
                ret = "플라스틱";
                break;
        }
        return ret;
    }
}
