using Sirenix.OdinInspector;
using Sirenix.Utilities;

[LabelWidth(100)]
public class ToolSettings : GlobalConfig<ToolSettings>
{
    [LabelWidth(90)]
    [FolderPath]
    [LabelText("动画资源文件夹")]
    public string AnimFolder;

    [LabelWidth(90)]
    
    [LabelText("动画资源标记")]
    public string Mark;

    [LabelWidth(75)]
    [FolderPath]
    [LabelText("Avatarfolder")]
    public string Avatarfolder;

    [LabelWidth(70)]
    [LabelText("FBXfolder")]
    [FolderPath]
    public string FBXfolder;


}