namespace SharedScripts.IFF
{
    public enum Iff{
        None     =0,
        Player   =1,
        Enemy    =2,
        Neutral  =9999,
        Everyone =99999
    }

    public static class IffHelpers
    {
        public static bool CanTargetIff(this Iff shooterIff,Iff targetIff){
            if(shooterIff==Iff.Everyone){
                return true;
            }
            if(targetIff==Iff.Neutral || shooterIff==Iff.None ||targetIff==Iff.None){
                return false;
            }
	
            return shooterIff!=targetIff;
        }
    }
}
