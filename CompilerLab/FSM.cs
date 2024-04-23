using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;

namespace FSMTextSearch
{
    public enum State { Start, Abbreviation, Hour, Minute, Second }

    public class FSM
    {
        private State currentState;
        private string input;
        private int index;

        public FSM(string input)
        {
            this.input = input;
            this.index = 0;
            this.currentState = State.Start;
        }

        public void Run()
        {

            while (index < input.Length)
            {
                char c = input[index];
                switch (currentState)
                {
                    case State.Start:
                        if (char.IsUpper(c))
                        {
                            currentState = State.Abbreviation;
                            abbreviationBuilder.Append(c);

                        }
                        else if (char.IsDigit(c))
                        {
                            currentState = State.Hour;
                            timeBuilder.Append(c);
                        }
                        break;
                    case State.Abbreviation:
                        if (char.IsUpper(c) || char.IsDigit(c))
                        {
                            currentState = State.Abbreviation;
                            abbreviationBuilder.Append(c);
                        }
                        else
                        {
                            currentState = State.Start;
                            if (abbreviationBuilder.Length > 1)
                            {
                                matches.Add($"Найдена аббревиатура '{abbreviationBuilder.ToString()}'");
                                position.Add($"на позиции {index - abbreviationBuilder.Length + 1}");
                            }
                            if (index == input.Length - 1) // Check if we've reached the end of the input string
                            {
                                break; // Exit the loop
                            }

                            abbreviationBuilder.Clear();

                        }
                        break;
                    case State.Hour:
                        if (c == ':')
                        {
                            timeBuilder.Append(c);
                            currentState = State.Minute;
                        }
                        else if (char.IsDigit(c))
                        {
                            timeBuilder.Append(c);
                            currentState = State.Hour;
                        }
                        else
                        {
                            currentState = State.Start;
                        }
                        break;
                    case State.Minute:
                        if (c == ':')
                        {
                            timeBuilder.Append(c);
                            currentState = State.Second;
                        }
                        else if (char.IsDigit(c))
                        {
                            timeBuilder.Append(c);
                            currentState = State.Minute;
                        }
                        else
                        {
                            currentState = State.Start;
                        }
                        break;
                    case State.Second:
                        if (char.IsDigit(c))
                        {
                            timeBuilder.Append(c);
                            currentState = State.Second;
                        }
                        else
                        {
                            currentState = State.Start;
                            if (timeBuilder.Length > 3)
                            {
                                string timeString = timeBuilder.ToString();
                                string[] timeParts = timeString.Split(':');
                                if (timeParts.Length == 3)
                                {
                                    int hour, minute, second;
                                    if (int.TryParse(timeParts[0], out hour) && int.TryParse(timeParts[1], out minute) && int.TryParse(timeParts[2], out second))
                                    {
                                        if (hour >= 0 && hour < 24 && minute >= 0 && minute < 60 && second >= 0 && second < 60)
                                        {
                                            matches.Add($"Найдено время {timeString}");
                                            position.Add($"на позиции {index - timeBuilder.Length + 1}");
                                        }
                                    }
                                }
                            }
                            if (index == input.Length - 1) // Check if we've reached the end of the input string
                            {
                                break; // Exit the loop
                            }

                            timeBuilder.Clear();
                        }
                        break;
                }
                index++;
            }
            if (abbreviationBuilder.Length > 1)
            {
                matches.Add($"Найдена аббревиатура '{abbreviationBuilder.ToString()}'");
                position.Add($"на позиции {index - abbreviationBuilder.Length + 1}");
            }
            if (timeBuilder.Length > 4)
            {
                string timeString = timeBuilder.ToString();
                string[] timeParts = timeString.Split(':');
                if (timeParts.Length == 3)
                {
                    int hour, minute, second;
                    if (int.TryParse(timeParts[0], out hour) && int.TryParse(timeParts[1], out minute) && int.TryParse(timeParts[2], out second))
                    {
                        if (hour >= 0 && hour < 24 && minute >= 0 && minute < 60 && second >= 0 && second < 60)
                        {
                            matches.Add($"Найдено время {timeString}");
                            position.Add($"на позиции {index - timeBuilder.Length + 1}");
                        }
                    }
                }

            }
        }

        public List<string> GetMatches()
        {
            return matches;
        }
        public List<string> GetPositions()
        {
            return position;
        }

        private StringBuilder timeBuilder = new StringBuilder();
        private StringBuilder abbreviationBuilder = new StringBuilder();
        private List<string> matches = new List<string>();
        private List<string> position = new List<string>();
    }

}
