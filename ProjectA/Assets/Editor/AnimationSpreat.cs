using System.IO;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor
{
    public class AnimationSpreat : AssetPostprocessor
    {
        private void OnPreprocessModel()
        {
            if (assetPath.Contains(ToolSettings.Instance.Mark))
            {
                ModelImporter modelImporter = assetImporter as ModelImporter;
                //禁止材质导入
                modelImporter.materialImportMode = ModelImporterMaterialImportMode.None;

                modelImporter.animationType = ModelImporterAnimationType.Human;
                //创建Avatar
                modelImporter.avatarSetup = ModelImporterAvatarSetup.NoAvatar;
            }
        }

        private void OnPostprocessModel(UnityEngine.GameObject gameObject)
        {
            string name = gameObject.name;

            EditorApplication.delayCall += () =>
            {
                if (assetPath.Contains(ToolSettings.Instance.Mark))
                {
                    Debug.Log("动画资源分离");

                    var assets = AssetDatabase.LoadAllAssetRepresentationsAtPath(assetPath);
                    //清除动画内部资源的标记

                    name = name.Split(ToolSettings.Instance.Mark)[1];

                    foreach (var asset in assets)
                    {
                        var newClip = UnityEngine.Object.Instantiate(asset);
                        var curClip = newClip as AnimationClip;

                        if (asset is AnimationClip)
                        {
                            //设置动画的烘焙位置
                            AnimationClipSettings clipSettings = AnimationUtility.GetAnimationClipSettings(curClip);
                            clipSettings.loopBlendOrientation = true;
                            clipSettings.loopBlendPositionXZ = true;
                            clipSettings.loopBlendPositionY = true;
                            clipSettings.keepOriginalOrientation = true;
                            clipSettings.keepOriginalPositionXZ = true;
                            clipSettings.keepOriginalPositionY = true;
                            AnimationUtility.SetAnimationClipSettings(curClip, clipSettings);
                            AssetDatabase.CreateAsset(curClip, ToolSettings.Instance.AnimFolder + "/" + name + ".anim");
                            AssetDatabase.SaveAssets();
                            AssetDatabase.Refresh();
                        }

                        if (assetPath != ToolSettings.Instance.FBXfolder)
                        {
                            Debug.Log("移动资源" + assetPath);
                            //去除移动标记的资源
                            string assetName = Path.GetFileName(assetPath).Replace(ToolSettings.Instance.Mark, "");
                            AssetDatabase.MoveAsset(assetPath, ToolSettings.Instance.FBXfolder + "/" + assetName); ;
                        }
                    }
                }
            };
        }
    }
}