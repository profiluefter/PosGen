using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;

namespace PosGen
{
    public class ProjectGenerator
    {
        //PROJECT_LOCATION_FIRST
        //PROJECT_NAME
        public static void CreateProject(FeatureSelection selection)
        {
            switch (selection.ProjectType)
            {
                case FeatureSelection.ProjectTypes.Ant:
                    Write("build.xml", GetStringResource("PosGen.res.build.xml"), selection);
                    Write("manifest.mf", GetStringResource("PosGen.res.manifest.mf"), selection);
                    Write("nbproject/build-impl.xml", GetStringResource("PosGen.res.build-impl.xml"), selection);
                    Write("nbproject/project.properties", GetStringResource("PosGen.res.project.properties"),
                        selection);
                    Write("nbproject/project.xml", GetStringResource("PosGen.res.project.xml"), selection);
                    Write("nbproject/private/private.properties", GetStringResource("PosGen.res.private.properties"),
                        selection);

                    var srcPath = Path.Combine(
                        selection.ProjectLocation,
                        "src",
                        selection.PackageName.Replace('.', Path.DirectorySeparatorChar)
                    );

                    Directory.CreateDirectory(srcPath);
                    Directory.CreateDirectory(Path.Combine(selection.ProjectLocation, "test"));

                    File.WriteAllText(Path.Combine(srcPath, "Main.java"),
                        GetStringResource(selection.InsertCliGui ? "PosGen.res.CLIMain.java" : "PosGen.res.Main.java")
                            .Replace("{{PACKAGE_NAME}}", selection.PackageName)
                            .Replace("{{PROJECT_NAME}}", selection.ProjectName));

                    if (selection.InsertCliGui)
                    {
                        File.AppendAllText(Path.Combine(selection.ProjectLocation, "nbproject/project.properties"),
                            @"\" + Environment.NewLine + @"${file.reference.CLIGui.jar}" + Environment.NewLine +
                            @"file.reference.CLIGui.jar=libs\\CLIGui.jar");
                        Directory.CreateDirectory(Path.Combine(selection.ProjectLocation, "libs", "CopyLibs"));
                        Write("libs/nblibraries.properties", GetStringResource("PosGen.res.nblibraries.properties"),
                            selection);

                        File.WriteAllBytes(Path.Combine(Path.Combine(selection.ProjectLocation, "libs", "CLIGui.jar")),
                            GetBinaryResource("PosGen.res.CLIGui.jar"));
                        File.WriteAllBytes(
                            Path.Combine(Path.Combine(selection.ProjectLocation, "libs", "CopyLibs",
                                "org-netbeans-modules-java-j2seproject-copylibstask.jar")),
                            GetBinaryResource("PosGen.res.copylibs.jar"));
                    }

                    break;
                case FeatureSelection.ProjectTypes.Maven:
                    throw new NotImplementedException();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static void Write(string path, string content, FeatureSelection selection)
        {
            var filePath = Path.Combine(selection.ProjectLocation, path);
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            var directoryName = Path.GetDirectoryName(selection.ProjectLocation).Split(Path.DirectorySeparatorChar)
                .Last();
            File.WriteAllText(filePath,
                content.Replace("{{PROJECT_NAME}}", selection.ProjectName)
                    .Replace("{{PROJECT_LOCATION_FIRST}}", directoryName)
                    .Replace("{{PACKAGE_NAME}}", selection.PackageName));
        }

        private static string GetStringResource(string path)
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path))
            {
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        private static byte[] GetBinaryResource(string path)
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path))
            {
                var data = new byte[stream.Length];
                stream.Read(data, 0, (int) stream.Length);
                return data;
            }
        }

        //File.WriteAllText(path,content);
    }
}