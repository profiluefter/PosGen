using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace PosGen
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            ProjectGenerator.CreateProject(FeatureSelection.Of("ProjectName", "./brigitte/project-location/", "ant", "packet.name.and",
                "y"));
            //InputFeatures();
        }

        //TODO: if maven: group id
        //TODO: setup junit4
        private static FeatureSelection InputFeatures()
        {
            //Print POSGEN banner
            Console.WriteLine(strings.banner);

            string projectName;
            string projectLocation;
            string projectType;
            string packageName;
            string insertCliGui;

            var valid = false;
            do
            {
                #region projectName

                do
                {
                    Console.Write("Name of the project: ");
                    projectName = Console.ReadLine();

                    Debug.Assert(projectName != null, nameof(projectName) + " != null");
                    valid = projectName.Length > 0;
                } while (!valid);

                #endregion

                #region projectLocation

                do
                {
                    Console.Write("Project location(./" + ToKebabCase(projectName) + "/): ");
                    projectLocation = Console.ReadLine();

                    Debug.Assert(projectLocation != null, nameof(projectLocation) + " != null");
                    if (projectLocation.Length < 1)
                        projectLocation = ToKebabCase(projectName);

                    valid = projectLocation.Length > 0;
                } while (!valid);

                #endregion

                #region projectType

                do
                {
                    Console.Write("Project type(ANT, maven): ");
                    projectType = Console.ReadLine();

                    Debug.Assert(projectType != null, nameof(projectType) + " != null");
                    switch (projectType.ToLower())
                    {
                        case "ant":
                        case "maven":
                            valid = true;
                            break;
                        default:
                            valid = false;
                            break;
                    }

                    projectType = projectType.ToLower();
                } while (!valid);

                #endregion

                #region packageName

                do
                {
                    Console.Write("Name of the package: ");
                    packageName = Console.ReadLine();

                    Debug.Assert(projectName != null, nameof(projectName) + " != null");
                    valid = projectName.Length > 0;
                } while (!valid);

                #endregion

                #region insertCliGui

                do
                {
                    Console.Write("Insert CLIGui(Y, n): ");
                    insertCliGui = Console.ReadLine();

                    Debug.Assert(insertCliGui != null, nameof(insertCliGui) + " != null");
                    switch (insertCliGui.ToLower())
                    {
                        case "y":
                        case "n":
                            valid = true;
                            break;
                        default:
                            valid = false;
                            break;
                    }

                    insertCliGui = insertCliGui.ToLower();
                } while (!valid);

                #endregion

                #region confirmed

                Console.WriteLine("\n\nProject name: " + projectName + "\nProject location: " + projectLocation +
                                  "\nProject type: " + projectType + "\nInsert CLIGui: " + insertCliGui);
                string confirmed;
                do
                {
                    Console.Write("Is this information correct(Y, n):");
                    confirmed = Console.ReadLine();

                    Debug.Assert(confirmed != null, nameof(confirmed) + " != null");
                    switch (confirmed.ToLower())
                    {
                        case "y":
                        case "n":
                            valid = true;
                            break;
                        default:
                            valid = false;
                            break;
                    }

                    confirmed = confirmed.ToLower();
                } while (!valid);

                #endregion

                valid = confirmed.Contains("y");
            } while (!valid);

            return FeatureSelection.Of(projectName, projectLocation, projectType, packageName, insertCliGui);
        }

        private static string ToKebabCase(string input)
        {
            var stringBuilder = new StringBuilder();
            foreach (var character in input)
            {
                if (char.IsLower(character))
                    stringBuilder.Append(character);
                else
                {
                    stringBuilder.Append("-");
                    stringBuilder.Append(char.ToLower(character));
                }
            }

            if (input.Length > 0 && char.IsUpper(input[0]))
            {
                stringBuilder.Remove(0, 1);
            }

            return stringBuilder.ToString();
        }
    }
}