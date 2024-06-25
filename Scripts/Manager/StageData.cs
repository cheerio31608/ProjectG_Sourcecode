using System;

[Serializable]
public class StageData
{
    public int StageNum = 1;
    public bool IsBossStage = false;

    public PlayerStat PlayerStat;
    public EnemyStat EnemyStat;
    public BossStat BossStat;

}
