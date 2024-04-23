using Avalonia.Controls.Shapes;
using FSMTextSearch;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static CompilerLab.MainWindow;
using static System.Net.Mime.MediaTypeNames;

namespace CompilerLab
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private Window1 helpwindow = new Window1();
        private Window2 Task = new Window2();
        private About AboutWindow = new About();
        private string condition = "Ожидание";
        private string lang = "rus";
        private string filename = "";
        private void CreateFileDialog(object sender, RoutedEventArgs e)
        {
            CloseFileWindow closeFileWindow = new CloseFileWindow();
            if (filename != "")
            {
                if (closeFileWindow.ShowDialog() == true)
                {
                    using (StreamWriter writer = new StreamWriter(filename, false))
                    {
                        writer.WriteLine(Input.Text);
                    }
                }
                else if (closeFileWindow.IsCanceled) { }
                else
                {
                    return;
                }
            }

            // Configure save file dialog box
            var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.FileName = "Document"; // Default file name
            dialog.DefaultExt = ".txt"; // Default file extension
            dialog.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

            // Show save file dialog box
            bool? result = dialog.ShowDialog();




            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                filename = dialog.FileName;

                FileStream fs = File.Create(filename);

                if (!Input.IsEnabled)
                {
                    Input.IsEnabled = true;
                    Input.Text = "";
                }
                else
                {
                    Input.Text = "";
                }
                fs.Close();

            }
            SaveAsOption.IsEnabled = true;
            SaveButton.IsEnabled = true;
            SaveOption.IsEnabled = true;
            RunButton.IsEnabled = true;
            RunOption.IsEnabled = true;
            CloseFileOption.IsEnabled = true;
            EditOption.IsEnabled = true;
            CancelButton.IsEnabled = true;
            RepeatButton.IsEnabled = true;
            CopyButton.IsEnabled = true;
            CutButton.IsEnabled = true;
            PasteButton.IsEnabled = true;
            if (lang == "rus")
            {
                condition = "Редактирование";
                Condition.Content = condition;
            }
            if (lang == "eng")
            {
                condition = "Editing";
                Condition.Content = condition;
            }
            
            Input.Text = "";


        }
        private void SaveAsFileDialog(object sender, RoutedEventArgs e)
        {
            // Configure save file dialog box
            var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.FileName = "Document"; // Default file name
            dialog.DefaultExt = ".txt"; // Default file extension
            dialog.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

            // Show save file dialog box
            bool? result = dialog.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                filename = dialog.FileName;

                FileStream fs = File.Create(filename);
                fs.Close();

                using (StreamWriter writer = new StreamWriter(filename, false))
                {
                    writer.WriteLine(Input.Text);
                }
                MessageBox.Show("Данные сохранены в " + filename);
            }
        }

        private void SaveFileDialog(object sender, RoutedEventArgs e)
        {
            using (StreamWriter writer = new StreamWriter(filename, false))
            {
                writer.WriteLine(Input.Text);
            }

            MessageBox.Show("Данные сохранены в " + filename);
        }

        private void OpenFileDialog(object sender, RoutedEventArgs e)
        {
            CloseFileWindow closeFileWindow = new CloseFileWindow();
            if (filename != "")
            {
                if (closeFileWindow.ShowDialog() == true)
                {
                    using (StreamWriter writer = new StreamWriter(filename, false))
                    {
                        writer.WriteLine(Input.Text);
                        writer.Close();
                    }
                }
                else if (closeFileWindow.IsCanceled) {}
                else
                {
                    return;
                }
            }
            // Configure open file dialog box
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "Document"; // Default file name
            dialog.DefaultExt = ".txt"; // Default file extension
            dialog.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                filename = dialog.FileName;

                if (!Input.IsEnabled)
                {
                    Input.IsEnabled = true;
                    Input.Text = "";
                }

                using (StreamReader reader = new StreamReader(filename))
                {
                    string text = reader.ReadToEnd();
                    Input.Text = text;
                }

                SaveAsOption.IsEnabled = true;
                SaveButton.IsEnabled = true;
                SaveOption.IsEnabled = true;
                RunButton.IsEnabled = true;
                RunOption.IsEnabled = true;
                CloseFileOption.IsEnabled = true;
                EditOption.IsEnabled = true;
                CancelButton.IsEnabled = true;
                RepeatButton.IsEnabled = true;
                CopyButton.IsEnabled = true;
                CutButton.IsEnabled = true;
                PasteButton.IsEnabled = true;
                if (lang == "rus")
                {
                    condition = "Редактирование";
                    Condition.Content = condition;
                }
                if (lang == "eng")
                {
                    condition = "Editing";
                    Condition.Content = condition;
                }
            }
        }



        private void CloseFile(object sender, RoutedEventArgs e)
        {
            CloseFileWindow closeFileWindow = new CloseFileWindow();

            if (closeFileWindow.ShowDialog() == true)
            {
                using (StreamWriter writer = new StreamWriter(filename, false))
                {
                    writer.WriteLine(Input.Text);
                }
            }
            else if (closeFileWindow.IsCanceled) { return; }
            else
            {
                return;
            }
            Input.IsEnabled = false;
            SaveAsOption.IsEnabled = false;
            SaveButton.IsEnabled = false;
            SaveOption.IsEnabled = false;
            RunButton.IsEnabled = false;
            RunOption.IsEnabled = false;
            CloseFileOption.IsEnabled = false;
            EditOption.IsEnabled = false;
            CancelButton.IsEnabled = false;
            RepeatButton.IsEnabled = false;
            CopyButton.IsEnabled = false;
            CutButton.IsEnabled = false;
            PasteButton.IsEnabled = false;
            filename = "";
            if (lang == "rus")
            {
                condition = "Ожидание";
                Condition.Content = condition;
            }
            if (lang == "eng")
            {
                condition = "Waiting";
                Condition.Content = condition;
            }
        }
        void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            if (filename != "")
            {
                CloseFileWindow closeFileWindow = new CloseFileWindow();

                if (closeFileWindow.ShowDialog() == true)
                {
                    using (StreamWriter writer = new StreamWriter(filename, false))
                    {
                        writer.WriteLine(Input.Text);
                    }
                }
                else if (closeFileWindow.IsCanceled) {  }
                else
                {
                    e.Cancel = true;
                    return;
                }
            }
            if (lang == "rus")
            {
                condition = "Выход";
                Condition.Content = condition;
            }
            if (lang == "eng")
            {
                condition = "Exit";
                Condition.Content = condition;
            }
        }

        private void Undo(object sender, RoutedEventArgs e)
        {
            Input.Undo();
        }

        private void Redo(object sender, RoutedEventArgs e)
        {
            Input.Redo();
        }

        private void Copy(object sender, RoutedEventArgs e)
        {
            Input.Copy();
        }

        private void Paste(object sender, RoutedEventArgs e)
        {
            Input.Paste();
        }

        private void Cut(object sender, RoutedEventArgs e)
        {
            Input.Cut();
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            Input.Cut();
            Clipboard.Clear();
        }

        private void SelectAll(object sender, RoutedEventArgs e)
        {
            Input.SelectAll();
        }

        private void OutputFont_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OutputFont.Text == "")
            {
                Output.FontSize = 14;
            }
            else
            {
                Output.FontSize = Convert.ToInt32(OutputFont.SelectedItem.ToString().Replace("System.Windows.Controls.ComboBoxItem: ", ""));
            }
            
        }
        private void InputFont_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (InputFont.Text == "")
            {
                Input.FontSize = 14;
            }
            else
            { 
                Input.FontSize = Convert.ToInt32(InputFont.SelectedItem.ToString().Replace("System.Windows.Controls.ComboBoxItem: ", ""));
            }
        }

        private void SwitchToRussian(object sender, RoutedEventArgs e)
        {
            string[] rus = {"Файл","Создать","Открыть","Сохранить","Сохранить как","Выход", "Правка","Отменить", "Повторить", 
                "Вырезать", "Копировать", "Вставить", "Удалить", "Выделить все", 
                "Текст", "Постановка задачи", "Грамматика", "Классификация грамматики",
                "Метод анализа", "Диагностика и нейтрализация ошибок", "Текстовый пример", "Список литературы", "Исходный код программы", "Справка"
                , "Вызов справки", "О программе", "Настройки", "Язык", "Пуск"};
            FileOption.Header = rus[0];
            CreateOption.Header = rus[1];
            OpenOption.Header = rus[2];
            SaveOption.Header = rus[3];
            SaveAsOption.Header = rus[4];
            CloseFileOption.Header = rus[5];
            EditOption.Header = rus[6];
            UndoOption.Header = rus[7];
            RedoOption.Header = rus[8];
            CutOption.Header = rus[9];
            CopyOption.Header = rus[10];
            PasteOption.Header = rus[11];
            DeleteOption.Header = rus[12];
            SelectAllOption.Header = rus[13];
            TextOption.Header = rus[14];
            Formulation.Header = rus[15];
            Grammatic.Header = rus[16];
            GrammaticClass.Header = rus[17];
            AnalysMethod.Header = rus[18];
            Troubleshooter.Header = rus[19];
            Example.Header = rus[20];
            Literature.Header = rus[21];
            SourceCode.Header = rus[22];
            Help.Header = rus[23];
            HelpOption.Header = rus[24];
            AboutProgram.Header = rus[25];
            Settings.Header = rus[26];
            Language.Header = rus[27];
            RunOption.Header = rus[28];
            CreateFileButton.ToolTip = "Создать файл";
            OpenFileButton.ToolTip = "Открыть файл";
            SaveButton.ToolTip = "Сохранить файл";
            CancelButton.ToolTip = "Отмена изменений";
            RepeatButton.ToolTip = "Повтор последнего изменения";
            CopyButton.ToolTip = "Копировать";
            CutButton.ToolTip = "Вырезать";
            PasteButton.ToolTip = "Вставить";
            RunButton.ToolTip = "Пуск";
            HelpButton.ToolTip = "Справка";
            AboutProgramButton.ToolTip = "О программе";
            InputFont.ToolTip = "Размер шрифта в окне редактирования";
            OutputFont.ToolTip = "Размер шрифта в окне вывода";
            helpwindow.FileLabel.Content = "Файл";
            helpwindow.CreateBlock.Text = "Создать - создает файл с указанным именем и с указанной дирректорией. Функция также доступна на панели инструментов.";
            helpwindow.OpenBlock.Text = "Открыть - открывает файл с указанным именем и с указанной дирректорией. Функция также доступна на панели инструментов.";
            helpwindow.SaveBlock.Text = "Сохранить - сохраняет изменения в открытом файле. Функция также доступна на панели инструментов.";
            helpwindow.SaveAsBlock.Text = "Сохранить как - сохраняет файл с указанным именем и с указанной дирректорией.";
            helpwindow.ExitBlock.Text = "Выход - закрывает открытый файл.";
            helpwindow.EditLabel.Content = "Правка";
            helpwindow.UndoBlock.Text = "Отменить - отменяет последнее изменение в файле. Функция также доступна на панели инструментов.";
            helpwindow.RedoBlock.Text = "Повторить - повторяет последнее отмененное изменение в файле. Функция также доступна на панели инструментов.";
            helpwindow.CutBlock.Text = "Вырезать - вырезает и сохраняет в буфере обмена выделенный текст. Функция также доступна на панели инструментов.";
            helpwindow.CopyBlock.Text = "Копировать - копирует  в буфер обмена выделенный текст. Функция также доступна на панели инструментов.";
            helpwindow.PasteBlock.Text = "Вставить - вставляет текст из буфера обмена. Функция также доступна на панели инструментов.";
            helpwindow.DeleteBlock.Text = "Удалить - удаляет выделенный выделенный текст.";
            helpwindow.SelectAllBLock.Text = "Выделить все - выделяет весь текст в документе.";
            helpwindow.SettingsLabel.Content = "Настройки";
            helpwindow.LangBlock.Text = "Язык - смена языка в программе.";
            helpwindow.InstPanel.Content = "Панель инструментов";
            helpwindow.Toolblock.Text = "Содержит функции для работы с текстом, а также кнопки запустить, вызов справки и информации о программе и окна изменения размера шрифта в программе. При наведении на элементы пользователь может увидеть подсказки описывающие действия назначенные кнопкам";
            lang = "rus";
            if (condition == "Waiting")
            {
                condition = "Ожидание";
                Condition.Content = condition;
            }
            if (condition == "Exit")
            {
                condition = "Выход";
                Condition.Content = condition;
            }
            if (condition == "Editing")
            {
                condition = "Редактирование";
                Condition.Content = condition;
            }
            AboutWindow.Name.Content = "Текстовый редактор";
            AboutWindow.Developed.Text = "Разработан";
            AboutWindow.Student.Text = "Студентом НГТУ";
            AboutWindow.Fac.Text = "Факультета АВТФ";
            AboutWindow.Group.Text = "Группы АВТ-114";
            AboutWindow.Me.Text = "Толмачевым В.Е.";
            AboutWindow.City.Text = "Новосибирск 2023";
        }
        private void SwitchToEnglish(object sender, RoutedEventArgs e)
        {
            string[] eng = {"File","Create","Open","Save","Save as","Exit", "Edit","Undo", "Redo",
             "Cut", "Copy", "Paste", "Delete", "Select all",
             "Text", "Problem statement", "Grammar", "Grammar classification",
             "Analysis method", "Diagnosis and error neutralization", "Text example", "List of literature", "Source code of the program", "Help"
             , "Call for help", "About the program", "Settings", "Language", "Start"};
            FileOption.Header = eng[0];
            CreateOption.Header = eng[1];
            OpenOption.Header = eng[2];
            SaveOption.Header = eng[3];
            SaveAsOption.Header = eng[4];
            CloseFileOption.Header = eng[5];
            EditOption.Header = eng[6];
            UndoOption.Header = eng[7];
            RedoOption.Header = eng[8];
            CutOption.Header = eng[9];
            CopyOption.Header = eng[10];
            PasteOption.Header = eng[11];
            DeleteOption.Header = eng[12];
            SelectAllOption.Header = eng[13];
            TextOption.Header = eng[14];
            Formulation.Header = eng[15];
            Grammatic.Header = eng[16];
            GrammaticClass.Header = eng[17];
            AnalysMethod.Header = eng[18];
            Troubleshooter.Header = eng[19];
            Example.Header = eng[20];
            Literature.Header = eng[21];
            SourceCode.Header = eng[22];
            Help.Header = eng[23];
            HelpOption.Header = eng[24];
            AboutProgram.Header = eng[25];
            Settings.Header = eng[26];
            Language.Header = eng[27];
            RunOption.Header = eng[28];
            CreateFileButton.ToolTip = "Create file";
            OpenFileButton.ToolTip = "Open file";
            SaveButton.ToolTip = "Save file";
            CancelButton.ToolTip = "Undo";
            RepeatButton.ToolTip = "Redo";
            CopyButton.ToolTip = "Copy";
            CutButton.ToolTip = "Cut";
            PasteButton.ToolTip = "Paste";
            RunButton.ToolTip = "Run";
            HelpButton.ToolTip = "Help";
            AboutProgramButton.ToolTip = "About program";
            InputFont.ToolTip = "Editor font-size";
            OutputFont.ToolTip = "Output font-size";
            helpwindow.FileLabel.Content = "File";
            helpwindow.CreateBlock.Text = "Create- Creates a file with the specified name and dirrectory. The feature is also available on the toolbar.";
            helpwindow.OpenBlock.Text = "Open - opens the file with the specified name and with the specified dirrectory. The feature is also available on the toolbar.";
            helpwindow.SaveBlock.Text = "Save - Saves changes to an open file. The feature is also available on the toolbar.";
            helpwindow.SaveAsBlock.Text = "Save As - Saves the file with the specified name and with the specified dirrectory.";
            helpwindow.ExitBlock.Text = "Exit - Closes the open file.";
            helpwindow.EditLabel.Content = "Edit";
            helpwindow.UndoBlock.Text = "Undo - Undoes the last change in the file. The feature is also available on the toolbar.";
            helpwindow.RedoBlock.Text = "Redo - Redoes the last undo change in the file. The feature is also available on the toolbar.";
            helpwindow.CutBlock.Text = "Cut - Cuts and saves the selected text to the clipboard. The feature is also available on the toolbar.";
            helpwindow.CopyBlock.Text = "Copy - Copies the selected text to the clipboard. The feature is also available on the toolbar.";
            helpwindow.PasteBlock.Text = "Paste - Inserts text from the clipboard. The feature is also available on the toolbar.";
            helpwindow.DeleteBlock.Text = "Delete - Deletes the selected selected text.";
            helpwindow.SelectAllBLock.Text = "Select All - Selects all text in the document.";
            helpwindow.SettingsLabel.Content = "Settings";
            helpwindow.LangBlock.Text = "Language - change language in program";
            helpwindow.InstPanel.Content = "Toolbar";
            helpwindow.Toolblock.Text = "Contains functions for working with text, as well as buttons to start, access help and information about the program and a window for changing the font size in the program. When hovering over elements, the user can see tips describing the actions assigned to the buttons";
            lang = "eng";
          
            if (condition == "Ожидание")
            {
                condition = "Waiting";
                Condition.Content = condition;
            }
            if (condition == "Выход")
            {
                condition = "Exit";
                Condition.Content = condition;
            }
            if (condition == "Редактирование")
            {
                condition = "Editing";
                Condition.Content = condition;
            }
            AboutWindow.Name.Content = "Text Editor";
            AboutWindow.Developed.Text = "Developed by";
            AboutWindow.Student.Text = "NSTU student";
            AboutWindow.Fac.Text = "AVTF Faculty";
            AboutWindow.Group.Text = "AVT-114";
            AboutWindow.Me.Text = "Tolmachev V.E.";
            AboutWindow.City.Text = "Novosibirsk 2023";

        }

        private void CallHelp(object sender, RoutedEventArgs e)
        {
            //helpwindow.Show();
            var p = new Process();
            p.StartInfo = new ProcessStartInfo(@"..\..\..\html\HTMLPage1.html")
            {
                UseShellExecute = true
            };
            p.Start();
            p.Close();
        }
        private void CallTask(object sender, RoutedEventArgs e)
        {
            //Task.Show();
            
        }

        private void ShowAbout(object sender, RoutedEventArgs e)
        {

            //AboutWindow.Show();
            var p = new Process();
            p.StartInfo = new ProcessStartInfo(@"..\..\..\html\About.html")
            {
                UseShellExecute = true
            };
            p.Start();
            p.Close();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var p = new Process();
            p.StartInfo = new ProcessStartInfo(@"..\..\..\html\problem.html")
            {
                UseShellExecute = true
            };
            p.Start();
            p.Close();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            var p = new Process();
            p.StartInfo = new ProcessStartInfo(@"..\..\..\html\Grammar.html")
            {
                UseShellExecute = true
            };
            p.Start();
            p.Close();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            var p = new Process();
            p.StartInfo = new ProcessStartInfo(@"..\..\..\html\Classification.html")
            {
                UseShellExecute = true
            };
            p.Start();
            p.Close();
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            var p = new Process();
            p.StartInfo = new ProcessStartInfo(@"..\..\..\html\methodAnalysis.html")
            {
                UseShellExecute = true
            };
            p.Start();
            p.Close();
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            var p = new Process();
            p.StartInfo = new ProcessStartInfo(@"..\..\..\html\Neutralizing.html")
            {
                UseShellExecute = true
            };
            p.Start();
            p.Close();
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            var p = new Process();
            p.StartInfo = new ProcessStartInfo(@"..\..\..\html\testExamples.html")
            {
                UseShellExecute = true
            };
            p.Start();
            p.Close();
        }

        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {
            var p = new Process();
            p.StartInfo = new ProcessStartInfo(@"..\..\..\html\LitList.html")
            {
                UseShellExecute = true
            };
            p.Start();
            p.Close();
        }

        private void MenuItem_Click_7(object sender, RoutedEventArgs e)
        {
            var p = new Process();
            p.StartInfo = new ProcessStartInfo(@"https://github.com/Psycho4ct/Parser")
            {
                UseShellExecute = true
            };
            p.Start();
            p.Close();
        }
        private FSM fsm;



        private void RunButton_Click(object sender, RoutedEventArgs e)
        {
            //LEXER

            //string text = Input.Text;
            //Lexer lexer = new Lexer();
            //List<Token> tokens = lexer.Scan(text);
            //dgTokens.ItemsSource = tokens;

            //ResultField.Text = "";
            //string text = Input.Text;
            //Parser parser = new Parser();
            //CharChain chain = new CharChain(text);
            //int count = 0;
            //while (chain.Next().Char != '\0')
            //{
            //    parser.Parse(chain);

            //    var errors = parser.GetErrors();

            //   PARSER

                //    ResultField.Text += error.Message;
                //    if (error.IncorrStr != null)
                //    {
                //        ResultField.Text += " (Отброшенный фрагмент: '" + error.IncorrStr + "' на позиции: " + error.Idx + ")";
                //    }
                //    ResultField.Text += "\r\n";

                
                //}

            //        ResultField.Text += error.Message;
            //        if (error.IncorrStr != null)
            //        {
            //            ResultField.Text += " (Отброшенный фрагмент: '" + error.IncorrStr + "' на позиции: " + error.Idx + ")";
            //        }
            //        ResultField.Text += "\r\n";


            //    }

            //}
            //if (count == 0)
            //{
            //    ResultField.Text += "Ошибок нет.\r\n";
            //}
            //else
            //{
            //    ResultField.Text += "Обнаружено " + count + " ошибок.\r\n";
            //    //ResultField.Text += "Исходная строка должна была быть:" + parser.rightstring + "\r\n";

            //    if (parser.number == ""||parser.numint < parser.symbolarray.Length)
            //    {
            //        parser.number = parser.symbolarray.Length.ToString();
            //    }
            //   // ResultField.Text += "Исходная строка должна была быть:" + "const " + "char " + parser.idarray +"[" + parser.number+ "]"+ "=" + "\"" + parser.symbolarray + "\""+ ";" +"\r\n";

            //}


            //TETRADS
            //string text = Input.Text;
            //try
            //{

            //    var pe = dataGridResult.SelectedValue as ParseError;
            //    TetradaParser polishNotationCalculator = new TetradaParser(text);
            //    polishNotationCalculator.PrintTetrads();
            //    dataGridResult.ItemsSource = polishNotationCalculator.tetradas;
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            //}


            //REGULARKI
            //1 TASK
            string text = Input.Text;
            string pattern = @"-?\d+\.?\d*";
            //2 TASK
            //string pattern = @"\b[A-Z]{2,}\b";
            //3 TASK
            //string pattern = @"\b(2[0-3]|[01]?[0-9]):[0-5]?[0-9]:[0-5]?[0-9]\b";


            //fsm = new FSM(text);
            //fsm.Run();

            //ResultField.Text = "";
            //List<string> finds = fsm.GetMatches();
            //List<string> startpositin = fsm.GetPositions();

            //for (int i = 0; i < finds.Count; i++)
            //{
            //    string find = finds[i];
            //    string startPosition = startpositin[i];
            //    string result1 = $"{find} {startPosition}\n";
            //    ResultField.Text += result1;
            //}

            MatchCollection matches = Regex.Matches(text, pattern);

            ResultField.Text = "";

            foreach (Match match in matches)
            {
                string result = $"Found: {match.Value} at position {match.Index}\n";
                ResultField.Text += result;
            }



            }


        }








        public enum TokenType
        {
            ArrayLength,
            KeywordConst,
            KeywordChar,
            Identifier,
            Operator,
            Digit,
            Delimiter,
            Error,
            StringLiteral,
            OperatorEnd,
            OperatorEqual
        }

        public class Token
        {
            public int Code { get; set; }
            public TokenType Type { get; set; }
            public string Lexeme { get; set; }
            public string Position { get; set; }
            public int Line { get; set; }


            public Token(int code, TokenType type, string lexeme, string position, int line)
            {
                Code = code;
                Type = type;
                Lexeme = lexeme;
                Position = position;
                Line = line;

            }
        }

        public class Lexer
        {
            private int position = 0;
            private char currentChar = '\0';
            private string text = "";


       


            public List<Token> Scan(string text)
            {
                var lines = text.Split('\n');
                int lineCount = 1;
                List<Token> tokens = new List<Token>();
                foreach (var line in lines)
                {
                    int lineStartPosition = 1;
                    int startPosition = 1;

                    while (position < text.Length)
                    {
                        currentChar = text[position];



                        // Определение типа лексемы
                        TokenType type = GetToken(currentChar);

                        // Формирование лексемы
                        string lexeme = "";
                        if (type == TokenType.StringLiteral)
                        {
                            // Это строковый литерал, добавляем в лексему все символы между кавычками
                            lexeme += currentChar;
                            position++;
                            while (position < text.Length && text[position] != currentChar)
                            {
                                lexeme += text[position];
                                position++;
                            }
                            lexeme += currentChar;
                            position++;
                        }
                        //else if (type == TokenType.ArrayLength)
                        //{
                        //    lexeme = ReadArrayLength();
                        //}
                        else
                        {
                            while (position < text.Length && GetToken(text[position]) == type)
                            {
                                lexeme += text[position];
                                position++;
                            }
                        }

                        // Сохранение начальной позиции лексемы
                        int lexemeStartPosition = startPosition;

                        // Обновление начальной позиции для следующей лексемы
                        startPosition = position;

                        // Добавление лексемы в список
                        string positionString = $"{lexemeStartPosition}-{lexemeStartPosition + lexeme.Length - 1}";

                        if (lexeme == "const")
                        {
                            type = TokenType.KeywordConst;
                        }
                        if (lexeme == "char")
                        {
                            type = TokenType.KeywordChar;
                        }
                        tokens.Add(new Token(GetTokenCode(type), type, lexeme, positionString, lineCount));
                    }
                    lineCount++;
                }

                    return tokens;
            }

            private TokenType GetToken(char c)
            {

                if (IsQuote(c))
                {
                    return TokenType.StringLiteral;
                }
                else if (IsDlina(c))
                {
                    return TokenType.ArrayLength;
                }
                else if (char.IsLetter(c))
                {
                    // Проверяем, является ли лексема ключевым словом
                    string lexeme = c.ToString();
                    while (position < text.Length && char.IsLetterOrDigit(text[position]))
                    {
                        lexeme += text[position];
                        position++;
                    }

                    return TokenType.Identifier;
                }
                else if (char.IsDigit(c))
                {
                    // Проверка на литерал с помощью регулярного выражения
                    if (Regex.IsMatch(currentChar.ToString() + LookAhead(), @"^[0-9]+(\.[0-9]+)?([eE][+-]?[0-9]+)?$"))
                    {
                        return TokenType.Digit;
                    }
                    else
                    {
                        return TokenType.Error;
                    }
                }
                else if (c == ' ' || c == '\t' || c == '\n')
                {
                    return TokenType.Delimiter;
                }
                else if (c == '=')
                {
                    return TokenType.OperatorEqual;
                }
                else if (c == ';')
                {
                    return TokenType.OperatorEnd;
                }
                else if (!char.IsLetterOrDigit(c))
                {
                    return TokenType.Error;
                }
                else
                {
                    return TokenType.Operator;
                }
            }

            private bool IsQuote(char c)
            {
                return c == '\"' || c == '\'';
            }

            private bool IsDlina(char c)
            {
                return c == '[' || c == ']';
            }

            private string LookAhead()
            {
                // Получение следующего символа
                if (position + 1 < text.Length)
                {
                    return text[position + 1].ToString();
                }
                else
                {
                    return "";
                }
            }

            private int GetTokenCode(TokenType type)
            {
                switch (type)
                {
                    case TokenType.KeywordConst:
                        return 1;
                    case TokenType.KeywordChar:
                        return 2;
                    case TokenType.Identifier:
                        return 3;
                    case TokenType.Operator:
                        return 3;
                    case TokenType.OperatorEnd:
                        return 9;
                    case TokenType.OperatorEqual:
                        return 7;
                    case TokenType.Digit:
                        return 5;
                    case TokenType.Delimiter:
                        return 4;
                    case TokenType.StringLiteral:
                        return 8;
                    case TokenType.Error:
                        return 10;
                    case TokenType.ArrayLength:
                        return 5;
                    default:
                        return 0;
                }
            }



        }

        private void TextOption_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
