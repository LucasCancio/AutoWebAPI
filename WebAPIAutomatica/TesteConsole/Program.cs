#region
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using VeeSharpTemplate;
using File = VeeSharpTemplate.File;

#endregion

namespace TesteConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //CreateFile();
            //SetProject();
            GenerateSolution();
        }  
       
        private static void GenerateSolution()
        {
            var previews = new List<string>();
            // "Projeto inicial" = projeto onde ficavam os arquivos que serão criados no projeto destino
            //Também é o nome da pasta que é criado
            var solution = new Solution("C:\\Users\\Lucas\\Desktop\\Nova pasta\\teste2.vstsln");
            try
            {
                
                var file = new File(solution, "ClasseTeste4");
                solution.Files.Add(file);

                var code = "using System;\r\nnamespace TesteConsole{\r\n\r\nclass " + file.FileName + "{ public void Testar(){var a = new ClasseTeste3().Testar();} \r\n\r\n}\r\n}";
                

                solution.Generate(previews,solution,code);
            }
            catch (Exception exception)
            {
                //foreach (var preview in previews)
                //    new PreviewWindow(false, "Generation Error", exception.Message + Environment.NewLine + preview).Show();
            }

            SyncFromSolution();
        }










        #region

        private readonly Solution _solution;

        //private bool _saveChoiceMade;

        //public Program()
        //{
        //    _solution = new Solution("C:\\Users\\Lucas\\Desktop\\Nova pasta\\teste.vstsln");        
        //    SyncFromSolution();
        //}

        private static void SyncFromSolution()
        {
            try
            {
                var solution = new Solution("C:\\Users\\Lucas\\Desktop\\Nova pasta\\teste.vstsln");
                //while (listBoxFiles.Items.Count > 5) listBoxFiles.Items.RemoveAt(5);

                foreach (var templateFile in solution.Files)
                {
                    //var item = new ListBoxItem
                    //{
                    //    Content = templateFile.TemplateName,
                    //    FontSize = 18,
                    //    FontStyle = FontStyles.Italic
                    //};

                    //listBoxFiles.Items.Add(item);

                    var file = templateFile;
                    //item.MouseDoubleClick += (sender, args) =>
                    //{
                    //    var editorWindow = new EditorWindow(file);
                    //    editorWindow.Show();
                    //    SyncFromSolution();
                    //};
                }

                var solutionName = Path.GetFileNameWithoutExtension(solution.Path);

                //textBlockTitle.Text = solutionName;

                //if (string.IsNullOrEmpty(_solution.ProjectFileName)) textBlockProject.Text = "no project set";
                //else textBlockProject.Text = "current project: " + _solution.ProjectFileName;

                //Title = string.Format("{0} - VeeSharpTemplate", solutionName);
            }
            catch (Exception exception)
            {
                //MessageBox.Show(exception.Message);
            }
        }

        //private void ButtonCreateFileClick(object sender, RoutedEventArgs e) { CreateFile(); }
        //private void ButtonSetProject(object sender, RoutedEventArgs e) { SetProject(); }
        //private void ButtonSaveClick(object sender, RoutedEventArgs e) { SaveSolution(); }
        //private void ButtonGenerateClick(object sender, RoutedEventArgs e) { GenerateSolution(); }
        //private void MetroWindowClosing(object sender, CancelEventArgs e)
        //{
        //    //if (_saveChoiceMade) return;

        //    //e.Cancel = true;
        //    //new ChoiceWindow("save before closing?", "yes", () =>
        //    //{
        //    //    SaveSolution();
        //    //    Close();
        //    //}, "no", Close).Show();
        //    //_saveChoiceMade = true;
        //}
        private static void CreateFile()
        {
            try
            {
                var solution = new Solution("C:\\Users\\Lucas\\Desktop\\Nova pasta\\teste.vstsln");
                var templateFile = Utils.FileCreateFromSaveDialog(solution);
                if (templateFile == null) return;
                solution.AddNewFile(templateFile);
            }
            catch (Exception exception)
            {
                // MessageBox.Show(exception.Message);
            }

            SyncFromSolution();
        }
        private void SaveSolution()
        {
            try
            {
                _solution.SaveToFile();
            }
            catch (Exception exception)
            {
                //MessageBox.Show(exception.Message);
            }

            SyncFromSolution();
        }
        private static void SetProject()
        {
            var solution = new Solution("C:\\Users\\Lucas\\Desktop\\Nova pasta\\teste.vstsln");
            var projectPath = Utils.CSProjectFromOpenDialog();
            if (projectPath == null) return;
            solution.SetProject(projectPath);
            SyncFromSolution();
        }

        #endregion

    }
}
